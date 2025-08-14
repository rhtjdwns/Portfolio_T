using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : BaseCharacter
{
    private NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>(
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    public string PlayerName => playerName.Value.ToString();

    [Header("Name UI")]
    [SerializeField] private GameObject nameUIPrefab;  // 이름 UI 프리팹
    [SerializeField] private Vector3 nameOffset = new Vector3(0, 0, 0);  // 이름 위치 오프셋
    private GameObject nameUIObject;
    private TMP_Text nameText;

    [Header("Movement")]
    private Joystick joystick;
    private Vector3 moveInput;

    [Header("Dead Reckoning")]
    private Vector3 networkPosition;
    private Vector3 networkVelocity;
    private float lastNetworkUpdate;
    private float errorThreshold = 0.5f; // 위치 오차 허용 범위

    [Header("Combat System")]
    [SerializeField] private Transform firePoint; // 총알 발사 위치
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] public float bulletDamage = 1f;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private bool autoFire = true; // 자동 발사 여부
    [SerializeField] private float detectionRadius = 10f; // 적 감지 반경
    [SerializeField] private LayerMask enemyLayerMask = 64; // 적 레이어 마스크 (Layer 6)

    private Coroutine fireCoroutine;
    private List<Enemy> detectedEnemies = new List<Enemy>();

    protected override void Awake()
    {
        base.Awake();

        // FirePoint가 없으면 자동으로 생성
        if (firePoint == null)
        {
            GameObject firePointObj = new GameObject("FirePoint");
            firePointObj.transform.SetParent(transform);
            firePointObj.transform.localPosition = new Vector3(0, 1f, 0.5f); // 플레이어 앞쪽
            firePoint = firePointObj.transform;
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        UpdatePlayerPosition();
        CreateNameUI();

        if (IsOwner)
        {
            if (MatchManager.instance != null)
            {
                playerName.Value = MatchManager.instance.PlayerName;
                joystick.GetComponent<UnityEngine.UI.Image>().raycastTarget = true;
                
                // 클라이언트에서 자신의 스탯을 서버를 통해 설정
                var userInfo = MatchManager.instance.clientUserInfo;
                InitializeStatsServerRpc(userInfo.maxHp, userInfo.attackDamage, userInfo.bossGrade);
            }
        }

        // 씬 변경 이벤트 구독
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        
        // 보스 스폰 이벤트 구독
        GameManager.OnBossSpawned += OnBossSpawned;
    }

    private void OnBossSpawned(Enemy boss)
    {
        if (boss.Stat is EnemyStat enemyStat && enemyStat.EnemyType == Define.EnemyType.BOSS)
        {
            boss.RegisterPlayer(this);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        
        // 이름 UI 제거
        if (nameUIObject != null)
        {
            Destroy(nameUIObject);
            nameUIObject = null;
        }
        
        // 이벤트 구독 해제
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        GameManager.OnBossSpawned -= OnBossSpawned;
        
        // 발사 코루틴 정지
        StopAutoFire();
    }

    public override void Die()
    {
        base.Die();
        
        // 플레이어가 죽었을 때 이름 UI 제거
        if (nameUIObject != null)
        {
            Destroy(nameUIObject);
            nameUIObject = null;
        }
    }

    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        UpdatePlayerPosition();
        
        // 씬 변경 시 이름 UI 다시 생성
        if (nameUIObject != null)
        {
            Destroy(nameUIObject);
        }
        CreateNameUI();
    }

    private void UpdatePlayerPosition()
    {
        // 씬 이름 확인
        string currentScene = SceneManager.GetActiveScene().name;
        
        // 조이스틱 갱신
        joystick = FindObjectOfType<FloatingJoystick>().GetComponent<Joystick>();

        if (currentScene == "LobbyScene")
        {
            if (MatchManager.instance != null)
            {
                transform.position = MatchManager.instance.spawnPoint.position;
            }
        }
        else if (currentScene == "GameScene")
        {
            if (GameManager.instance != null)
            {
                transform.position = GameManager.instance.spawnPoint.position;
                StartCoroutine(AutoFireRoutine());
            }
        }
    }

    private void CreateNameUI()
    {
        // 이름 UI 생성
        nameUIObject = Instantiate(nameUIPrefab, transform.position + nameOffset, Quaternion.identity);
        nameUIObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        nameUIObject.GetComponent<TracePlayerName>().Init(transform, nameOffset);
        nameText = nameUIObject.GetComponentInChildren<TMP_Text>();

        if (nameText != null)
        {
            nameText.text = playerName.Value.ToString();
        }
    }
    
    protected override void Update()
    {
        if (!IsOwner)
        {
            // 다른 플레이어의 움직임을 데드레커닝으로 보간
            float timeSinceLastUpdate = Time.time - lastNetworkUpdate;
            Vector3 predictedPosition = networkPosition + (networkVelocity * timeSinceLastUpdate);
            transform.position = Vector3.Lerp(transform.position, predictedPosition, Time.deltaTime * 10f);
            return;
        }

        base.Update();
        moveInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

        // 로컬 플레이어의 즉각적인 이동 처리
        if (moveInput != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = newRotation;
        }
    }

    protected override void FixedUpdate()
    {
        if (!IsOwner || Rb == null) return;

        base.FixedUpdate();
        
        // 서버에 이동 정보 전송
        MoveServerRpc(moveInput);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 input)
    {
        if (Rb == null) return;

        Vector3 velocity = Vector3.zero;
        if (input != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(input);
            transform.rotation = newRotation;
            
            velocity = input.normalized * Stat.MoveSpeed;
            Vector3 newPosition = transform.position + (velocity * Time.fixedDeltaTime);
            
            // 서버에서 위치 업데이트
            transform.position = newPosition;

            // 모든 클라이언트에게 위치, 속도 정보 전송
            UpdatePositionClientRpc(newPosition, velocity);
            UpdateRotationClientRpc(newRotation);
        }
        else
        {
            // 입력이 없을 때는 속도를 0으로 전송
            UpdatePositionClientRpc(transform.position, Vector3.zero);
        }
        
        UpdateAnimationClientRpc(input != Vector3.zero);
    }

    [ClientRpc]
    private void UpdatePositionClientRpc(Vector3 position, Vector3 velocity)
    {
        // 다른 클라이언트들의 데드레커닝 정보 업데이트
        networkPosition = position;
        networkVelocity = velocity;
        lastNetworkUpdate = Time.time;

        if (IsOwner)
        {
            // 오차가 크면 위치 보정
            float positionError = Vector3.Distance(transform.position, position);
            if (positionError > errorThreshold)
            {
                transform.position = position;
            }
        }
    }

    [ClientRpc]
    private void UpdateRotationClientRpc(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }

    [ClientRpc]
    private void UpdateAnimationClientRpc(bool isMoving)
    {
        if (Anim != null)
        {
            Anim.SetBool("IsMove", isMoving);
        }
    }

    // 적 감지 업데이트
    private void UpdateDetectedEnemies()
    {
        detectedEnemies.Clear();
        
        // OverlapSphere를 사용하여 성능 최적화
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out var enemy))
            {
                // 네트워크 스폰된 적이고 살아있는 경우만 감지
                if (enemy.IsSpawned && enemy.Alive)
                {
                    detectedEnemies.Add(enemy);
                }
            }
        }
        
        // 거리순으로 정렬 (가장 가까운 적이 먼저)
        detectedEnemies.Sort((a, b) => 
            Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
    }

    // 가장 가까운 적 가져오기
    private Enemy GetNearestEnemy()
    {
        UpdateDetectedEnemies();
        if (detectedEnemies.Count == 0) return null;
        return detectedEnemies[0];
    }

    // 자동 발사 정지
    public void StopAutoFire()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    // 자동 발사 코루틴
    private IEnumerator AutoFireRoutine()
    {
        while (true)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null)
            {
                FireAtNearestEnemy();
            }

            yield return new WaitForSeconds(Stat.AttackDelay);
        }
    }

    // 가장 가까운 적에게 발사
    private void FireAtNearestEnemy()
    {
        Enemy nearestEnemy = GetNearestEnemy();
        if (nearestEnemy != null)
        {
            // 적을 향한 방향 계산
            Vector3 direction = (nearestEnemy.transform.position - transform.position).normalized;
            direction.y = 0; // Y축 회전만

            if (IsServer)
            {
                FireBullet(firePoint.position, direction, bulletDamage, nearestEnemy.transform.position);
            }
            //BulletInitClientRpc(firePoint.position, direction, bulletDamage);
        }
    }

    private void FireBullet(Vector3 startPosition, Vector3 direction, float damage, Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        targetDirection.y = 0;
        if (targetDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        
        SpawnObject(startPosition, direction, damage);
    }

    [ClientRpc]
    private void BulletInitClientRpc(NetworkObjectReference obj, Vector3 startPos, Vector3 dir, float damage)
    {
        NetworkObject noGameObject = obj;

        if (noGameObject != null)
        {
            noGameObject.gameObject.SetActive(true);
            var bulletComponent = noGameObject.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.Initialize(startPos, dir, damage);
            }
        }
    }

    private void SpawnObject(Vector3 startPosition, Vector3 direction, float damage)
    {
        NetworkObject bulletNetworkObject = NetworkObjectPool.Singleton.GetNetworkObject(bulletPrefab, startPosition, Quaternion.LookRotation(direction));
        
        if (!bulletNetworkObject.IsSpawned)
        {
            bulletNetworkObject.Spawn();
        }

        NetworkObjectReference objectReference = new NetworkObjectReference(bulletNetworkObject);
        BulletInitClientRpc(objectReference, startPosition, direction, damage);
    }

    private void OnEnable()
    {
        playerName.OnValueChanged += OnPlayerNameChanged;
    }

    private void OnDisable()
    {
        playerName.OnValueChanged -= OnPlayerNameChanged;
    }

    private void OnPlayerNameChanged(FixedString32Bytes previousValue, FixedString32Bytes newValue)
    {
        if (nameText != null)
        {
            nameText.text = newValue.ToString();
        }
    }
} 