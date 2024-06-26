/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-21 16:02:38 */ using System.Linq;
/* @Lee SJ  - 2024-05-21 16:02:38 */ using UnityEngine;
/* @Lee SJ  - 2024-05-21 16:02:38 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-21 16:02:38 */ using Unity.Burst;
/* @Lee SJ  - 2024-05-21 16:02:38 */ 
/* @Lee SJ  - 2024-05-21 16:02:38 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-05-21 16:02:38 */ {
/* @Lee SJ  - 2024-05-21 16:02:38 */     [BurstCompile]
/* @Lee SJ  - 2024-05-21 16:02:38 */     public class HammerUnitIdle : IState
/* @Lee SJ  - 2024-05-21 16:02:38 */     {
/* @Lee SJ  - 2024-05-21 16:02:38 */         private readonly HammerUnitController controller;
/* @Lee SJ  - 2024-05-21 16:02:38 */ 
/* @Lee SJ  - 2024-05-21 16:02:38 */         public HammerUnitIdle(HammerUnitController controller)
/* @Lee SJ  - 2024-05-21 16:02:38 */         {
/* @Lee SJ  - 2024-05-21 16:02:38 */             this.controller = controller;
/* @Lee SJ  - 2024-05-21 16:02:38 */         }
/* @Lee SJ  - 2024-05-21 16:02:38 */ 
/* @Lee SJ  - 2024-05-21 16:02:38 */         public void Enter()
/* @Lee SJ  - 2024-05-21 16:02:38 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             controller.AnimationController.SetAnimation("Idle");
/* @Lee SJ  - 2024-05-21 16:02:38 */         }
/* @Lee SJ  - 2024-05-21 16:02:38 */ 
/* @Lee SJ  - 2024-05-21 16:02:38 */         public void Execute() { }
/* @Lee SJ  - 2024-05-21 16:02:38 */ 
/* @Lee SJ  - 2024-05-21 16:02:38 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-05-21 16:02:38 */ 
/* @Lee SJ  - 2024-05-21 16:02:38 */         public void Exit() { }
/* @Lee SJ  - 2024-05-21 16:02:38 */     }
/* @Lee SJ  - 2024-05-21 16:02:38 */ }