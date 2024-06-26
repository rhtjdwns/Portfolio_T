/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-03-27 22:58:51 */ using MyBox;
/* @Lee SJ  - 2024-03-27 22:58:51 */ using UnityEngine;
/* @Lee SJ  - 2024-03-27 22:58:51 */ using UnityEngine.Events;
/* @Lee SJ  - 2024-03-27 22:58:51 */ using System.Collections.Generic;
/* @Lee SJ  - 2024-05-08 21:01:50 */ using Cysharp.Threading.Tasks;
/* @Lee SJ  - 2024-03-27 22:58:51 */ 
/* @Lee SJ  - 2024-03-27 22:58:51 */ namespace FullMoon.Util
/* @Lee SJ  - 2024-03-27 22:58:51 */ {
/* @Lee SJ  - 2024-03-27 22:58:51 */     public class ColliderTriggerEvents : MonoBehaviour
/* @Lee SJ  - 2024-03-27 22:58:51 */     {
/* @Lee SJ  - 2024-03-27 22:58:51 */         [SerializeField, Tag] private List<string> filterTags;
/* @Lee SJ  - 2024-03-27 22:58:51 */         [SerializeField, Space(10)] private bool executeEnterAfterFrame;
/* @Lee SJ  - 2024-03-27 22:58:51 */         [SerializeField] private UnityEvent<Collider> onEnterEvent;
/* @Lee SJ  - 2024-03-27 22:58:51 */         [SerializeField, Space(10)] private bool executeExitAfterFrame;
/* @Lee SJ  - 2024-03-27 22:58:51 */         [SerializeField] private UnityEvent<Collider> onExitEvent;
/* @Lee SJ  - 2024-03-27 22:58:51 */ 
/* @Lee SJ  - 2024-05-08 21:01:50 */         public List<string> GetFilterTags => filterTags;
/* @Lee SJ  - 2024-05-08 21:01:50 */         
/* @Lee SJ  - 2024-05-08 21:01:50 */         private async void OnTriggerEnter(Collider other)
/* @Lee SJ  - 2024-03-27 22:58:51 */         {
/* @Lee SJ  - 2024-03-27 22:58:51 */             bool filterResult = false;
/* @Lee SJ  - 2024-05-08 21:01:50 */             foreach (var filterTag in filterTags)
/* @Lee SJ  - 2024-03-27 22:58:51 */             {
/* @Lee SJ  - 2024-05-08 21:01:50 */                 if (other.CompareTag(filterTag))
/* @Lee SJ  - 2024-03-27 22:58:51 */                 {
/* @Lee SJ  - 2024-03-27 22:58:51 */                     filterResult = true;
/* @Lee SJ  - 2024-03-27 22:58:51 */                 }
/* @Lee SJ  - 2024-03-27 22:58:51 */             }
/* @Lee SJ  - 2024-03-27 22:58:51 */ 
/* @Lee SJ  - 2024-03-27 22:58:51 */             if (filterResult == false)
/* @Lee SJ  - 2024-03-27 22:58:51 */             {
/* @Lee SJ  - 2024-03-27 22:58:51 */                 return;
/* @Lee SJ  - 2024-03-27 22:58:51 */             }
/* @Lee SJ  - 2024-03-27 22:58:51 */         
/* @Lee SJ  - 2024-03-27 22:58:51 */             if (executeEnterAfterFrame)
/* @Lee SJ  - 2024-03-27 22:58:51 */             {
/* @Lee SJ  - 2024-05-08 21:01:50 */                 await UniTask.NextFrame();
/* @Lee SJ  - 2024-05-08 21:01:50 */                 onEnterEvent?.Invoke(other);
/* @Lee SJ  - 2024-03-27 22:58:51 */             }
/* @Lee SJ  - 2024-03-27 22:58:51 */         
/* @Lee SJ  - 2024-03-27 22:58:51 */             onEnterEvent?.Invoke(other);
/* @Lee SJ  - 2024-03-27 22:58:51 */         }
/* @Lee SJ  - 2024-03-27 22:58:51 */ 
/* @Lee SJ  - 2024-05-08 21:01:50 */         private async void OnTriggerExit(Collider other)
/* @Lee SJ  - 2024-03-27 22:58:51 */         {
/* @Lee SJ  - 2024-03-27 22:58:51 */             bool filterResult = false;
/* @Lee SJ  - 2024-05-08 21:01:50 */             foreach (var filterTag in filterTags)
/* @Lee SJ  - 2024-03-27 22:58:51 */             {
/* @Lee SJ  - 2024-05-08 21:01:50 */                 if (other.CompareTag(filterTag))
/* @Lee SJ  - 2024-03-27 22:58:51 */                 {
/* @Lee SJ  - 2024-03-27 22:58:51 */                     filterResult = true;
/* @Lee SJ  - 2024-03-27 22:58:51 */                 }
/* @Lee SJ  - 2024-03-27 22:58:51 */             }
/* @Lee SJ  - 2024-03-27 22:58:51 */ 
/* @Lee SJ  - 2024-03-27 22:58:51 */             if (filterResult == false)
/* @Lee SJ  - 2024-03-27 22:58:51 */             {
/* @Lee SJ  - 2024-03-27 22:58:51 */                 return;
/* @Lee SJ  - 2024-03-27 22:58:51 */             }
/* @Lee SJ  - 2024-03-27 22:58:51 */         
/* @Lee SJ  - 2024-03-27 22:58:51 */             if (executeExitAfterFrame)
/* @Lee SJ  - 2024-03-27 22:58:51 */             {
/* @Lee SJ  - 2024-05-08 21:01:50 */                 await UniTask.NextFrame();
/* @Lee SJ  - 2024-05-08 21:01:50 */                 onEnterEvent?.Invoke(other);
/* @Lee SJ  - 2024-03-27 22:58:51 */                 return;
/* @Lee SJ  - 2024-03-27 22:58:51 */             }
/* @Lee SJ  - 2024-03-27 22:58:51 */ 
/* @Lee SJ  - 2024-03-27 22:58:51 */             onExitEvent?.Invoke(other);
/* @Lee SJ  - 2024-03-27 22:58:51 */         }
/* @Lee SJ  - 2024-03-27 22:58:51 */     }
/* @Lee SJ  - 2024-03-27 22:58:51 */ }