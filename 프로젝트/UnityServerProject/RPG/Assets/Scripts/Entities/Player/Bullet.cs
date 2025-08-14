using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private LayerMask targetLayers = -1;
    
    private Vector3 direction;
    private float currentLifetime;
    private NetworkObject networkObject;
    
    public void Initialize(Vector3 startPosition, Vector3 direction, float damage)
    {
        transform.position = startPosition;
        this.direction = direction.normalized;
        this.damage = damage;
        currentLifetime = 0f;
        
        // 총알이 발사 방향을 향하도록 rotation 설정
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        networkObject = GetComponent<NetworkObject>();
    }
    
    private void OnEnable() 
    {
        ResetBullet();
    }

    private void Update()
    {
        // 모든 클라이언트에서 총알 이동 (시각적 동기화)
        transform.position += direction * speed * Time.deltaTime;
        
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime && gameObject.activeSelf)
        {
            DeactivateBullet();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & targetLayers) == 0) 
        {
            return;
        }
        
        if (other.TryGetComponent<BaseCharacter>(out var character) && IsOwner)
        {
            character.ReceiveDamageServerRpc(damage);
        }

        DeactivateBullet();
    }
    
    private void DeactivateBullet()
    {
        GameObject bulletPrefab = NetworkObjectPool.Singleton.GetPrefabByName("Bullet");
        if (bulletPrefab != null && IsServer)
        {
            NetworkObjectPool.Singleton.ReturnNetworkObject(networkObject, bulletPrefab);

            NetworkObjectReference objectReference = new NetworkObjectReference(networkObject);
            DeactivateBulletClientRpc(objectReference);
        }
    }

    [ClientRpc]
    private void DeactivateBulletClientRpc(NetworkObjectReference obj)
    {
        NetworkObject noObject = obj;
        noObject.gameObject.SetActive(false);
    }

    public void ResetBullet()
    {
        currentLifetime = 0f;
        direction = Vector3.zero;
    }
}