/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-04-17 16:41:43 */ using UnityEngine;
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */ namespace FullMoon.UI
/* @Lee SJ  - 2024-04-17 16:41:43 */ {
/* @Lee SJ  - 2024-04-17 16:41:43 */     public class LookAtCamera : MonoBehaviour
/* @Lee SJ  - 2024-04-17 16:41:43 */     {
/* @Lee SJ  - 2024-04-17 16:41:43 */         private UnityEngine.Camera Cam { get; set; }
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */         [SerializeField] private bool followX = true;
/* @Lee SJ  - 2024-04-17 16:41:43 */         [SerializeField] private bool followY = true;
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */         private void Start()
/* @Lee SJ  - 2024-04-17 16:41:43 */         {
/* @Lee SJ  - 2024-04-17 16:41:43 */             Cam = UnityEngine.Camera.main;
/* @Lee SJ  - 2024-04-17 16:41:43 */             UpdateRotation();
/* @Lee SJ  - 2024-04-17 16:41:43 */         }
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @LiF     - 2024-05-16 02:58:26 */         private void LateUpdate()
/* @Lee SJ  - 2024-04-17 16:41:43 */         {
/* @Lee SJ  - 2024-04-17 16:41:43 */             UpdateRotation();
/* @Lee SJ  - 2024-04-17 16:41:43 */         }
/* @Lee SJ  - 2024-04-17 16:41:43 */     
/* @Lee SJ  - 2024-04-17 16:41:43 */         private void UpdateRotation()
/* @Lee SJ  - 2024-04-17 16:41:43 */         {
/* @Lee SJ  - 2024-04-17 16:41:43 */             if (!followX && !followY)
/* @Lee SJ  - 2024-04-17 16:41:43 */                 return;
/* @Lee SJ  - 2024-04-17 16:41:43 */         
/* @Lee SJ  - 2024-04-17 16:41:43 */             float cameraYRotation = followX ? Cam.transform.eulerAngles.y : transform.eulerAngles.y;
/* @Lee SJ  - 2024-04-17 16:41:43 */             float cameraXRotation = followY ? Cam.transform.eulerAngles.x : transform.eulerAngles.x;
/* @Lee SJ  - 2024-04-17 16:41:43 */         
/* @Lee SJ  - 2024-04-17 16:41:43 */             transform.rotation = Quaternion.Euler(cameraXRotation, cameraYRotation, transform.eulerAngles.z);
/* @Lee SJ  - 2024-04-17 16:41:43 */         }
/* @Lee SJ  - 2024-04-17 16:41:43 */     }
/* @Lee SJ  - 2024-04-17 16:41:43 */ }