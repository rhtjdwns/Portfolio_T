/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-05-21 16:02:38 */ using System.Linq;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using System.Threading;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using Cysharp.Threading.Tasks;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using UnityEngine;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using Unity.Burst;
/* @Lee SJ    - 2024-05-21 16:02:38 */ using FullMoon.FSM;
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ    - 2024-05-21 16:02:38 */ {
/* @Lee SJ    - 2024-05-21 16:02:38 */     [BurstCompile]
/* @Lee SJ    - 2024-05-21 16:02:38 */     public class HammerUnitMove : IState
/* @Lee SJ    - 2024-05-21 16:02:38 */     {
/* @Lee SJ    - 2024-05-21 16:02:38 */         private readonly HammerUnitController controller;
/* @Lee SJ    - 2024-05-21 16:02:38 */         private CancellationTokenSource cts;
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public HammerUnitMove(HammerUnitController controller)
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             this.controller = controller;
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */         
/* @Lee SJ    - 2024-05-21 16:02:38 */         public void Enter()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = false;
/* @Lee SJ    - 2024-05-21 16:02:38 */             controller.Agent.speed = controller.OverridenUnitData.MovementSpeed;
/* @Lee SJ    - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Move", 0.1f);
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */         
/* @Lee SJ    - 2024-05-21 16:02:38 */         [BurstCompile]
/* @Lee SJ    - 2024-05-21 16:02:38 */         public void Execute()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @Lee SJ    - 2024-05-21 16:02:38 */             if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
/* @Lee SJ    - 2024-05-21 16:02:38 */             {
/* @rhtjdwns  - 2024-05-22 11:51:10 */                 controller.StateMachine.ChangeState(new HammerUnitCraft(controller));
/* @Lee SJ    - 2024-05-21 16:02:38 */                 return;
/* @Lee SJ    - 2024-05-21 16:02:38 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */             
/* @Lee SJ    - 2024-05-21 16:02:38 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @LiF       - 2024-05-30 14:02:30 */             var ownTypeUnits = unitsInView.Where(t => controller.UnitType.Equals(t.UnitType) && t.IsStopped);
/* @Lee SJ    - 2024-05-21 16:02:38 */             var destination = controller.LatestDestination;
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */             BaseUnitController closestUnit = ownTypeUnits.FirstOrDefault(t =>
/* @Lee SJ    - 2024-05-21 16:02:38 */                 Mathf.Approximately(destination.x, t.LatestDestination.x) &&
/* @Lee SJ    - 2024-05-21 16:02:38 */                 Mathf.Approximately(destination.y, t.LatestDestination.y) &&
/* @Lee SJ    - 2024-05-21 16:02:38 */                 Mathf.Approximately(destination.z, t.LatestDestination.z) &&
/* @Lee SJ    - 2024-05-31 20:49:59 */                 Vector3.Distance(controller.transform.position, destination) <= 1f &&
/* @Lee SJ    - 2024-05-21 16:02:38 */                 Vector3.Distance(controller.transform.position, t.transform.position) <= 2f);
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */             if (closestUnit != null)
/* @Lee SJ    - 2024-05-21 16:02:38 */             {
/* @rhtjdwns  - 2024-05-31 19:03:08 */                 controller.StateMachine.ChangeState(new HammerUnitCraft(controller));
/* @Lee SJ    - 2024-05-21 16:02:38 */             }
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public void FixedExecute() { }
/* @Lee SJ    - 2024-05-21 16:02:38 */ 
/* @Lee SJ    - 2024-05-21 16:02:38 */         public void Exit()
/* @Lee SJ    - 2024-05-21 16:02:38 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = true; 
/* @Lee SJ    - 2024-05-21 16:02:38 */         }
/* @Lee SJ    - 2024-05-21 16:02:38 */     }
/* @Lee SJ    - 2024-05-21 16:02:38 */ }