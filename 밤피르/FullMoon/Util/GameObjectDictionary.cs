/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-30 21:35:08 */ using System;
/* @Lee SJ  - 2024-05-30 21:35:08 */ using System.Collections.Generic;
/* @Lee SJ  - 2024-05-30 21:35:08 */ using UnityEngine;
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */ namespace FullMoon.Util
/* @Lee SJ  - 2024-05-30 21:35:08 */ {
/* @Lee SJ  - 2024-05-30 21:35:08 */     [Serializable]
/* @Lee SJ  - 2024-05-30 21:35:08 */     struct ObjectDictionaryItem
/* @Lee SJ  - 2024-05-30 21:35:08 */     {
/* @Lee SJ  - 2024-05-30 21:35:08 */         public string name;
/* @Lee SJ  - 2024-05-30 21:35:08 */         public GameObject gameObject;
/* @Lee SJ  - 2024-05-30 21:35:08 */     }
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */     public class GameObjectDictionary : MonoBehaviour
/* @Lee SJ  - 2024-05-30 21:35:08 */     {
/* @Lee SJ  - 2024-05-30 21:35:08 */         [SerializeField] private List<ObjectDictionaryItem> items;
/* @Lee SJ  - 2024-05-30 21:35:08 */ 
/* @Lee SJ  - 2024-05-30 21:35:08 */         public GameObject GetGameObjectByName(string objectName)
/* @Lee SJ  - 2024-05-30 21:35:08 */         {
/* @Lee SJ  - 2024-05-30 21:35:08 */             foreach (var item in items)
/* @Lee SJ  - 2024-05-30 21:35:08 */             {
/* @Lee SJ  - 2024-05-30 21:35:08 */                 if (item.name == objectName)
/* @Lee SJ  - 2024-05-30 21:35:08 */                 {
/* @Lee SJ  - 2024-05-30 21:35:08 */                     return item.gameObject;
/* @Lee SJ  - 2024-05-30 21:35:08 */                 }
/* @Lee SJ  - 2024-05-30 21:35:08 */             }
/* @Lee SJ  - 2024-05-30 21:35:08 */             
/* @Lee SJ  - 2024-05-30 21:35:08 */             Debug.LogWarning($"Object with name {objectName} not found.");
/* @Lee SJ  - 2024-05-30 21:35:08 */             return null;
/* @Lee SJ  - 2024-05-30 21:35:08 */         }
/* @LiF     - 2024-06-02 11:54:10 */         
/* @LiF     - 2024-06-02 11:54:10 */         public T GetComponentByName<T>(string objectName) where T : Component
/* @LiF     - 2024-06-02 11:54:10 */         {
/* @LiF     - 2024-06-02 11:54:10 */             var requestObject = GetGameObjectByName(objectName);
/* @LiF     - 2024-06-02 11:54:10 */             if (requestObject != null)
/* @LiF     - 2024-06-02 11:54:10 */             {
/* @LiF     - 2024-06-02 11:54:10 */                 T component = requestObject.GetComponent<T>();
/* @LiF     - 2024-06-02 11:54:10 */                 if (component != null)
/* @LiF     - 2024-06-02 11:54:10 */                 {
/* @LiF     - 2024-06-02 11:54:10 */                     return component;
/* @LiF     - 2024-06-02 11:54:10 */                 }
/* @LiF     - 2024-06-02 11:54:10 */ 
/* @LiF     - 2024-06-02 11:54:10 */                 Debug.LogWarning($"Component of type {typeof(T)} not found on object with name {objectName}.");
/* @LiF     - 2024-06-02 11:54:10 */             }
/* @LiF     - 2024-06-02 11:54:10 */             return null;
/* @LiF     - 2024-06-02 11:54:10 */         }
/* @Lee SJ  - 2024-05-30 21:35:08 */     }
/* @Lee SJ  - 2024-05-30 21:35:08 */ }