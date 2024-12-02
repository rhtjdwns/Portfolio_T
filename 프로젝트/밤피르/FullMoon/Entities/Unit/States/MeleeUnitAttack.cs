/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-04-02 05:20:57 */ using System.Linq;
/* @Lee SJ    - 2024-04-02 05:20:57 */ using UnityEngine;
/* @Lee SJ    - 2024-04-02 05:20:57 */ using FullMoon.FSM;
/* @Lee SJ    - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ    - 2024-04-02 05:20:57 */ {
/* @Lee SJ    - 2024-05-03 19:25:37 */     [BurstCompile]
/* @Lee SJ    - 2024-04-02 05:20:57 */     public class MeleeUnitAttack : IState
/* @Lee SJ    - 2024-04-02 05:20:57 */     {
/* @Lee SJ    - 2024-04-02 05:20:57 */         private readonly MeleeUnitController controller;
/* @rhtjdwns  - 2024-05-20 20:22:30 */         private BaseUnitController target;
/* @Lee SJ    - 2024-04-22 20:03:35 */         private float attackDelay;
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */         public MeleeUnitAttack(MeleeUnitController controller)
/* @Lee SJ    - 2024-04-02 05:20:57 */         {
/* @Lee SJ    - 2024-04-02 05:20:57 */             this.controller = controller;
/* @Lee SJ    - 2024-04-02 05:20:57 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */         public void Enter()
/* @Lee SJ    - 2024-04-02 05:20:57 */         {
/* @Lee SJ    - 2024-04-22 20:03:35 */             attackDelay = controller.OverridenUnitData.AttackDelay;
/* @rhtjdwns  - 2024-05-20 19:18:43 */ 
/* @rhtjdwns  - 2024-05-21 00:58:15 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @rhtjdwns  - 2024-05-21 01:22:35 */             target = unitsInView
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .Where(t => t.gameObject.activeInHierarchy && t.Alive)
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .Where(t => !controller.UnitType.Equals(t.UnitType))
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .OrderBy(t => (t.transform.position - controller.transform.position).sqrMagnitude)
/* @Lee SJ    - 2024-05-31 20:49:59 */                 .FirstOrDefault();
/* @Lee SJ    - 2024-05-21 02:19:56 */             
/* @Lee SJ    - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Idle");
/* @rhtjdwns  - 2024-05-21 01:22:35 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @rhtjdwns  - 2024-05-21 01:22:35 */         [BurstCompile]
/* @rhtjdwns  - 2024-05-21 01:22:35 */         public void Execute()
/* @rhtjdwns  - 2024-05-21 01:22:35 */         {
/* @Lee SJ    - 2024-05-21 01:45:04 */             if (target == null || target.gameObject.activeInHierarchy == false || target.Alive == false)
/* @Lee SJ    - 2024-04-02 05:20:57 */             {
/* @Lee SJ    - 2024-04-22 01:15:59 */                 controller.StateMachine.ChangeState(new MeleeUnitIdle(controller));
/* @Lee SJ    - 2024-04-02 05:20:57 */                 return;
/* @Lee SJ    - 2024-04-02 05:20:57 */             }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @rhtjdwns  - 2024-05-20 20:22:30 */             Vector3 targetDirection = target.transform.position - controller.transform.position;
/* @Lee SJ    - 2024-05-08 18:25:40 */             controller.transform.forward = targetDirection.normalized;
/* @Lee SJ    - 2024-05-08 18:25:40 */             controller.transform.eulerAngles = new Vector3(0f, controller.transform.eulerAngles.y, controller.transform.eulerAngles.z);
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             float sqrAttackRadius = controller.OverridenUnitData.AttackRadius * controller.OverridenUnitData.AttackRadius;
/* @rhtjdwns  - 2024-05-20 20:22:30 */             if ((target.transform.position - controller.transform.position).sqrMagnitude > sqrAttackRadius)
/* @Lee SJ    - 2024-04-02 05:20:57 */             {
/* @Lee SJ    - 2024-04-02 05:20:57 */                 controller.StateMachine.ChangeState(new MeleeUnitChase(controller));
/* @Lee SJ    - 2024-04-02 05:20:57 */                 return;
/* @Lee SJ    - 2024-04-02 05:20:57 */             }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-04-22 20:03:35 */             if (attackDelay > 0)
/* @Lee SJ    - 2024-04-02 05:20:57 */             {
/* @Lee SJ    - 2024-04-22 20:03:35 */                 attackDelay -= Time.deltaTime;
/* @Lee SJ    - 2024-04-02 05:20:57 */                 return;
/* @Lee SJ    - 2024-04-02 05:20:57 */             }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-04-22 20:03:35 */             if (controller.CurrentAttackCoolTime > 0)
/* @Lee SJ    - 2024-04-22 20:03:35 */             {
/* @Lee SJ    - 2024-04-22 20:03:35 */                 return;
/* @Lee SJ    - 2024-04-22 20:03:35 */             }
/* @Lee SJ    - 2024-04-22 20:03:35 */ 
/* @Lee SJ    - 2024-04-22 20:03:35 */             controller.CurrentAttackCoolTime = controller.OverridenUnitData.AttackCoolTime;
/* @rhtjdwns  - 2024-05-20 20:22:30 */             controller.ExecuteAttack(target.transform).Forget();
/* @Lee SJ    - 2024-04-02 05:20:57 */         }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-22 01:15:59 */         public void FixedExecute() { }
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-22 01:15:59 */         public void Exit() { }
/* @Lee SJ    - 2024-04-02 05:20:57 */     }
/* @LiF       - 2024-05-19 02:04:44 */ }