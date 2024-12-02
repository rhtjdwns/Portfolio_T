/* Git Blame Auto Generated */

/* @LiF     - 2024-05-16 02:58:26 */ using System;
/* @Lee SJ  - 2024-05-13 21:39:50 */ using MyBox;
/* @Lee SJ  - 2024-05-13 21:39:50 */ using UnityEngine;
/* @Lee SJ  - 2024-05-14 00:57:12 */ using System.Linq;
/* @Lee SJ  - 2024-05-13 21:39:50 */ using System.Collections.Generic;
/* @LiF     - 2024-06-03 02:22:18 */ using Cysharp.Threading.Tasks;
/* @Lee SJ  - 2024-05-30 21:35:08 */ using DG.Tweening;
/* @Lee SJ  - 2024-06-02 20:34:51 */ using FullMoon.UI;
/* @Lee SJ  - 2024-05-14 00:57:12 */ using FullMoon.Util;
/* @Lee SJ  - 2024-05-30 21:53:26 */ using UnityEngine.Rendering.Universal;
/* @Lee SJ  - 2024-05-13 21:39:50 */ 
/* @Lee SJ  - 2024-05-13 21:39:50 */ namespace FullMoon.Entities.Unit
/* @Lee SJ  - 2024-05-13 21:39:50 */ {
/* @Lee SJ  - 2024-05-13 21:39:50 */     public class UnitFlagController : MonoBehaviour
/* @Lee SJ  - 2024-05-13 21:39:50 */     {
/* @Lee SJ  - 2024-05-30 21:53:26 */         [SerializeField] private float viewRangeRadius;
/* @Lee SJ  - 2024-05-14 00:57:12 */         [SerializeField] private SphereCollider viewRange;
/* @Lee SJ  - 2024-05-30 21:35:08 */         [SerializeField] private GameObjectDictionary flagModel;
/* @Lee SJ  - 2024-05-14 00:57:12 */         [SerializeField] private List<BaseUnitController> unitPreset;
/* @LiF     - 2024-06-03 21:40:38 */         private List<Vector3> localPositionsPreset = new();
/* @Lee SJ  - 2024-05-14 00:57:12 */         
/* @Lee SJ  - 2024-05-21 15:27:49 */         public HashSet<BaseUnitController> UnitInsideViewArea { get; private set; }
/* @Lee SJ  - 2024-06-02 20:34:51 */         public Vector3 BuildingPosition { get; set; }
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */         private GameObjectDictionary currentFlagModel;
/* @Lee SJ  - 2024-05-30 21:35:08 */         private Tween flagMoveTween;
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-14 00:57:12 */         private void OnEnable()
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @Lee SJ  - 2024-05-14 00:57:12 */             UnitInsideViewArea = new HashSet<BaseUnitController>();
/* @Lee SJ  - 2024-05-14 00:57:12 */             InitViewRange();
/* @LiF     - 2024-06-03 19:45:11 */             if (localPositionsPreset.Count == unitPreset.Count)
/* @LiF     - 2024-06-03 19:45:11 */             {
/* @LiF     - 2024-06-03 21:40:38 */                 Debug.Log($"{localPositionsPreset.Count} {unitPreset.Count}");
/* @LiF     - 2024-06-03 19:45:11 */                 InitUnitPositions();
/* @LiF     - 2024-06-03 19:45:11 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */             foreach (var unit in unitPreset)
/* @Lee SJ  - 2024-05-14 00:57:12 */             {
/* @Lee SJ  - 2024-05-14 00:57:12 */                 unit.Flag = this;
/* @LiF     - 2024-06-03 02:22:18 */                 unit.gameObject.SetActive(true);
/* @Lee SJ  - 2024-05-14 00:57:12 */             }
/* @Lee SJ  - 2024-05-30 21:35:08 */             ChangeFlagModelPosition();
/* @Lee SJ  - 2024-05-30 21:53:26 */             InitViewRangeRadius();
/* @Lee SJ  - 2024-05-30 21:35:08 */             Deselect();
/* @Lee SJ  - 2024-05-30 21:35:08 */         }
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @LiF     - 2024-06-03 14:28:23 */         private void Start()
/* @LiF     - 2024-06-03 14:28:23 */         {
/* @LiF     - 2024-06-03 14:28:23 */             SaveLocalPositions();
/* @LiF     - 2024-06-03 14:28:23 */         }
/* @LiF     - 2024-06-03 14:28:23 */ 
/* @LiF     - 2024-06-03 02:22:18 */         private async void Update()
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @LiF     - 2024-05-30 14:02:30 */             UnitInsideViewArea.RemoveWhere(unit => unit == null || !unit.gameObject.activeInHierarchy || (!unit.Alive && unit is not MainUnitController));
/* @LiF     - 2024-06-01 19:36:00 */             
/* @LiF     - 2024-06-02 00:02:59 */             if (unitPreset.All(unit => unit.Alive is false && unit.gameObject.activeInHierarchy is false && unit is not MainUnitController))
/* @Lee SJ  - 2024-05-31 20:49:59 */             {
/* @LiF     - 2024-06-03 02:22:18 */                 await UniTask.Delay(500);
/* @Lee SJ  - 2024-05-31 20:49:59 */                 if (currentFlagModel != null)
/* @Lee SJ  - 2024-05-31 20:49:59 */                 {
/* @Lee SJ  - 2024-05-31 20:49:59 */                     ObjectPoolManager.Instance.ReturnObjectToPool(currentFlagModel.gameObject);
/* @LiF     - 2024-06-03 02:22:18 */                     currentFlagModel = null;
/* @Lee SJ  - 2024-05-31 20:49:59 */                 }
/* @Lee SJ  - 2024-06-02 20:34:51 */                 if (BuildingPosition != Vector3.zero)
/* @Lee SJ  - 2024-06-02 20:34:51 */                 {
/* @Lee SJ  - 2024-06-02 20:34:51 */                     TileController.Instance.DeleteTile(BuildingPosition);
/* @Lee SJ  - 2024-06-02 20:34:51 */                     BuildingPosition = Vector3.zero;
/* @Lee SJ  - 2024-06-02 20:34:51 */                 }
/* @Lee SJ  - 2024-05-31 20:49:59 */                 ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
/* @Lee SJ  - 2024-05-31 20:49:59 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */         }
/* @Lee SJ  - 2024-05-14 00:57:12 */ 
/* @Lee SJ  - 2024-05-14 00:57:12 */         [ButtonMethod]
/* @Lee SJ  - 2024-05-14 00:57:12 */         private void AutoFindUnit()
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @Lee SJ  - 2024-05-14 00:57:12 */             unitPreset = GetComponentsInChildren<BaseUnitController>().ToList();
/* @Lee SJ  - 2024-05-14 00:57:12 */             SaveLocalPositions();
/* @Lee SJ  - 2024-05-14 00:57:12 */         }
/* @LiF     - 2024-05-16 02:58:26 */         
/* @Lee SJ  - 2024-05-14 00:57:12 */         [ButtonMethod]
/* @LiF     - 2024-05-16 02:58:26 */         public void InitUnitPositions()
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @Lee SJ  - 2024-05-14 00:57:12 */             for (int i = 0; i < unitPreset.Count; i++)
/* @Lee SJ  - 2024-05-14 00:57:12 */             {
/* @Lee SJ  - 2024-05-14 00:57:12 */                 Vector3 worldPosition = transform.TransformPoint(localPositionsPreset[i]);
/* @Lee SJ  - 2024-05-14 00:57:12 */                 unitPreset[i].transform.position = worldPosition;
/* @Lee SJ  - 2024-05-14 00:57:12 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */         }
/* @LiF     - 2024-05-16 02:58:26 */         
/* @Lee SJ  - 2024-05-30 21:35:08 */         public void Select()
/* @Lee SJ  - 2024-05-30 21:35:08 */         {
/* @Lee SJ  - 2024-05-30 21:35:08 */             currentFlagModel.GetGameObjectByName("Decal")?.SetActive(true);
/* @Lee SJ  - 2024-05-30 21:35:08 */         }
/* @Lee SJ  - 2024-05-30 21:35:08 */         
/* @Lee SJ  - 2024-05-30 21:35:08 */         public void Deselect()
/* @Lee SJ  - 2024-05-30 21:35:08 */         {
/* @Lee SJ  - 2024-05-30 21:35:08 */             currentFlagModel.GetGameObjectByName("Decal")?.SetActive(false);
/* @Lee SJ  - 2024-05-30 21:35:08 */         }
/* @Lee SJ  - 2024-05-30 21:35:08 */         
/* @LiF     - 2024-05-16 02:58:26 */         public Vector3 GetPresetPosition(BaseUnitController targetObject)
/* @LiF     - 2024-05-16 02:58:26 */         {
/* @LiF     - 2024-05-16 02:58:26 */             int index = unitPreset.IndexOf(targetObject);
/* @LiF     - 2024-05-16 02:58:26 */             if (index == -1)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 throw new ArgumentException("The object does not exist in the preset.");
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @LiF     - 2024-05-16 02:58:26 */             Vector3 localPosition = localPositionsPreset[index];
/* @LiF     - 2024-05-16 02:58:26 */             Vector3 worldPosition = transform.TransformPoint(localPosition);
/* @LiF     - 2024-05-16 02:58:26 */             return worldPosition;
/* @LiF     - 2024-05-16 02:58:26 */         }
/* @LiF     - 2024-05-16 02:58:26 */         
/* @LiF     - 2024-05-16 02:58:26 */         public void MoveToPosition(Vector3 newPosition)
/* @LiF     - 2024-05-16 02:58:26 */         {
/* @LiF     - 2024-05-16 02:58:26 */             ChangeParentPosition(newPosition);
/* @LiF     - 2024-05-16 02:58:26 */             for (int i = 0; i < unitPreset.Count; i++)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 if (unitPreset[i] is null || unitPreset[i].gameObject.activeInHierarchy == false || unitPreset[i].Alive == false)
/* @LiF     - 2024-05-16 02:58:26 */                 {
/* @LiF     - 2024-05-16 02:58:26 */                     continue;
/* @LiF     - 2024-05-16 02:58:26 */                 }
/* @LiF     - 2024-05-16 02:58:26 */                 Vector3 worldPosition = transform.TransformPoint(localPositionsPreset[i]);
/* @LiF     - 2024-05-16 02:58:26 */                 unitPreset[i].MoveToPosition(worldPosition);
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @Lee SJ  - 2024-05-30 21:35:08 */             ChangeFlagModelPosition();
/* @LiF     - 2024-05-16 02:58:26 */         }
/* @LiF     - 2024-05-16 02:58:26 */         
/* @LiF     - 2024-05-16 02:58:26 */         private void SaveLocalPositions()
/* @LiF     - 2024-05-16 02:58:26 */         {
/* @LiF     - 2024-05-16 02:58:26 */             localPositionsPreset = unitPreset.Select(u => u.transform.localPosition).ToList();
/* @LiF     - 2024-05-16 02:58:26 */         }
/* @LiF     - 2024-05-16 02:58:26 */         
/* @LiF     - 2024-05-16 02:58:26 */         private void ChangeParentPosition(Vector3 newPosition)
/* @LiF     - 2024-05-16 02:58:26 */         {
/* @LiF     - 2024-05-16 02:58:26 */             List<Vector3> currentWorldPositions = new List<Vector3>();
/* @LiF     - 2024-05-16 02:58:26 */             foreach (var t in unitPreset)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 currentWorldPositions.Add(t.transform.position);
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */ 
/* @LiF     - 2024-05-16 02:58:26 */             transform.position = newPosition;
/* @LiF     - 2024-05-16 02:58:26 */ 
/* @LiF     - 2024-05-16 02:58:26 */             for (int i = 0; i < unitPreset.Count; i++)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 unitPreset[i].transform.position = currentWorldPositions[i];
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @LiF     - 2024-05-16 02:58:26 */         }
/* @LiF     - 2024-05-16 02:58:26 */         
/* @Lee SJ  - 2024-05-14 00:57:12 */         private void InitViewRange()
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @Lee SJ  - 2024-05-14 00:57:12 */             var triggerEvent = viewRange.GetComponent<ColliderTriggerEvents>();
/* @Lee SJ  - 2024-05-14 00:57:12 */             if (triggerEvent is not null)
/* @Lee SJ  - 2024-05-14 00:57:12 */             {
/* @Lee SJ  - 2024-05-14 00:57:12 */                 float worldRadius = viewRange.radius *
/* @Lee SJ  - 2024-05-14 00:57:12 */                                     Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
/* @Lee SJ  - 2024-05-14 00:57:12 */ 
/* @Lee SJ  - 2024-05-14 00:57:12 */                 var units = Physics.OverlapSphere(transform.position + viewRange.center, worldRadius)
/* @Lee SJ  - 2024-05-14 00:57:12 */                     .Where(t => triggerEvent.GetFilterTags.Contains(t.tag) && t.gameObject != gameObject)
/* @Lee SJ  - 2024-05-14 00:57:12 */                     .Where(t => t.GetComponent<BaseUnitController>() is not null)
/* @Lee SJ  - 2024-05-14 00:57:12 */                     .ToList();
/* @Lee SJ  - 2024-05-14 00:57:12 */ 
/* @Lee SJ  - 2024-05-14 00:57:12 */                 foreach (var unit in units)
/* @Lee SJ  - 2024-05-14 00:57:12 */                 {
/* @Lee SJ  - 2024-05-14 00:57:12 */                     UnitInsideViewArea.Add(unit.GetComponent<BaseUnitController>());
/* @Lee SJ  - 2024-05-14 00:57:12 */                 }
/* @Lee SJ  - 2024-05-14 00:57:12 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */         }
/* @Lee SJ  - 2024-05-14 00:57:12 */         
/* @Lee SJ  - 2024-05-14 00:57:12 */         public void EnterViewRange(Collider unit)
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @Lee SJ  - 2024-05-14 00:57:12 */             BaseUnitController controller = unit.GetComponent<BaseUnitController>();
/* @Lee SJ  - 2024-05-14 00:57:12 */             if (controller is null)
/* @Lee SJ  - 2024-05-14 00:57:12 */             {
/* @Lee SJ  - 2024-05-14 00:57:12 */                 return;
/* @Lee SJ  - 2024-05-14 00:57:12 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */             UnitInsideViewArea.Add(controller);
/* @Lee SJ  - 2024-05-14 00:57:12 */         }
/* @Lee SJ  - 2024-05-14 00:57:12 */ 
/* @Lee SJ  - 2024-05-14 00:57:12 */         public void ExitViewRange(Collider unit)
/* @Lee SJ  - 2024-05-14 00:57:12 */         {
/* @Lee SJ  - 2024-05-14 00:57:12 */             BaseUnitController controller = unit.GetComponent<BaseUnitController>();
/* @Lee SJ  - 2024-05-14 00:57:12 */             if (controller is null)
/* @Lee SJ  - 2024-05-14 00:57:12 */             {
/* @Lee SJ  - 2024-05-14 00:57:12 */                 return;
/* @Lee SJ  - 2024-05-14 00:57:12 */             }
/* @Lee SJ  - 2024-05-14 00:57:12 */             UnitInsideViewArea.Remove(controller);
/* @Lee SJ  - 2024-05-14 00:57:12 */         }
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */         private void ChangeFlagModelPosition()
/* @Lee SJ  - 2024-05-30 21:35:08 */         {
/* @Lee SJ  - 2024-05-30 21:35:08 */             if (currentFlagModel == null)
/* @Lee SJ  - 2024-05-30 21:35:08 */             {
/* @Lee SJ  - 2024-05-30 21:35:08 */                 currentFlagModel = ObjectPoolManager.Instance.SpawnObject(flagModel.gameObject, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity).GetComponent<GameObjectDictionary>();
/* @Lee SJ  - 2024-05-30 21:53:26 */                 var decalProjector = currentFlagModel.GetGameObjectByName("Decal")?.GetComponent<DecalProjector>();
/* @Lee SJ  - 2024-05-30 21:53:26 */                 if (decalProjector != null)
/* @Lee SJ  - 2024-05-30 21:53:26 */                 {
/* @Lee SJ  - 2024-05-30 21:53:26 */                     decalProjector.size = new Vector3(viewRangeRadius * 2f, viewRangeRadius * 2f, decalProjector.size.z);
/* @Lee SJ  - 2024-05-30 21:53:26 */                 }
/* @Lee SJ  - 2024-05-30 21:35:08 */             }
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */             flagMoveTween?.Kill();
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */             currentFlagModel.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
/* @Lee SJ  - 2024-05-30 21:35:08 */             flagMoveTween = currentFlagModel.transform.DOMove(transform.position, .3f).SetEase(Ease.OutBounce);
/* @Lee SJ  - 2024-05-30 21:35:08 */         }
/* @Lee SJ  - 2024-05-30 21:53:26 */         
/* @Lee SJ  - 2024-05-30 21:53:26 */         private void InitViewRangeRadius()
/* @Lee SJ  - 2024-05-30 21:53:26 */         {
/* @Lee SJ  - 2024-05-30 21:53:26 */             if (viewRange != null)
/* @Lee SJ  - 2024-05-30 21:53:26 */             {
/* @Lee SJ  - 2024-05-30 21:53:26 */                 viewRange.radius = viewRangeRadius;
/* @Lee SJ  - 2024-05-30 21:53:26 */             }
/* @Lee SJ  - 2024-05-30 21:53:26 */         }
/* @Lee SJ  - 2024-05-30 21:53:26 */         
/* @Lee SJ  - 2024-05-30 21:53:26 */ #if UNITY_EDITOR
/* @Lee SJ  - 2024-05-31 20:49:59 */         protected void OnDrawGizmos()
/* @Lee SJ  - 2024-05-30 21:53:26 */         {
/* @Lee SJ  - 2024-05-30 21:53:26 */             InitViewRangeRadius();
/* @Lee SJ  - 2024-05-30 21:53:26 */         }
/* @Lee SJ  - 2024-05-30 21:53:26 */ #endif
/* @Lee SJ  - 2024-05-13 21:39:50 */     }
/* @Lee SJ  - 2024-05-30 21:35:08 */ }