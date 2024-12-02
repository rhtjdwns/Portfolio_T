/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-03-21 17:35:17 */ using UnityEngine;
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */ namespace FullMoon.Util
/* @Lee SJ  - 2024-03-21 17:35:17 */ {
/* @Lee SJ  - 2024-03-21 17:35:17 */     public abstract class ComponentSingleton<T> : MonoBehaviour where T : ComponentSingleton<T>
/* @Lee SJ  - 2024-03-21 17:35:17 */     {
/* @Lee SJ  - 2024-03-21 17:47:11 */         private static T _instance;
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:47:11 */         public static bool Exists => _instance != null;
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */         public static T Instance
/* @Lee SJ  - 2024-03-21 17:35:17 */         {
/* @Lee SJ  - 2024-03-21 17:35:17 */             get
/* @Lee SJ  - 2024-03-21 17:35:17 */             {
/* @Lee SJ  - 2024-03-21 17:47:11 */                 if (_instance == null)
/* @Lee SJ  - 2024-03-21 17:35:17 */                 {
/* @Lee SJ  - 2024-03-21 17:47:11 */                     _instance = FindOrCreateInstance();
/* @Lee SJ  - 2024-03-21 17:35:17 */                 }
/* @Lee SJ  - 2024-03-21 17:47:11 */                 return _instance;
/* @Lee SJ  - 2024-03-21 17:35:17 */             }
/* @Lee SJ  - 2024-03-21 17:35:17 */         }
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */         private static T FindOrCreateInstance()
/* @Lee SJ  - 2024-03-21 17:35:17 */         {
/* @Lee SJ  - 2024-03-21 17:35:17 */             T existingInstance = FindObjectOfType<T>();
/* @Lee SJ  - 2024-03-21 17:35:17 */             if (existingInstance != null)
/* @Lee SJ  - 2024-03-21 17:35:17 */             {
/* @Lee SJ  - 2024-03-21 17:35:17 */                 return existingInstance;
/* @Lee SJ  - 2024-03-21 17:35:17 */             }
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */             return CreateNewSingleton();
/* @Lee SJ  - 2024-03-21 17:35:17 */         }
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */         protected virtual string GetGameObjectName() => typeof(T).Name;
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */         private static T CreateNewSingleton()
/* @Lee SJ  - 2024-03-21 17:35:17 */         {
/* @Lee SJ  - 2024-05-08 18:25:40 */             GameObject singletonObject = new GameObject
/* @Lee SJ  - 2024-03-21 17:35:17 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 name = typeof(T).Name + " (Singleton)"
/* @Lee SJ  - 2024-05-08 18:25:40 */             };
/* @Lee SJ  - 2024-05-08 18:25:40 */             
/* @LiF     - 2024-05-16 02:58:26 */             if (Application.isPlaying)
/* @LiF     - 2024-05-16 02:58:26 */             {
/* @LiF     - 2024-05-16 02:58:26 */                 DontDestroyOnLoad(singletonObject);
/* @LiF     - 2024-05-16 02:58:26 */             }
/* @Lee SJ  - 2024-05-08 18:25:40 */             
/* @Lee SJ  - 2024-03-21 17:35:17 */             return singletonObject.AddComponent<T>();
/* @Lee SJ  - 2024-03-21 17:35:17 */         }
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */         protected virtual void Awake()
/* @Lee SJ  - 2024-03-21 17:35:17 */         {
/* @Lee SJ  - 2024-03-21 17:47:11 */             if (_instance != null && _instance != this)
/* @Lee SJ  - 2024-03-21 17:35:17 */             {
/* @Lee SJ  - 2024-03-21 17:35:17 */                 DestroyImmediate(gameObject);
/* @Lee SJ  - 2024-03-21 17:35:17 */             }
/* @Lee SJ  - 2024-03-21 17:35:17 */             else
/* @Lee SJ  - 2024-03-21 17:35:17 */             {
/* @Lee SJ  - 2024-03-21 17:47:11 */                 _instance = this as T;
/* @Lee SJ  - 2024-03-21 17:35:17 */             }
/* @Lee SJ  - 2024-03-21 17:35:17 */         }
/* @Lee SJ  - 2024-03-21 17:35:17 */ 
/* @Lee SJ  - 2024-03-21 17:35:17 */         public static void DestroySingleton()
/* @Lee SJ  - 2024-03-21 17:35:17 */         {
/* @Lee SJ  - 2024-03-21 17:35:17 */             if (Exists)
/* @Lee SJ  - 2024-03-21 17:35:17 */             {
/* @Lee SJ  - 2024-03-21 17:47:11 */                 DestroyImmediate(_instance.gameObject);
/* @Lee SJ  - 2024-03-21 17:47:11 */                 _instance = null;
/* @Lee SJ  - 2024-03-21 17:35:17 */             }
/* @Lee SJ  - 2024-03-21 17:35:17 */         }
/* @Lee SJ  - 2024-03-21 17:35:17 */     }
/* @Lee SJ  - 2024-03-21 17:35:17 */ }