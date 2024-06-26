/* Git Blame Auto Generated */

/* @LiF  - 2024-06-03 17:17:34 */ using UnityEngine;
/* @LiF  - 2024-06-03 17:17:34 */ 
/* @LiF  - 2024-06-03 17:17:34 */ namespace FullMoon.Util
/* @LiF  - 2024-06-03 17:17:34 */ {
/* @LiF  - 2024-06-03 17:17:34 */     public class DestroyOnBuild : MonoBehaviour
/* @LiF  - 2024-06-03 17:17:34 */     {
/* @LiF  - 2024-06-03 17:17:34 */         [SerializeField] private bool destroyInDevelopmentBuild;
/* @LiF  - 2024-06-03 17:17:34 */ 
/* @LiF  - 2024-06-03 17:17:34 */         private void Awake()
/* @LiF  - 2024-06-03 17:17:34 */         {
/* @LiF  - 2024-06-03 17:17:34 */ #if DEVELOPMENT_BUILD
/* @LiF  - 2024-06-03 17:17:34 */             if (destroyInDevelopmentBuild)
/* @LiF  - 2024-06-03 17:17:34 */             {
/* @LiF  - 2024-06-03 17:17:34 */                 Destroy(gameObject);
/* @LiF  - 2024-06-03 17:17:34 */             }
/* @LiF  - 2024-06-03 18:58:33 */ #elif !UNITY_EDITOR
/* @LiF  - 2024-06-03 17:17:34 */             Destroy(gameObject);
/* @LiF  - 2024-06-03 17:17:34 */ #endif
/* @LiF  - 2024-06-03 17:17:34 */         }
/* @LiF  - 2024-06-03 17:17:34 */     }
/* @LiF  - 2024-06-03 17:17:34 */ }