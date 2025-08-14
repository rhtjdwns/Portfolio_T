using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cysharp.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance { get; private set; }

    // 보스 스폰 이벤트
    public static event System.Action<Enemy> OnBossSpawned;

    public class GamePlayer
    {
        public string PlayerId { get; private set; }
        public string PlayerName { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public NetworkObject NetworkObject { get; private set; }

        public GamePlayer(string playerId, string playerName, PlayerController playerController, NetworkObject networkObject = null)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerController = playerController;
            NetworkObject = networkObject;
        }
    }

    [Header("Player List")]
    private NetworkList<NetworkObjectReference> networkPlayers;
    private List<GamePlayer> players;

    [Header("Player Spawn")]
    [SerializeField] public Transform spawnPoint;
    [SerializeField] private PlayerDataSO playerData;

    [Header("Boss Spawn")]
    [SerializeField] public Transform bossSpawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        networkPlayers = new NetworkList<NetworkObjectReference>();
        players = new List<GamePlayer>();
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            InitializeNetworkObjectPool();
            InitializePlayers();
            // NetworkObjectPool이 초기화될 때까지 잠시 대기
            StartCoroutine(SpawnBossAfterDelay());
        }
    }

    private void InitializeNetworkObjectPool()
    {
        // NetworkObjectPool이 없으면 생성
        if (NetworkObjectPool.Singleton == null)
        {
            GameObject poolObject = new GameObject("NetworkObjectPool");
            poolObject.AddComponent<NetworkObjectPool>();
            DontDestroyOnLoad(poolObject);
        }
    }


#region Boss Spawn

    private IEnumerator SpawnBossAfterDelay()
    {
        // 플레이어가 모두 스폰될 때까지 대기
        yield return new WaitForSeconds(2f);

        SpawnBossServerRpc();
    }

    [ServerRpc]
    private void SpawnBossServerRpc()
    {
        // 호스트 플레이어 찾기 (첫 번째 플레이어가 호스트)
        PlayerController hostPlayer = null;
        if (players.Count > 0)
        {
            hostPlayer = players.Find(p => p.PlayerController.IsHost).PlayerController;
        }
        
        if (hostPlayer == null)
        {
            Debug.LogWarning("[GameManager] 호스트 플레이어를 찾을 수 없습니다.");
        }
        
        int bossGrade = hostPlayer.Stat.BossGrade;
        Debug.Log($"[GameManager] 보스 등급: {bossGrade}");

        SpawnBossClientRpc(bossGrade);
        SpawnBoss(bossGrade);
    }

    [ClientRpc]
    private void SpawnBossClientRpc(int bossGrade)
    {
        Debug.LogError($"[GameManager] 보스 등급: {bossGrade}");
    }

    private void SpawnBoss(int bossGrade)
    {
        GameObject bossPrefab = NetworkObjectPool.Singleton.GetPrefabByName("BossAPrefab");
        
        if (bossPrefab == null)
        {
            Debug.LogError("[GameManager] BossAPrefab을 찾을 수 없습니다!");
            return;
        }

        NetworkObject bossNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        Enemy boss = bossNetworkObject.GetComponent<Enemy>();
        
        boss.Stat.MaxHp = bossGrade * 100;
        boss.Stat.AttackDelay = 3 - (bossGrade * 0.1f);
        boss.Stat.MoveSpeed = 1 + (bossGrade * 0.1f);
        boss.Stat.BossGrade = bossGrade;

        bossNetworkObject.Spawn();
        
        Debug.Log($"[GameManager] 보스 스탯: {boss.Stat.MaxHp}, {boss.Stat.AttackDelay}, {boss.Stat.MoveSpeed}");

        // 보스 스폰 이벤트 발생
        OnBossSpawned?.Invoke(boss);
    }

#endregion

#region Player Spawn

    private void InitializePlayers()
    {
        if (playerData == null || playerData.Players == null) return;

        players.Clear();
        foreach (var playerInfo in playerData.Players)
        {
            AddPlayerServerRpc(playerInfo.Controller.GetComponent<NetworkObject>());
        }
        
        Debug.Log($"Initialized {players.Count} players from saved data");
    }

    // 새 플레이어를 서버에 추가
    // 플레이어의 네트워크 객체를 참조하여 컨트롤러와 스탯을 설정
    [ServerRpc(RequireOwnership = false)]
    public void AddPlayerServerRpc(NetworkObjectReference playerRef)
    {
        if (!networkPlayers.Contains(playerRef))
        {
            networkPlayers.Add(playerRef);
            if (playerRef.TryGet(out NetworkObject networkObject))
            {
                var playerController = networkObject.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    // 내부 클래스 GamePlayer 인스턴스 생성
                    var gamePlayer = new GamePlayer(networkObject.NetworkObjectId.ToString(), 
                                                    networkObject.name, 
                                                    playerController, 
                                                    networkObject);

                    var userInfo = playerData.Players.Find(p => p.Controller == playerController)?.UserInfo;
                    if (userInfo != null)
                    {
                    }
                    
                    if (!players.Any(p => p.PlayerController == playerController))
                    {
                        players.Add(gamePlayer);
                    }
                    
                    // 다른 클라이언트들에게도 플레이어 추가
                    SyncGamePlayerClientRpc(playerRef);
                }
            }
        }
    }

    // 클라이언트들에게 플레이어 정보를 동기화
    [ClientRpc]
    private void SyncGamePlayerClientRpc(NetworkObjectReference playerRef)
    {
        if (!IsServer)
        {
            if (playerRef.TryGet(out NetworkObject networkObject))
            {
                var playerController = networkObject.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    var gamePlayer = new GamePlayer(networkObject.NetworkObjectId.ToString(), 
                                                    networkObject.name, 
                                                    playerController, 
                                                    networkObject);
                    
                    if (!players.Any(p => p.PlayerController == playerController))
                    {
                        players.Add(gamePlayer);
                    }
                }
            }
        }
    }

    public void RemovePlayer(string playerId)
    {
        players.RemoveAll(p => p.PlayerId == playerId);
    }

    public void ClearPlayers()
    {
        players.Clear();
    }

    private async void OnApplicationQuit()
    {
        if (IsServer)
        {
            foreach (var player in players)
            {
                if (player != null && player.PlayerController != null && player.PlayerController.IsOwner)
                {
                    var playerController = player.PlayerController;
                    await APIClient.instance.SaveInfo(
                        playerController.PlayerName,
                        playerController.Stat.MaxHp,
                        playerController.Stat.AttackDamage,
                        playerController.Stat.BossGrade
                    );

                    Debug.Log($"Saved info for player: {playerController.PlayerName}");
                }
            }
        }
    }

#endregion
}
