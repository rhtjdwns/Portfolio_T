/* Git Blame Auto Generated */

/* @LiF  - 2024-06-02 02:43:11 */ using DG.Tweening;
/* @LiF  - 2024-05-29 11:33:09 */ using UnityEngine.UIElements;
/* @LiF  - 2024-05-29 11:33:09 */ 
/* @LiF  - 2024-05-29 11:33:09 */ namespace FullMoon.UI
/* @LiF  - 2024-05-29 11:33:09 */ {
/* @LiF  - 2024-05-29 11:33:09 */     public static class UIElementsExtensions
/* @LiF  - 2024-05-29 11:33:09 */     {
/* @LiF  - 2024-06-02 02:43:11 */         public static void SetVisible(this VisualElement element, bool enabled, float duration = 0f)
/* @LiF  - 2024-05-29 11:33:09 */         {
/* @LiF  - 2024-06-02 02:43:11 */             if (enabled)
/* @LiF  - 2024-06-02 02:43:11 */             {
/* @LiF  - 2024-06-02 02:43:11 */                 element.style.display = DisplayStyle.Flex;
/* @LiF  - 2024-06-02 02:43:11 */                 element.FadeIn(duration);
/* @LiF  - 2024-06-02 02:43:11 */             }
/* @LiF  - 2024-06-02 02:43:11 */             else
/* @LiF  - 2024-06-02 02:43:11 */             {
/* @LiF  - 2024-06-02 02:43:11 */                 element.FadeOut(duration, () => element.style.display = DisplayStyle.None);
/* @LiF  - 2024-06-02 02:43:11 */             }
/* @LiF  - 2024-05-29 11:33:09 */         }
/* @LiF  - 2024-06-02 02:43:11 */ 
/* @LiF  - 2024-05-29 11:33:09 */         public static void SetEnabledRecursive(this VisualElement element, bool enabled)
/* @LiF  - 2024-05-29 11:33:09 */         {
/* @LiF  - 2024-05-29 11:33:09 */             element.SetEnabled(enabled);
/* @LiF  - 2024-05-29 11:33:09 */ 
/* @LiF  - 2024-05-29 11:33:09 */             foreach (var child in element.Children())
/* @LiF  - 2024-05-29 11:33:09 */             {
/* @LiF  - 2024-05-29 11:33:09 */                 child.SetEnabledRecursive(enabled);
/* @LiF  - 2024-05-29 11:33:09 */             }
/* @LiF  - 2024-05-29 11:33:09 */         }
/* @LiF  - 2024-06-02 02:43:11 */ 
/* @LiF  - 2024-06-02 02:43:11 */         private static void FadeIn(this VisualElement element, float duration)
/* @LiF  - 2024-06-02 02:43:11 */         {
/* @LiF  - 2024-06-02 02:43:11 */             element.style.opacity = 0;
/* @LiF  - 2024-06-02 02:43:11 */             DOTween.To(() => element.resolvedStyle.opacity, x => element.style.opacity = x, 1, duration).SetEase(Ease.Linear);
/* @LiF  - 2024-06-02 02:43:11 */         }
/* @LiF  - 2024-06-02 02:43:11 */ 
/* @LiF  - 2024-06-02 02:43:11 */         private static void FadeOut(this VisualElement element, float duration, System.Action onCompleted = null)
/* @LiF  - 2024-06-02 02:43:11 */         {
/* @LiF  - 2024-06-02 02:43:11 */             element.style.opacity = 1;
/* @LiF  - 2024-06-02 02:43:11 */             DOTween.To(() => element.resolvedStyle.opacity, x => element.style.opacity = x, 0, duration)
/* @LiF  - 2024-06-02 02:43:11 */                 .SetEase(Ease.Linear)
/* @LiF  - 2024-06-02 02:43:11 */                 .OnComplete(() => onCompleted?.Invoke());
/* @LiF  - 2024-06-02 02:43:11 */         }
/* @LiF  - 2024-05-29 11:33:09 */     }
/* @LiF  - 2024-05-29 11:33:09 */ }