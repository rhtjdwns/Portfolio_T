/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-04-15 23:12:39 */ using System.Collections.Generic;
/* @LiF     - 2024-05-29 11:38:58 */ using MyBox;
/* @Lee SJ  - 2024-04-18 18:19:57 */ using System.Linq;
/* @Lee SJ  - 2024-05-07 16:52:51 */ using Cysharp.Threading.Tasks;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using UnityEngine;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using UnityEngine.AI;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using UnityEngine.Rendering.Universal;
/* @Lee SJ  - 2024-05-06 18:31:44 */ using FullMoon.Interfaces;
/* @Lee SJ  - 2024-05-06 18:31:44 */ using FullMoon.Entities.Unit.States;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using FullMoon.ScriptableObject;
/* @Lee SJ  - 2024-04-15 23:12:39 */ using FullMoon.Util;
/* @Lee SJ  - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */ namespace FullMoon.Entities.Unit
/* @Lee SJ  - 2024-04-15 23:12:39 */ {
/* @Lee SJ  - 2024-05-03 19:25:37 */     [RequireComponent(typeof(NavMeshAgent)), BurstCompile]
/* @LiF     - 2024-05-19 02:04:44 */     public class MainUnitController : BaseUnitController, IAttackable
/* @Lee SJ  - 2024-04-15 23:12:39 */     {
/* @Lee SJ  - 2024-04-15 23:12:39 */         [Foldout("Main Unit Settings")]
/* @Lee SJ  - 2024-04-15 23:12:39 */         public DecalProjector decalProjector;
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         [Foldout("Main Unit Settings")]
/* @LiF     - 2024-05-29 11:38:58 */         public List<GameObject> attackEffects;
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-16 16:01:37 */         [Foldout("Main Unit Settings")]
/* @LiF     - 2024-05-29 11:38:58 */         public List<GameObject> attackPointEffects;
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */         public MainUnitData OverridenUnitData { get; private set; }
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */         public float CurrentAttackCoolTime { get; set; }
/* @LiF     - 2024-05-19 02:04:44 */         
/* @Lee SJ  - 2024-05-08 18:25:40 */         protected override void OnEnable()
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @Lee SJ  - 2024-05-08 18:25:40 */             base.OnEnable();
/* @Lee SJ  - 2024-04-20 02:31:39 */             OverridenUnitData = unitData as MainUnitData;
/* @Lee SJ  - 2024-05-06 18:31:44 */             CurrentAttackCoolTime = unitData.AttackCoolTime;
/* @김태홍노트북  - 2024-04-16 02:17:10 */ 
/* @LiF     - 2024-05-19 02:04:44 */             InitializeViewRange();
/* @Lee SJ  - 2024-05-08 21:01:50 */ 
/* @LiF     - 2024-05-19 02:04:44 */             if (decalProjector != null)
/* @김태홍노트북  - 2024-04-16 02:17:10 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 InitializeDecalProjector();
/* @김태홍노트북  - 2024-04-16 02:17:10 */             }
/* @김태홍노트북  - 2024-04-16 02:17:10 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */             StateMachine.ChangeState(new MainUnitIdle(this));
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @Lee SJ  - 2024-05-08 18:25:40 */ 
/* @Lee SJ  - 2024-05-03 19:25:37 */         [BurstCompile]
/* @Lee SJ  - 2024-04-24 16:49:30 */         protected override void Update()
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @Lee SJ  - 2024-05-06 18:31:44 */             ReduceAttackCoolTime();
/* @Lee SJ  - 2024-04-24 16:49:30 */             base.Update();
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @LiF     - 2024-05-07 22:54:34 */         public override void Die()
/* @LiF     - 2024-05-07 22:54:34 */         {
/* @LiF     - 2024-05-07 22:54:34 */             base.Die();
/* @LiF     - 2024-05-30 13:40:00 */             StateMachine.ChangeState(new MainUnitGroggy(this));
/* @LiF     - 2024-05-07 22:54:34 */         }
/* @LiF     - 2024-05-07 22:54:34 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */         public void EnterViewRange(Collider unit)
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @Lee SJ  - 2024-04-15 23:12:39 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 UnitInsideViewArea.Add(controller);
/* @Lee SJ  - 2024-04-15 23:12:39 */             }
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */         public void ExitViewRange(Collider unit)
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (unit.TryGetComponent(out BaseUnitController controller))
/* @Lee SJ  - 2024-04-15 23:12:39 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 UnitInsideViewArea.Remove(controller);
/* @Lee SJ  - 2024-04-15 23:12:39 */             }
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @Lee SJ  - 2024-04-18 17:16:02 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         [BurstCompile]
/* @Lee SJ  - 2024-05-07 16:52:51 */         public async UniTaskVoid ExecuteAttack(Transform target)
/* @Lee SJ  - 2024-05-06 18:31:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (target.TryGetComponent(out BaseUnitController targetController) && targetController.gameObject.activeInHierarchy)
/* @Lee SJ  - 2024-05-06 18:31:44 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 Vector3 targetDirection = target.position - transform.position;
/* @LiF     - 2024-05-19 02:04:44 */                 Vector3 hitPosition = CalculateHitPosition(targetDirection);
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */                 AlignToTarget(targetDirection);
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @LiF     - 2024-05-29 11:38:58 */                 int effectType = Random.Range(0, 2);
/* @LiF     - 2024-05-29 11:38:58 */                 
/* @LiF     - 2024-06-01 19:11:50 */                 if (targetController.gameObject.activeInHierarchy && targetController.Alive)
/* @LiF     - 2024-06-01 19:11:50 */                 {
/* @LiF     - 2024-06-02 01:03:15 */                     AnimationController.SetAnimation(effectType == 0 ? "Attack" : "Attack2", 0.1f);
/* @LiF     - 2024-06-01 19:11:50 */                     PlayAttackEffects(effectType, targetDirection, hitPosition);
/* @LiF     - 2024-06-01 19:11:50 */                 }
/* @LiF     - 2024-06-01 19:11:50 */                 
/* @LiF     - 2024-05-19 02:04:44 */                 await UniTask.DelayFrame(OverridenUnitData.HitAnimationFrame);
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-06-01 19:11:50 */                 if (targetController.gameObject.activeInHierarchy && targetController.Alive)
/* @LiF     - 2024-05-19 02:04:44 */                 {
/* @LiF     - 2024-05-19 02:04:44 */                     targetController.ReceiveDamage(OverridenUnitData.AttackDamage, this);
/* @LiF     - 2024-05-19 02:04:44 */                 }
/* @LiF     - 2024-05-16 16:01:37 */             }
/* @Lee SJ  - 2024-05-06 18:31:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @Lee SJ  - 2024-04-18 17:16:02 */         public override void Select()
/* @Lee SJ  - 2024-04-18 17:16:02 */         {
/* @Lee SJ  - 2024-04-18 17:16:02 */             base.Select();
/* @Lee SJ  - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ  - 2024-05-30 21:35:08 */             {
/* @Lee SJ  - 2024-05-30 21:35:08 */                 Flag.Select();
/* @Lee SJ  - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ  - 2024-05-30 21:35:08 */             }
/* @LiF     - 2024-05-19 02:04:44 */             decalProjector?.gameObject.SetActive(true);
/* @Lee SJ  - 2024-04-18 17:16:02 */         }
/* @Lee SJ  - 2024-04-18 17:16:02 */ 
/* @Lee SJ  - 2024-04-18 17:16:02 */         public override void Deselect()
/* @Lee SJ  - 2024-04-18 17:16:02 */         {
/* @Lee SJ  - 2024-04-18 17:16:02 */             base.Deselect();
/* @Lee SJ  - 2024-05-30 21:35:08 */             if (Flag != null)
/* @Lee SJ  - 2024-05-30 21:35:08 */             {
/* @Lee SJ  - 2024-05-30 21:35:08 */                 Flag.Deselect();
/* @Lee SJ  - 2024-05-30 21:35:08 */                 return;
/* @Lee SJ  - 2024-05-30 21:35:08 */             }
/* @LiF     - 2024-05-19 02:04:44 */             decalProjector?.gameObject.SetActive(false);
/* @Lee SJ  - 2024-04-18 17:16:02 */         }
/* @Lee SJ  - 2024-04-18 17:16:02 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */         public override void MoveToPosition(Vector3 location)
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @Lee SJ  - 2024-04-15 23:12:39 */             base.MoveToPosition(location);
/* @Lee SJ  - 2024-04-15 23:12:39 */             StateMachine.ChangeState(new MainUnitMove(this));
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-05-06 18:31:44 */         private void ReduceAttackCoolTime()
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @Lee SJ  - 2024-05-06 18:31:44 */             if (CurrentAttackCoolTime > 0)
/* @Lee SJ  - 2024-04-22 01:49:35 */             {
/* @Lee SJ  - 2024-05-06 18:31:44 */                 CurrentAttackCoolTime -= Time.deltaTime;
/* @Lee SJ  - 2024-04-22 01:49:35 */             }
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @Lee SJ  - 2024-05-06 18:31:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */         private void InitializeViewRange()
/* @LiF     - 2024-05-19 02:04:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (viewRange != null && unitData != null)
/* @LiF     - 2024-05-19 02:04:44 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 float worldRadius = viewRange.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
/* @LiF     - 2024-05-19 02:04:44 */                 var triggerEvent = viewRange.GetComponent<ColliderTriggerEvents>();
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */                 if (triggerEvent != null)
/* @LiF     - 2024-05-19 02:04:44 */                 {
/* @LiF     - 2024-05-19 02:04:44 */                     var units = Physics.OverlapSphere(transform.position + viewRange.center, worldRadius)
/* @LiF     - 2024-05-19 02:04:44 */                         .Where(t => triggerEvent.GetFilterTags.Contains(t.tag) && t.gameObject != gameObject)
/* @LiF     - 2024-05-19 02:04:44 */                         .Select(t => t.GetComponent<BaseUnitController>())
/* @LiF     - 2024-05-19 02:04:44 */                         .Where(unit => unit != null)
/* @LiF     - 2024-05-19 02:04:44 */                         .ToList();
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */                     UnitInsideViewArea.UnionWith(units);
/* @LiF     - 2024-05-19 02:04:44 */                 }
/* @LiF     - 2024-05-19 02:04:44 */             }
/* @LiF     - 2024-05-19 02:04:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */         private void InitializeDecalProjector()
/* @LiF     - 2024-05-19 02:04:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             decalProjector.gameObject.SetActive(false);
/* @LiF     - 2024-05-19 02:04:44 */             decalProjector.size = new Vector3(unitData.AttackRadius * 2f, unitData.AttackRadius * 2f, decalProjector.size.z);
/* @LiF     - 2024-05-19 02:04:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */         private void AlignToTarget(Vector3 targetDirection)
/* @LiF     - 2024-05-19 02:04:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             transform.forward = targetDirection.normalized;
/* @LiF     - 2024-05-19 02:04:44 */             transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, transform.eulerAngles.z);
/* @LiF     - 2024-05-19 02:04:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-19 02:04:44 */         private Vector3 CalculateHitPosition(Vector3 targetDirection)
/* @LiF     - 2024-05-19 02:04:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (Physics.Raycast(unitModel.transform.position, targetDirection.normalized, out var hit, OverridenUnitData.AttackRadius, 1 << LayerMask.NameToLayer("Unit")))
/* @LiF     - 2024-05-19 02:04:44 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 return hit.point;
/* @LiF     - 2024-05-19 02:04:44 */             }
/* @LiF     - 2024-05-19 02:04:44 */             return targetDirection;
/* @LiF     - 2024-05-19 02:04:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-29 11:38:58 */         private void PlayAttackEffects(int effectType, Vector3 targetDirection, Vector3 hitPosition)
/* @LiF     - 2024-05-19 02:04:44 */         {
/* @LiF     - 2024-05-29 11:38:58 */             if (attackEffects != null)
/* @LiF     - 2024-05-19 02:04:44 */             {
/* @LiF     - 2024-05-29 11:38:58 */                 GameObject attackFX = ObjectPoolManager.Instance.SpawnObject(attackEffects[effectType], unitModel.transform.position, Quaternion.identity);
/* @LiF     - 2024-05-19 02:04:44 */                 attackFX.transform.forward = targetDirection.normalized;
/* @LiF     - 2024-05-19 02:04:44 */             }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-05-29 11:38:58 */             if (attackPointEffects != null)
/* @LiF     - 2024-05-19 02:04:44 */             {
/* @LiF     - 2024-05-29 11:38:58 */                 GameObject attackPointFX = ObjectPoolManager.Instance.SpawnObject(attackPointEffects[effectType], hitPosition, Quaternion.identity);
/* @LiF     - 2024-05-19 02:04:44 */                 attackPointFX.transform.forward = targetDirection.normalized;
/* @LiF     - 2024-05-19 02:04:44 */             }
/* @LiF     - 2024-05-19 02:04:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @LiF     - 2024-04-24 17:38:50 */ #if UNITY_EDITOR
/* @Lee SJ  - 2024-04-15 23:12:39 */         protected override void OnDrawGizmos()
/* @Lee SJ  - 2024-04-15 23:12:39 */         {
/* @Lee SJ  - 2024-04-15 23:12:39 */             base.OnDrawGizmos();
/* @Lee SJ  - 2024-04-15 23:12:39 */ 
/* @Lee SJ  - 2024-04-15 23:12:39 */             if (decalProjector != null)
/* @Lee SJ  - 2024-04-15 23:12:39 */             {
/* @Lee SJ  - 2024-05-06 18:31:44 */                 decalProjector.size = new Vector3(unitData.AttackRadius * 2f, unitData.AttackRadius * 2f, decalProjector.size.z);
/* @Lee SJ  - 2024-04-15 23:12:39 */             }
/* @Lee SJ  - 2024-04-15 23:12:39 */         }
/* @LiF     - 2024-04-24 17:38:50 */ #endif
/* @Lee SJ  - 2024-04-15 23:12:39 */     }
/* @Lee SJ  - 2024-04-15 23:12:39 */ }
