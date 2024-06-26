/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-22 02:05:43 */ using System.Threading;
/* @Lee SJ  - 2024-05-22 02:05:43 */ using Cysharp.Threading.Tasks;
/* @Lee SJ  - 2024-05-22 02:05:43 */ using UnityEngine;
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */ namespace FullMoon.Animation
/* @Lee SJ  - 2024-05-22 02:05:43 */ {
/* @LiF     - 2024-06-01 15:37:58 */     public class AnimationController
/* @Lee SJ  - 2024-05-22 02:05:43 */     {
/* @Lee SJ  - 2024-05-22 02:05:43 */         private Animator unitAnimator;
/* @Lee SJ  - 2024-05-22 02:05:43 */         private CancellationTokenSource cancellationTokenSource;
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-31 20:49:59 */         public (string, bool) CurrentStateInfo { get; private set; }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */         public void SetAnimator(Animator animator)
/* @Lee SJ  - 2024-05-22 02:05:43 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             CancelAnimationLoop();
/* @Lee SJ  - 2024-05-22 02:05:43 */             unitAnimator = animator;
/* @Lee SJ  - 2024-05-22 02:05:43 */         }
/* @Lee SJ  - 2024-05-22 02:05:43 */         
/* @Lee SJ  - 2024-05-22 02:05:43 */         public bool SetAnimation(string stateName, float transitionDuration = 0.3f)
/* @Lee SJ  - 2024-05-22 02:05:43 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             if (unitAnimator == null)
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-22 02:05:43 */                 return false;
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */             int stateHash = Animator.StringToHash(stateName);
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */             if (!unitAnimator.HasState(0, stateHash))
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-28 12:17:25 */                 Debug.LogWarning($"{stateName} 애니메이션이 존재하지 않습니다.");
/* @Lee SJ  - 2024-05-22 02:05:43 */                 return false;
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-31 20:49:59 */             if (CurrentStateInfo.Item1 == stateName && CurrentStateInfo.Item2)
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-22 02:05:43 */                 return true;
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */             PlayAnimation(stateName, stateHash, transitionDuration);
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */             return true;
/* @Lee SJ  - 2024-05-22 02:05:43 */         }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */         private void CancelAnimationLoop()
/* @Lee SJ  - 2024-05-22 02:05:43 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             if (cancellationTokenSource != null)
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-22 02:05:43 */                 cancellationTokenSource.Cancel();
/* @Lee SJ  - 2024-05-22 02:05:43 */                 cancellationTokenSource.Dispose();
/* @Lee SJ  - 2024-05-22 02:05:43 */                 cancellationTokenSource = null;
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */         }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */         private void PlayAnimation(string stateName, int stateHash, float transitionDuration = 0.3f)
/* @Lee SJ  - 2024-05-22 02:05:43 */         {
/* @Lee SJ  - 2024-05-31 20:49:59 */             CurrentStateInfo = ("", false);
/* @Lee SJ  - 2024-05-22 02:05:43 */             
/* @Lee SJ  - 2024-05-28 12:17:25 */             AnimationClip[] clips = unitAnimator.runtimeAnimatorController.animationClips;
/* @Lee SJ  - 2024-05-28 12:17:25 */             foreach (AnimationClip clip in clips)
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-28 12:17:25 */                 if (clip.name == stateName)
/* @Lee SJ  - 2024-05-22 02:05:43 */                 {
/* @Lee SJ  - 2024-05-31 20:49:59 */                     CurrentStateInfo = (clip.name, clip.isLooping);
/* @LiF     - 2024-05-27 17:13:35 */                     break;
/* @Lee SJ  - 2024-05-22 02:05:43 */                 }
/* @Lee SJ  - 2024-05-28 12:17:25 */             }   
/* @Lee SJ  - 2024-05-22 02:05:43 */             
/* @Lee SJ  - 2024-05-22 02:05:43 */             unitAnimator.Play(stateHash, 0, 0);
/* @Lee SJ  - 2024-05-22 02:05:43 */             unitAnimator.CrossFade(stateHash, transitionDuration);
/* @Lee SJ  - 2024-05-22 02:05:43 */         }
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */         public async UniTaskVoid PlayAnimationAndContinueLoop(string stateName, float transitionDuration = 0.3f)
/* @Lee SJ  - 2024-05-22 02:05:43 */         {
/* @Lee SJ  - 2024-05-22 02:05:43 */             if (unitAnimator == null)
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-22 02:05:43 */                 return;
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */             
/* @Lee SJ  - 2024-05-22 02:05:43 */             CancelAnimationLoop();
/* @Lee SJ  - 2024-05-22 02:05:43 */             
/* @Lee SJ  - 2024-05-22 02:05:43 */             int stateHash = Animator.StringToHash(stateName);
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */             if (!unitAnimator.HasState(0, stateHash))
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-28 12:17:25 */                 Debug.LogWarning($"{stateName} 애니메이션이 존재하지 않습니다.");
/* @Lee SJ  - 2024-05-22 02:05:43 */                 return;
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */             
/* @Lee SJ  - 2024-05-22 02:05:43 */             cancellationTokenSource = new CancellationTokenSource();
/* @Lee SJ  - 2024-05-22 02:05:43 */             CancellationToken token = cancellationTokenSource.Token;
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-31 20:49:59 */             string latestStateName = CurrentStateInfo.Item1;
/* @Lee SJ  - 2024-05-31 20:49:59 */             bool latestLooping = CurrentStateInfo.Item2;
/* @Lee SJ  - 2024-05-22 02:05:43 */ 
/* @Lee SJ  - 2024-05-22 02:05:43 */             PlayAnimation(stateName, stateHash, transitionDuration);
/* @Lee SJ  - 2024-05-22 02:05:43 */             
/* @Lee SJ  - 2024-05-22 02:05:43 */             do
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-22 02:05:43 */                 await UniTask.Yield(PlayerLoopTiming.Update, token);
/* @Lee SJ  - 2024-05-22 02:05:43 */             } while (!token.IsCancellationRequested && latestLooping && unitAnimator.IsInTransition(0));
/* @Lee SJ  - 2024-05-28 12:17:25 */             
/* @Lee SJ  - 2024-05-31 20:49:59 */             if (latestLooping && CurrentStateInfo.Item1 == stateName)
/* @Lee SJ  - 2024-05-22 02:05:43 */             {
/* @Lee SJ  - 2024-05-22 02:05:43 */                 PlayAnimation(latestStateName, Animator.StringToHash(latestStateName), transitionDuration);
/* @Lee SJ  - 2024-05-22 02:05:43 */             }
/* @Lee SJ  - 2024-05-22 02:05:43 */         }
/* @Lee SJ  - 2024-05-22 02:05:43 */     }
/* @Lee SJ  - 2024-05-22 02:05:43 */ }
