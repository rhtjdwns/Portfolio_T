/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-03-26 16:38:08 */ using MyBox;
/* @Lee SJ  - 2024-03-26 16:38:08 */ using UnityEngine;
/* @Lee SJ  - 2024-03-26 16:38:08 */ 
/* @Lee SJ  - 2024-03-31 16:27:54 */ namespace FullMoon.ScriptableObject
/* @Lee SJ  - 2024-03-26 16:38:08 */ {
/* @Lee SJ  - 2024-03-26 16:38:08 */     [System.Serializable]
/* @Lee SJ  - 2024-03-26 16:38:08 */     [CreateAssetMenu(fileName = "RangedUnit", menuName = "Unit Data/Ranged Unit Data")]
/* @Lee SJ  - 2024-03-26 16:38:08 */     public class RangedUnitData : BaseUnitData
/* @Lee SJ  - 2024-03-26 16:38:08 */     {
/* @Lee SJ  - 2024-03-26 16:38:08 */         [Separator("Ranged Unit Settings")]
/* @Lee SJ  - 2024-03-26 16:38:08 */     
/* @Lee SJ  - 2024-04-12 19:33:46 */         [SerializeField, OverrideLabel("총알 속도")] private float bulletSpeed = 70f;
/* @Lee SJ  - 2024-03-30 23:12:08 */         public float BulletSpeed => bulletSpeed;
/* @Lee SJ  - 2024-05-07 20:09:25 */         
/* @Lee SJ  - 2024-05-07 20:09:25 */         [SerializeField, OverrideLabel("공격 애니메이션 프레임")] private int hitAnimationFrame = 12;
/* @Lee SJ  - 2024-05-07 20:09:25 */         public int HitAnimationFrame => hitAnimationFrame;
/* @Lee SJ  - 2024-03-26 16:38:08 */     }
/* @Lee SJ  - 2024-03-26 16:38:08 */ }