/* Git Blame Auto Generated */

/* @LiF  - 2024-04-22 14:32:04 */ using UnityEngine;
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */ namespace FullMoon.FOW
/* @LiF  - 2024-04-22 14:32:04 */ {
/* @LiF  - 2024-04-22 14:32:04 */     public class FogOfWarController : MonoBehaviour
/* @LiF  - 2024-04-22 14:32:04 */     {
/* @LiF  - 2024-04-22 14:32:04 */         public Vector3 areaSize = new(10f, 10f, 10f);
/* @LiF  - 2024-04-22 14:32:04 */         public float gridSpacing = 1f;
/* @LiF  - 2024-04-22 14:32:04 */         public float gridBoxSize = 1f;
/* @LiF  - 2024-04-22 14:32:04 */         public LayerMask layerMask;
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */         private void OnDrawGizmos()
/* @LiF  - 2024-04-22 14:32:04 */         {
/* @LiF  - 2024-04-22 14:32:04 */             // 기본 큐브 그리기
/* @LiF  - 2024-04-22 14:32:04 */             Gizmos.color = Color.green;
/* @LiF  - 2024-04-22 14:32:04 */             Gizmos.DrawWireCube(transform.position, areaSize);
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */             // 윗면에서 그리드 포인트를 계산
/* @LiF  - 2024-04-22 14:32:04 */             Vector3 startPoint = transform.position + new Vector3(-areaSize.x / 2, areaSize.y / 2, -areaSize.z / 2);
/* @LiF  - 2024-04-22 14:32:04 */             int gridX = Mathf.CeilToInt(areaSize.x / gridSpacing);
/* @LiF  - 2024-04-22 14:32:04 */             int gridZ = Mathf.CeilToInt(areaSize.z / gridSpacing);
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */             for (int x = 0; x <= gridX; x++)
/* @LiF  - 2024-04-22 14:32:04 */             {
/* @LiF  - 2024-04-22 14:32:04 */                 for (int z = 0; z <= gridZ; z++)
/* @LiF  - 2024-04-22 14:32:04 */                 {
/* @LiF  - 2024-04-22 14:32:04 */                     Vector3 rayOrigin = startPoint + new Vector3(x * gridSpacing, 0, z * gridSpacing);
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */                     if (Physics.Raycast(rayOrigin, Vector3.down, out var hit, areaSize.y, ~layerMask))
/* @LiF  - 2024-04-22 14:32:04 */                     {
/* @LiF  - 2024-04-22 14:32:04 */                         // 바닥으로부터의 높이에 따라 색상 계산
/* @LiF  - 2024-04-22 14:32:04 */                         float heightAboveGround = hit.point.y - (transform.position.y - areaSize.y / 2);
/* @LiF  - 2024-04-22 14:32:04 */                         float maxPossibleHeight = areaSize.y;
/* @LiF  - 2024-04-22 14:32:04 */                         float colorFactor = heightAboveGround / maxPossibleHeight;
/* @LiF  - 2024-04-22 14:32:04 */ 
/* @LiF  - 2024-04-22 14:32:04 */                         // 색상은 바닥에서부터의 높이가 증가함에 따라 녹색에서 빨간색으로 변화
/* @LiF  - 2024-04-22 14:32:04 */                         Color lerpedColor = Color.Lerp(Color.green, Color.red, colorFactor);
/* @LiF  - 2024-04-22 14:32:04 */                         Gizmos.color = lerpedColor;
/* @LiF  - 2024-04-22 14:32:04 */                         Gizmos.DrawCube(hit.point, new Vector3(gridBoxSize, gridBoxSize, gridBoxSize));
/* @LiF  - 2024-04-22 14:32:04 */                     }
/* @LiF  - 2024-04-22 14:32:04 */                 }
/* @LiF  - 2024-04-22 14:32:04 */             }
/* @LiF  - 2024-04-22 14:32:04 */         }
/* @LiF  - 2024-04-22 14:32:04 */     }
/* @LiF  - 2024-04-22 14:32:04 */ }