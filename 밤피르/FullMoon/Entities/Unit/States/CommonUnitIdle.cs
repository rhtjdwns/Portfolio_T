/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-21 00:43:20 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-21 00:43:20 */ using Unity.Burst;
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-05-21 00:43:20 */ {
/* @Lee SJ  - 2024-05-21 00:43:20 */     [BurstCompile]
/* @Lee SJ  - 2024-05-21 00:43:20 */     public class CommonUnitIdle : IState
/* @Lee SJ  - 2024-05-21 00:43:20 */     {
/* @Lee SJ  - 2024-05-21 00:43:20 */         private readonly CommonUnitController controller;
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public CommonUnitIdle(CommonUnitController controller)
/* @Lee SJ  - 2024-05-21 00:43:20 */         {
/* @Lee SJ  - 2024-05-21 00:43:20 */             this.controller = controller;
/* @Lee SJ  - 2024-05-21 00:43:20 */         }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void Enter()
/* @Lee SJ  - 2024-05-21 00:43:20 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Idle");
/* @Lee SJ  - 2024-05-21 00:43:20 */         }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         [BurstCompile]
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void Execute()
/* @Lee SJ  - 2024-05-21 00:43:20 */         {
/* @Lee SJ  - 2024-05-25 16:45:42 */             if (controller.MainUnit == null || 
/* @Lee SJ  - 2024-05-25 16:45:42 */                 controller.gameObject.activeInHierarchy == false || controller.MainUnit.gameObject.activeInHierarchy == false || 
/* @Lee SJ  - 2024-05-25 16:45:42 */                 controller.Alive == false || controller.MainUnit.Alive == false)
/* @Lee SJ  - 2024-05-21 00:43:20 */             {
/* @Lee SJ  - 2024-05-21 00:43:20 */                 return;
/* @Lee SJ  - 2024-05-21 00:43:20 */             }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */             if (controller.UnitInsideViewArea.Contains(controller.MainUnit) == false)
/* @Lee SJ  - 2024-05-21 00:43:20 */             {
/* @Lee SJ  - 2024-05-21 00:43:20 */                 controller.MoveToPosition(controller.MainUnit.transform.position);
/* @Lee SJ  - 2024-05-21 00:43:20 */             }
/* @Lee SJ  - 2024-05-21 00:43:20 */         }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void Exit() { }
/* @Lee SJ  - 2024-05-21 00:43:20 */     }
/* @Lee SJ  - 2024-05-21 00:43:20 */ }