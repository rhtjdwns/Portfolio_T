/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-03-26 16:38:08 */ using MyBox;
/* @Lee SJ    - 2024-03-31 15:18:30 */ using System.Linq;
/* @Lee SJ    - 2024-05-07 16:52:51 */ using Cysharp.Threading.Tasks;
/* @Lee SJ    - 2024-03-26 16:38:08 */ using UnityEngine;
/* @Lee SJ    - 2024-03-26 16:38:08 */ using UnityEngine.AI;
/* @Lee SJ    - 2024-04-02 05:20:57 */ using UnityEngine.Rendering.Universal;
/* @Lee SJ    - 2024-03-31 15:18:30 */ using FullMoon.Util;
/* @Lee SJ    - 2024-03-31 15:18:30 */ using FullMoon.Effect;
/* @Lee SJ    - 2024-03-26 16:38:08 */ using FullMoon.Interfaces;
/* @Lee SJ    - 2024-03-31 15:18:30 */ using FullMoon.Entities.Unit.States;
/* @Lee SJ    - 2024-03-31 16:27:54 */ using FullMoon.ScriptableObject;
/* @Lee SJ    - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @Lee SJ    - 2024-03-26 16:38:08 */ namespace FullMoon.Entities.Unit
/* @Lee SJ    - 2024-03-26 16:38:08 */ {
/* @Lee SJ    - 2024-05-03 19:25:37 */     [RequireComponent(typeof(NavMeshAgent)), BurstCompile]
/* @LiF       - 2024-05-19 02:04:44 */     public class RangedUnitController : BaseUnitController, IAttackable
/* @Lee SJ    - 2024-03-26 16:38:08 */     {
/* @Lee SJ    - 2024-04-02 05:20:57 */         [Foldout("Ranged Unit Settings")]
/* @Lee SJ    - 2024-04-02 05:20:57 */         public DecalProjector decalProjector;
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-03-26 16:38:08 */         [Foldout("Ranged Unit Settings")]
/* @Lee SJ    - 2024-03-30 23:12:08 */         public GameObject attackEffect;
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @Lee SJ    - 2024-04-08 19:40:53 */         public RangedUnitData OverridenUnitData { get; private set; }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-04-22 20:03:35 */         public float CurrentAttackCoolTime { get; set; }
/* @LiF       - 2024-03-27 02:08:23 */ 
/* @Lee SJ    - 2024-05-08 18:25:40 */         protected override void OnEnable()
/* @Lee SJ    - 2024-03-26 16:38:08 */         {
/* @Lee SJ    - 2024-05-08 18:25:40 */             base.OnEnable();
/* @Lee SJ    - 2024-04-20 02:31:39 */             OverridenUnitData = unitData as RangedUnitData;
/* @Lee SJ    - 2024-04-22 20:03:35 */             CurrentAttackCoolTime = unitData.AttackCoolTime;
/* @김태홍노트북    - 2024-04-16 02:17:10 */ 
/* @LiF       - 2024-05-19 02:04:44 */             InitializeViewRange();
/* @Lee SJ    - 2024-05-08 21:01:50 */ 
/* @LiF       - 2024-05-19 02:04:44 */             if (decalProjector != null)
/* @김태홍노트북    - 2024-04-16 02:17:10 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 InitializeDecalProjector();
/* @김태홍노트북    - 2024-04-16 02:17:10 */             }
/* @김태홍노트북    - 2024-04-16 02:17:10 */ 
/* @Lee SJ    - 2024-03-31 15:18:30 */             StateMachine.ChangeState(new RangedUnitIdle(this));
/* @Lee SJ    - 2024-03-27 22:58:51 */         }
/* @Lee SJ    - 2024-04-24 16:49:30 */ 
/* @Lee SJ    - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ    - 2024-04-24 16:49:30 */         protected override void Update()
/* @Lee SJ    - 2024-04-08 22:31:23 */         {
/* @Lee SJ    - 2024-04-22 20:03:35 */             ReduceAttackCoolTime();
/* @Lee SJ    - 2024-04-24 16:49:30 */             base.Update();
/* @Lee SJ    - 2024-04-08 22:31:23 */         }
/* @Lee SJ    - 2024-03-27 22:58:51 */ 
/* @Lee SJ    - 2024-05-07 20:09:25 */         public override void Die()
/* @Lee SJ    - 2024-05-07 20:09:25 */         {
/* @Lee SJ    - 2024-05-07 20:09:25 */             base.Die();
/* @Lee SJ    - 2024-05-07 20:09:25 */             StateMachine.ChangeState(new RangedUnitDead(this));
/* @Lee SJ    - 2024-05-07 20:09:25 */         }
/* @Lee SJ    - 2024-05-07 20:09:25 */ 
/* @Lee SJ    - 2024-03-27 22:58:51 */         public void EnterViewRange(Collider unit)
/* @Lee SJ    - 2024-03-27 22:58:51 */         {
/* @LiF       - 2024-05-19 02:04:44 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @Lee SJ    - 2024-03-27 22:58:51 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 UnitInsideViewArea.Add(controller);
/* @Lee SJ    - 2024-03-27 22:58:51 */             }
/* @Lee SJ    - 2024-03-27 22:58:51 */         }
/* @Lee SJ    - 2024-03-27 22:58:51 */ 
/* @Lee SJ    - 2024-03-27 22:58:51 */         public void ExitViewRange(Collider unit)
/* @Lee SJ    - 2024-03-27 22:58:51 */         {
/* @LiF       - 2024-05-19 02:04:44 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @Lee SJ    - 2024-03-27 22:58:51 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 UnitInsideViewArea.Remove(controller);
/* @Lee SJ    - 2024-03-27 22:58:51 */             }
/* @Lee SJ    - 2024-03-26 16:38:08 */         }
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @Lee SJ    - 2024-05-07 16:52:51 */         public async UniTaskVoid ExecuteAttack(Transform target)
/* @Lee SJ    - 2024-03-26 16:38:08 */         {
/* @LiF       - 2024-05-19 02:04:44 */             Vector3 targetDirection = target.position - transform.position;
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             AlignToTarget(targetDirection);
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-06-02 01:03:15 */             AnimationController.SetAnimation("Attack", 0.1f);
/* @Lee SJ    - 2024-05-07 20:09:25 */ 
/* @Lee SJ    - 2024-05-07 20:09:25 */             await UniTask.DelayFrame(OverridenUnitData.HitAnimationFrame);
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */             FireBullet(target);
/* @Lee SJ    - 2024-03-26 16:38:08 */         }
/* @Lee SJ    - 2024-03-27 22:58:51 */ 
/* @Lee SJ    - 2024-04-18 17:16:02 */         public override void Select()
/* @Lee SJ    - 2024-04-18 17:16:02 */         {
/* @Lee SJ    - 2024-04-18 17:16:02 */             base.Select();
/* @Lee SJ    - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ    - 2024-05-30 21:35:08 */             {
/* @Lee SJ    - 2024-05-30 21:35:08 */                 Flag.Select();
/* @Lee SJ    - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ    - 2024-05-30 21:35:08 */             }
/* @LiF       - 2024-05-19 02:04:44 */             decalProjector?.gameObject.SetActive(true);
/* @Lee SJ    - 2024-04-18 17:16:02 */         }
/* @Lee SJ    - 2024-04-18 17:16:02 */ 
/* @Lee SJ    - 2024-04-18 17:16:02 */         public override void Deselect()
/* @Lee SJ    - 2024-04-18 17:16:02 */         {
/* @Lee SJ    - 2024-04-18 17:16:02 */             base.Deselect();
/* @Lee SJ    - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ    - 2024-05-30 21:35:08 */             {
/* @Lee SJ    - 2024-05-30 21:35:08 */                 Flag.Deselect();
/* @Lee SJ    - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ    - 2024-05-30 21:35:08 */             }
/* @LiF       - 2024-05-19 02:04:44 */             decalProjector?.gameObject.SetActive(false);
/* @Lee SJ    - 2024-04-18 17:16:02 */         }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-03-28 23:40:16 */         public override void MoveToPosition(Vector3 location)
/* @Lee SJ    - 2024-03-28 21:47:38 */         {
/* @Lee SJ    - 2024-03-28 23:40:16 */             base.MoveToPosition(location);
/* @Lee SJ    - 2024-03-31 15:18:30 */             StateMachine.ChangeState(new RangedUnitMove(this));
/* @Lee SJ    - 2024-03-28 21:47:38 */         }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @Lee SJ    - 2024-04-22 20:03:35 */         private void ReduceAttackCoolTime()
/* @Lee SJ    - 2024-04-22 20:03:35 */         {
/* @Lee SJ    - 2024-04-22 20:03:35 */             if (CurrentAttackCoolTime > 0)
/* @Lee SJ    - 2024-04-22 20:03:35 */             {
/* @Lee SJ    - 2024-04-22 20:03:35 */                 CurrentAttackCoolTime -= Time.deltaTime;
/* @Lee SJ    - 2024-04-22 20:03:35 */             }
/* @Lee SJ    - 2024-04-22 20:03:35 */         }
/* @rhtjdwns  - 2024-04-18 09:55:38 */ 
/* @LiF       - 2024-05-19 02:04:44 */         private void InitializeViewRange()
/* @LiF       - 2024-05-19 02:04:44 */         {
/* @LiF       - 2024-05-19 02:04:44 */             if (viewRange != null && unitData != null)
/* @LiF       - 2024-05-19 02:04:44 */             {
/* @LiF       - 2024-05-19 02:04:44 */                 float worldRadius = viewRange.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
/* @LiF       - 2024-05-19 02:04:44 */                 var triggerEvent = viewRange.GetComponent<ColliderTriggerEvents>();
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */                 if (triggerEvent != null)
/* @LiF       - 2024-05-19 02:04:44 */                 {
/* @LiF       - 2024-05-19 02:04:44 */                     var units = Physics.OverlapSphere(transform.position + viewRange.center, worldRadius)
/* @LiF       - 2024-05-19 02:04:44 */                         .Where(t => triggerEvent.GetFilterTags.Contains(t.tag) && t.gameObject != gameObject)
/* @LiF       - 2024-05-19 02:04:44 */                         .Select(t => t.GetComponent<BaseUnitController>())
/* @LiF       - 2024-05-19 02:04:44 */                         .Where(unit => unit != null)
/* @LiF       - 2024-05-19 02:04:44 */                         .ToList();
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */                     UnitInsideViewArea.UnionWith(units);
/* @LiF       - 2024-05-19 02:04:44 */                 }
/* @LiF       - 2024-05-19 02:04:44 */             }
/* @LiF       - 2024-05-19 02:04:44 */         }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */         private void InitializeDecalProjector()
/* @LiF       - 2024-05-19 02:04:44 */         {
/* @LiF       - 2024-05-19 02:04:44 */             decalProjector.gameObject.SetActive(false);
/* @LiF       - 2024-05-19 02:04:44 */             decalProjector.size = new Vector3(unitData.AttackRadius * 2f, unitData.AttackRadius * 2f, decalProjector.size.z);
/* @LiF       - 2024-05-19 02:04:44 */         }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */         private void AlignToTarget(Vector3 targetDirection)
/* @LiF       - 2024-05-19 02:04:44 */         {
/* @LiF       - 2024-05-19 02:04:44 */             transform.forward = targetDirection.normalized;
/* @LiF       - 2024-05-19 02:04:44 */             transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
/* @LiF       - 2024-05-19 02:04:44 */         }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-05-19 02:04:44 */         private void FireBullet(Transform target)
/* @LiF       - 2024-05-19 02:04:44 */         {
/* @LiF       - 2024-05-19 02:04:44 */             GameObject bullet = ObjectPoolManager.Instance.SpawnObject(attackEffect,
/* @LiF       - 2024-05-19 02:04:44 */                 transform.TransformPoint(GetComponent<CapsuleCollider>().center), Quaternion.identity);
/* @LiF       - 2024-05-19 02:04:44 */             bullet.GetComponent<BulletEffectController>().Fire(target, transform, OverridenUnitData.BulletSpeed, OverridenUnitData.AttackDamage);
/* @LiF       - 2024-05-19 02:04:44 */         }
/* @LiF       - 2024-05-19 02:04:44 */ 
/* @LiF       - 2024-04-24 17:38:50 */ #if UNITY_EDITOR
/* @Lee SJ    - 2024-04-02 04:24:46 */         protected override void OnDrawGizmos()
/* @Lee SJ    - 2024-03-27 22:58:51 */         {
/* @Lee SJ    - 2024-04-02 04:24:46 */             base.OnDrawGizmos();
/* @Lee SJ    - 2024-04-02 05:20:57 */ 
/* @Lee SJ    - 2024-04-02 05:20:57 */             if (decalProjector != null)
/* @Lee SJ    - 2024-04-02 05:20:57 */             {
/* @Lee SJ    - 2024-04-12 19:33:46 */                 decalProjector.size = new Vector3(unitData.AttackRadius * 2f, unitData.AttackRadius * 2f, decalProjector.size.z);
/* @Lee SJ    - 2024-04-02 05:20:57 */             }
/* @Lee SJ    - 2024-03-27 22:58:51 */         }
/* @LiF       - 2024-04-24 17:38:50 */ #endif
/* @Lee SJ    - 2024-03-26 16:38:08 */     }
/* @LiF       - 2024-05-19 02:04:44 */ }