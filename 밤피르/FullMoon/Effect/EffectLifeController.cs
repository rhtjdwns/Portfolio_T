/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-04-22 22:13:47 */ using UnityEngine;
/* @Lee SJ  - 2024-04-22 22:13:47 */ using FullMoon.Util;
/* @Lee SJ  - 2024-04-22 22:13:47 */ 
/* @Lee SJ  - 2024-04-22 22:13:47 */ namespace FullMoon.Effect
/* @Lee SJ  - 2024-04-22 22:13:47 */ {
/* @Lee SJ  - 2024-05-08 18:25:40 */     public class EffectLifeController : MonoBehaviour
/* @Lee SJ  - 2024-04-22 22:13:47 */     {
/* @Lee SJ  - 2024-05-08 18:25:40 */         [SerializeField] private float lifeDuration = 2f;
/* @Lee SJ  - 2024-05-08 18:25:40 */         
/* @Lee SJ  - 2024-04-22 22:13:47 */         private void OnEnable()
/* @Lee SJ  - 2024-04-22 22:13:47 */         {
/* @Lee SJ  - 2024-04-22 22:13:47 */             CancelInvoke(nameof(DestroyEffect));
/* @Lee SJ  - 2024-05-08 18:25:40 */             Invoke(nameof(DestroyEffect), lifeDuration);
/* @Lee SJ  - 2024-04-22 22:13:47 */         }
/* @Lee SJ  - 2024-04-22 22:13:47 */ 
/* @Lee SJ  - 2024-04-22 22:13:47 */         private void DestroyEffect()
/* @Lee SJ  - 2024-04-22 22:13:47 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (gameObject.activeInHierarchy)
/* @Lee SJ  - 2024-04-22 22:13:47 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
/* @Lee SJ  - 2024-04-22 22:13:47 */             }
/* @Lee SJ  - 2024-04-22 22:13:47 */         }
/* @Lee SJ  - 2024-04-22 22:13:47 */     }
/* @Lee SJ  - 2024-04-22 22:13:47 */ }