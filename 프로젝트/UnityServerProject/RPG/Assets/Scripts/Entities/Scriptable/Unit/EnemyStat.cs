using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyStat", menuName = "Unit Data/Enemy Unit Data")]
public class EnemyStat : BaseStat
{
    [Header("Enemy Setting")]
    [SerializeField] private Define.EnemyType enemyType;
    public Define.EnemyType EnemyType => enemyType;
}
