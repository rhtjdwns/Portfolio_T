/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-06-01 00:35:06 */ using System;
/* @rhtjdwns  - 2024-05-22 11:51:10 */ using FullMoon.FSM;
/* @rhtjdwns  - 2024-06-01 00:35:06 */ using FullMoon.Util;
/* @rhtjdwns  - 2024-05-22 11:51:10 */ 
/* @rhtjdwns  - 2024-05-22 11:51:10 */ namespace FullMoon.Entities.Unit.States
/* @rhtjdwns  - 2024-05-22 11:51:10 */ {
/* @rhtjdwns  - 2024-05-22 11:51:10 */     public class HammerUnitCraft : IState
/* @rhtjdwns  - 2024-05-22 11:51:10 */     {
/* @rhtjdwns  - 2024-05-22 11:51:10 */         private readonly HammerUnitController controller;
/* @rhtjdwns  - 2024-05-22 11:51:10 */ 
/* @rhtjdwns  - 2024-05-22 11:51:10 */         public HammerUnitCraft(HammerUnitController controller)
/* @rhtjdwns  - 2024-05-22 11:51:10 */         {
/* @rhtjdwns  - 2024-05-22 11:51:10 */             this.controller = controller;
/* @rhtjdwns  - 2024-05-22 11:51:10 */         }
/* @rhtjdwns  - 2024-05-22 11:51:10 */ 
/* @rhtjdwns  - 2024-05-22 11:51:10 */         public void Enter()
/* @rhtjdwns  - 2024-05-22 11:51:10 */         {
/* @LiF       - 2024-05-30 14:02:30 */             controller.IsStopped = true;
/* @rhtjdwns  - 2024-05-22 11:51:10 */             controller.AnimationController.SetAnimation("Craft", 0.1f);
/* @rhtjdwns  - 2024-05-22 11:51:10 */         }
/* @rhtjdwns  - 2024-05-22 11:51:10 */ 
/* @Lee SJ    - 2024-05-31 20:49:59 */         public void Execute() { }
/* @rhtjdwns  - 2024-05-22 11:51:10 */ 
/* @rhtjdwns  - 2024-05-22 11:51:10 */         public void FixedExecute() { }
/* @rhtjdwns  - 2024-05-22 11:51:10 */ 
/* @rhtjdwns  - 2024-05-22 11:51:10 */         public void Exit() { }
/* @rhtjdwns  - 2024-05-22 11:51:10 */     }
/* @rhtjdwns  - 2024-05-22 11:51:10 */ }
