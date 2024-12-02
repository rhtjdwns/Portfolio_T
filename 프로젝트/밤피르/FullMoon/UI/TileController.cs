/* Git Blame Auto Generated */

/* @LiF       - 2024-06-02 11:54:10 */ using MyBox;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ using System.Collections.Generic;
/* @rhtjdwns  - 2024-05-13 16:30:04 */ using UnityEngine;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ using UnityEngine.AI;
/* @LiF       - 2024-06-02 11:54:10 */ using Unity.AI.Navigation;
/* @rhtjdwns  - 2024-05-14 18:13:00 */ using UnityEngine.Tilemaps;
/* @LiF       - 2024-06-02 11:54:10 */ using FullMoon.Util;
/* @LiF       - 2024-06-02 11:54:10 */ using FullMoon.NavMesh;
/* @rhtjdwns  - 2024-05-13 16:30:04 */ 
/* @rhtjdwns  - 2024-05-13 16:30:04 */ namespace FullMoon.UI
/* @rhtjdwns  - 2024-05-13 16:30:04 */ {
/* @rhtjdwns  - 2024-05-30 11:28:19 */     public enum BuildingType
/* @rhtjdwns  - 2024-05-30 11:28:19 */     {
/* @rhtjdwns  - 2024-05-30 11:28:19 */         None,
/* @rhtjdwns  - 2024-06-02 06:42:57 */         Ground,         // 타일
/* @rhtjdwns  - 2024-05-30 11:28:19 */         LumberMill,     // 벌목소
/* @rhtjdwns  - 2024-05-30 11:28:19 */         SwordArmy,      // 훈련소(검)
/* @rhtjdwns  - 2024-05-30 11:28:19 */         SpearArmy,      // 훈련소(창)
/* @rhtjdwns  - 2024-05-30 11:28:19 */         CrossbowArmy,   // 훈련소(석궁)
/* @rhtjdwns  - 2024-05-30 11:28:19 */     }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-13 16:30:04 */     public class TileController : ComponentSingleton<TileController>
/* @rhtjdwns  - 2024-05-13 16:30:04 */     {
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Separator("TileMap")]
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         [SerializeField, OverrideLabel("타일 프리팹")]
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private GameObject tilePrefab;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         [SerializeField, OverrideLabel("타일 가로 사이즈")]
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private int tileWidthSize = 1;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         [SerializeField, OverrideLabel("타일 세로 사이즈")]
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private int tileHeightSize = 1;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         [Separator]
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("벌목소 프리팹")] 
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject lumberPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("벌목소 가로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int lumberWidthSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("벌목소 세로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int lumberHeightSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Separator]
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(검) 프리팹")]
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject swordArmyPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(검) 가로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int swordWidthSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(검) 세로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int swordHeightSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Separator]
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(창) 프리팹")]
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject spearArmyPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(창) 가로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int spearWidthSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(창) 세로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int spearHeightSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Separator]
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(석궁) 프리팹")]
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject crossbowArmyPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(석궁) 가로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int crossbowWidthSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("훈련소(석궁) 세로 사이즈")]
/* @LiF       - 2024-06-01 15:37:58 */         private int crossbowHeightSize = 1;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         [Separator]
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         [SerializeField, OverrideLabel("건설 UI 버튼")]
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject buildUIButton;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         [SerializeField, OverrideLabel("건설 UI")]
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject buildUI;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         [SerializeField, OverrideLabel("건설 UI 취소 버튼")]
/* @LiF       - 2024-06-01 15:37:58 */         private GameObject cancelBuildUI;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         [Separator]
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         public NavMeshSurface playerSurfaces;
/* @rhtjdwns  - 2024-06-02 06:42:57 */         public NavMeshSurface enemySurfaces;
/* @LiF       - 2024-06-02 11:54:10 */         
/* @LiF       - 2024-06-02 11:54:10 */         private List<NavMeshBuildSource> playerSources = new();
/* @LiF       - 2024-06-02 11:54:10 */         private List<NavMeshBuildSource> enemySources = new();
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private Tilemap buildingTileMap;
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private Tilemap groundTileMap;
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private Tilemap baseTile;
/* @rhtjdwns  - 2024-05-13 16:30:04 */ 
/* @Lee SJ    - 2024-06-02 20:34:51 */         private void Start()
/* @rhtjdwns  - 2024-05-13 16:30:04 */         {
/* @Lee SJ    - 2024-06-02 20:34:51 */             buildingTileMap = GetComponent<GameObjectDictionary>().GetComponentByName<Tilemap>("Building");
/* @Lee SJ    - 2024-06-02 20:34:51 */             groundTileMap = GetComponent<GameObjectDictionary>().GetComponentByName<Tilemap>("Ground");
/* @LiF       - 2024-06-02 11:54:10 */             
/* @LiF       - 2024-06-02 11:54:10 */             if (playerSurfaces == null)
/* @LiF       - 2024-06-02 11:54:10 */             {
/* @LiF       - 2024-06-02 11:54:10 */                 playerSurfaces = GameObject.Find("Player Navmesh Surface").GetComponent<NavMeshSurface>();
/* @LiF       - 2024-06-02 11:54:10 */             }
/* @LiF       - 2024-06-02 11:54:10 */             
/* @LiF       - 2024-06-02 11:54:10 */             if (enemySurfaces == null)
/* @LiF       - 2024-06-02 11:54:10 */             {
/* @LiF       - 2024-06-02 11:54:10 */                 enemySurfaces = GameObject.Find("Enemy Navmesh Surface").GetComponent<NavMeshSurface>();
/* @LiF       - 2024-06-02 11:54:10 */             }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             BuildNavMesh();
/* @rhtjdwns  - 2024-05-31 16:04:17 */         }
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         public void OffBuildingUI()
/* @rhtjdwns  - 2024-05-31 16:04:17 */         {
/* @rhtjdwns  - 2024-05-31 16:04:17 */             buildUIButton.SetActive(true);
/* @rhtjdwns  - 2024-05-31 16:04:17 */             buildUI.SetActive(false);
/* @rhtjdwns  - 2024-05-31 16:04:17 */         }
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         public void OnBuildingUI()
/* @rhtjdwns  - 2024-05-31 16:04:17 */         {
/* @rhtjdwns  - 2024-05-31 16:04:17 */             buildUIButton.SetActive(false);
/* @rhtjdwns  - 2024-05-31 16:04:17 */             buildUI.SetActive(true);
/* @rhtjdwns  - 2024-05-13 16:30:04 */         }
/* @rhtjdwns  - 2024-05-13 16:30:04 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */         public void CreateTile(Vector3 pos, BuildingType building)
/* @rhtjdwns  - 2024-05-14 18:13:00 */         {
/* @rhtjdwns  - 2024-05-30 11:28:19 */             GameObject tile;
/* @LiF       - 2024-06-01 15:37:58 */             float width;
/* @LiF       - 2024-06-01 15:37:58 */             float height;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-31 16:04:17 */             switch (building)
/* @rhtjdwns  - 2024-05-30 11:28:19 */             {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 case BuildingType.Ground:
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     tile = tilePrefab;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     width = tileWidthSize;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     height = tileHeightSize;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     baseTile = groundTileMap;
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     break;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 case BuildingType.LumberMill:
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     tile = lumberPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     width = lumberWidthSize;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     height = lumberHeightSize;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     baseTile = buildingTileMap;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     break;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 case BuildingType.SwordArmy:
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     tile = swordArmyPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     width = swordWidthSize;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     height = swordHeightSize;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     baseTile = buildingTileMap;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     break;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 case BuildingType.SpearArmy:
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     tile = spearArmyPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     width = spearWidthSize;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     height = spearHeightSize;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     baseTile = buildingTileMap;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     break;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 case BuildingType.CrossbowArmy:
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     tile = crossbowArmyPrefab;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     width = crossbowWidthSize;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     height = crossbowHeightSize;
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */                     baseTile = buildingTileMap;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     break;
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 default:
/* @rhtjdwns  - 2024-05-30 11:28:19 */                     return;
/* @rhtjdwns  - 2024-05-30 11:28:19 */             }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             Vector3Int vector = baseTile.WorldToCell(pos);
/* @rhtjdwns  - 2024-06-01 00:35:06 */ 
/* @LiF       - 2024-06-01 15:37:58 */             var localScale = tile.transform.localScale;
/* @LiF       - 2024-06-01 15:37:58 */             localScale = new Vector3(localScale.x * width, localScale.y * height, localScale.z);
/* @LiF       - 2024-06-01 15:37:58 */             
/* @LiF       - 2024-06-01 15:37:58 */             tile.transform.localScale = localScale;
/* @LiF       - 2024-06-01 15:37:58 */             
/* @LiF       - 2024-06-01 15:37:58 */             var tileInstance = UnityEngine.ScriptableObject.CreateInstance<Tile>();
/* @LiF       - 2024-06-01 15:37:58 */             tileInstance.gameObject = tile;
/* @LiF       - 2024-06-01 15:37:58 */             
/* @rhtjdwns  - 2024-06-02 06:42:57 */             baseTile.SetTile(vector, tileInstance);
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             if (building is BuildingType.Ground)
/* @rhtjdwns  - 2024-06-02 06:42:57 */             {
/* @rhtjdwns  - 2024-06-02 06:42:57 */                 BuildNavMesh();
/* @rhtjdwns  - 2024-06-02 06:42:57 */             }
/* @rhtjdwns  - 2024-06-02 06:42:57 */         }
/* @Lee SJ    - 2024-06-02 20:34:51 */         
/* @Lee SJ    - 2024-06-02 20:34:51 */         public void DeleteTile(Vector3 position)
/* @Lee SJ    - 2024-06-02 20:34:51 */         {
/* @Lee SJ    - 2024-06-02 20:34:51 */             Vector3Int vector = baseTile.WorldToCell(position);
/* @Lee SJ    - 2024-06-02 20:34:51 */             buildingTileMap.SetTile(vector, null);
/* @Lee SJ    - 2024-06-02 20:34:51 */         }
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */         private void BuildNavMesh()
/* @rhtjdwns  - 2024-06-02 06:42:57 */         {
/* @rhtjdwns  - 2024-06-02 06:42:57 */             NavMeshTag.PlayerCollect(ref playerSources);
/* @rhtjdwns  - 2024-06-02 06:42:57 */             NavMeshTag.EnemyCollect(ref enemySources);
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             Bounds bounds = new Bounds(new Vector3(0, 0, 0), new Vector3(500, 500, 500));
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             var playerBuildSettings = playerSurfaces.GetBuildSettings();
/* @rhtjdwns  - 2024-06-02 06:42:57 */             UnityEngine.AI.NavMeshBuilder.UpdateNavMeshDataAsync(playerSurfaces.navMeshData, playerBuildSettings, playerSources, bounds);
/* @rhtjdwns  - 2024-06-02 06:42:57 */ 
/* @rhtjdwns  - 2024-06-02 06:42:57 */             var enemyBuildSettings = enemySurfaces.GetBuildSettings();
/* @rhtjdwns  - 2024-06-02 06:42:57 */             UnityEngine.AI.NavMeshBuilder.UpdateNavMeshDataAsync(enemySurfaces.navMeshData, enemyBuildSettings, enemySources, bounds);
/* @rhtjdwns  - 2024-05-13 16:30:04 */         }
/* @rhtjdwns  - 2024-05-13 16:30:04 */     }
/* @rhtjdwns  - 2024-05-13 16:30:04 */ }