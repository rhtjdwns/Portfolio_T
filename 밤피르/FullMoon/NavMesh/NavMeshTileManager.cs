/* Git Blame Auto Generated */

/* @LiF  - 2024-05-27 02:55:49 */ using MyBox;
/* @LiF  - 2024-05-27 02:55:49 */ using UnityEngine;
/* @LiF  - 2024-05-27 02:55:49 */ using Unity.AI.Navigation;
/* @LiF  - 2024-05-27 02:55:49 */ using UnityEngine.AI;
/* @LiF  - 2024-05-27 02:55:49 */ using UnityEditor;
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */ namespace FullMoon.NavMesh
/* @LiF  - 2024-05-27 02:55:49 */ {
/* @LiF  - 2024-05-27 02:55:49 */     public class NavMeshTileManager : MonoBehaviour
/* @LiF  - 2024-05-27 02:55:49 */     {
/* @LiF  - 2024-05-27 02:55:49 */         [DefinedValues(nameof(GetChildGameObjects))]
/* @LiF  - 2024-05-27 02:55:49 */         public Transform ground;
/* @LiF  - 2024-05-27 02:55:49 */         
/* @LiF  - 2024-05-27 02:55:49 */         [DefinedValues(nameof(GetChildGameObjects))]
/* @LiF  - 2024-05-27 02:55:49 */         public Transform water;
/* @LiF  - 2024-05-27 02:55:49 */         
/* @LiF  - 2024-05-27 02:55:49 */         [DefinedValues("Ground", "Water")]
/* @LiF  - 2024-05-27 02:55:49 */         public string tileType = "Ground";
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */         private string _currentTileType;
/* @LiF  - 2024-05-27 02:55:49 */         private NavMeshData _originalNavMeshData;
/* @LiF  - 2024-05-27 17:17:22 */ 
/* @LiF  - 2024-05-27 02:55:49 */         [ButtonMethod]
/* @LiF  - 2024-05-27 02:55:49 */         public void ChangeTileType()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             tileType = tileType == "Ground" ? "Water" : "Ground";
/* @LiF  - 2024-05-27 02:55:49 */             UpdateTileActivation();
/* @LiF  - 2024-05-27 17:17:22 */ #if UNITY_EDITOR
/* @LiF  - 2024-05-27 02:55:49 */             BuildAllNavMeshSurfacesInEditor();
/* @LiF  - 2024-05-27 17:17:22 */ #else
/* @LiF  - 2024-05-27 17:17:22 */             BuildAllNavMeshSurfaces();
/* @LiF  - 2024-05-27 17:17:22 */ #endif
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 17:17:22 */         
/* @LiF  - 2024-05-27 02:55:49 */         private void Start()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             _currentTileType = tileType;
/* @LiF  - 2024-05-27 02:55:49 */             UpdateTileActivation();
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */         private void Update()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             if (_currentTileType != tileType)
/* @LiF  - 2024-05-27 02:55:49 */             {
/* @LiF  - 2024-05-27 02:55:49 */                 _currentTileType = tileType;
/* @LiF  - 2024-05-27 02:55:49 */                 UpdateTileActivation();
/* @LiF  - 2024-05-27 02:55:49 */                 BuildAllNavMeshSurfaces();
/* @LiF  - 2024-05-27 02:55:49 */             }
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */         private Transform[] GetChildGameObjects()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             return GetComponentsInChildren<Transform>(true);
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */         private void UpdateTileActivation()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             ground.gameObject.SetActive(tileType == "Ground");
/* @LiF  - 2024-05-27 02:55:49 */             water.gameObject.SetActive(tileType == "Water");
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */         private void BuildAllNavMeshSurfaces()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             NavMeshSurface[] navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
/* @LiF  - 2024-05-27 02:55:49 */             foreach (NavMeshSurface surface in navMeshSurfaces)
/* @LiF  - 2024-05-27 02:55:49 */             {
/* @LiF  - 2024-05-27 02:55:49 */                 // 원본 NavMeshData를 저장
/* @LiF  - 2024-05-27 02:55:49 */                 _originalNavMeshData = surface.navMeshData;
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */                 // 새로운 NavMeshData 인스턴스를 생성하여 기존 데이터를 덮어쓰지 않도록 함
/* @LiF  - 2024-05-27 02:55:49 */                 surface.navMeshData = new NavMeshData();
/* @LiF  - 2024-05-27 02:55:49 */                 surface.BuildNavMesh();
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */                 // 원본 NavMeshData로 복원
/* @LiF  - 2024-05-27 02:55:49 */                 surface.navMeshData = _originalNavMeshData;
/* @LiF  - 2024-05-27 02:55:49 */             }
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */ #if UNITY_EDITOR
/* @LiF  - 2024-05-27 02:55:49 */         private void OnValidate()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             if (Application.isPlaying == false && _currentTileType != tileType)
/* @LiF  - 2024-05-27 02:55:49 */             {
/* @LiF  - 2024-05-27 02:55:49 */                 _currentTileType = tileType;
/* @LiF  - 2024-05-27 02:55:49 */                 UpdateTileActivation();
/* @LiF  - 2024-05-27 02:55:49 */                 BuildAllNavMeshSurfacesInEditor();
/* @LiF  - 2024-05-27 02:55:49 */             }
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */         private void BuildAllNavMeshSurfacesInEditor()
/* @LiF  - 2024-05-27 02:55:49 */         {
/* @LiF  - 2024-05-27 02:55:49 */             NavMeshSurface[] navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();
/* @LiF  - 2024-05-27 02:55:49 */             foreach (NavMeshSurface surface in navMeshSurfaces)
/* @LiF  - 2024-05-27 02:55:49 */             {
/* @LiF  - 2024-05-27 02:55:49 */                 // 원본 NavMeshData를 저장
/* @LiF  - 2024-05-27 02:55:49 */                 _originalNavMeshData = surface.navMeshData;
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */                 // 새로운 NavMeshData 인스턴스를 생성하여 기존 데이터를 덮어쓰지 않도록 함
/* @LiF  - 2024-05-27 02:55:49 */                 surface.navMeshData = new NavMeshData();
/* @LiF  - 2024-05-27 02:55:49 */                 surface.BuildNavMesh();
/* @LiF  - 2024-05-27 02:55:49 */ 
/* @LiF  - 2024-05-27 02:55:49 */                 // 원본 NavMeshData로 복원
/* @LiF  - 2024-05-27 02:55:49 */                 surface.navMeshData = _originalNavMeshData;
/* @LiF  - 2024-05-27 02:55:49 */                 EditorUtility.SetDirty(surface);
/* @LiF  - 2024-05-27 02:55:49 */             }
/* @LiF  - 2024-05-27 02:55:49 */         }
/* @LiF  - 2024-05-27 02:55:49 */ #endif
/* @LiF  - 2024-05-27 02:55:49 */     }
/* @LiF  - 2024-05-27 02:55:49 */ }
