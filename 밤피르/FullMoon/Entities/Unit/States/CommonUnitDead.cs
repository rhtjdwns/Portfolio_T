/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-21 00:43:20 */ using Cysharp.Threading.Tasks;
/* @Lee SJ  - 2024-05-21 00:43:20 */ using UnityEngine;
/* @Lee SJ  - 2024-05-21 00:43:20 */ using FullMoon.FSM;
/* @Lee SJ  - 2024-05-21 00:43:20 */ using FullMoon.Util;
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */ namespace FullMoon.Entities.Unit.States
/* @Lee SJ  - 2024-05-21 00:43:20 */ {
/* @Lee SJ  - 2024-05-21 00:43:20 */     public class CommonUnitDead : IState
/* @Lee SJ  - 2024-05-21 00:43:20 */     {
/* @Lee SJ  - 2024-05-21 00:43:20 */         private readonly CommonUnitController controller;
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public CommonUnitDead(CommonUnitController controller)
/* @Lee SJ  - 2024-05-21 00:43:20 */         {
/* @Lee SJ  - 2024-05-21 00:43:20 */             this.controller = controller;
/* @Lee SJ  - 2024-05-21 00:43:20 */         }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void Enter()
/* @Lee SJ  - 2024-05-21 00:43:20 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             DisableAfterAnimation("Dead").Forget();
/* @Lee SJ  - 2024-05-21 00:43:20 */         }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void Execute() { }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void FixedExecute() { }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-21 00:43:20 */         public void Exit() { }
/* @Lee SJ  - 2024-05-21 00:43:20 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */         private async UniTask DisableAfterAnimation(string animationName)
/* @Lee SJ  - 2024-05-21 00:43:20 */         {
/* @Lee SJ  - 2024-06-03 00:12:04 */             try
/* @Lee SJ  - 2024-05-21 00:43:20 */             {
/* @Lee SJ  - 2024-06-03 00:12:04 */                 if (controller.AnimationController.SetAnimation(animationName))
/* @Lee SJ  - 2024-05-21 00:43:20 */                 {
/* @Lee SJ  - 2024-06-03 00:12:04 */                     await UniTask.WaitUntil(() => 
/* @Lee SJ  - 2024-06-03 00:12:04 */                     {
/* @Lee SJ  - 2024-06-03 00:12:04 */                         var stateInfo = controller.unitAnimator.GetCurrentAnimatorStateInfo(0);
/* @Lee SJ  - 2024-06-03 00:12:04 */                         if (controller.AnimationController.CurrentStateInfo.Item1 != animationName)
/* @Lee SJ  - 2024-06-03 00:12:04 */                         {
/* @Lee SJ  - 2024-06-03 00:12:04 */                             controller.AnimationController.SetAnimation(animationName);
/* @Lee SJ  - 2024-06-03 00:12:04 */                         }
/* @LiF     - 2024-06-03 02:22:18 */                         return (controller.AnimationController.CurrentStateInfo.Item1 == animationName && stateInfo.normalizedTime >= 1.0f);
/* @Lee SJ  - 2024-06-03 00:12:04 */                     });
/* @LiF     - 2024-06-03 02:22:18 */                     await UniTask.Delay(1000);
/* @Lee SJ  - 2024-06-03 00:12:04 */                 }
/* @Lee SJ  - 2024-06-03 00:12:04 */             }
/* @Lee SJ  - 2024-06-03 00:12:04 */             finally
/* @Lee SJ  - 2024-06-03 00:12:04 */             {
/* @Lee SJ  - 2024-06-03 00:12:04 */                 ObjectPoolManager.Instance.ReturnObjectToPool(controller.gameObject);
/* @Lee SJ  - 2024-05-21 00:43:20 */             }
/* @Lee SJ  - 2024-05-21 00:43:20 */         }
/* @Lee SJ  - 2024-05-21 00:43:20 */     }
/* @Lee SJ  - 2024-05-21 00:43:20 */ }