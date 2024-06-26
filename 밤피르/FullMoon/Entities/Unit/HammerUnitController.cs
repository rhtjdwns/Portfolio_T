/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-05-21 16:02:38 */ using MyBox;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using System.Linq;
/* @LiF       - 2024-06-01 15:37:58 */ using Cysharp.Threading.Tasks;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using FullMoon.Entities.Unit.States;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using UnityEngine;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using UnityEngine.AI;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using FullMoon.ScriptableObject;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using FullMoon.Util;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using Unity.Burst;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using UnityEngine.Rendering.Universal;
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */ namespace FullMoon.Entities.Unit
/* @Lee SJ    - 2024-05-21 16:02:38 */ {
/* @Lee SJ    - 2024-05-21 16:02:38 */     [RequireComponent(typeof(NavMeshAgent)), BurstCompile]
/* @Lee SJ    - 2024-05-21 16:02:38 */     public class HammerUnitController : BaseUnitController
/* @Lee SJ    - 2024-05-21 16:02:38 */     {
/* @Lee SJ    - 2024-05-21 16:02:38 */         [Foldout("Hammer Unit Settings")]
/* @Lee SJ    - 2024-05-21 16:02:38 */         public DecalProjector decalProjector;
/* @Lee SJ    - 2024-05-21 16:02:38 */         
/* @Lee SJ    - 2024-05-21 16:02:38 */         public HammerUnitData OverridenUnitData { get; private set; }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         protected override void OnEnable()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             base.OnEnable();
/* @Lee SJ    - 2024-05-21 16:02:38 */             OverridenUnitData = unitData as HammerUnitData;
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */             InitializeViewRange();
/* @Lee SJ    - 2024-05-21 16:02:38 */             
/* @Lee SJ    - 2024-05-21 16:02:38 */             if (decalProjector != null)
/* @Lee SJ    - 2024-05-21 16:02:38 */             {
/* @Lee SJ    - 2024-05-21 16:02:38 */                 InitializeDecalProjector();
/* @Lee SJ    - 2024-05-21 16:02:38 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */             
/* @rhtjdwns  - 2024-05-31 16:04:17 */             StateMachine.ChangeState(new HammerUnitCraft(this));
/* @rhtjdwns  - 2024-06-01 00:35:06 */ 
/* @LiF       - 2024-06-01 16:42:01 */             ObjectPoolManager.Instance.ReturnObjectToPool(gameObject, 5f).Forget();
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public override void Die()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             base.Die();
/* @Lee SJ    - 2024-05-21 16:02:38 */             StateMachine.ChangeState(new HammerUnitDead(this));
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public void EnterViewRange(Collider unit)
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @Lee SJ    - 2024-05-21 16:02:38 */             {
/* @Lee SJ    - 2024-05-21 16:02:38 */                 UnitInsideViewArea.Add(controller);
/* @Lee SJ    - 2024-05-21 16:02:38 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public void ExitViewRange(Collider unit)
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @Lee SJ    - 2024-05-21 16:02:38 */             {
/* @Lee SJ    - 2024-05-21 16:02:38 */                 UnitInsideViewArea.Remove(controller);
/* @Lee SJ    - 2024-05-21 16:02:38 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */         
/* @Lee SJ    - 2024-05-21 16:02:38 */         public override void Select()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             base.Select();
/* @Lee SJ    - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ    - 2024-05-30 21:35:08 */             {
/* @Lee SJ    - 2024-05-30 21:35:08 */                 Flag.Select();
/* @Lee SJ    - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ    - 2024-05-30 21:35:08 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */             decalProjector?.gameObject.SetActive(true);
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public override void Deselect()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             base.Deselect();
/* @Lee SJ    - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ    - 2024-05-30 21:35:08 */             {
/* @Lee SJ    - 2024-05-30 21:35:08 */                 Flag.Deselect();
/* @Lee SJ    - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ    - 2024-05-30 21:35:08 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */             decalProjector?.gameObject.SetActive(false);
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public override void MoveToPosition(Vector3 location)
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             base.MoveToPosition(location);
/* @Lee SJ    - 2024-05-21 16:02:38 */             StateMachine.ChangeState(new HammerUnitMove(this));
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         private void InitializeDecalProjector()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             decalProjector.gameObject.SetActive(false);
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */         
/* @Lee SJ    - 2024-05-21 16:02:38 */         private void InitializeViewRange()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             if (viewRange != null && unitData != null)
/* @Lee SJ    - 2024-05-21 16:02:38 */             {
/* @Lee SJ    - 2024-05-21 16:02:38 */                 float worldRadius = viewRange.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
/* @Lee SJ    - 2024-05-21 16:02:38 */                 var triggerEvent = viewRange.GetComponent<ColliderTriggerEvents>();
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */                 if (triggerEvent != null)
/* @Lee SJ    - 2024-05-21 16:02:38 */                 {
/* @Lee SJ    - 2024-05-21 16:02:38 */                     var units = Physics.OverlapSphere(transform.position + viewRange.center, worldRadius)
/* @Lee SJ    - 2024-05-21 16:02:38 */                         .Where(t => triggerEvent.GetFilterTags.Contains(t.tag) && t.gameObject != gameObject)
/* @Lee SJ    - 2024-05-21 16:02:38 */                         .Select(t => t.GetComponent<BaseUnitController>())
/* @Lee SJ    - 2024-05-21 16:02:38 */                         .Where(unit => unit != null)
/* @Lee SJ    - 2024-05-21 16:02:38 */                         .ToList();
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */                     UnitInsideViewArea.UnionWith(units);
/* @Lee SJ    - 2024-05-21 16:02:38 */                 }
/* @Lee SJ    - 2024-05-21 16:02:38 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */     }
/* @Lee SJ    - 2024-05-21 16:02:38 */ }
