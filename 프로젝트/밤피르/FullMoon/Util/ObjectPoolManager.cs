/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-03-21 17:47:11 */ using System.Collections.Generic;
/* @Lee SJ    - 2024-05-08 21:01:50 */ using System.Linq;
/* @rhtjdwns  - 2024-05-07 22:36:45 */ using System.Text.RegularExpressions;
/* @Lee SJ    - 2024-05-08 18:25:40 */ using UnityEngine;
/* @rhtjdwns  - 2024-05-11 15:20:14 */ using Cysharp.Threading.Tasks;
/* @rhtjdwns  - 2024-05-11 15:32:31 */ using System;
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */ namespace FullMoon.Util
/* @Lee SJ    - 2024-03-21 17:47:11 */ {
/* @Lee SJ    - 2024-05-08 18:25:40 */     public class ObjectPoolManager : ComponentSingleton<ObjectPoolManager>
/* @Lee SJ    - 2024-03-21 17:47:11 */     {
/* @Lee SJ    - 2024-05-08 18:25:40 */         private readonly Dictionary<string, PooledObjectInfo> objectPools = new();
/* @Lee SJ    - 2024-05-08 18:25:40 */         
/* @Lee SJ    - 2024-05-08 18:25:40 */         public GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
/* @Lee SJ    - 2024-03-21 17:47:11 */         {
/* @rhtjdwns  - 2024-05-07 22:49:41 */             string goName = NameReplace(objectToSpawn);
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */             if (!objectPools.TryGetValue(goName, out PooledObjectInfo pool))
/* @Lee SJ    - 2024-03-21 17:47:11 */             {
/* @Lee SJ    - 2024-05-08 18:25:40 */                 pool = new PooledObjectInfo();
/* @Lee SJ    - 2024-05-08 18:25:40 */                 objectPools.Add(goName, pool);
/* @Lee SJ    - 2024-03-21 17:47:11 */             }
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */             GameObject spawnableObj = pool.GetInactiveObject();
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */             if (spawnableObj == null)
/* @Lee SJ    - 2024-03-21 17:47:11 */             {
/* @Lee SJ    - 2024-03-21 17:47:11 */                 spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
/* @Lee SJ    - 2024-03-21 17:47:11 */             }
/* @Lee SJ    - 2024-03-21 17:47:11 */             else
/* @Lee SJ    - 2024-03-21 17:47:11 */             {
/* @Lee SJ    - 2024-03-21 17:47:11 */                 spawnableObj.transform.position = spawnPosition;
/* @Lee SJ    - 2024-03-21 17:47:11 */                 spawnableObj.transform.rotation = spawnRotation;
/* @Lee SJ    - 2024-03-21 17:47:11 */                 spawnableObj.SetActive(true);
/* @Lee SJ    - 2024-03-21 17:47:11 */             }
/* @Lee SJ    - 2024-03-21 17:47:11 */             return spawnableObj;
/* @Lee SJ    - 2024-03-21 17:47:11 */         }
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */         public void ReturnObjectToPool(GameObject obj)
/* @Lee SJ    - 2024-03-21 17:47:11 */         {
/* @rhtjdwns  - 2024-05-07 22:49:41 */             string goName = NameReplace(obj);
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */             if (!objectPools.TryGetValue(goName, out PooledObjectInfo pool))
/* @Lee SJ    - 2024-03-21 17:47:11 */             {
/* @Lee SJ    - 2024-05-08 18:25:40 */                 pool = new PooledObjectInfo();
/* @Lee SJ    - 2024-05-08 18:25:40 */                 objectPools[goName] = pool;
/* @Lee SJ    - 2024-03-21 17:47:11 */             }
/* @Lee SJ    - 2024-05-08 18:25:40 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */             pool.ReturnObject(obj);
/* @Lee SJ    - 2024-03-21 17:47:11 */         }
/* @rhtjdwns  - 2024-05-07 22:36:45 */ 
/* @rhtjdwns  - 2024-05-11 15:32:31 */         public async UniTask ReturnObjectToPool(GameObject obj, float time)
/* @rhtjdwns  - 2024-05-11 15:20:14 */         {
/* @rhtjdwns  - 2024-05-11 15:20:14 */             string goName = NameReplace(obj);
/* @rhtjdwns  - 2024-05-11 15:20:14 */ 
/* @rhtjdwns  - 2024-05-11 15:20:14 */             if (!objectPools.TryGetValue(goName, out PooledObjectInfo pool))
/* @rhtjdwns  - 2024-05-11 15:20:14 */             {
/* @rhtjdwns  - 2024-05-11 15:20:14 */                 pool = new PooledObjectInfo();
/* @rhtjdwns  - 2024-05-11 15:20:14 */                 objectPools[goName] = pool;
/* @rhtjdwns  - 2024-05-11 15:20:14 */             }
/* @rhtjdwns  - 2024-05-11 15:20:14 */ 
/* @rhtjdwns  - 2024-05-11 15:32:31 */             await UniTask.Delay(TimeSpan.FromSeconds(time));
/* @rhtjdwns  - 2024-05-11 15:20:14 */ 
/* @rhtjdwns  - 2024-05-11 15:20:14 */             pool.ReturnObject(obj);
/* @rhtjdwns  - 2024-05-11 15:20:14 */         }
/* @rhtjdwns  - 2024-05-11 15:20:14 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */         private string NameReplace(GameObject obj)
/* @rhtjdwns  - 2024-05-07 22:36:45 */         {
/* @rhtjdwns  - 2024-05-07 22:36:45 */             string originalString = obj.name;
/* @Lee SJ    - 2024-05-08 18:25:40 */             return Regex.Replace(originalString, @" (\d+)|\(Clone\)", "");
/* @rhtjdwns  - 2024-05-07 22:36:45 */         }
/* @Lee SJ    - 2024-03-21 17:47:11 */     }
/* @Lee SJ    - 2024-03-21 17:47:11 */ 
/* @Lee SJ    - 2024-03-21 17:47:11 */     public class PooledObjectInfo
/* @Lee SJ    - 2024-03-21 17:47:11 */     {
/* @Lee SJ    - 2024-05-08 21:01:50 */         private readonly HashSet<GameObject> inactiveObject = new();
/* @Lee SJ    - 2024-05-08 18:25:40 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */         public GameObject GetInactiveObject()
/* @Lee SJ    - 2024-05-08 18:25:40 */         {
/* @Lee SJ    - 2024-05-08 18:25:40 */             if (inactiveObject.Count > 0)
/* @Lee SJ    - 2024-05-08 18:25:40 */             {
/* @Lee SJ    - 2024-05-08 21:01:50 */                 GameObject obj = inactiveObject.First();
/* @Lee SJ    - 2024-05-08 21:01:50 */                 inactiveObject.Remove(obj);
/* @Lee SJ    - 2024-05-08 18:25:40 */                 return obj;
/* @Lee SJ    - 2024-05-08 18:25:40 */             }
/* @Lee SJ    - 2024-05-08 18:25:40 */             return null;
/* @Lee SJ    - 2024-05-08 18:25:40 */         }
/* @Lee SJ    - 2024-05-08 18:25:40 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */         public void ReturnObject(GameObject obj)
/* @Lee SJ    - 2024-05-08 18:25:40 */         {
/* @Lee SJ    - 2024-05-08 18:25:40 */             inactiveObject.Add(obj);
/* @Lee SJ    - 2024-05-08 18:25:40 */             obj.SetActive(false);
/* @Lee SJ    - 2024-05-08 18:25:40 */         }
/* @Lee SJ    - 2024-03-21 17:47:11 */     }
/* @Lee SJ    - 2024-05-08 18:25:40 */ }
