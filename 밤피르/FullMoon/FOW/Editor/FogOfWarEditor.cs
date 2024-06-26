/* Git Blame Auto Generated */

/* @LiF  - 2024-04-22 14:32:04 */ using UnityEditor;
/* @LiF  - 2024-04-22 14:32:04 */ using UnityEngine;
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */ namespace FullMoon.FOW.Editor
/* @LiF  - 2024-04-22 14:32:04 */ {
/* @LiF  - 2024-04-22 14:32:04 */     [CustomEditor(typeof(FogOfWarController))]
/* @LiF  - 2024-04-22 14:32:04 */     public class FogOfWarEditor : UnityEditor.Editor
/* @LiF  - 2024-04-22 14:32:04 */     {
/* @LiF  - 2024-04-22 14:32:04 */         private void OnSceneGUI()
/* @LiF  - 2024-04-22 14:32:04 */         {
/* @LiF  - 2024-04-22 14:32:04 */             FogOfWarController fow = target as FogOfWarController;
/* @LiF  - 2024-04-22 14:32:04 */             Transform handleTransform = fow.transform;
/* @LiF  - 2024-04-22 14:32:04 */             
/* @LiF  - 2024-04-22 14:32:04 */             Vector3 localPosition = handleTransform.localPosition;
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */             Vector3[] points = new Vector3[6];
/* @LiF  - 2024-04-22 14:32:04 */             points[0] = localPosition + Vector3.right * fow.areaSize.x / 2;    // Right
/* @LiF  - 2024-04-22 14:32:04 */             points[1] = localPosition - Vector3.right * fow.areaSize.x / 2;    // Left
/* @LiF  - 2024-04-22 14:32:04 */             points[2] = localPosition + Vector3.forward * fow.areaSize.z / 2;  // Forward
/* @LiF  - 2024-04-22 14:32:04 */             points[3] = localPosition - Vector3.forward * fow.areaSize.z / 2;  // Back
/* @LiF  - 2024-04-22 14:32:04 */             points[4] = localPosition + Vector3.up * fow.areaSize.y / 2;       // Up
/* @LiF  - 2024-04-22 14:32:04 */             points[5] = localPosition - Vector3.up * fow.areaSize.y / 2;       // Down
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */             Handles.color = Color.yellow;
/* @LiF  - 2024-04-22 14:32:04 */             Vector3 snap = new Vector3(0.5f, 0.5f, 0.5f);
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */             for (int i = 0; i < points.Length; i++)
/* @LiF  - 2024-04-22 14:32:04 */             {
/* @LiF  - 2024-04-22 14:32:04 */                 EditorGUI.BeginChangeCheck();
/* @LiF  - 2024-04-22 14:32:04 */                 Vector3 newPoint = Handles.FreeMoveHandle(points[i],  0.2f, snap, Handles.CircleHandleCap);
/* @LiF  - 2024-04-22 14:32:04 */                 if (EditorGUI.EndChangeCheck())
/* @LiF  - 2024-04-22 14:32:04 */                 {
/* @LiF  - 2024-04-22 14:32:04 */                     Undo.RecordObject(fow, "Resize Fog of War Area");
/* @LiF  - 2024-04-22 14:32:04 */                     Undo.RecordObject(handleTransform, "Resize Fog of War Area Transform");
/* @LiF  - 2024-04-22 14:32:04 */                     
/* @LiF  - 2024-04-22 14:32:04 */                     float delta;
/* @LiF  - 2024-04-22 14:32:04 */                     switch (i)
/* @LiF  - 2024-04-22 14:32:04 */                     {
/* @LiF  - 2024-04-22 14:32:04 */                         case 0: // Right
/* @LiF  - 2024-04-22 14:32:04 */                             delta = newPoint.x - points[i].x;
/* @LiF  - 2024-04-22 14:32:04 */                             fow.areaSize.x += delta;
/* @LiF  - 2024-04-22 14:32:04 */                             localPosition.x += delta / 2;
/* @LiF  - 2024-04-22 14:32:04 */                             break;
/* @LiF  - 2024-04-22 14:32:04 */                         case 1: // Left
/* @LiF  - 2024-04-22 14:32:04 */                             delta = newPoint.x - points[i].x;
/* @LiF  - 2024-04-22 14:32:04 */                             fow.areaSize.x -= delta;
/* @LiF  - 2024-04-22 14:32:04 */                             localPosition.x += delta / 2;
/* @LiF  - 2024-04-22 14:32:04 */                             break;
/* @LiF  - 2024-04-22 14:32:04 */                         case 2: // Forward
/* @LiF  - 2024-04-22 14:32:04 */                             delta = newPoint.z - points[i].z;
/* @LiF  - 2024-04-22 14:32:04 */                             fow.areaSize.z += delta;
/* @LiF  - 2024-04-22 14:32:04 */                             localPosition.z += delta / 2;
/* @LiF  - 2024-04-22 14:32:04 */                             break;
/* @LiF  - 2024-04-22 14:32:04 */                         case 3: // Back
/* @LiF  - 2024-04-22 14:32:04 */                             delta = newPoint.z - points[i].z;
/* @LiF  - 2024-04-22 14:32:04 */                             fow.areaSize.z -= delta;
/* @LiF  - 2024-04-22 14:32:04 */                             localPosition.z += delta / 2;
/* @LiF  - 2024-04-22 14:32:04 */                             break;
/* @LiF  - 2024-04-22 14:32:04 */                         case 4: // Up
/* @LiF  - 2024-04-22 14:32:04 */                             delta = newPoint.y - points[i].y;
/* @LiF  - 2024-04-22 14:32:04 */                             fow.areaSize.y += delta;
/* @LiF  - 2024-04-22 14:32:04 */                             localPosition.y += delta / 2;
/* @LiF  - 2024-04-22 14:32:04 */                             break;
/* @LiF  - 2024-04-22 14:32:04 */                         case 5: // Down
/* @LiF  - 2024-04-22 14:32:04 */                             delta = newPoint.y - points[i].y;
/* @LiF  - 2024-04-22 14:32:04 */                             fow.areaSize.y -= delta;
/* @LiF  - 2024-04-22 14:32:04 */                             localPosition.y += delta / 2;
/* @LiF  - 2024-04-22 14:32:04 */                             break;
/* @LiF  - 2024-04-22 14:32:04 */                     }
/* @LiF  - 2024-04-22 14:32:04 */                     
/* @LiF  - 2024-04-22 14:32:04 */                     fow.areaSize = new Vector3(Mathf.Max(1f, fow.areaSize.x), Mathf.Max(1f, fow.areaSize.y), Mathf.Max(1f, fow.areaSize.z));
/* @LiF  - 2024-04-22 14:32:04 */                     handleTransform.localPosition = localPosition;
/* @LiF  - 2024-04-22 14:32:04 */                 }
/* @LiF  - 2024-04-22 14:32:04 */             }
/* @LiF  - 2024-04-22 14:32:04 */         }
/* @LiF  - 2024-04-22 14:32:04 */     }
/* @LiF  - 2024-04-22 14:32:04 */ }
