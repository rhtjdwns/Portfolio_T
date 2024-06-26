/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-04-02 05:20:57 */ using System.Linq;
/* @Lee SJ    - 2024-04-02 05:20:57 */ using UnityEngine;
/* @Lee SJ    - 2024-04-02 05:20:57 */ using FullMoon.FSM;
/* @Lee SJ    - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ    - 2024-04-02 05:20:57 */ {
/* @Lee SJ    - 2024-05-03 19:25:37 */     [BurstCompile]
/* @Lee SJ    - 2024-04-02 05:20:57 */     public class MeleeUnitMove : IState
/* @Lee SJ    - 2024-04-02 05:20:57 */     {
/* @Lee SJ    - 2024-04-02 05:20:57 */         private readonly MeleeUnitController controller;
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */         public MeleeUnitMove(MeleeUnitController controller)
/* @Lee SJ    - 2024-04-02 05:20:57 */         {
/* @Lee SJ    - 2024-04-02 05:20:57 */             this.controller = controller;
/* @Lee SJ    - 2024-04-02 05:20:57 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */         
/* @Lee SJ    - 2024-04-02 05:20:57 */         public void Enter()
/* @Lee SJ    - 2024-04-02 05:20:57 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = false;
/* @Lee SJ    - 2024-04-02 05:20:57 */             controller.Agent.speed = controller.OverridenUnitData.MovementSpeed;
/* @Lee SJ    - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Move", 0.1f);
/* @Lee SJ    - 2024-04-02 05:20:57 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ    - 2024-04-02 05:20:57 */         public void Execute()
/* @Lee SJ    - 2024-04-02 05:20:57 */         {
/* @Lee SJ    - 2024-04-02 05:20:57 */             if (!controller.Agent.pathPending && controller.Agent.remainingDistance <= controller.Agent.stoppingDistance)
/* @Lee SJ    - 2024-04-02 05:20:57 */             {
/* @Lee SJ    - 2024-04-02 05:20:57 */                 controller.StateMachine.ChangeState(new MeleeUnitIdle(controller));
/* @Lee SJ    - 2024-04-02 05:20:57 */                 return;
/* @Lee SJ    - 2024-04-02 05:20:57 */             }
/* @Lee SJ    - 2024-04-02 05:20:57 */             
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
/* @Lee SJ    - 2024-04-02 05:20:57 */             {
/* @Lee SJ    - 2024-04-02 05:20:57 */                 controller.StateMachine.ChangeState(new MeleeUnitIdle(controller));
/* @김태홍노트북    - 2024-04-16 02:17:10 */                 return;
/* @김태홍노트북    - 2024-04-16 02:17:10 */             }
/* @김태홍노트북    - 2024-04-16 02:17:10 */ 
/* @LiF       - 2024-05-19 02:04:44 */             if (controller.UnitType is "Player" or "Enemy")
/* @김태홍노트북    - 2024-04-16 02:17:10 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 closestUnit = unitsInView
/* @Lee SJ    - 2024-05-31 20:49:59 */                     .Where(t => t.gameObject.activeInHierarchy && t.Alive)
/* @rhtjdwns  - 2024-04-18 09:55:38 */                     .Where(t => !controller.UnitType.Equals(t.UnitType))
/* @rhtjdwns  - 2024-04-18 09:55:38 */                     .OrderBy(t => (t.transform.position - controller.transform.position).sqrMagnitude)
/* @rhtjdwns  - 2024-04-18 09:55:38 */                     .FirstOrDefault();
/* @김태홍노트북    - 2024-04-16 02:17:10 */ 
/* @LiF       - 2024-05-19 02:04:44 */                 if (closestUnit == null) return;
/* @김태홍노트북    - 2024-04-16 02:17:10 */ 
/* @LiF       - 2024-05-19 02:04:44 */                 float sqrViewRadius = controller.OverridenUnitData.ViewRadius * controller.OverridenUnitData.ViewRadius;
/* @LiF       - 2024-05-19 02:04:44 */                 if ((closestUnit.transform.position - controller.transform.position).sqrMagnitude <= sqrViewRadius)
/* @rhtjdwns  - 2024-04-18 09:55:38 */                 {
/* @Lee SJ    - 2024-04-19 20:04:37 */                     controller.StateMachine.ChangeState(new MeleeUnitChase(controller));
/* @rhtjdwns  - 2024-04-18 09:55:38 */                 }
/* @Lee SJ    - 2024-04-02 05:20:57 */             }
/* @Lee SJ    - 2024-04-02 05:20:57 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @LiF       - 2024-05-19 02:04:44 */         public void FixedExecute() { }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */         public void Exit()
/* @Lee SJ    - 2024-04-02 05:20:57 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = true; 
/* @Lee SJ    - 2024-04-02 05:20:57 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */     }
/* @Lee SJ    - 2024-04-02 05:20:57 */ }
