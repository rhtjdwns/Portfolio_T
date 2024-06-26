/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-04-15 23:12:39 */ using System.Linq;
/* @Lee SJ    - 2024-04-15 23:12:39 */ using UnityEngine;
/* @rhtjdwns  - 2024-05-24 19:17:10 */ using UnityEngine.AI;
/* @Lee SJ    - 2024-04-15 23:12:39 */ using FullMoon.FSM;
/* @Lee SJ    - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @rhtjdwns  - 2024-05-24 19:17:10 */ using System.IO;
/* @Lee SJ    - 2024-04-15 23:12:39 */ 
/* @Lee SJ    - 2024-04-15 23:12:39 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ    - 2024-04-15 23:12:39 */ {
/* @Lee SJ    - 2024-05-03 19:25:37 */     [BurstCompile]
/* @Lee SJ    - 2024-04-15 23:12:39 */     public class MainUnitMove : IState
/* @Lee SJ    - 2024-04-15 23:12:39 */     {
/* @Lee SJ    - 2024-04-15 23:12:39 */         private readonly MainUnitController controller;
/* @Lee SJ    - 2024-04-15 23:12:39 */ 
/* @Lee SJ    - 2024-04-15 23:12:39 */         public MainUnitMove(MainUnitController controller)
/* @Lee SJ    - 2024-04-15 23:12:39 */         {
/* @Lee SJ    - 2024-04-15 23:12:39 */             this.controller = controller;
/* @Lee SJ    - 2024-04-15 23:12:39 */         }
/* @Lee SJ    - 2024-04-15 23:12:39 */         
/* @Lee SJ    - 2024-04-15 23:12:39 */         public void Enter()
/* @Lee SJ    - 2024-04-15 23:12:39 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = false;
/* @Lee SJ    - 2024-04-15 23:12:39 */             controller.Agent.speed = controller.OverridenUnitData.MovementSpeed;
/* @Lee SJ    - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Move", 0.1f);
/* @Lee SJ    - 2024-04-15 23:12:39 */         }
/* @Lee SJ    - 2024-04-15 23:12:39 */ 
/* @Lee SJ    - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ    - 2024-04-15 23:12:39 */         public void Execute()
/* @Lee SJ    - 2024-04-15 23:12:39 */         {
/* @Lee SJ    - 2024-04-15 23:12:39 */             if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
/* @Lee SJ    - 2024-04-15 23:12:39 */             {
/* @Lee SJ    - 2024-04-15 23:12:39 */                 controller.StateMachine.ChangeState(new MainUnitIdle(controller));
/* @Lee SJ    - 2024-04-15 23:12:39 */                 return;
/* @Lee SJ    - 2024-04-15 23:12:39 */             }
/* @Lee SJ    - 2024-04-15 23:12:39 */             
/* @Lee SJ    - 2024-05-21 00:43:20 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @LiF       - 2024-05-30 14:02:30 */             var ownTypeUnits = unitsInView.Where(t => controller.UnitType.Equals(t.UnitType) && t.IsStopped);
/* @LiF       - 2024-05-19 02:04:44 */             var destination = controller.LatestDestination;
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             BaseUnitController closestUnit = ownTypeUnits.FirstOrDefault(t =>
/* @LiF       - 2024-05-19 02:04:44 */                 Mathf.Approximately(destination.x, t.LatestDestination.x) &&
/* @LiF       - 2024-05-19 02:04:44 */                 Mathf.Approximately(destination.y, t.LatestDestination.y) &&
/* @LiF       - 2024-05-19 02:04:44 */                 Mathf.Approximately(destination.z, t.LatestDestination.z) &&
/* @LiF       - 2024-05-19 02:04:44 */                 Vector3.Distance(controller.transform.position, t.transform.position) <= 2f);
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             if (closestUnit != null)
/* @Lee SJ    - 2024-04-15 23:12:39 */             {
/* @Lee SJ    - 2024-04-15 23:12:39 */                 controller.StateMachine.ChangeState(new MainUnitIdle(controller));
/* @Lee SJ    - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ    - 2024-05-06 18:31:44 */             }
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             if (controller.UnitType is "Player" or "Enemy")
/* @Lee SJ    - 2024-05-06 18:31:44 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 closestUnit = unitsInView
/* @Lee SJ    - 2024-05-31 20:49:59 */                     .Where(t => t.gameObject.activeInHierarchy && t.Alive)
/* @Lee SJ    - 2024-05-06 18:31:44 */                     .Where(t => !controller.UnitType.Equals(t.UnitType))
/* @Lee SJ    - 2024-05-06 18:31:44 */                     .OrderBy(t => (t.transform.position - controller.transform.position).sqrMagnitude)
/* @Lee SJ    - 2024-05-06 18:31:44 */                     .FirstOrDefault();
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */                 if (closestUnit == null) return;
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */                 float sqrViewRadius = controller.OverridenUnitData.ViewRadius * controller.OverridenUnitData.ViewRadius;
/* @LiF       - 2024-05-19 02:04:44 */                 if ((closestUnit.transform.position - controller.transform.position).sqrMagnitude <= sqrViewRadius)
/* @Lee SJ    - 2024-05-06 18:31:44 */                 {
/* @Lee SJ    - 2024-05-06 18:31:44 */                     controller.StateMachine.ChangeState(new MainUnitChase(controller));
/* @Lee SJ    - 2024-05-06 18:31:44 */                 }
/* @Lee SJ    - 2024-04-15 23:12:39 */             }
/* @Lee SJ    - 2024-04-15 23:12:39 */         }
/* @Lee SJ    - 2024-04-15 23:12:39 */ 
/* @LiF       - 2024-05-19 02:04:44 */         public void FixedExecute() { }
/* @Lee SJ    - 2024-04-15 23:12:39 */ 
/* @Lee SJ    - 2024-04-15 23:12:39 */         public void Exit()
/* @Lee SJ    - 2024-04-15 23:12:39 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = true; 
/* @Lee SJ    - 2024-04-15 23:12:39 */         }
/* @Lee SJ    - 2024-04-15 23:12:39 */     }
/* @Lee SJ    - 2024-04-15 23:12:39 */ }
