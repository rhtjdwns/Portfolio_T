/* Git Blame Auto Generated */

/* @LiF       - 2024-06-02 01:42:09 */ using System;
/* @Lee SJ    - 2024-05-08 21:01:50 */ using System.Collections.Generic;
/* @Lee SJ    - 2024-03-26 16:38:08 */ using MyBox;
/* @Lee SJ    - 2024-05-31 20:49:59 */ using Unity.Burst;
/* @Lee SJ    - 2024-03-26 16:38:08 */ using UnityEngine;
/* @LiF       - 2024-03-27 04:16:11 */ using UnityEngine.AI;
/* @Lee SJ    - 2024-05-31 20:49:59 */ using FullMoon.Util;
/* @LiF       - 2024-03-27 02:08:23 */ using FullMoon.Interfaces;
/* @Lee SJ    - 2024-03-31 16:27:54 */ using FullMoon.ScriptableObject;
/* @LiF       - 2024-06-02 03:38:55 */ using FullMoon.UI;
/* @LiF       - 2024-06-02 01:42:09 */ using Random = UnityEngine.Random;
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @LiF       - 2024-03-27 02:08:23 */ namespace FullMoon.Entities.Unit
/* @Lee SJ    - 2024-03-26 16:38:08 */ {
/* @Lee SJ    - 2024-05-03 19:25:37 */     [BurstCompile]
/* @LiF       - 2024-03-27 02:08:23 */     public abstract class BaseUnitController
/* @LiF       - 2024-03-27 04:16:11 */         : MonoBehaviour, IDamageable, ISelectable, INavigation
/* @Lee SJ    - 2024-03-26 16:38:08 */     {
/* @Lee SJ    - 2024-03-26 16:38:08 */         [Foldout("Base Unit Settings"), DisplayInspector] 
/* @Lee SJ    - 2024-03-26 16:38:08 */         public BaseUnitData unitData;
/* @Lee SJ    - 2024-03-26 16:38:08 */         
/* @Lee SJ    - 2024-04-19 01:54:52 */         [Foldout("Base Unit Settings")] 
/* @Lee SJ    - 2024-04-19 01:54:52 */         public GameObject unitModel;
/* @Lee SJ    - 2024-04-19 01:54:52 */         
/* @Lee SJ    - 2024-05-06 22:26:59 */         [Foldout("Base Unit Settings")] 
/* @Lee SJ    - 2024-05-06 22:26:59 */         public Animator unitAnimator;
/* @Lee SJ    - 2024-05-06 22:26:59 */         
/* @Lee SJ    - 2024-03-27 20:08:19 */         [Foldout("Base Unit Settings")] 
/* @Lee SJ    - 2024-03-27 20:08:19 */         public SphereCollider viewRange;
/* @Lee SJ    - 2024-03-27 20:08:19 */         
/* @LiF       - 2024-05-19 02:04:44 */         public readonly FSM.StateMachine StateMachine = new();
/* @Lee SJ    - 2024-05-22 02:05:43 */         public readonly Animation.AnimationController AnimationController = new();
/* @Lee SJ    - 2024-03-26 16:38:08 */         
/* @Lee SJ    - 2024-03-26 16:38:08 */         public Rigidbody Rb { get; private set; }
/* @LiF       - 2024-03-27 04:16:11 */         public NavMeshAgent Agent { get; set; }
/* @Lee SJ    - 2024-05-14 00:57:12 */         public UnitFlagController Flag { get; set; }
/* @Lee SJ    - 2024-03-28 23:40:16 */         public Vector3 LatestDestination { get; set; }
/* @Lee SJ    - 2024-03-26 16:38:08 */         public int Hp { get; set; }
/* @LiF       - 2024-05-19 02:04:44 */         public bool Alive { get; private set; }
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @LiF       - 2024-05-19 02:04:44 */         public string UnitType { get; private set; }
/* @LiF       - 2024-05-19 02:04:44 */         public string UnitClass { get; private set; }
/* @Lee SJ    - 2024-05-08 21:01:50 */         
/* @LiF       - 2024-05-30 14:02:30 */         public bool IsStopped
/* @LiF       - 2024-05-30 14:02:30 */         {
/* @LiF       - 2024-05-30 14:02:30 */             get => Agent == null || !Agent.enabled || !Agent.isOnNavMesh || Agent.isStopped;
/* @LiF       - 2024-05-30 14:02:30 */             set
/* @LiF       - 2024-05-30 14:02:30 */             {
/* @LiF       - 2024-05-30 14:02:30 */                 if (Agent != null && Agent.enabled && Agent.isOnNavMesh)
/* @LiF       - 2024-05-30 14:02:30 */                 {
/* @LiF       - 2024-05-30 14:02:30 */                     Agent.isStopped = value;
/* @LiF       - 2024-05-30 14:02:30 */                 }
/* @LiF       - 2024-05-30 14:02:30 */             }
/* @LiF       - 2024-05-30 14:02:30 */         }
/* @LiF       - 2024-05-30 14:02:30 */         
/* @Lee SJ    - 2024-05-21 15:27:49 */         public HashSet<BaseUnitController> UnitInsideViewArea { get; private set; }
/* @rhtjdwns  - 2024-04-18 09:55:38 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */         protected virtual void OnEnable()
/* @Lee SJ    - 2024-03-26 16:38:08 */         {
/* @Lee SJ    - 2024-03-26 16:38:08 */             Rb = GetComponent<Rigidbody>();
/* @LiF       - 2024-03-27 04:16:11 */             Agent = GetComponent<NavMeshAgent>();
/* @Lee SJ    - 2024-05-08 21:01:50 */             UnitInsideViewArea = new HashSet<BaseUnitController>();
/* @Lee SJ    - 2024-05-22 02:05:43 */             AnimationController.SetAnimator(unitAnimator);
/* @Lee SJ    - 2024-05-22 02:05:43 */             
/* @Lee SJ    - 2024-04-12 20:21:54 */             UnitType = unitData.UnitType;
/* @Lee SJ    - 2024-04-12 20:21:54 */             UnitClass = unitData.UnitClass;
/* @LiF       - 2024-05-30 13:40:00 */             
/* @LiF       - 2024-05-30 13:40:00 */             OnAlive();
/* @김태홍노트북    - 2024-04-16 02:17:10 */ 
/* @Lee SJ    - 2024-04-23 00:24:30 */             if (viewRange != null && unitData != null)
/* @Lee SJ    - 2024-04-23 00:24:30 */             {
/* @Lee SJ    - 2024-04-23 00:24:30 */                 viewRange.radius = unitData.ViewRadius;
/* @Lee SJ    - 2024-04-23 00:24:30 */             }
/* @Lee SJ    - 2024-03-26 16:38:08 */         }
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @Lee SJ    - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ    - 2024-03-28 21:47:38 */         protected virtual void Update()
/* @Lee SJ    - 2024-03-28 21:47:38 */         {
/* @LiF       - 2024-05-30 14:02:30 */             UnitInsideViewArea.RemoveWhere(unit => unit == null || !unit.gameObject.activeInHierarchy || (!unit.Alive && unit is not MainUnitController));
/* @Lee SJ    - 2024-03-28 21:47:38 */             StateMachine.ExecuteCurrentState();
/* @Lee SJ    - 2024-03-28 21:47:38 */         }
/* @Lee SJ    - 2024-03-28 21:47:38 */ 
/* @Lee SJ    - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ    - 2024-03-28 21:47:38 */         protected virtual void FixedUpdate()
/* @Lee SJ    - 2024-03-28 21:47:38 */         {
/* @Lee SJ    - 2024-03-28 21:47:38 */             StateMachine.FixedExecuteCurrentState();
/* @Lee SJ    - 2024-03-28 21:47:38 */         }
/* @LiF       - 2024-05-30 13:40:00 */         
/* @LiF       - 2024-05-30 13:40:00 */         public virtual void OnAlive()
/* @LiF       - 2024-05-30 13:40:00 */         {
/* @LiF       - 2024-05-30 13:40:00 */             Alive = true;
/* @LiF       - 2024-05-30 13:40:00 */             Agent.enabled = true;
/* @LiF       - 2024-05-30 13:40:00 */             LatestDestination = transform.position;
/* @LiF       - 2024-05-30 13:40:00 */             Hp = unitData.MaxHp;
/* @LiF       - 2024-05-30 13:40:00 */             UnitInsideViewArea.Clear();
/* @LiF       - 2024-05-30 13:40:00 */         }
/* @Lee SJ    - 2024-03-28 21:47:38 */ 
/* @LiF       - 2024-03-27 02:08:23 */         public virtual void ReceiveDamage(int amount, BaseUnitController attacker)
/* @Lee SJ    - 2024-03-26 16:38:08 */         {
/* @Lee SJ    - 2024-05-07 16:52:51 */             if (Alive == false)
/* @Lee SJ    - 2024-05-07 16:52:51 */             {
/* @Lee SJ    - 2024-05-07 16:52:51 */                 return;
/* @Lee SJ    - 2024-05-07 16:52:51 */             }
/* @rhtjdwns  - 2024-05-28 12:04:17 */ 
/* @LiF       - 2024-06-02 01:42:09 */             if (Enum.TryParse(attacker.UnitClass, out UnitClassFlag attackerClass) == false)
/* @rhtjdwns  - 2024-05-28 12:04:17 */             {
/* @LiF       - 2024-06-02 02:43:11 */                 Debug.LogWarning($"Invalid UnitClass string: {attacker.UnitClass}");
/* @LiF       - 2024-06-02 01:42:09 */                 Hp = Mathf.Clamp(Hp - amount, 0, int.MaxValue);
/* @rhtjdwns  - 2024-05-28 12:04:17 */             }
/* @LiF       - 2024-06-02 02:43:11 */             else
/* @rhtjdwns  - 2024-05-28 12:04:17 */             {
/* @LiF       - 2024-06-02 02:43:11 */                 // 비트플래그를 사용한 상성 비교
/* @LiF       - 2024-06-02 02:43:11 */                 if ((attackerClass & unitData.UnitCounter) is not 0)
/* @rhtjdwns  - 2024-05-28 12:04:17 */                 {
/* @rhtjdwns  - 2024-06-02 16:08:39 */                     amount = (int)(amount * (unitData.CounterDamage / 100f));
/* @LiF       - 2024-06-02 02:43:11 */                     Hp = Mathf.Clamp(Hp - amount, 0, int.MaxValue);
/* @LiF       - 2024-06-02 02:43:11 */                 }
/* @LiF       - 2024-06-02 02:43:11 */                 else if ((attackerClass & unitData.UnitAdvance) is not 0)
/* @LiF       - 2024-06-02 02:43:11 */                 {
/* @Lee SJ    - 2024-06-02 20:40:10 */                     float rand = Random.Range(0f, 100f);
/* @김태홍노트북    - 2024-06-02 17:19:19 */                     if (rand > unitData.CounterGuard)
/* @김태홍노트북    - 2024-06-02 17:19:19 */                     {
/* @김태홍노트북    - 2024-06-02 17:19:19 */                         Hp = Mathf.Clamp(Hp - amount, 0, int.MaxValue);
/* @김태홍노트북    - 2024-06-02 17:19:19 */                     }
/* @김태홍노트북    - 2024-06-02 17:19:19 */                     else
/* @LiF       - 2024-06-02 02:43:11 */                     {
/* @LiF       - 2024-06-02 02:43:11 */                         return;
/* @LiF       - 2024-06-02 02:43:11 */                     }
/* @LiF       - 2024-06-02 02:43:11 */                 }
/* @rhtjdwns  - 2024-06-02 20:12:29 */                 else
/* @rhtjdwns  - 2024-06-02 20:12:29 */                 {
/* @rhtjdwns  - 2024-06-02 20:12:29 */                     Hp = Mathf.Clamp(Hp - amount, 0, int.MaxValue);
/* @rhtjdwns  - 2024-06-02 20:12:29 */                 }
/* @rhtjdwns  - 2024-05-28 12:04:17 */             }
/* @LiF       - 2024-06-02 01:42:09 */ 
/* @LiF       - 2024-05-19 02:04:44 */             if (unitAnimator != null)
/* @Lee SJ    - 2024-05-07 16:52:51 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 AnimatorStateInfo stateInfo = unitAnimator.GetCurrentAnimatorStateInfo(0);
/* @Lee SJ    - 2024-05-31 20:49:59 */                 if (stateInfo.loop || (AnimationController.CurrentStateInfo.Item1 != "Hit" && stateInfo.normalizedTime >= 0.9f))
/* @LiF       - 2024-05-19 02:04:44 */                 {
/* @Lee SJ    - 2024-05-22 02:05:43 */                     AnimationController.PlayAnimationAndContinueLoop("Hit").Forget();
/* @LiF       - 2024-05-19 02:04:44 */                 }
/* @Lee SJ    - 2024-05-07 16:52:51 */             }
/* @Lee SJ    - 2024-04-19 01:54:52 */ 
/* @Lee SJ    - 2024-05-22 02:05:43 */             if (Hp <= 0)
/* @Lee SJ    - 2024-04-08 22:31:23 */             {
/* @Lee SJ    - 2024-05-22 02:05:43 */                 Die();
/* @Lee SJ    - 2024-04-08 22:31:23 */             }
/* @Lee SJ    - 2024-03-26 16:38:08 */         }
/* @LiF       - 2024-04-11 01:52:04 */ 
/* @LiF       - 2024-06-02 01:42:09 */ 
/* @LiF       - 2024-04-11 01:52:04 */         public virtual void Die()
/* @LiF       - 2024-04-11 01:52:04 */         {
/* @LiF       - 2024-05-30 13:40:00 */             Hp = 0;
/* @Lee SJ    - 2024-05-07 16:52:51 */             Alive = false;
/* @LiF       - 2024-05-30 13:40:00 */             Agent.enabled = false;
/* @Lee SJ    - 2024-05-07 16:52:51 */             
/* @LiF       - 2024-06-02 03:38:55 */             if (UnitType is "Enemy")
/* @LiF       - 2024-04-11 01:52:04 */             {
/* @LiF       - 2024-06-02 03:38:55 */                 MainUIController.Instance.ChangeEnemyAmount(-1);
/* @LiF       - 2024-06-02 03:38:55 */                 
/* @LiF       - 2024-06-02 03:38:55 */                 if (unitData.RespawnUnitObject == null)
/* @LiF       - 2024-06-02 03:38:55 */                 {
/* @LiF       - 2024-06-02 03:38:55 */                     return;
/* @LiF       - 2024-06-02 03:38:55 */                 }
/* @LiF       - 2024-06-02 03:38:55 */                 
/* @Lee SJ    - 2024-06-02 20:40:10 */                 for (int i = 0; i < unitData.UnitDrop; i++)
/* @Lee SJ    - 2024-05-21 01:40:06 */                 {
/* @LiF       - 2024-06-01 19:11:50 */                     Vector2 randomPosition = Random.insideUnitCircle * 1f;
/* @LiF       - 2024-06-01 19:11:50 */                     Vector3 spawnPosition = new Vector3(transform.position.x + randomPosition.x, transform.position.y, transform.position.z + randomPosition.y);
/* @LiF       - 2024-06-01 19:11:50 */                     ObjectPoolManager.Instance.SpawnObject(unitData.RespawnUnitObject, spawnPosition, Quaternion.identity);
/* @Lee SJ    - 2024-05-21 01:40:06 */                 }
/* @LiF       - 2024-04-11 01:52:04 */             }
/* @LiF       - 2024-04-11 01:52:04 */         }
/* @LiF       - 2024-04-11 01:52:04 */ 
/* @Lee SJ    - 2024-04-18 17:16:02 */         public virtual void Select()
/* @LiF       - 2024-03-27 04:16:11 */         {
/* @Lee SJ    - 2024-04-19 01:54:52 */             switch (UnitType)
/* @Lee SJ    - 2024-04-19 01:54:52 */             {
/* @Lee SJ    - 2024-04-19 01:54:52 */                 case "Player":
/* @Lee SJ    - 2024-04-19 01:54:52 */                     unitModel.layer = LayerMask.NameToLayer("SelectPlayer");
/* @Lee SJ    - 2024-04-19 01:54:52 */                     break;
/* @Lee SJ    - 2024-04-19 01:54:52 */                 case "Enemy":
/* @Lee SJ    - 2024-04-19 01:54:52 */                     unitModel.layer = LayerMask.NameToLayer("SelectEnemy");
/* @Lee SJ    - 2024-04-19 01:54:52 */                     break;
/* @Lee SJ    - 2024-04-19 01:54:52 */             }
/* @LiF       - 2024-03-27 04:16:11 */         }
/* @LiF       - 2024-03-27 04:16:11 */         
/* @Lee SJ    - 2024-04-18 17:16:02 */         public virtual void Deselect()
/* @LiF       - 2024-03-27 04:16:11 */         {
/* @Lee SJ    - 2024-04-19 01:54:52 */             unitModel.layer = LayerMask.NameToLayer("Default");
/* @LiF       - 2024-03-27 04:16:11 */         }
/* @rhtjdwns  - 2024-04-16 04:03:48 */ 
/* @Lee SJ    - 2024-03-28 23:40:16 */         public virtual void MoveToPosition(Vector3 location)
/* @LiF       - 2024-03-27 04:16:11 */         {
/* @Lee SJ    - 2024-05-07 16:52:51 */             if (Alive == false)
/* @Lee SJ    - 2024-05-07 16:52:51 */             {
/* @Lee SJ    - 2024-05-07 16:52:51 */                 return;
/* @Lee SJ    - 2024-05-07 16:52:51 */             }
/* @Lee SJ    - 2024-05-31 19:57:19 */ 
/* @Lee SJ    - 2024-05-31 19:57:19 */             if (UnityEngine.AI.NavMesh.SamplePosition(location, out var hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas))
/* @Lee SJ    - 2024-05-31 19:57:19 */             {
/* @Lee SJ    - 2024-05-31 19:57:19 */                 NavMeshPath path = new NavMeshPath();
/* @Lee SJ    - 2024-05-31 19:57:19 */                 Agent.CalculatePath(hit.position, path);
/* @Lee SJ    - 2024-05-31 19:57:19 */                 Agent.SetPath(path);
/* @Lee SJ    - 2024-05-31 19:57:19 */                 LatestDestination = location;
/* @Lee SJ    - 2024-05-31 19:57:19 */             }
/* @LiF       - 2024-03-27 04:16:11 */         }
/* @Lee SJ    - 2024-05-06 22:26:59 */         
/* @LiF       - 2024-04-24 17:38:50 */ #if UNITY_EDITOR
/* @Lee SJ    - 2024-04-02 04:24:46 */         protected virtual void OnDrawGizmos()
/* @Lee SJ    - 2024-03-27 20:08:19 */         {
/* @Lee SJ    - 2024-04-02 05:20:57 */             if (viewRange != null && unitData != null)
/* @Lee SJ    - 2024-03-27 22:58:51 */             {
/* @Lee SJ    - 2024-04-02 16:51:40 */                 viewRange.radius = unitData.ViewRadius;
/* @Lee SJ    - 2024-03-27 22:58:51 */             }
/* @Lee SJ    - 2024-03-27 20:08:19 */         }
/* @LiF       - 2024-04-24 17:38:50 */ #endif
/* @Lee SJ    - 2024-03-26 16:38:08 */     }
/* @Lee SJ    - 2024-04-23 00:24:30 */ }