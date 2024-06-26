/* Git Blame Auto Generated */

/* @LiF     - 2024-06-03 10:45:04 */ using MyBox;
/* @LiF     - 2024-06-03 10:45:04 */ using System.Linq;
/* @Lee SJ  - 2024-03-30 23:12:08 */ using UnityEngine;
/* @Lee SJ  - 2024-03-30 23:12:08 */ using FullMoon.Util;
/* @Lee SJ  - 2024-03-30 23:12:08 */ using FullMoon.Entities.Unit;
/* @Lee SJ  - 2024-03-30 23:12:08 */ 
/* @Lee SJ  - 2024-03-30 23:12:08 */ namespace FullMoon.Effect
/* @Lee SJ  - 2024-03-30 23:12:08 */ {
/* @Lee SJ  - 2024-03-30 23:12:08 */     public class BulletEffectController : MonoBehaviour
/* @Lee SJ  - 2024-03-30 23:12:08 */     {
/* @Lee SJ  - 2024-03-30 23:12:08 */         [SerializeField] private GameObject firingEffect;
/* @Lee SJ  - 2024-03-30 23:12:08 */         [SerializeField] private GameObject hitEffect;
/* @LiF     - 2024-06-03 10:18:55 */         [SerializeField] private float detectionRadius = 0.1f;
/* @LiF     - 2024-06-03 10:45:04 */         
/* @LiF     - 2024-06-03 10:45:04 */         [Separator]
/* @LiF     - 2024-06-03 10:45:04 */         
/* @LiF     - 2024-06-03 10:45:04 */         [SerializeField] private bool isExplosive;
/* @LiF     - 2024-06-03 10:45:04 */         [SerializeField, ConditionalField(nameof(isExplosive))]
/* @LiF     - 2024-06-03 10:45:04 */         private float explosionRadius = 0.1f;
/* @LiF     - 2024-06-03 10:18:55 */ 
/* @Lee SJ  - 2024-04-08 22:31:23 */         private BaseUnitController target;
/* @Lee SJ  - 2024-05-08 18:25:40 */         private BaseUnitController shooter;
/* @Lee SJ  - 2024-03-30 23:12:08 */         private float speed;
/* @Lee SJ  - 2024-03-30 23:12:08 */         private int damage;
/* @Lee SJ  - 2024-03-30 23:12:08 */ 
/* @Lee SJ  - 2024-03-31 15:18:30 */         private Vector3 lastPosition;
/* @Lee SJ  - 2024-03-30 23:12:08 */         private int groundLayer;
/* @Lee SJ  - 2024-03-30 23:12:08 */         private int unitLayer;
/* @Lee SJ  - 2024-05-22 02:05:43 */         private int unitNonSelectableLayer;
/* @Lee SJ  - 2024-03-31 15:18:30 */         private bool isFired;
/* @Lee SJ  - 2024-03-30 23:12:08 */ 
/* @Lee SJ  - 2024-03-31 15:18:30 */         private void OnEnable()
/* @Lee SJ  - 2024-03-30 23:12:08 */         {
/* @LiF     - 2024-05-19 02:04:44 */             isFired = false;
/* @Lee SJ  - 2024-03-30 23:12:08 */             groundLayer = LayerMask.NameToLayer("Ground");
/* @Lee SJ  - 2024-03-30 23:12:08 */             unitLayer = LayerMask.NameToLayer("Unit");
/* @Lee SJ  - 2024-05-22 02:05:43 */             unitNonSelectableLayer = LayerMask.NameToLayer("UnitNonSelectable");
/* @Lee SJ  - 2024-03-31 15:18:30 */             lastPosition = transform.position;
/* @Lee SJ  - 2024-04-03 23:06:27 */             CancelInvoke(nameof(DestroyEffect));
/* @LiF     - 2024-05-19 02:04:44 */             Invoke(nameof(DestroyEffect), 3f);
/* @Lee SJ  - 2024-03-30 23:12:08 */         }
/* @Lee SJ  - 2024-03-30 23:12:08 */ 
/* @Lee SJ  - 2024-03-30 23:12:08 */         private void Update()
/* @Lee SJ  - 2024-03-30 23:12:08 */         {
/* @Lee SJ  - 2024-03-31 15:18:30 */             if (isFired == false)
/* @Lee SJ  - 2024-03-31 15:18:30 */             {
/* @Lee SJ  - 2024-03-31 15:18:30 */                 return;
/* @Lee SJ  - 2024-03-31 15:18:30 */             }
/* @Lee SJ  - 2024-03-31 15:18:30 */ 
/* @LiF     - 2024-06-03 10:18:55 */             if (target != null && target.gameObject.activeInHierarchy && target.Alive)
/* @Lee SJ  - 2024-04-08 21:34:01 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 Vector3 targetDirection = (GetTargetCenter() - transform.position).normalized;
/* @Lee SJ  - 2024-04-08 21:34:01 */                 transform.forward = targetDirection;
/* @Lee SJ  - 2024-04-08 21:34:01 */             }
/* @Lee SJ  - 2024-04-08 21:34:01 */ 
/* @Lee SJ  - 2024-03-31 15:18:30 */             float step = speed * Time.deltaTime;
/* @Lee SJ  - 2024-03-31 15:18:30 */             Vector3 currentPosition = transform.position;
/* @Lee SJ  - 2024-03-31 15:18:30 */             Vector3 direction = currentPosition - lastPosition;
/* @Lee SJ  - 2024-03-31 15:18:30 */             float distance = direction.magnitude;
/* @Lee SJ  - 2024-03-31 15:18:30 */ 
/* @LiF     - 2024-06-03 10:18:55 */             if (distance >= 0)
/* @Lee SJ  - 2024-03-31 15:18:30 */             {
/* @LiF     - 2024-06-03 10:18:55 */                 Collider[] hits = Physics.OverlapSphere(lastPosition, detectionRadius);
/* @LiF     - 2024-06-03 10:45:04 */                 var targetHit = hits
/* @LiF     - 2024-06-03 10:45:04 */                     .FirstOrDefault(hit => (hit.gameObject.layer == unitLayer || hit.gameObject.layer == unitNonSelectableLayer) && 
/* @LiF     - 2024-06-03 10:45:04 */                                            target != null && hit.gameObject == target.gameObject);
/* @LiF     - 2024-06-03 10:45:04 */                 if (targetHit != null)
/* @LiF     - 2024-06-03 10:18:55 */                 {
/* @LiF     - 2024-06-03 10:45:04 */                     HandleCollision(targetHit);
/* @LiF     - 2024-06-03 10:18:55 */                 }
/* @Lee SJ  - 2024-03-31 15:18:30 */             }
/* @Lee SJ  - 2024-03-31 15:18:30 */ 
/* @Lee SJ  - 2024-03-31 15:18:30 */             lastPosition = currentPosition;
/* @Lee SJ  - 2024-03-31 15:18:30 */             transform.Translate(Vector3.forward * step);
/* @Lee SJ  - 2024-03-30 23:12:08 */         }
/* @Lee SJ  - 2024-03-30 23:12:08 */         
/* @Lee SJ  - 2024-04-03 23:06:27 */         public void Fire(Transform targetTransform, Transform shooterTransform, float speedValue, int damageValue)
/* @Lee SJ  - 2024-03-30 23:12:08 */         {
/* @Lee SJ  - 2024-04-08 22:31:23 */             target = targetTransform.GetComponent<BaseUnitController>();
/* @Lee SJ  - 2024-04-08 22:31:23 */             shooter = shooterTransform.GetComponent<BaseUnitController>();
/* @Lee SJ  - 2024-03-30 23:12:08 */             speed = speedValue;
/* @Lee SJ  - 2024-03-30 23:12:08 */             damage = damageValue;
/* @Lee SJ  - 2024-04-03 23:06:27 */ 
/* @LiF     - 2024-05-19 02:04:44 */             float missRate = target.unitData.MissRate;
/* @LiF     - 2024-05-19 02:04:44 */             Vector3 targetDirection = (GetTargetCenter() - transform.position).normalized;
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @Lee SJ  - 2024-04-03 23:06:27 */             if (Random.Range(0f, 100f) < missRate)
/* @Lee SJ  - 2024-04-03 23:06:27 */             {
/* @Lee SJ  - 2024-04-03 23:06:27 */                 target = null;
/* @LiF     - 2024-05-19 02:04:44 */                 Vector3 randomDirection = Random.insideUnitSphere * 0.1f;
/* @Lee SJ  - 2024-04-08 21:34:01 */                 randomDirection -= Vector3.Project(randomDirection, targetDirection);
/* @Lee SJ  - 2024-04-08 21:34:01 */                 targetDirection += randomDirection;
/* @Lee SJ  - 2024-04-03 23:06:27 */             }
/* @Lee SJ  - 2024-04-03 23:06:27 */ 
/* @Lee SJ  - 2024-04-08 21:34:01 */             transform.forward = targetDirection.normalized;
/* @Lee SJ  - 2024-04-03 23:57:51 */ 
/* @LiF     - 2024-06-01 19:11:50 */             GameObject fireFX = ObjectPoolManager.Instance.SpawnObject(firingEffect, shooter.unitModel.transform.position, Quaternion.identity);
/* @LiF     - 2024-06-01 19:11:50 */             fireFX.transform.eulerAngles = shooter.unitModel.transform.eulerAngles;
/* @LiF     - 2024-05-19 02:04:44 */ 
/* @Lee SJ  - 2024-03-31 15:18:30 */             isFired = true;
/* @Lee SJ  - 2024-03-30 23:12:08 */         }
/* @Lee SJ  - 2024-03-31 15:18:30 */ 
/* @LiF     - 2024-06-03 10:18:55 */         private void HandleCollision(Collider hit)
/* @Lee SJ  - 2024-03-30 23:12:08 */         {
/* @LiF     - 2024-06-03 10:45:04 */             if (isExplosive)
/* @Lee SJ  - 2024-03-30 23:12:08 */             {
/* @LiF     - 2024-06-03 10:45:04 */                 Collider[] colliders = Physics.OverlapSphere(hit.transform.position, explosionRadius);
/* @LiF     - 2024-06-03 10:45:04 */                 var unitControllers = colliders
/* @LiF     - 2024-06-03 10:45:04 */                     .Select(unitCollider => unitCollider.GetComponent<BaseUnitController>())
/* @LiF     - 2024-06-03 14:20:46 */                     .Where(unitController => unitController != null && unitController.UnitType != shooter.UnitType);
/* @LiF     - 2024-06-03 10:45:04 */ 
/* @LiF     - 2024-06-03 10:45:04 */                 foreach (var unitController in unitControllers)
/* @LiF     - 2024-06-03 10:45:04 */                 {
/* @LiF     - 2024-06-03 10:45:04 */                     unitController.ReceiveDamage(damage, shooter);
/* @LiF     - 2024-06-03 10:45:04 */                 }
/* @Lee SJ  - 2024-03-30 23:12:08 */             }
/* @LiF     - 2024-06-03 10:45:04 */             else
/* @LiF     - 2024-06-03 10:45:04 */             {
/* @LiF     - 2024-06-03 10:45:04 */                 var unitController = hit.GetComponent<BaseUnitController>();
/* @LiF     - 2024-06-03 10:45:04 */                 if (unitController != null)
/* @LiF     - 2024-06-03 10:45:04 */                 {
/* @LiF     - 2024-06-03 10:45:04 */                     unitController.ReceiveDamage(damage, shooter);
/* @LiF     - 2024-06-03 10:45:04 */                 }
/* @LiF     - 2024-06-03 10:45:04 */             }
/* @LiF     - 2024-06-03 10:45:04 */ 
/* @LiF     - 2024-06-03 10:45:04 */             ObjectPoolManager.Instance.SpawnObject(hitEffect, hit.transform.position, Quaternion.identity);
/* @LiF     - 2024-06-03 10:45:04 */             ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
/* @Lee SJ  - 2024-03-30 23:12:08 */         }
/* @Lee SJ  - 2024-04-03 23:06:27 */         
/* @LiF     - 2024-05-19 02:04:44 */         private Vector3 GetTargetCenter()
/* @LiF     - 2024-05-19 02:04:44 */         {
/* @LiF     - 2024-05-19 02:04:44 */             CapsuleCollider capsuleCollider = target.GetComponent<CapsuleCollider>();
/* @LiF     - 2024-05-19 02:04:44 */             return capsuleCollider != null ? target.transform.TransformPoint(capsuleCollider.center) : target.transform.position;
/* @LiF     - 2024-05-19 02:04:44 */         }
/* @LiF     - 2024-05-19 02:04:44 */         
/* @Lee SJ  - 2024-04-03 23:06:27 */         private void DestroyEffect()
/* @Lee SJ  - 2024-04-03 23:06:27 */         {
/* @LiF     - 2024-05-19 02:04:44 */             if (gameObject.activeInHierarchy)
/* @Lee SJ  - 2024-04-03 23:06:27 */             {
/* @LiF     - 2024-05-19 02:04:44 */                 ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
/* @Lee SJ  - 2024-04-03 23:06:27 */             }
/* @Lee SJ  - 2024-04-03 23:06:27 */         }
/* @LiF     - 2024-06-03 10:18:55 */ 
/* @LiF     - 2024-06-03 10:18:55 */         private void OnDrawGizmos()
/* @LiF     - 2024-06-03 10:18:55 */         {
/* @LiF     - 2024-06-03 10:18:55 */             if (isFired)
/* @LiF     - 2024-06-03 10:18:55 */             {
/* @LiF     - 2024-06-03 10:18:55 */                 Gizmos.color = Color.red;
/* @LiF     - 2024-06-03 10:18:55 */                 Gizmos.DrawWireSphere(transform.position, detectionRadius);
/* @LiF     - 2024-06-03 10:18:55 */             }
/* @LiF     - 2024-06-03 10:45:04 */             
/* @LiF     - 2024-06-03 10:45:04 */             if (isExplosive)
/* @LiF     - 2024-06-03 10:45:04 */             {
/* @LiF     - 2024-06-03 10:45:04 */                 Gizmos.color = Color.yellow;
/* @LiF     - 2024-06-03 10:45:04 */                 Gizmos.DrawWireSphere(transform.position, explosionRadius);
/* @LiF     - 2024-06-03 10:45:04 */             }
/* @LiF     - 2024-06-03 10:18:55 */         }
/* @Lee SJ  - 2024-03-30 23:12:08 */     }
/* @Lee SJ  - 2024-03-31 15:18:30 */ }