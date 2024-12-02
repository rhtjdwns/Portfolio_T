/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-06 18:31:44 */ using System.Linq;
/* @Lee SJ  - 2024-05-06 18:31:44 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-06 18:31:44 */ using Unity.Burst;
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-05-06 18:31:44 */ {
/* @Lee SJ  - 2024-05-06 18:31:44 */     [BurstCompile]
/* @Lee SJ  - 2024-05-06 18:31:44 */     public class MainUnitChase : IState
/* @Lee SJ  - 2024-05-06 18:31:44 */     {
/* @Lee SJ  - 2024-05-06 18:31:44 */         private readonly MainUnitController controller;
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         public MainUnitChase(MainUnitController controller)
/* @Lee SJ  - 2024-05-06 18:31:44 */         {
/* @Lee SJ  - 2024-05-06 18:31:44 */             this.controller = controller;
/* @Lee SJ  - 2024-05-06 18:31:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         public void Enter()
/* @Lee SJ  - 2024-05-06 18:31:44 */         {
/* @LiF     - 2024-05-30 14:02:30 */             controller.IsStopped = false;
/* @Lee SJ  - 2024-05-06 18:31:44 */             controller.Agent.speed = controller.OverridenUnitData.MovementSpeed;
/* @Lee SJ  - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Move");
/* @Lee SJ  - 2024-05-06 18:31:44 */         }
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         [BurstCompile]
/* @Lee SJ  - 2024-05-06 18:31:44 */         public void Execute()
/* @Lee SJ  - 2024-05-06 18:31:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @LiF     - 2024-05-19 02:04:44 */             BaseUnitController closestUnit = unitsInView
/* @Lee SJ  - 2024-05-31 20:49:59 */                 .Where(t => t.gameObject.activeInHierarchy && t.Alive)
/* @Lee SJ  - 2024-05-06 18:31:44 */                 .Where(t => !controller.UnitType.Equals(t.UnitType))
/* @Lee SJ  - 2024-05-06 18:31:44 */                 .OrderBy(t => (t.transform.position - controller.transform.position).sqrMagnitude)
/* @Lee SJ  - 2024-05-06 18:31:44 */                 .FirstOrDefault();
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */             if (closestUnit == null)
/* @Lee SJ  - 2024-05-06 18:31:44 */             {
/* @Lee SJ  - 2024-05-06 18:31:44 */                 controller.StateMachine.ChangeState(new MainUnitIdle(controller));
/* @Lee SJ  - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ  - 2024-05-06 18:31:44 */             }
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */             float sqrAttackRadius = controller.OverridenUnitData.AttackRadius * controller.OverridenUnitData.AttackRadius;
/* @LiF     - 2024-05-19 02:04:44 */             if ((closestUnit.transform.position - controller.transform.position).sqrMagnitude <= sqrAttackRadius)
/* @Lee SJ  - 2024-05-06 18:31:44 */             {
/* @Lee SJ  - 2024-05-06 18:31:44 */                 controller.LatestDestination = controller.transform.position;
/* @Lee SJ  - 2024-05-06 18:31:44 */                 controller.StateMachine.ChangeState(new MainUnitAttack(controller));
/* @Lee SJ  - 2024-05-06 18:31:44 */             }
/* @Lee SJ  - 2024-05-06 18:31:44 */             else
/* @Lee SJ  - 2024-05-06 18:31:44 */             {
/* @Lee SJ  - 2024-05-06 18:31:44 */                 controller.Agent.SetDestination(closestUnit.transform.position);
/* @Lee SJ  - 2024-05-06 18:31:44 */             }
/* @Lee SJ  - 2024-05-06 18:31:44 */         }
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         public void Exit()
/* @Lee SJ  - 2024-05-06 18:31:44 */         {
/* @LiF     - 2024-05-30 14:02:30 */             controller.IsStopped = true;
/* @Lee SJ  - 2024-05-06 18:31:44 */         }
/* @Lee SJ  - 2024-05-06 18:31:44 */     }
/* @LiF     - 2024-05-19 02:04:44 */ }