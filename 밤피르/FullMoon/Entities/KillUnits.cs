/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-07 16:52:51 */ using System.Linq;
/* @Lee SJ  - 2024-05-07 16:52:51 */ using FullMoon.Entities.Unit;
/* @Lee SJ  - 2024-05-07 16:52:51 */ using MyBox;
/* @Lee SJ  - 2024-05-07 16:52:51 */ using UnityEngine;
/* @Lee SJ  - 2024-05-07 16:52:51 */ 
/* @Lee SJ  - 2024-05-07 16:52:51 */ namespace FullMoon.Entities
/* @Lee SJ  - 2024-05-07 16:52:51 */ {
/* @Lee SJ  - 2024-05-07 16:52:51 */     public class KillUnits : MonoBehaviour
/* @Lee SJ  - 2024-05-07 16:52:51 */     {
/* @Lee SJ  - 2024-05-07 16:52:51 */         [ButtonMethod]
/* @LiF     - 2024-05-30 13:40:00 */         public void KillAllUnits()
/* @Lee SJ  - 2024-05-07 16:52:51 */         {
/* @Lee SJ  - 2024-05-07 16:52:51 */             var units = GameObject.FindGameObjectsWithTag("Player")
/* @Lee SJ  - 2024-05-07 16:52:51 */                 .Where(unit => unit is not null 
/* @Lee SJ  - 2024-05-07 16:52:51 */                                && unit.activeInHierarchy 
/* @Lee SJ  - 2024-05-07 16:52:51 */                                && unit.GetComponent<BaseUnitController>().Alive)
/* @Lee SJ  - 2024-05-07 16:52:51 */                 .ToList();
/* @Lee SJ  - 2024-05-07 16:52:51 */             
/* @Lee SJ  - 2024-05-07 16:52:51 */             foreach (var unit in units)
/* @Lee SJ  - 2024-05-07 16:52:51 */             {
/* @Lee SJ  - 2024-05-07 16:52:51 */                 unit.GetComponent<BaseUnitController>().Die();
/* @Lee SJ  - 2024-05-07 16:52:51 */             }
/* @Lee SJ  - 2024-05-07 16:52:51 */         }
/* @Lee SJ  - 2024-05-07 16:52:51 */         
/* @Lee SJ  - 2024-05-07 16:52:51 */         [ButtonMethod]
/* @LiF     - 2024-05-30 13:40:00 */         public void KillAllEnemies()
/* @Lee SJ  - 2024-05-07 16:52:51 */         {
/* @Lee SJ  - 2024-05-07 16:52:51 */             var units = GameObject.FindGameObjectsWithTag("Enemy")
/* @Lee SJ  - 2024-05-07 16:52:51 */                 .Where(unit => unit is not null 
/* @Lee SJ  - 2024-05-07 16:52:51 */                                && unit.activeInHierarchy 
/* @Lee SJ  - 2024-05-07 16:52:51 */                                && unit.GetComponent<BaseUnitController>().Alive)
/* @Lee SJ  - 2024-05-07 16:52:51 */                 .ToList();
/* @Lee SJ  - 2024-05-07 16:52:51 */             
/* @Lee SJ  - 2024-05-07 16:52:51 */             foreach (var unit in units)
/* @Lee SJ  - 2024-05-07 16:52:51 */             {
/* @Lee SJ  - 2024-05-07 16:52:51 */                 unit.GetComponent<BaseUnitController>().Die();
/* @Lee SJ  - 2024-05-07 16:52:51 */             }
/* @Lee SJ  - 2024-05-07 16:52:51 */         }
/* @Lee SJ  - 2024-05-07 16:52:51 */     }
/* @Lee SJ  - 2024-05-07 16:52:51 */ }