/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-04-02 05:20:57 */ using System.Linq;
/* @Lee SJ  - 2024-04-02 05:20:57 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ  - 2024-05-06 22:26:59 */ using UnityEngine;
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @Lee SJ  - 2024-04-02 05:20:57 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-04-02 05:20:57 */ {
/* @Lee SJ  - 2024-05-03 19:25:37 */     [BurstCompile]
/* @Lee SJ  - 2024-04-02 05:20:57 */     public class MeleeUnitChase : IState
/* @Lee SJ  - 2024-04-02 05:20:57 */     {
/* @Lee SJ  - 2024-04-02 05:20:57 */         private readonly MeleeUnitController controller;
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @Lee SJ  - 2024-04-02 05:20:57 */         public MeleeUnitChase(MeleeUnitController controller)
/* @Lee SJ  - 2024-04-02 05:20:57 */         {
/* @Lee SJ  - 2024-04-02 05:20:57 */             this.controller = controller;
/* @Lee SJ  - 2024-04-02 05:20:57 */         }
/* @Lee SJ  - 2024-04-02 05:20:57 */         
/* @Lee SJ  - 2024-04-02 05:20:57 */         public void Enter()
/* @Lee SJ  - 2024-04-02 05:20:57 */         {
/* @LiF     - 2024-05-30 14:02:30 */             controller.IsStopped = false;
/* @Lee SJ  - 2024-04-02 05:20:57 */             controller.Agent.speed = controller.OverridenUnitData.MovementSpeed;
/* @Lee SJ  - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Move");
/* @Lee SJ  - 2024-04-02 05:20:57 */         }
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @Lee SJ  - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ  - 2024-04-02 05:20:57 */         public void Execute()
/* @Lee SJ  - 2024-04-02 05:20:57 */         {
/* @LiF     - 2024-05-19 02:04:44 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @LiF     - 2024-05-19 02:04:44 */             BaseUnitController closestUnit = unitsInView
/* @Lee SJ  - 2024-05-31 20:49:59 */                 .Where(t => t.gameObject.activeInHierarchy && t.Alive)
/* @Lee SJ  - 2024-04-12 20:21:54 */                 .Where(t => !controller.UnitType.Equals(t.UnitType))
/* @Lee SJ  - 2024-04-02 05:20:57 */                 .OrderBy(t => (t.transform.position - controller.transform.position).sqrMagnitude)
/* @Lee SJ  - 2024-04-02 05:20:57 */                 .FirstOrDefault();
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @Lee SJ  - 2024-04-02 05:20:57 */             if (closestUnit == null)
/* @Lee SJ  - 2024-04-02 05:20:57 */             {
/* @Lee SJ  - 2024-04-02 05:20:57 */                 controller.StateMachine.ChangeState(new MeleeUnitIdle(controller));
/* @Lee SJ  - 2024-04-02 05:20:57 */                 return;
/* @Lee SJ  - 2024-04-02 05:20:57 */             }
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @LiF     - 2024-05-19 02:04:44 */             float sqrAttackRadius = controller.OverridenUnitData.AttackRadius * controller.OverridenUnitData.AttackRadius;
/* @LiF     - 2024-05-19 02:04:44 */             if ((closestUnit.transform.position - controller.transform.position).sqrMagnitude <= sqrAttackRadius)
/* @Lee SJ  - 2024-04-02 05:20:57 */             {
/* @Lee SJ  - 2024-04-19 20:04:37 */                 controller.LatestDestination = controller.transform.position;
/* @Lee SJ  - 2024-04-02 05:20:57 */                 controller.StateMachine.ChangeState(new MeleeUnitAttack(controller));
/* @Lee SJ  - 2024-04-02 05:20:57 */             }
/* @Lee SJ  - 2024-04-02 05:20:57 */             else
/* @Lee SJ  - 2024-04-02 05:20:57 */             {
/* @Lee SJ  - 2024-04-02 05:20:57 */                 controller.Agent.SetDestination(closestUnit.transform.position);
/* @Lee SJ  - 2024-04-02 05:20:57 */             }
/* @Lee SJ  - 2024-04-02 05:20:57 */         }
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-04-02 05:20:57 */ 
/* @Lee SJ  - 2024-04-02 05:20:57 */         public void Exit()
/* @Lee SJ  - 2024-04-02 05:20:57 */         {
/* @LiF     - 2024-05-30 14:02:30 */             controller.IsStopped = true;
/* @Lee SJ  - 2024-04-02 05:20:57 */         }
/* @Lee SJ  - 2024-04-02 05:20:57 */     }
/* @LiF     - 2024-05-19 02:04:44 */ }