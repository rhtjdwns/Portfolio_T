/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-05-06 18:31:44 */ using System.Linq;
/* @Lee SJ    - 2024-05-06 18:31:44 */ using UnityEngine;
/* @Lee SJ    - 2024-05-06 18:31:44 */ using FullMoon.FSM;
/* @Lee SJ    - 2024-05-06 18:31:44 */ using Unity.Burst;
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ    - 2024-05-06 18:31:44 */ {
/* @Lee SJ    - 2024-05-06 18:31:44 */     [BurstCompile]
/* @Lee SJ    - 2024-05-06 18:31:44 */     public class MainUnitAttack : IState
/* @Lee SJ    - 2024-05-06 18:31:44 */     {
/* @Lee SJ    - 2024-05-06 18:31:44 */         private readonly MainUnitController controller;
/* @rhtjdwns  - 2024-05-21 01:28:31 */         private BaseUnitController target;
/* @Lee SJ    - 2024-05-06 18:31:44 */         private float attackDelay;
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */         public MainUnitAttack(MainUnitController controller)
/* @Lee SJ    - 2024-05-06 18:31:44 */         {
/* @Lee SJ    - 2024-05-06 18:31:44 */             this.controller = controller;
/* @Lee SJ    - 2024-05-06 18:31:44 */         }
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */         public void Enter()
/* @Lee SJ    - 2024-05-06 18:31:44 */         {
/* @Lee SJ    - 2024-05-06 18:31:44 */             attackDelay = controller.OverridenUnitData.AttackDelay;
/* @rhtjdwns  - 2024-05-21 01:28:31 */ 
/* @rhtjdwns  - 2024-05-21 01:28:31 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @rhtjdwns  - 2024-05-21 01:28:31 */             target = unitsInView
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .Where(t => t.gameObject.activeInHierarchy && t.Alive)
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .Where(t => !controller.UnitType.Equals(t.UnitType))
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .OrderBy(t => (t.transform.position - controller.transform.position).sqrMagnitude)
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .FirstOrDefault();
/* @Lee SJ    - 2024-05-21 02:19:56 */             
/* @LiF       - 2024-05-27 17:13:35 */             controller.AnimationController.SetAnimation("FightIdle");
/* @Lee SJ    - 2024-05-06 18:31:44 */         }
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */         [BurstCompile]
/* @Lee SJ    - 2024-05-06 18:31:44 */         public void Execute()
/* @Lee SJ    - 2024-05-06 18:31:44 */         {
/* @Lee SJ    - 2024-05-21 01:45:04 */             if (target == null || target.gameObject.activeInHierarchy == false || target.Alive == false)
/* @Lee SJ    - 2024-05-06 18:31:44 */             {
/* @Lee SJ    - 2024-05-06 18:31:44 */                 controller.StateMachine.ChangeState(new MainUnitIdle(controller));
/* @Lee SJ    - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ    - 2024-05-06 18:31:44 */             }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @rhtjdwns  - 2024-05-21 01:28:31 */             Vector3 targetDirection = target.transform.position - controller.transform.position;
/* @Lee SJ    - 2024-05-08 18:25:40 */             controller.transform.forward = targetDirection.normalized;
/* @Lee SJ    - 2024-05-08 18:25:40 */             controller.transform.eulerAngles = new Vector3(0f, controller.transform.eulerAngles.y, controller.transform.eulerAngles.z);
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             float sqrAttackRadius = controller.OverridenUnitData.AttackRadius * controller.OverridenUnitData.AttackRadius;
/* @rhtjdwns  - 2024-05-21 01:28:31 */             if ((target.transform.position - controller.transform.position).sqrMagnitude > sqrAttackRadius)
/* @Lee SJ    - 2024-05-06 18:31:44 */             {
/* @Lee SJ    - 2024-05-06 18:31:44 */                 controller.StateMachine.ChangeState(new MainUnitChase(controller));
/* @Lee SJ    - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ    - 2024-05-06 18:31:44 */             }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */             if (attackDelay > 0)
/* @Lee SJ    - 2024-05-06 18:31:44 */             {
/* @Lee SJ    - 2024-05-06 18:31:44 */                 attackDelay -= Time.deltaTime;
/* @Lee SJ    - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ    - 2024-05-06 18:31:44 */             }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */             if (controller.CurrentAttackCoolTime > 0)
/* @Lee SJ    - 2024-05-06 18:31:44 */             {
/* @Lee SJ    - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ    - 2024-05-06 18:31:44 */             }
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */             controller.CurrentAttackCoolTime = controller.OverridenUnitData.AttackCoolTime;
/* @rhtjdwns  - 2024-05-21 01:28:31 */             controller.ExecuteAttack(target.transform).Forget();
/* @Lee SJ    - 2024-05-06 18:31:44 */         }
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */         public void FixedExecute() { }
/* @Lee SJ    - 2024-05-06 18:31:44 */ 
/* @Lee SJ    - 2024-05-06 18:31:44 */         public void Exit() { }
/* @Lee SJ    - 2024-05-06 18:31:44 */     }
/* @LiF       - 2024-05-19 02:04:44 */ }