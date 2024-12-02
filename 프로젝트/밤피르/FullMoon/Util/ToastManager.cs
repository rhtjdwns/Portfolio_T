/* Git Blame Auto Generated */

/* @LiF  - 2024-06-01 20:28:02 */ using TMPro;
/* @LiF  - 2024-06-01 20:28:02 */ using UnityEngine;
/* @LiF  - 2024-06-01 20:28:02 */ using DG.Tweening;
/* @LiF  - 2024-06-01 20:28:02 */ using System.Collections.Generic;
/* @LiF  - 2024-06-01 20:28:02 */ 
/* @LiF  - 2024-06-01 20:28:02 */ namespace FullMoon.Util
/* @LiF  - 2024-06-01 20:28:02 */ {
/* @LiF  - 2024-06-01 20:28:02 */     public class ToastManager : ComponentSingleton<ToastManager>
/* @LiF  - 2024-06-01 20:28:02 */     {
/* @LiF  - 2024-06-01 20:28:02 */         public GameObject toastPrefab;
/* @LiF  - 2024-06-01 20:28:02 */         public Transform toastContainer;
/* @LiF  - 2024-06-01 20:28:02 */         public float displayDuration = 2.0f;
/* @LiF  - 2024-06-01 20:28:02 */         public float fadeDuration = 0.5f;
/* @LiF  - 2024-06-01 20:28:02 */ 
/* @LiF  - 2024-06-01 20:28:02 */         private Queue<GameObject> toasts = new();
/* @LiF  - 2024-06-01 20:28:02 */ 
/* @LiF  - 2024-06-01 20:29:46 */         public void ShowToast(string message, string color = "white")
/* @LiF  - 2024-06-01 20:28:02 */         {
/* @LiF  - 2024-06-01 20:28:02 */             GameObject toastInstance = Instantiate(toastPrefab, toastContainer);
/* @LiF  - 2024-06-01 20:28:02 */             toastInstance.transform.SetSiblingIndex(0);
/* @LiF  - 2024-06-01 20:28:02 */             TextMeshProUGUI toastText = toastInstance.GetComponent<TextMeshProUGUI>();
/* @LiF  - 2024-06-01 20:44:18 */             toastText.raycastTarget = false;
/* @LiF  - 2024-06-01 20:29:46 */             toastText.text = $"<color={color}>{message}</color>";
/* @LiF  - 2024-06-01 20:28:02 */         
/* @LiF  - 2024-06-01 20:28:02 */             toasts.Enqueue(toastInstance);
/* @LiF  - 2024-06-01 20:28:02 */             if (toasts.Count > 5)
/* @LiF  - 2024-06-01 20:28:02 */             {
/* @LiF  - 2024-06-01 20:28:02 */                 GameObject oldToast = toasts.Dequeue();
/* @LiF  - 2024-06-01 20:28:02 */                 Destroy(oldToast);
/* @LiF  - 2024-06-01 20:28:02 */             }
/* @LiF  - 2024-06-01 20:28:02 */ 
/* @LiF  - 2024-06-01 20:28:02 */             CanvasGroup canvasGroup = toastInstance.GetComponent<CanvasGroup>();
/* @LiF  - 2024-06-01 20:28:02 */             if (canvasGroup == null)
/* @LiF  - 2024-06-01 20:28:02 */             {
/* @LiF  - 2024-06-01 20:28:02 */                 canvasGroup = toastInstance.AddComponent<CanvasGroup>();
/* @LiF  - 2024-06-01 20:28:02 */             }
/* @LiF  - 2024-06-01 20:28:02 */ 
/* @LiF  - 2024-06-01 20:28:02 */             Sequence sequence = DOTween.Sequence();
/* @LiF  - 2024-06-01 20:28:02 */             sequence.Append(canvasGroup.DOFade(1, fadeDuration))
/* @LiF  - 2024-06-01 20:28:02 */                 .AppendInterval(displayDuration)
/* @LiF  - 2024-06-01 20:28:02 */                 .Append(canvasGroup.DOFade(0, fadeDuration))
/* @LiF  - 2024-06-01 20:28:02 */                 .OnComplete(() => Destroy(toastInstance));
/* @LiF  - 2024-06-01 20:28:02 */         }
/* @LiF  - 2024-06-01 20:28:02 */     }
/* @LiF  - 2024-06-01 20:28:02 */ }
