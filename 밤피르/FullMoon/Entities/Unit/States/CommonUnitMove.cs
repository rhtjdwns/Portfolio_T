/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-05-21 00:43:20 */ using System.Linq;
/* @Lee SJ    - 2024-05-21 15:27:49 */ using System.Threading;
/* @Lee SJ    - 2024-05-21 15:27:49 */ using Cysharp.Threading.Tasks;
/* @Lee SJ    - 2024-05-21 00:43:20 */ using UnityEngine;
/* @Lee SJ    - 2024-05-21 00:43:20 */ using Unity.Burst;
/* @Lee SJ    - 2024-05-21 15:27:49 */ using FullMoon.FSM;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ using FullMoon.Util;
/* @Lee SJ    - 2024-05-21 00:43:20 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ    - 2024-05-21 00:43:20 */ {
/* @Lee SJ    - 2024-05-21 00:43:20 */     [BurstCompile]
/* @Lee SJ    - 2024-05-21 00:43:20 */     public class CommonUnitMove : IState
/* @Lee SJ    - 2024-05-21 00:43:20 */     {
/* @Lee SJ    - 2024-05-21 00:43:20 */         private readonly CommonUnitController controller;
/* @Lee SJ    - 2024-05-21 15:27:49 */         private CancellationTokenSource cts;
/* @Lee SJ    - 2024-05-21 00:43:20 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */         public CommonUnitMove(CommonUnitController controller)
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @Lee SJ    - 2024-05-21 00:43:20 */             this.controller = controller;
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @Lee SJ    - 2024-05-21 00:43:20 */         
/* @Lee SJ    - 2024-05-21 00:43:20 */         public void Enter()
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = true;
/* @Lee SJ    - 2024-05-21 00:43:20 */             controller.Agent.speed = controller.OverridenUnitData.MovementSpeed;
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @Lee SJ    - 2024-05-21 15:27:49 */             cts = new CancellationTokenSource();
/* @Lee SJ    - 2024-05-21 15:27:49 */             Shock(cts.Token).Forget();
/* @Lee SJ    - 2024-05-21 15:27:49 */         }
/* @Lee SJ    - 2024-05-21 15:27:49 */         
/* @Lee SJ    - 2024-05-21 15:27:49 */         private async UniTask Shock(CancellationToken token)
/* @Lee SJ    - 2024-05-21 15:27:49 */         {
/* @Lee SJ    - 2024-05-22 02:05:43 */             if (controller.AnimationController.SetAnimation("Shock", 0f))
/* @Lee SJ    - 2024-05-21 15:27:49 */             {
/* @Lee SJ    - 2024-05-22 02:05:43 */                 int shockHash = Animator.StringToHash("Shock");
/* @Lee SJ    - 2024-05-21 15:27:49 */                 try
/* @Lee SJ    - 2024-05-21 15:27:49 */                 {
/* @Lee SJ    - 2024-05-21 15:27:49 */                     await UniTask.WaitUntil(() => 
/* @Lee SJ    - 2024-05-21 15:27:49 */                     {
/* @Lee SJ    - 2024-05-21 15:27:49 */                         var stateInfo = controller.unitAnimator.GetCurrentAnimatorStateInfo(0);
/* @Lee SJ    - 2024-05-22 02:05:43 */                         return stateInfo.shortNameHash == shockHash && stateInfo.normalizedTime >= 1f;
/* @Lee SJ    - 2024-05-21 15:27:49 */                     }, cancellationToken: token);
/* @Lee SJ    - 2024-05-21 15:27:49 */                 }
/* @Lee SJ    - 2024-05-21 15:27:49 */                 catch
/* @Lee SJ    - 2024-05-21 15:27:49 */                 {
/* @Lee SJ    - 2024-05-21 15:27:49 */                     controller.StateMachine.ChangeState(new CommonUnitIdle(controller));
/* @Lee SJ    - 2024-05-21 15:27:49 */                     return;
/* @Lee SJ    - 2024-05-21 15:27:49 */                 }
/* @Lee SJ    - 2024-05-21 15:27:49 */             }
/* @Lee SJ    - 2024-05-21 01:40:06 */             controller.moveDustEffect.SetActive(true);
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = false;
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @Lee SJ    - 2024-05-21 15:27:49 */         
/* @Lee SJ    - 2024-05-21 00:43:20 */         [BurstCompile]
/* @Lee SJ    - 2024-05-21 00:43:20 */         public void Execute()
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @LiF       - 2024-05-30 14:02:30 */             if (!controller.Agent.enabled || controller.IsStopped)
/* @Lee SJ    - 2024-05-21 15:27:49 */             {
/* @Lee SJ    - 2024-05-21 15:27:49 */                 return;
/* @Lee SJ    - 2024-05-21 15:27:49 */             }
/* @Lee SJ    - 2024-05-21 15:27:49 */             
/* @Lee SJ    - 2024-05-21 00:43:20 */             if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
/* @Lee SJ    - 2024-05-21 00:43:20 */             {
/* @LiF       - 2024-06-01 15:37:58 */                 if (controller.IsCraft)
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 {
/* @rhtjdwns  - 2024-05-31 16:04:17 */                     ObjectPoolManager.Instance.ReturnObjectToPool(controller.gameObject);
/* @rhtjdwns  - 2024-06-01 00:35:06 */                     ObjectPoolManager.Instance.SpawnObject(controller.hammerPrefab, controller.transform.position, controller.transform.rotation);
/* @LiF       - 2024-06-01 15:37:58 */                     controller.IsCraft = false;
/* @rhtjdwns  - 2024-05-31 16:04:17 */                     return;
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 }
/* @Lee SJ    - 2024-05-31 20:49:59 */ 
/* @Lee SJ    - 2024-05-31 20:49:59 */                 controller.StateMachine.ChangeState(new CommonUnitIdle(controller));
/* @Lee SJ    - 2024-05-31 20:49:59 */                 return;
/* @Lee SJ    - 2024-05-21 00:43:20 */             }
/* @Lee SJ    - 2024-05-21 00:43:20 */             
/* @Lee SJ    - 2024-05-21 00:43:20 */             var unitsInView = controller.UnitInsideViewArea;
/* @LiF       - 2024-05-30 14:02:30 */             var ownTypeUnits = unitsInView.Where(t => controller.UnitType.Equals(t.UnitType) && t.IsStopped);
/* @Lee SJ    - 2024-05-21 00:43:20 */             var destination = controller.LatestDestination;
/* @Lee SJ    - 2024-05-21 15:27:49 */             
/* @Lee SJ    - 2024-05-21 00:43:20 */             BaseUnitController closestUnit = ownTypeUnits.FirstOrDefault(t =>
/* @Lee SJ    - 2024-05-21 00:43:20 */                 Mathf.Approximately(destination.x, t.LatestDestination.x) &&
/* @Lee SJ    - 2024-05-21 00:43:20 */                 Mathf.Approximately(destination.y, t.LatestDestination.y) &&
/* @Lee SJ    - 2024-05-21 00:43:20 */                 Mathf.Approximately(destination.z, t.LatestDestination.z) &&
/* @Lee SJ    - 2024-05-21 00:43:20 */                 Vector3.Distance(controller.transform.position, destination) <= controller.viewRange.radius &&
/* @Lee SJ    - 2024-05-21 01:40:06 */                 Vector3.Distance(controller.transform.position, t.transform.position) <= 3f);
/* @rhtjdwns  - 2024-05-31 16:04:17 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */             if (closestUnit != null)
/* @Lee SJ    - 2024-05-21 00:43:20 */             {
/* @LiF       - 2024-06-01 15:37:58 */                 if (controller.IsCraft)
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 {
/* @rhtjdwns  - 2024-05-31 16:04:17 */                     ObjectPoolManager.Instance.ReturnObjectToPool(controller.gameObject);
/* @rhtjdwns  - 2024-06-01 00:35:06 */                     ObjectPoolManager.Instance.SpawnObject(controller.hammerPrefab, controller.transform.position, controller.transform.rotation);
/* @LiF       - 2024-06-01 15:37:58 */                     controller.IsCraft = false;
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 }
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 else
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 {
/* @rhtjdwns  - 2024-05-31 16:04:17 */                     controller.StateMachine.ChangeState(new CommonUnitIdle(controller));
/* @rhtjdwns  - 2024-05-31 16:04:17 */                 }
/* @Lee SJ    - 2024-05-21 00:43:20 */             }
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @Lee SJ    - 2024-05-21 00:43:20 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */         public void FixedExecute() { }
/* @Lee SJ    - 2024-05-21 00:43:20 */ 
/* @Lee SJ    - 2024-05-21 00:43:20 */         public void Exit()
/* @Lee SJ    - 2024-05-21 00:43:20 */         {
/* @Lee SJ    - 2024-05-21 15:27:49 */             cts?.Cancel();
/* @Lee SJ    - 2024-05-21 01:40:06 */             controller.moveDustEffect.SetActive(false);
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = true;
/* @Lee SJ    - 2024-05-21 00:43:20 */         }
/* @Lee SJ    - 2024-05-21 00:43:20 */     }
/* @Lee SJ    - 2024-05-21 15:27:49 */ }