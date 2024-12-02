/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-05-30 11:28:19 */ using MyBox;
/* @LiF       - 2024-06-01 15:37:58 */ using System;
/* @LiF       - 2024-06-01 16:42:01 */ using System.Collections.Generic;
/* @LiF       - 2024-06-01 15:37:58 */ using Unity.Burst;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ using UnityEngine;
/* @LiF       - 2024-06-01 15:37:58 */ using Cysharp.Threading.Tasks;
/* @LiF       - 2024-06-01 15:37:58 */ using FullMoon.Interfaces;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ using FullMoon.Entities.Unit;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ using FullMoon.ScriptableObject;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */ namespace FullMoon.Entities.Building
/* @rhtjdwns  - 2024-05-30 11:28:19 */ {
/* @rhtjdwns  - 2024-05-30 11:28:19 */     [BurstCompile]
/* @rhtjdwns  - 2024-05-30 11:28:19 */     public class BaseBuildingController 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         : MonoBehaviour, IDamageable, ISelectable
/* @rhtjdwns  - 2024-05-30 11:28:19 */     {
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Foldout("Base Building Settings"), DisplayInspector] 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public BaseBuildingData buildingData;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @LiF       - 2024-06-01 16:42:01 */         [SerializeField, OverrideLabel("Frame Progress")]
/* @LiF       - 2024-06-01 16:42:01 */         private List<GameObject> frameProgress;
/* @rhtjdwns  - 2024-06-01 00:35:06 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public int Hp { get; set; }
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public bool Alive { get; private set; }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         protected virtual void OnEnable()
/* @rhtjdwns  - 2024-05-30 11:28:19 */         {
/* @rhtjdwns  - 2024-05-30 11:28:19 */             Alive = true;
/* @rhtjdwns  - 2024-05-30 11:28:19 */             Hp = buildingData.MaxHp;
/* @LiF       - 2024-06-01 16:42:01 */ 
/* @LiF       - 2024-06-01 16:42:01 */             foreach (var model in frameProgress)
/* @LiF       - 2024-06-01 15:37:58 */             {
/* @LiF       - 2024-06-01 16:42:01 */                 model.SetActive(false);
/* @LiF       - 2024-06-01 15:37:58 */             }
/* @rhtjdwns  - 2024-05-30 11:28:19 */         }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public virtual void ReceiveDamage(int amount, BaseUnitController attacker)
/* @rhtjdwns  - 2024-05-30 11:28:19 */         {
/* @rhtjdwns  - 2024-05-30 11:28:19 */             if (Alive == false)
/* @rhtjdwns  - 2024-05-30 11:28:19 */             {
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 return;
/* @rhtjdwns  - 2024-05-30 11:28:19 */             }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @LiF       - 2024-06-01 15:37:58 */             Hp = Mathf.Clamp(Hp - amount, 0, Int32.MaxValue);
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */             if (Hp <= 0)
/* @rhtjdwns  - 2024-05-30 11:28:19 */             {
/* @rhtjdwns  - 2024-05-30 11:28:19 */                 Die();
/* @rhtjdwns  - 2024-05-30 11:28:19 */             }
/* @rhtjdwns  - 2024-05-30 11:28:19 */         }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @LiF       - 2024-06-01 16:42:01 */         protected async UniTaskVoid ShowFrame(float totalDelay = 0f)
/* @rhtjdwns  - 2024-06-01 00:35:06 */         {
/* @LiF       - 2024-06-01 16:42:01 */             int modelCount = frameProgress.Count;
/* @LiF       - 2024-06-01 16:42:01 */             if (modelCount == 0)
/* @LiF       - 2024-06-01 16:42:01 */             {
/* @LiF       - 2024-06-01 16:42:01 */                 return;
/* @LiF       - 2024-06-01 16:42:01 */             }
/* @LiF       - 2024-06-01 16:42:01 */ 
/* @LiF       - 2024-06-01 16:42:01 */             float delayPerModel = totalDelay / modelCount;
/* @LiF       - 2024-06-01 16:42:01 */ 
/* @LiF       - 2024-06-01 16:42:01 */             for (int i = 0; i < modelCount; i++)
/* @LiF       - 2024-06-01 15:37:58 */             {
/* @LiF       - 2024-06-01 16:42:01 */                 await UniTask.Delay(TimeSpan.FromSeconds(delayPerModel));
/* @LiF       - 2024-06-01 16:42:01 */                 if (i > 0)
/* @LiF       - 2024-06-01 16:42:01 */                 {
/* @LiF       - 2024-06-01 16:42:01 */                     frameProgress[i - 1].SetActive(false);
/* @LiF       - 2024-06-01 16:42:01 */                 }
/* @LiF       - 2024-06-01 16:42:01 */                 frameProgress[i].SetActive(true);
/* @LiF       - 2024-06-01 15:37:58 */             }
/* @rhtjdwns  - 2024-06-01 00:35:06 */         }
/* @rhtjdwns  - 2024-06-01 00:35:06 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public virtual void Die()
/* @rhtjdwns  - 2024-05-30 11:28:19 */         {
/* @rhtjdwns  - 2024-05-30 11:28:19 */             Alive = false;
/* @rhtjdwns  - 2024-05-30 11:28:19 */         }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @LiF       - 2024-06-01 15:37:58 */         public virtual void Select() { }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @LiF       - 2024-06-01 15:37:58 */         public virtual void Deselect() { }
/* @rhtjdwns  - 2024-05-30 11:28:19 */     }
/* @LiF       - 2024-06-01 16:42:01 */ }