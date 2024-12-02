/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-05-30 11:28:19 */ using MyBox;
/* @LiF       - 2024-06-01 15:37:58 */ using System;
/* @LiF       - 2024-06-01 15:37:58 */ using Cysharp.Threading.Tasks;
/* @Lee SJ    - 2024-06-02 20:34:51 */ using FullMoon.Entities.Unit;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ using Unity.Burst;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ using UnityEngine;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.Util;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.ScriptableObject;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */ namespace FullMoon.Entities.Building
/* @rhtjdwns  - 2024-05-30 11:28:19 */ {
/* @rhtjdwns  - 2024-05-30 11:28:19 */     [BurstCompile]
/* @rhtjdwns  - 2024-05-30 11:28:19 */     public class MeleeBuildingController : BaseBuildingController
/* @rhtjdwns  - 2024-05-30 11:28:19 */     {
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Foldout("Melee Building Settings")]
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public GameObject spawnUnitObject;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public MeleeBuildingData OverridenBuildingData { get; private set; }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         protected override void OnEnable()
/* @rhtjdwns  - 2024-05-30 11:28:19 */         {
/* @rhtjdwns  - 2024-05-30 11:28:19 */             base.OnEnable();
/* @rhtjdwns  - 2024-05-30 11:28:19 */             OverridenBuildingData = buildingData as MeleeBuildingData;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @Lee SJ    - 2024-06-02 20:22:27 */             ShowFrame(buildingData.BuildTime).Forget();
/* @Lee SJ    - 2024-06-02 20:22:27 */             SpawnUnit(buildingData.BuildTime).Forget();
/* @rhtjdwns  - 2024-05-30 11:28:19 */         }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @LiF       - 2024-06-01 15:37:58 */         private async UniTaskVoid SpawnUnit(float delay = 0f)
/* @rhtjdwns  - 2024-05-30 11:28:19 */         {
/* @LiF       - 2024-06-01 15:37:58 */             await UniTask.Delay(TimeSpan.FromSeconds(delay));
/* @LiF       - 2024-06-01 15:37:58 */             if (spawnUnitObject != null)
/* @LiF       - 2024-06-01 15:37:58 */             {
/* @Lee SJ    - 2024-06-02 20:34:51 */                 var flag = ObjectPoolManager.Instance.SpawnObject(spawnUnitObject, transform.position, Quaternion.identity).GetComponent<UnitFlagController>();
/* @Lee SJ    - 2024-06-02 20:34:51 */                 flag.BuildingPosition = transform.position;
/* @LiF       - 2024-06-01 15:37:58 */             }
/* @rhtjdwns  - 2024-05-30 11:28:19 */         }
/* @rhtjdwns  - 2024-05-30 11:28:19 */     }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ }
