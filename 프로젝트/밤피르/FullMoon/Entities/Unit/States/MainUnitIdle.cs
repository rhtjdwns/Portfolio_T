/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-06 18:31:44 */ using System.Linq;
/* @Lee SJ  - 2024-05-06 18:31:44 */ using UnityEngine;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-06 18:31:44 */ using Unity.Burst;
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-04-15 23:12:39 */ {
/* @Lee SJ  - 2024-05-06 18:31:44 */     [BurstCompile]
/* @Lee SJ  - 2024-04-15 23:12:39 */     public class MainUnitIdle : IState
/* @Lee SJ  - 2024-04-15 23:12:39 */     {
/* @Lee SJ  - 2024-04-15 23:12:39 */         private readonly MainUnitController controller;
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */         public MainUnitIdle(MainUnitController controller)
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @Lee SJ  - 2024-04-15 23:12:39 */             this.controller = controller;
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @LiF     - 2024-05-16 16:01:37 */ 
/* @LiF     - 2024-05-16 16:01:37 */         public void Enter()
/* @LiF     - 2024-05-16 16:01:37 */         {
/* @LiF     - 2024-06-02 01:03:15 */             controller.AnimationController.SetAnimation("FightIdle");
/* @LiF     - 2024-05-16 16:01:37 */         }
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         [BurstCompile]
/* @Lee SJ  - 2024-05-06 18:31:44 */         public void Execute()
/* @Lee SJ  - 2024-05-06 18:31:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             var unitsInView = controller.Flag != null ? controller.Flag.UnitInsideViewArea : controller.UnitInsideViewArea;
/* @Lee SJ  - 2024-05-31 20:49:59 */             int enemyCount = unitsInView.Count(t => !controller.UnitType.Equals(t.UnitType) && t.gameObject.activeInHierarchy && t.Alive);
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-16 02:58:26 */             if (enemyCount > 0)
/* @Lee SJ  - 2024-05-06 18:31:44 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 controller.StateMachine.ChangeState(new MainUnitChase(controller));
/* @Lee SJ  - 2024-05-06 18:31:44 */                 return;
/* @Lee SJ  - 2024-05-06 18:31:44 */             }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */             if (controller.Flag != null)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 Vector3 targetPosition = controller.Flag.GetPresetPosition(controller);
/* @LiF     - 2024-05-16 16:01:37 */                 if (Vector3.Distance(controller.transform.position, targetPosition) > controller.Agent.stoppingDistance * 3f)
/* @LiF     - 2024-05-16 02:58:26 */                 {
/* @LiF     - 2024-05-16 02:58:26 */                     controller.MoveToPosition(targetPosition);
/* @LiF     - 2024-05-16 02:58:26 */                 }
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @Lee SJ  - 2024-05-06 18:31:44 */         }
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public void Exit() { }
/* @Lee SJ  - 2024-04-15 23:12:39 */     }
/* @LiF     - 2024-05-19 02:04:44 */ }