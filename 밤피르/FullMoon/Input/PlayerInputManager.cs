/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-03-21 17:47:11 */ using System;
/* @Lee SJ    - 2024-03-21 17:47:11 */ using UnityEngine;
/* @Lee SJ    - 2024-03-21 17:47:11 */ using FullMoon.Util;
/* @rhtjdwns  - 2024-04-18 09:55:38 */ using MyBox;
/* @Lee SJ    - 2024-04-18 17:50:09 */ using UnityEngine.Serialization;
/* @rhtjdwns  - 2024-04-18 09:55:38 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */ #if ENABLE_INPUT_SYSTEM
/* @Lee SJ    - 2024-03-21 17:47:11 */ using UnityEngine.InputSystem;
/* @Lee SJ    - 2024-03-21 17:47:11 */ #endif
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */ namespace FullMoon.Input
/* @Lee SJ    - 2024-03-21 17:47:11 */ {
/* @Lee SJ    - 2024-03-21 17:47:11 */     [Serializable]
/* @rhtjdwns  - 2024-04-18 09:55:38 */     public enum CursorLockType
/* @Lee SJ    - 2024-03-21 17:47:11 */     {
/* @Lee SJ    - 2024-03-21 17:47:11 */         None,
/* @Lee SJ    - 2024-03-21 17:47:11 */         Locked,
/* @rhtjdwns  - 2024-04-18 09:55:38 */         Confined,
/* @rhtjdwns  - 2024-04-18 09:55:38 */     }
/* @rhtjdwns  - 2024-04-18 09:55:38 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */     [RequireComponent(typeof(PlayerInput))]
/* @Lee SJ    - 2024-03-21 17:47:11 */     public class PlayerInputManager : ComponentSingleton<PlayerInputManager>
/* @Lee SJ    - 2024-03-21 17:47:11 */     {
/* @Lee SJ    - 2024-03-21 17:47:11 */         [Header("Camera Input Values")]
/* @Lee SJ    - 2024-03-21 17:47:11 */         public Vector2 move;
/* @Lee SJ    - 2024-03-21 17:47:11 */         public bool analogMovement;
/* @Lee SJ    - 2024-03-21 22:29:59 */         public bool shift;
/* @Lee SJ    - 2024-03-21 22:29:59 */         public Vector2 zoom;
/* @Lee SJ    - 2024-04-09 01:45:23 */         public bool rotation;
/* @rhtjdwns  - 2024-04-20 13:13:17 */         public bool cancel;
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @rhtjdwns  - 2024-04-18 09:55:38 */         [Header("Mouse Cursor Lock Settings")] 
/* @rhtjdwns  - 2024-04-18 09:55:38 */         public CursorLockType cursorLockType;
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */ #if ENABLE_INPUT_SYSTEM
/* @Lee SJ    - 2024-03-21 17:47:11 */         public void OnMove(InputValue value)
/* @Lee SJ    - 2024-03-21 17:47:11 */         {
/* @Lee SJ    - 2024-03-21 17:47:11 */             MoveInput(value.Get<Vector2>());
/* @Lee SJ    - 2024-03-21 17:47:11 */         }
/* @Lee SJ    - 2024-03-21 22:29:59 */         
/* @Lee SJ    - 2024-03-21 22:29:59 */         public void OnZoom(InputValue value)
/* @Lee SJ    - 2024-03-21 22:29:59 */         {
/* @Lee SJ    - 2024-03-21 22:29:59 */             ZoomInput(value.Get<Vector2>());
/* @Lee SJ    - 2024-03-21 22:29:59 */         }
/* @rhtjdwns  - 2024-04-08 11:10:43 */ 
/* @Lee SJ    - 2024-04-09 01:06:08 */         public void OnRotation(InputValue value)
/* @Lee SJ    - 2024-04-09 01:06:08 */         {
/* @Lee SJ    - 2024-04-09 01:45:23 */             RotationInput(value.isPressed);
/* @Lee SJ    - 2024-04-09 01:06:08 */         }
/* @rhtjdwns  - 2024-04-20 12:50:25 */ 
/* @rhtjdwns  - 2024-04-20 13:13:17 */         public void OnCancel(InputValue value)
/* @rhtjdwns  - 2024-04-20 12:50:25 */         {
/* @rhtjdwns  - 2024-04-20 13:13:17 */             CancelInput(value.isPressed);
/* @rhtjdwns  - 2024-04-20 12:50:25 */         }
/* @Lee SJ    - 2024-03-21 17:47:11 */ #endif
/* @Lee SJ    - 2024-03-21 17:47:11 */ 		
/* @rhtjdwns  - 2024-04-08 20:24:40 */         public readonly GenericEventSystem<Vector2> MoveEvent = new();
/* @Lee SJ    - 2024-03-21 22:29:59 */         public void MoveInput(Vector2 input)
/* @Lee SJ    - 2024-03-21 17:47:11 */         {
/* @Lee SJ    - 2024-03-21 22:29:59 */             move = input;
/* @rhtjdwns  - 2024-04-08 20:24:40 */             MoveEvent.TriggerEvent(input);
/* @Lee SJ    - 2024-03-21 17:47:11 */         } 
/* @Lee SJ    - 2024-03-21 17:47:11 */         
/* @Lee SJ    - 2024-03-21 22:29:59 */         public readonly GenericEventSystem<Vector2> ZoomEvent = new();
/* @Lee SJ    - 2024-03-21 22:29:59 */         public void ZoomInput(Vector2 input)
/* @Lee SJ    - 2024-03-21 22:29:59 */         {
/* @Lee SJ    - 2024-03-21 22:29:59 */             zoom = input;
/* @Lee SJ    - 2024-03-21 22:29:59 */             ZoomEvent.TriggerEvent(input);
/* @Lee SJ    - 2024-03-21 22:29:59 */         }
/* @Lee SJ    - 2024-03-21 22:29:59 */         
/* @Lee SJ    - 2024-04-09 01:45:23 */         public readonly GenericEventSystem<bool> RotationEvent = new();
/* @Lee SJ    - 2024-04-09 01:45:23 */         public void RotationInput(bool input)
/* @Lee SJ    - 2024-04-09 01:06:08 */         {
/* @Lee SJ    - 2024-04-09 01:06:08 */             rotation = input;
/* @Lee SJ    - 2024-04-09 01:06:08 */             RotationEvent.TriggerEvent(input);
/* @Lee SJ    - 2024-04-09 01:06:08 */         } 
/* @rhtjdwns  - 2024-04-20 12:50:25 */ 
/* @rhtjdwns  - 2024-04-20 13:13:17 */         public readonly GenericEventSystem<bool> CancelEvent = new();
/* @rhtjdwns  - 2024-04-20 13:13:17 */         public void CancelInput(bool input)
/* @rhtjdwns  - 2024-04-20 12:50:25 */         {
/* @rhtjdwns  - 2024-04-20 13:13:17 */             cancel = input;
/* @rhtjdwns  - 2024-04-20 13:13:17 */             CancelEvent.TriggerEvent(input);
/* @rhtjdwns  - 2024-04-20 12:50:25 */         }
/* @rhtjdwns  - 2024-04-24 20:44:13 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */         private void OnApplicationFocus(bool hasFocus)
/* @Lee SJ    - 2024-03-21 17:47:11 */         {
/* @rhtjdwns  - 2024-04-18 09:55:38 */             SetCursorLockState(cursorLockType);
/* @Lee SJ    - 2024-03-21 17:47:11 */         }
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @rhtjdwns  - 2024-04-18 09:55:38 */         private void SetCursorLockState(CursorLockType type)
/* @Lee SJ    - 2024-03-21 17:47:11 */         {
/* @Lee SJ    - 2024-03-21 17:47:11 */             switch (type)
/* @Lee SJ    - 2024-03-21 17:47:11 */             {
/* @rhtjdwns  - 2024-04-18 09:55:38 */                 case CursorLockType.None:
/* @Lee SJ    - 2024-03-21 17:47:11 */                     Cursor.lockState = CursorLockMode.None;
/* @Lee SJ    - 2024-03-21 17:47:11 */                     break;
/* @rhtjdwns  - 2024-04-18 09:55:38 */                 case CursorLockType.Locked:
/* @Lee SJ    - 2024-03-21 17:47:11 */                     Cursor.lockState = CursorLockMode.Locked;
/* @Lee SJ    - 2024-03-21 17:47:11 */                     break;
/* @rhtjdwns  - 2024-04-18 09:55:38 */                 case CursorLockType.Confined:
/* @Lee SJ    - 2024-03-21 17:47:11 */                     Cursor.lockState = CursorLockMode.Confined;
/* @Lee SJ    - 2024-03-21 17:47:11 */                     break;
/* @Lee SJ    - 2024-03-21 17:47:11 */             }
/* @Lee SJ    - 2024-03-21 17:47:11 */         }
/* @Lee SJ    - 2024-03-21 17:47:11 */     }
/* @Lee SJ    - 2024-03-21 17:47:11 */ }