/* Git Blame Auto Generated */

/* @LiF     - 2024-04-11 01:52:04 */ using System.Linq;
/* @LiF     - 2024-04-11 01:52:04 */ using UnityEngine;
/* @Lee SJ  - 2024-04-02 04:24:46 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ  - 2024-04-02 04:24:46 */ 
/* @Lee SJ  - 2024-04-02 04:24:46 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-04-02 04:24:46 */ {
/* @Lee SJ  - 2024-05-03 19:25:37 */     [BurstCompile]
/* @Lee SJ  - 2024-04-02 04:24:46 */     public class MeleeUnitIdle : IState
/* @Lee SJ  - 2024-04-02 04:24:46 */     {
/* @Lee SJ  - 2024-04-02 04:24:46 */         private readonly MeleeUnitController controller;
/* @Lee SJ  - 2024-04-02 04:24:46 */ 
/* @Lee SJ  - 2024-04-02 04:24:46 */         public MeleeUnitIdle(MeleeUnitController controller)
/* @Lee SJ  - 2024-04-02 04:24:46 */         {
/* @Lee SJ  - 2024-04-02 04:24:46 */             this.controller = controller;
/* @Lee SJ  - 2024-04-02 04:24:46 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void Enter()
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Idle");
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-04-02 04:24:46 */ 
/* @Lee SJ  - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ  - 2024-04-02 04:24:46 */         public void Execute()
/* @Lee SJ  - 2024-04-02 04:24:46 */         {
/* @LiF     - 2024-05-19 02:04:44 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @Lee SJ  - 2024-05-31 20:49:59 */             int enemyCount = unitsInView.Count(t => !controller.UnitType.Equals(t.UnitType) && t.gameObject.activeInHierarchy && t.Alive);
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-16 02:58:26 */             if (enemyCount > 0)
/* @Lee SJ  - 2024-04-02 04:24:46 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 controller.StateMachine.ChangeState(new MeleeUnitChase(controller));
/* @Lee SJ  - 2024-04-02 04:24:46 */                 return;
/* @Lee SJ  - 2024-04-02 04:24:46 */             }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */             if (controller.Flag != null)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 Vector3 targetPosition = controller.Flag.GetPresetPosition(controller);
/* @LiF     - 2024-05-16 16:01:37 */                 if (Vector3.Distance(controller.transform.position, targetPosition) > controller.Agent.stoppingDistance * 3f)
/* @LiF     - 2024-05-16 02:58:26 */                 {
/* @LiF     - 2024-05-16 02:58:26 */                     controller.MoveToPosition(targetPosition);
/* @LiF     - 2024-06-03 18:58:33 */                     return;
/* @LiF     - 2024-05-16 02:58:26 */                 }
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @LiF     - 2024-06-03 18:58:33 */             
/* @LiF     - 2024-06-03 18:58:33 */             if (controller.UnitType.Equals("Enemy"))
/* @LiF     - 2024-06-03 18:58:33 */             {
/* @LiF     - 2024-06-03 18:58:33 */                 controller.MoveToPosition(Vector3.zero);
/* @LiF     - 2024-06-03 18:58:33 */             }
/* @Lee SJ  - 2024-04-02 04:24:46 */         }
/* @Lee SJ  - 2024-04-02 04:24:46 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-04-02 04:24:46 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public void Exit() { }
/* @Lee SJ  - 2024-04-02 04:24:46 */     }
/* @LiF     - 2024-05-19 02:04:44 */ }