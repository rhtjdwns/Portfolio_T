using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    public static MatchManager instance { get; private set; }
    public string PlayerName => playerName;  // 플레이어 이름 접근자 추가

    // 로비 검색 목록이 변경되면 UI에게 알려 UI에서 목록을 갱신하는 이벤트
    public event Action<List<Lobby>> lobbyListChangedEvent;
    // 로비에 들어오면 호출할 이벤트, 로비 관련 UI함수들이 구독중
    public event Action<Lobby> joinLobbyEvent;
    // 로비를 나갈때 UI를 제어해줄 이벤트
    public event Action leaveLobbyEvent;
    // 강퇴되었을때 UI를 제어해줄 이벤트
    public event Action kickedFromLobbyEvent;
    // 게임이 시작되면 호출될 이벤트, Relay와 관련
    public event Action gameStartEvent;
    // 로그인 확인
    public bool isLogin = false;

    // 현재 참가한 로비를 저장할 클래스 필드
    private Lobby joinedLobby;
    // 인증에 사용한 플레이어 이름 저장
    private string playerName;
    // 로비 관련 타이머
    private float lobbyMaintainTimer = 0.0f;
    private float lobbyInformationUpdateTimer = 0.0f;
    private float lastLobbyListUpdateTime = 0f;
    private float lobbyListCooldown = 3f;
    public APIClient.UserInfo clientUserInfo;

    [Header("Lobby UI")]
    [SerializeField] private TMP_InputField lobbyNameInput;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button refreshLobbyButton;
    [SerializeField] private Button gameStartButton;
    [SerializeField] private RoomUI roomUI;

    [Header("View UI")]
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject lobbyCamera;

    [Header("Player Spawn")]
    [SerializeField] public Transform spawnPoint;
    [SerializeField] private PlayerDataSO playerData;  // 추가


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        createLobbyButton.onClick.AddListener(() => OnCreateLobby());
        refreshLobbyButton.onClick.AddListener(() => GetLobbyList().Forget());
        gameStartButton.onClick.AddListener(() => OnGameStart());

        MaintainLobbyAlive().Forget();
        RefreshLobbyInformation().Forget();
        
        // 게임 시작 버튼 초기 상태 설정
        UpdateGameStartButtonState();
    }

    private void Update()
    {
        if (joinedLobby != null)
        {
            playerCamera.SetActive(true);
            lobbyCamera.SetActive(false);
            UpdateGameStartButtonState();
        }
        else
        {
            playerCamera.SetActive(false);
            lobbyCamera.SetActive(true);
            gameStartButton.gameObject.SetActive(false);
        }
    }

    private void UpdateGameStartButtonState()
    {
        if (gameStartButton != null)
        {
            gameStartButton.gameObject.SetActive(IsLobbyHost());
            gameStartButton.interactable = IsLobbyHost() && joinedLobby != null && joinedLobby.Players.Count >= 1;
        }
    }

    private void OnGameStart()
    {
        if (!IsLobbyHost()) return;
        
        // 게임 시작 이벤트 발생
        gameStartEvent?.Invoke();
        
        // 플레이어 데이터 초기화 및 저장
        playerData.ClearPlayers();
        foreach (var player in joinedLobby.Players)
        {
            string playerId = player.Id;
            string playerName = player.Data[NetworkConstants.PLAYERNAME_KEY].Value;

            // Netcode Client ID 가져오기
            if (!player.Data.TryGetValue(NetworkConstants.NETCODE_CLIENT_ID_KEY, out var clientIdData))
            {
                Debug.LogError($"Netcode Client ID not found for player: {playerId}");
                continue;
            }

            if (!ulong.TryParse(clientIdData.Value, out ulong clientId))
            {
                Debug.LogError($"Failed to parse Netcode Client ID: {clientIdData.Value}");
                continue;
            }

            // NetworkManager의 ConnectedClients에서 플레이어 찾기
            if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out NetworkClient networkClient))
            {
                Debug.LogError($"Client not found for Netcode ID: {clientId}");
                continue;
            }

            var playerObject = networkClient.PlayerObject;
            var playerController = playerObject.GetComponent<PlayerController>();

            playerData.AddPlayer(playerId, playerName, playerController);
        }

        // 씬 전환
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

    public async UniTask<bool> OnRegister(TMP_InputField usernameInputField, TMP_InputField passwordInputField, TMP_Text stateText)
    {
        if (usernameInputField.text == "" || passwordInputField.text == "")
        {
            stateText.text = "Please enter your ID and password.";
            return false;
        }

        string name = usernameInputField.text;
        string password = passwordInputField.text;

        bool registerSuccess = await APIClient.instance.Register(name, password);
        
        if (registerSuccess)
        {
            stateText.text = "Register Completed";
            return true;
        }
        else
        {
            stateText.text = "Register Failed";
            return false;
        }
    }

    public async UniTask<bool> OnLogin(TMP_InputField usernameInputField, TMP_InputField passwordInputField, TMP_Text stateText)
    {
        if (usernameInputField.text == "" || passwordInputField.text == "")
        {
            stateText.text = "Please enter your ID and password.";
            return false;
        }

        string name = usernameInputField.text;
        string password = passwordInputField.text;

        bool loginSuccess = await APIClient.instance.Login(name, password);
        
        if (loginSuccess)
        {
            APIClient.UserInfo userInfo = await APIClient.instance.GetUserInfo(name);
            clientUserInfo = userInfo;

            if (userInfo != null)
            {
                playerData.AddPlayer(userInfo.userId, userInfo.userId, null, userInfo);
            }
            else
            {
                stateText.text = "Login Failed";
                return false;
            }

            await Authenticate(name, stateText);
            return true;
        }
        else
        {
            stateText.text = "Login Failed";
            return false;
        }
    }

    private void OnCreateLobby()
    {
        if (lobbyNameInput.text == "") return;

        string lobbyName = lobbyNameInput.text;
        int maxPlayers = 4;
        bool isPrivate = false;
        CreateLobby(lobbyName, maxPlayers, isPrivate).Forget();
        GetLobbyList().Forget();
    }

    // 로그인 및 인증
    public async UniTask Authenticate(string playerName, TMP_Text stateText)
    {
        this.playerName = playerName;
        InitializationOptions initializationOptions = new InitializationOptions();
        initializationOptions.SetProfile(playerName);

        await UnityServices.InitializeAsync(initializationOptions);
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        isLogin = true;
        stateText.text = "Login Completed";
    }

    // 로비 생성
    public async UniTaskVoid CreateLobby(string lobbyName, int maxPlayers, bool isPrivate)
    {
        try
        {
            // 1. Relay 서버에 호스트로 접속하기 위한 데이터 생성
            var relayHostData = await RelayManager.SetupRelay(maxPlayers, "production");
            
            // 2. NetworkManager의 Transport 설정
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            // 3. 호스트의 Relay 서버 연결 데이터 설정
            // - IPv4Address: Relay 서버의 IP 주소
            // - Port: 서버 포트 번호
            // - AllocationIdBytes: 할당 ID (서버에서 이 호스트에 할당한 고유 식별자)
            // - Key: 암호화 키
            // - ConnectionData: 연결에 필요한 추가 데이터
            // - false: 보안 연결 사용 여부 (DTLS)
            transport.SetHostRelayData(
                relayHostData.IPv4Address,
                (ushort)relayHostData.Port,
                relayHostData.AllocationIdBytes,
                relayHostData.Key,
                relayHostData.ConnectionData,
                false
            );

            // 4. 로비 생성 옵션 설정
            CreateLobbyOptions createOptions = new CreateLobbyOptions
            {
                Player = GetPlayer(),
                IsPrivate = isPrivate,
                Data = new Dictionary<string, DataObject>
                {
                    { NetworkConstants.GAMEMODE_KEY,
                    new DataObject(DataObject.VisibilityOptions.Public, "DefaultGameMode") },
                    { NetworkConstants.GAMESTART_KEY,
                    new DataObject(DataObject.VisibilityOptions.Member, NetworkConstants.GAMESTART_KEY_DEFAULT) },
                    // Relay 서버 접속을 위한 조인 코드
                    { "JoinCode",
                    new DataObject(DataObject.VisibilityOptions.Member, relayHostData.JoinCode) }
                }
            };

            // 5. Unity Lobby 서비스에 로비 생성 요청
            var lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createOptions);
            joinedLobby = lobby;

            // 6. 네트워크 호스트 시작
            NetworkManager.Singleton.StartHost();
            
            joinLobbyEvent?.Invoke(joinedLobby);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to create lobby: {e.Message}");
        }
    }

    // 로비가 사라지지 않도록 재활성화
    public async UniTaskVoid MaintainLobbyAlive()
    {
        while (true)
        {
            await UniTask.Delay(1);

            if (!IsLobbyHost()) continue;

            lobbyMaintainTimer += Time.deltaTime;
            if (lobbyMaintainTimer < NetworkConstants.LOBBY_MAINTAIN_TIME) continue;

            lobbyMaintainTimer = 0.0f;
            // 시간이 다 되면 아래 함수로 로비가 유지될 수 있도록 함.
            await LobbyService.Instance.SendHeartbeatPingAsync(joinedLobby.Id);
        }
    }

    // 로비 검색
    public async UniTaskVoid GetLobbyList()
    {
        if (!isLogin) return;

        if (Time.time - lastLobbyListUpdateTime < lobbyListCooldown) return;

        lastLobbyListUpdateTime = Time.time;

        try
        {
            /* 
                Count : 최대 검색할 로비의 수 1~100 가능
                SampleResults : 필터 결과값에서 랜덤 하게 방을 반환할건지 여부
                Filters : 검색 필터 설정
                Order : 순서를 어떻게 할지에 대한 설정
                ContinuationToken : 다음 검색결과 페이지를 위한 토큰
            */
            QueryLobbiesOptions options = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(
                        field: QueryFilter.FieldOptions.AvailableSlots,
                        op: QueryFilter.OpOptions.GT,
                        value: "0"
                    )
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(
                        asc: false,
                        field: QueryOrder.FieldOptions.Created
                    )
                }
            };

            QueryResponse lobbyListQueryResponse = await Lobbies.Instance.QueryLobbiesAsync();
            // List<Lobby>를 이벤트로 전달
            lobbyListChangedEvent?.Invoke(lobbyListQueryResponse.Results);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e.Message);
        }
    }

    // UI에서 호출하는 로비 참가 함수
    public async UniTaskVoid JoinLobbyByUI(Lobby lobby)
    {
        try
        {
            // 1. 선택한 로비에 참가
            var joinOption = new JoinLobbyByIdOptions { Player = GetPlayer() };
            joinedLobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobby.Id, joinOption);

            // 2. 로비 데이터에서 Relay 조인 코드 추출
            string joinCode = joinedLobby.Data["JoinCode"].Value;
            // 3. Relay 서버 접속 데이터 생성
            var relayJoinData = await RelayManager.JoinRelay(joinCode, "production");
            
            // 4. NetworkManager의 Transport 설정
            var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            // 5. 클라이언트의 Relay 서버 연결 데이터 설정
            // - IPv4Address: Relay 서버의 IP 주소
            // - Port: 서버 포트 번호
            // - AllocationIdBytes: 할당 ID
            // - Key: 암호화 키
            // - ConnectionData: 클라이언트 연결 데이터
            // - HostConnectionData: 호스트 연결 데이터 (클라이언트가 호스트에 연결하는데 필요)
            // - false: 보안 연결 사용 여부
            transport.SetClientRelayData(
                relayJoinData.IPv4Address,
                (ushort)relayJoinData.Port,
                relayJoinData.AllocationIdBytes,
                relayJoinData.Key,
                relayJoinData.ConnectionData,
                relayJoinData.HostConnectionData,
                false
            );

            // 6. 네트워크 클라이언트 시작
            NetworkManager.Singleton.StartClient();

            // 7. 이벤트 발생 및 플레이어 스폰
            joinLobbyEvent?.Invoke(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError($"Failed to join lobby: {e.Message}");
        }
    }

    // 퀵 매치
    public async UniTaskVoid QuickMatch()
    {
        try
        {
            QuickJoinLobbyOptions options = new QuickJoinLobbyOptions { Player = GetPlayer() };
            joinedLobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);

            joinLobbyEvent?.Invoke(joinedLobby);
        }
        catch (LobbyServiceException e)
        {
            if (e.Reason == LobbyExceptionReason.NoOpenLobbies)
            {
                Debug.LogError("No Open Lobbies");
            }
            Debug.LogError(e);
        }
    }

    // 로비가 사라지지 않도록 로비 갱신
    public async UniTaskVoid RefreshLobbyInformation()
    {
        while (true)
        {
            await UniTask.Delay(1);

            if (joinedLobby == null) continue;

            // 씬이 전환 중이거나 게임 씬인 경우 갱신 중단
            if (SceneManager.GetActiveScene().name == "GameScene" || 
                SceneManager.GetActiveScene().name == "LoadingScene")
            {
                continue;
            }

            lobbyInformationUpdateTimer += Time.deltaTime;
            if (lobbyInformationUpdateTimer < NetworkConstants.LOBBY_INFO_UPDATE_TIME) continue;

            lobbyInformationUpdateTimer = 0f;
            try
            {
                joinedLobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);

                if (roomUI != null)
                {
                    roomUI.UpdatePlayerList(joinedLobby);
                }
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError($"로비 정보 갱신 실패: {e.Message}");
            }
        }
    }

    // 플레이어가 로비에 있는지 확인
    private bool IsPlayerInLobby()
    {
        if (joinedLobby == null || joinedLobby.Players == null) return false;

        foreach (var player in joinedLobby.Players)
        {
            if (player.Id != AuthenticationService.Instance.PlayerId) continue;
            return true;
        }

        return false;
    }

    // 플레이어 강퇴
    public async UniTaskVoid KickPlayer(string playerId)
    {
        // 호스트가 아니면 권한 없음
        if (!IsLobbyHost()) return;
        // 호스트 스스로 강퇴 불가가
        if (playerId == AuthenticationService.Instance.PlayerId) return;

        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    // 호스트 넘겨주기
    private async void MigrateHost()
    {
        // 로비 호스트거나 플레이어가 호스트 밖에 없다면
        if (!IsLobbyHost() || joinedLobby.Players.Count <= 1) return;

        try
        {
            joinedLobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id,
            new UpdateLobbyOptions
            {
                HostId = joinedLobby.Players[1].Id
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    // 로비를 나갈 때 네트워크 연결도 종료
    public async UniTaskVoid LeaveLobby()
    {
        if (joinedLobby != null)
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError(e);
            }
            finally
            {
                joinedLobby = null;
                leaveLobbyEvent?.Invoke();

                // 네트워크 연결 종료
                if (NetworkManager.Singleton != null)
                {
                    if (NetworkManager.Singleton.IsHost)
                    {
                        NetworkManager.Singleton.Shutdown();
                    }
                    else if (NetworkManager.Singleton.IsClient)
                    {
                        NetworkManager.Singleton.Shutdown();
                    }
                }

                GetLobbyList().Forget();
            }
        }
    }

    private bool IsLobbyHost()
    {
        return joinedLobby != null && joinedLobby.HostId == AuthenticationService.Instance.PlayerId;
    }

    private Player GetPlayer()
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                {NetworkConstants.PLAYERNAME_KEY,
                new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName)},
                {NetworkConstants.NETCODE_CLIENT_ID_KEY,
                new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, NetworkManager.Singleton.LocalClientId.ToString())}
            }
        };
    }

    private void OnApplicationQuit()
    {
        LeaveLobby().Forget();
    }
}
