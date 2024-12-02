/* Git Blame Auto Generated */

/* @LiF     - 2024-05-30 13:40:00 */ using System;
/* @LiF     - 2024-05-30 13:40:00 */ using Cysharp.Threading.Tasks;
/* @LiF     - 2024-05-30 13:40:00 */ using FullMoon.FSM;
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */ namespace FullMoon.Entities.Unit.States
/* @LiF     - 2024-05-30 13:40:00 */ {
/* @LiF     - 2024-05-30 13:40:00 */     public class MainUnitGroggy : IState
/* @LiF     - 2024-05-30 13:40:00 */     {
/* @LiF     - 2024-05-30 13:40:00 */         private readonly MainUnitController controller;
/* @LiF     - 2024-05-30 13:40:00 */         
/* @LiF     - 2024-05-30 13:40:00 */         public MainUnitGroggy(MainUnitController controller)
/* @LiF     - 2024-05-30 13:40:00 */         {
/* @LiF     - 2024-05-30 13:40:00 */             this.controller = controller;
/* @LiF     - 2024-05-30 13:40:00 */         }
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */         public void Enter()
/* @LiF     - 2024-05-30 13:40:00 */         {
/* @LiF     - 2024-05-30 13:40:00 */             AliveAfterSeconds("Groggy").Forget();
/* @LiF     - 2024-05-30 13:40:00 */         }
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */         public void Execute() { }
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */         public void FixedExecute() { }
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */         public void Exit() { }
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */         private async UniTask AliveAfterSeconds(string animationName)
/* @LiF     - 2024-05-30 13:40:00 */         {
/* @LiF     - 2024-05-30 13:40:00 */             controller.AnimationController.SetAnimation(animationName);
/* @LiF     - 2024-05-30 13:40:00 */             
/* @Lee SJ  - 2024-06-02 21:06:48 */             await UniTask.Delay(TimeSpan.FromSeconds(controller.OverridenUnitData.GroggyTime));
/* @LiF     - 2024-05-30 13:40:00 */ 
/* @LiF     - 2024-05-30 13:40:00 */             controller.OnAlive();
/* @LiF     - 2024-05-30 13:40:00 */             controller.StateMachine.ChangeState(new MainUnitIdle(controller));
/* @LiF     - 2024-05-30 13:40:00 */         }
/* @LiF     - 2024-05-30 13:40:00 */     }
/* @LiF     - 2024-05-30 13:40:00 */ }