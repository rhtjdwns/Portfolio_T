/* Git Blame Auto Generated */

/* @LiF       - 2024-05-20 22:02:10 */ using MyBox;
/* @LiF       - 2024-05-20 22:02:10 */ using System.Linq;
/* @LiF       - 2024-06-01 15:37:58 */ using Unity.Burst;
/* @LiF       - 2024-05-20 22:02:10 */ using UnityEngine;
/* @LiF       - 2024-05-20 22:02:10 */ using UnityEngine.AI;
/* @Lee SJ    - 2024-05-21 00:43:20 */ using UnityEngine.Rendering.Universal;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ using FullMoon.UI;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.Util;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.ScriptableObject;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.Entities.Unit.States;
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */ namespace FullMoon.Entities.Unit
/* @LiF       - 2024-05-20 22:02:10 */ {
/* @LiF       - 2024-05-20 22:02:10 */     [RequireComponent(typeof(NavMeshAgent)), BurstCompile]
/* @LiF       - 2024-05-20 22:02:10 */     public class CommonUnitController : BaseUnitController
/* @LiF       - 2024-05-20 22:02:10 */     {
/* @Lee SJ    - 2024-05-21 00:43:20 */         [Foldout("Common Unit Settings")]
/* @LiF       - 2024-05-20 22:02:10 */         public DecalProjector decalProjector;
/* @LiF       - 2024-05-20 22:02:10 */         
/* @Lee SJ    - 2024-05-21 00:43:20 */         [Foldout("Common Unit Settings")]
/* @Lee SJ    - 2024-05-21 00:43:20 */         public GameObject moveDustEffect;
/* @Lee SJ    - 2024-05-21 00:43:20 */         
/* @Lee SJ    - 2024-05-21 00:43:20 */         public CommonUnitData OverridenUnitData { get; private set; }
/* @Lee SJ    - 2024-05-21 00:43:20 */         public BaseUnitController MainUnit { get; private set; }
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-06-01 15:37:58 */         public bool IsCraft { get; set; }
/* @LiF       - 2024-06-01 15:37:58 */         
/* @rhtjdwns  - 2024-05-31 16:04:17 */         public GameObject hammerPrefab;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @LiF       - 2024-05-20 22:02:10 */         protected override void OnEnable()
/* @LiF       - 2024-05-20 22:02:10 */         {
/* @LiF       - 2024-05-20 22:02:10 */             base.OnEnable();
/* @Lee SJ    - 2024-05-21 00:43:20 */             OverridenUnitData = unitData as CommonUnitData;
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */             InitializeViewRange();
/* @Lee SJ    - 2024-05-21 00:43:20 */             
/* @LiF       - 2024-05-20 22:02:10 */             if (decalProjector != null)
/* @LiF       - 2024-05-20 22:02:10 */             {
/* @LiF       - 2024-05-20 22:02:10 */                 InitializeDecalProjector();
/* @LiF       - 2024-05-20 22:02:10 */             }
/* @Lee SJ    - 2024-05-21 00:43:20 */             
/* @Lee SJ    - 2024-05-21 00:43:20 */             StateMachine.ChangeState(new CommonUnitIdle(this));
/* @Lee SJ    - 2024-05-21 00:43:20 */             
/* @Lee SJ    - 2024-05-21 00:43:20 */             MainUnit = FindObjectsOfType<BaseUnitController>()
/* @Lee SJ    - 2024-05-21 00:43:20 */                 .FirstOrDefault(unit => unit.unitData.UnitType.Equals("Player") && unit.unitData.UnitClass.Equals("Main"));
/* @LiF       - 2024-06-02 03:38:55 */             
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.ChangeCommonAmount(1);
/* @LiF       - 2024-05-20 22:02:10 */         }
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @rhtjdwns  - 2024-06-01 00:35:06 */         public void CraftBuilding(Vector3 pos)
/* @rhtjdwns  - 2024-05-31 16:04:17 */         {
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.ChangeCommonAmount(-1);
/* @rhtjdwns  - 2024-05-31 19:03:08 */             ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
/* @LiF       - 2024-06-02 03:38:55 */             
/* @rhtjdwns  - 2024-05-31 19:03:08 */             HammerUnitController hammerUnit = ObjectPoolManager.Instance.SpawnObject(hammerPrefab, transform.position, transform.rotation)
/* @LiF       - 2024-06-01 15:37:58 */                 .GetComponent<HammerUnitController>();
/* @LiF       - 2024-06-02 03:38:55 */             
/* @rhtjdwns  - 2024-05-31 19:03:08 */             hammerUnit.MoveToPosition(pos);
/* @rhtjdwns  - 2024-05-31 16:04:17 */         }
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @LiF       - 2024-05-20 22:02:10 */         public override void Die()
/* @LiF       - 2024-05-20 22:02:10 */         {
/* @LiF       - 2024-05-20 22:02:10 */             base.Die();
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.ChangeCommonAmount(-1);
/* @Lee SJ    - 2024-05-21 00:43:20 */             StateMachine.ChangeState(new CommonUnitDead(this));
/* @LiF       - 2024-05-20 22:02:10 */         }
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */         public void EnterViewRange(Collider unit)
/* @LiF       - 2024-05-20 22:02:10 */         {
/* @LiF       - 2024-05-20 22:02:10 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @LiF       - 2024-05-20 22:02:10 */             {
/* @LiF       - 2024-05-20 22:02:10 */                 UnitInsideViewArea.Add(controller);
/* @LiF       - 2024-05-20 22:02:10 */             }
/* @LiF       - 2024-05-20 22:02:10 */         }
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */         public void ExitViewRange(Collider unit)
/* @LiF       - 2024-05-20 22:02:10 */         {
/* @LiF       - 2024-05-20 22:02:10 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @LiF       - 2024-05-20 22:02:10 */             {
/* @LiF       - 2024-05-20 22:02:10 */                 UnitInsideViewArea.Remove(controller);
/* @LiF       - 2024-05-20 22:02:10 */             }
/* @LiF       - 2024-05-20 22:02:10 */         }
/* @Lee SJ    - 2024-05-21 00:43:20 */         
/* @Lee SJ    - 2024-05-21 00:43:20 */         public override void Select()
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @Lee SJ    - 2024-05-21 00:43:20 */             base.Select();
/* @Lee SJ    - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ    - 2024-05-30 21:35:08 */             {
/* @Lee SJ    - 2024-05-30 21:35:08 */                 Flag.Select();
/* @Lee SJ    - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ    - 2024-05-30 21:35:08 */             }
/* @Lee SJ    - 2024-05-21 00:43:20 */             decalProjector?.gameObject.SetActive(true);
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @Lee SJ    - 2024-05-21 00:43:20 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */         public override void Deselect()
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @Lee SJ    - 2024-05-21 00:43:20 */             base.Deselect();
/* @Lee SJ    - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ    - 2024-05-30 21:35:08 */             {
/* @Lee SJ    - 2024-05-30 21:35:08 */                 Flag.Deselect();
/* @Lee SJ    - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ    - 2024-05-30 21:35:08 */             }
/* @Lee SJ    - 2024-05-21 00:43:20 */             decalProjector?.gameObject.SetActive(false);
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */         public override void MoveToPosition(Vector3 location)
/* @LiF       - 2024-05-20 22:02:10 */         {
/* @LiF       - 2024-05-20 22:02:10 */             base.MoveToPosition(location);
/* @Lee SJ    - 2024-05-21 00:43:20 */             StateMachine.ChangeState(new CommonUnitMove(this));
/* @LiF       - 2024-05-20 22:02:10 */         }
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */         private void InitializeDecalProjector()
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @Lee SJ    - 2024-05-21 00:43:20 */             decalProjector.gameObject.SetActive(false);
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @Lee SJ    - 2024-05-21 00:43:20 */         
/* @LiF       - 2024-05-20 22:02:10 */         private void InitializeViewRange()
/* @LiF       - 2024-05-20 22:02:10 */         {
/* @LiF       - 2024-05-20 22:02:10 */             if (viewRange != null && unitData != null)
/* @LiF       - 2024-05-20 22:02:10 */             {
/* @LiF       - 2024-05-20 22:02:10 */                 float worldRadius = viewRange.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
/* @LiF       - 2024-05-20 22:02:10 */                 var triggerEvent = viewRange.GetComponent<ColliderTriggerEvents>();
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */                 if (triggerEvent != null)
/* @LiF       - 2024-05-20 22:02:10 */                 {
/* @LiF       - 2024-05-20 22:02:10 */                     var units = Physics.OverlapSphere(transform.position + viewRange.center, worldRadius)
/* @LiF       - 2024-05-20 22:02:10 */                         .Where(t => triggerEvent.GetFilterTags.Contains(t.tag) && t.gameObject != gameObject)
/* @LiF       - 2024-05-20 22:02:10 */                         .Select(t => t.GetComponent<BaseUnitController>())
/* @LiF       - 2024-05-20 22:02:10 */                         .Where(unit => unit != null)
/* @LiF       - 2024-05-20 22:02:10 */                         .ToList();
/* @LiF       - 2024-05-20 22:02:10 */ 
/* @LiF       - 2024-05-20 22:02:10 */                     UnitInsideViewArea.UnionWith(units);
/* @LiF       - 2024-05-20 22:02:10 */                 }
/* @LiF       - 2024-05-20 22:02:10 */             }
/* @LiF       - 2024-05-20 22:02:10 */         }
/* @LiF       - 2024-05-20 22:02:10 */     }
/* @LiF       - 2024-05-20 22:02:10 */ }
