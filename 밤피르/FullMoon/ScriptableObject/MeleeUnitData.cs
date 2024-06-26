/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-07 19:41:53 */ using MyBox;
/* @Lee SJ  - 2024-04-02 04:24:46 */ using UnityEngine;
/* @Lee SJ  - 2024-04-02 04:24:46 */ 
/* @Lee SJ  - 2024-04-02 04:24:46 */ namespace FullMoon.ScriptableObject
/* @Lee SJ  - 2024-04-02 04:24:46 */ {
/* @Lee SJ  - 2024-04-02 04:24:46 */     [System.Serializable]
/* @Lee SJ  - 2024-04-02 04:24:46 */     [CreateAssetMenu(fileName = "MeleeUnit", menuName = "Unit Data/Melee Unit Data")]
/* @Lee SJ  - 2024-04-02 04:24:46 */     public class MeleeUnitData : BaseUnitData
/* @Lee SJ  - 2024-04-02 04:24:46 */     {
/* @Lee SJ  - 2024-05-07 19:41:53 */         [Separator("Melee Unit Settings")]
/* @Lee SJ  - 2024-05-07 19:41:53 */         
/* @Lee SJ  - 2024-05-07 19:41:53 */         [SerializeField, OverrideLabel("공격 애니메이션 프레임")] private int hitAnimationFrame = 12;
/* @Lee SJ  - 2024-05-07 19:41:53 */         public int HitAnimationFrame => hitAnimationFrame;
/* @LiF     - 2024-05-16 01:07:34 */         
/* @LiF     - 2024-05-16 01:07:34 */         [SerializeField, OverrideLabel("창 밀치기 프레임"), ConditionalField(nameof(UnitType), false, "Spear")] 
/* @LiF     - 2024-05-16 01:07:34 */         private int spearPushFrame = 2;
/* @LiF     - 2024-05-16 01:07:34 */         public int SpearPushFrame => spearPushFrame;
/* @LiF     - 2024-05-16 01:07:34 */         
/* @LiF     - 2024-05-16 01:07:34 */         [SerializeField, OverrideLabel("창 밀치기 힘"), ConditionalField(nameof(UnitType), false, "Spear")] 
/* @LiF     - 2024-05-16 01:07:34 */         private float spearPushForce = 10f;
/* @LiF     - 2024-05-16 01:07:34 */         public float SpearPushForce => spearPushForce;
/* @Lee SJ  - 2024-04-02 04:24:46 */     }
/* @Lee SJ  - 2024-04-02 04:24:46 */ }