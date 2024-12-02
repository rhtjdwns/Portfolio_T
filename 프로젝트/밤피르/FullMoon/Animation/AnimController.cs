/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-04-21 14:58:17 */ using UnityEngine;
/* @rhtjdwns  - 2024-04-21 14:58:17 */ using FullMoon.Util;
/* @rhtjdwns  - 2024-04-21 14:58:17 */ 
/* @rhtjdwns  - 2024-04-21 14:58:17 */ namespace FullMoon.Animation
/* @rhtjdwns  - 2024-04-21 14:58:17 */ {
/* @rhtjdwns  - 2024-04-21 14:58:17 */     public class AnimController : MonoBehaviour
/* @rhtjdwns  - 2024-04-21 14:58:17 */     {
/* @rhtjdwns  - 2024-04-21 14:58:17 */         public void OnAnimDie()
/* @rhtjdwns  - 2024-04-21 14:58:17 */         {
/* @Lee SJ    - 2024-05-08 18:25:40 */             ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
/* @rhtjdwns  - 2024-04-21 14:58:17 */         }
/* @rhtjdwns  - 2024-04-21 14:58:17 */     }
/* @rhtjdwns  - 2024-04-21 14:58:17 */ }
/* @rhtjdwns  - 2024-04-21 14:58:17 */ 
