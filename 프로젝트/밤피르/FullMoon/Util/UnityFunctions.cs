/* Git Blame Auto Generated */

/* @LiF  - 2024-06-03 18:58:33 */ using UnityEngine;
/* @LiF  - 2024-06-03 18:58:33 */ 
/* @LiF  - 2024-06-03 18:58:33 */ namespace FullMoon.Util
/* @LiF  - 2024-06-03 18:58:33 */ {
/* @LiF  - 2024-06-03 18:58:33 */     public class UnityFunctions : MonoBehaviour
/* @LiF  - 2024-06-03 18:58:33 */     {
/* @LiF  - 2024-06-03 18:58:33 */         public static void Quit()
/* @LiF  - 2024-06-03 18:58:33 */         {
/* @LiF  - 2024-06-03 18:58:33 */ #if UNITY_EDITOR
/* @LiF  - 2024-06-03 18:58:33 */             UnityEditor.EditorApplication.isPlaying = false;
/* @LiF  - 2024-06-03 18:58:33 */ #else
/* @LiF  - 2024-06-03 18:58:33 */             Application.Quit();
/* @LiF  - 2024-06-03 18:58:33 */ #endif
/* @LiF  - 2024-06-03 18:58:33 */         }
/* @LiF  - 2024-06-03 18:58:33 */     }
/* @LiF  - 2024-06-03 18:58:33 */ }
