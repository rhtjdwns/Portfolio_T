/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-06 22:26:59 */ using System;
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */ namespace FullMoon.Util
/* @Lee SJ  - 2024-05-06 22:26:59 */ {
/* @Lee SJ  - 2024-05-06 22:26:59 */     public class SimpleEventSystem
/* @Lee SJ  - 2024-05-06 22:26:59 */     {
/* @Lee SJ  - 2024-05-06 22:26:59 */         private event Action OnEventTriggered;
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void TriggerEvent()
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-06 22:26:59 */             OnEventTriggered?.Invoke();
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void AddEvent(Action newEvent)
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-06 22:26:59 */             OnEventTriggered += newEvent;
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void RemoveEvent(Action existingEvent)
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-06 22:26:59 */             OnEventTriggered -= existingEvent;
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */     }
/* @Lee SJ  - 2024-05-06 22:26:59 */     
/* @Lee SJ  - 2024-05-06 22:26:59 */     public class GenericEventSystem<T>
/* @Lee SJ  - 2024-05-06 22:26:59 */     {
/* @Lee SJ  - 2024-05-06 22:26:59 */         private event Action<T> OnEventTriggered;
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void TriggerEvent(T item)
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-06 22:26:59 */             OnEventTriggered?.Invoke(item);
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void AddEvent(Action<T> newEvent)
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-06 22:26:59 */             OnEventTriggered += newEvent;
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */ 
/* @Lee SJ  - 2024-05-06 22:26:59 */         public void RemoveEvent(Action<T> existingEvent)
/* @Lee SJ  - 2024-05-06 22:26:59 */         {
/* @Lee SJ  - 2024-05-06 22:26:59 */             OnEventTriggered -= existingEvent;
/* @Lee SJ  - 2024-05-06 22:26:59 */         }
/* @Lee SJ  - 2024-05-06 22:26:59 */     }
/* @Lee SJ  - 2024-05-06 22:26:59 */ }