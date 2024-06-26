/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-03-26 16:38:08 */ using UnityEngine;
/* @Lee SJ  - 2024-03-26 16:38:08 */ using UnityEngine.AI;
/* @Lee SJ  - 2024-03-26 16:38:08 */ 
/* @Lee SJ  - 2024-03-26 16:38:08 */ namespace FullMoon.Interfaces
/* @Lee SJ  - 2024-03-26 16:38:08 */ {
/* @Lee SJ  - 2024-03-26 16:38:08 */     public interface INavigation
/* @Lee SJ  - 2024-03-26 16:38:08 */     {
/* @Lee SJ  - 2024-03-26 16:38:08 */         NavMeshAgent Agent { get; set; }
/* @Lee SJ  - 2024-03-28 23:40:16 */         Vector3 LatestDestination { get; set; }
/* @Lee SJ  - 2024-03-28 23:40:16 */         void MoveToPosition(Vector3 location);
/* @Lee SJ  - 2024-03-26 16:38:08 */     }
/* @Lee SJ  - 2024-03-26 16:38:08 */ }
/* @Lee SJ  - 2024-03-26 16:38:08 */ 
