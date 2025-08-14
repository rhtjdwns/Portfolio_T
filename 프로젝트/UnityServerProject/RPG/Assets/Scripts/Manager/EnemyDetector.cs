using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask enemyLayerMask = -1;
    [SerializeField] private float updateInterval = 0.2f; // 0.2초마다 감지 업데이트
    
    [Header("Performance")]
    [SerializeField] private int maxDetectedEnemies = 10;
    [SerializeField] private bool useSpatialPartitioning = true;
    
    private List<Enemy> detectedEnemies = new List<Enemy>();
    private Coroutine detectionCoroutine;
    
    public List<Enemy> DetectedEnemies => detectedEnemies;
    public bool HasDetectedEnemies => detectedEnemies.Count > 0;
    
    private void Start()
    {
        if (useSpatialPartitioning)
        {
            StartDetectionCoroutine();
        }
    }
    
    private void StartDetectionCoroutine()
    {
        if (detectionCoroutine != null)
        {
            StopCoroutine(detectionCoroutine);
        }
        detectionCoroutine = StartCoroutine(DetectionRoutine());
    }
    
    private IEnumerator DetectionRoutine()
    {
        while (true)
        {
            UpdateDetectedEnemies();
            yield return new WaitForSeconds(updateInterval);
        }
    }
    
    private void UpdateDetectedEnemies()
    {
        detectedEnemies.Clear();
        
        // OverlapSphere를 사용하여 성능 최적화
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        
        int enemyCount = 0;
        foreach (var collider in colliders)
        {
            if (enemyCount >= maxDetectedEnemies) break;
            
            if (collider.TryGetComponent<Enemy>(out var enemy))
            {
                if (enemy.IsSpawned && enemy.Alive)
                {
                    detectedEnemies.Add(enemy);
                    enemyCount++;
                }
            }
        }
        
        // 거리순으로 정렬 (가장 가까운 적이 먼저)
        detectedEnemies.Sort((a, b) => 
            Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
    }
    
    public Enemy GetNearestEnemy()
    {
        if (detectedEnemies.Count == 0) return null;
        return detectedEnemies[0];
    }
    
    public List<Enemy> GetEnemiesInRange(float range)
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        
        foreach (var enemy in detectedEnemies)
        {
            if (enemy == null) continue;
            
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }
        
        return enemiesInRange;
    }
    
    public bool IsEnemyInRange(Enemy enemy, float range)
    {
        if (enemy == null) return false;
        
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        return distance <= range;
    }
    
    private void OnDrawGizmosSelected()
    {
        // 감지 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        // 감지된 적들 표시
        Gizmos.color = Color.red;
        foreach (var enemy in detectedEnemies)
        {
            if (enemy != null)
            {
                Gizmos.DrawLine(transform.position, enemy.transform.position);
            }
        }
    }
    
    private void OnDestroy()
    {
        if (detectionCoroutine != null)
        {
            StopCoroutine(detectionCoroutine);
        }
    }
} 