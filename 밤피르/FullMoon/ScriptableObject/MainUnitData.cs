/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-04-15 23:12:39 */ using MyBox;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using UnityEngine;
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */ namespace FullMoon.ScriptableObject
/* @Lee SJ  - 2024-04-15 23:12:39 */ {
/* @Lee SJ  - 2024-04-15 23:12:39 */     [System.Serializable]
/* @Lee SJ  - 2024-04-15 23:12:39 */     [CreateAssetMenu(fileName = "MainUnit", menuName = "Unit Data/Main Unit Data")]
/* @Lee SJ  - 2024-04-15 23:12:39 */     public class MainUnitData : BaseUnitData
/* @Lee SJ  - 2024-04-15 23:12:39 */     {
/* @Lee SJ  - 2024-04-17 22:24:33 */         [Separator("Main Unit Settings")] 
/* @Lee SJ  - 2024-04-17 22:24:33 */         
/* @Lee SJ  - 2024-06-02 21:06:48 */         [SerializeField, OverrideLabel("그로기 시간")] private float groggyTime = 5f;
/* @Lee SJ  - 2024-06-02 21:06:48 */         public float GroggyTime => groggyTime;
/* @Lee SJ  - 2024-04-17 22:24:33 */         
/* @Lee SJ  - 2024-04-17 22:24:33 */         [Separator]
/* @Lee SJ  - 2024-06-02 21:06:48 */         
/* @Lee SJ  - 2024-06-02 21:06:48 */         [SerializeField, OverrideLabel("공격 애니메이션 프레임")] private int hitAnimationFrame = 12;
/* @Lee SJ  - 2024-06-02 21:06:48 */         public int HitAnimationFrame => hitAnimationFrame;
/* @Lee SJ  - 2024-04-15 23:12:39 */     }
/* @Lee SJ  - 2024-04-15 23:12:39 */ }