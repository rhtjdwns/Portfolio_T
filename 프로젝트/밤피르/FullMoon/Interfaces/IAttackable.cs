/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-03-26 16:38:08 */ using UnityEngine;
/* @Lee SJ  - 2024-03-27 22:58:51 */ using FullMoon.Entities.Unit;
/* @Lee SJ  - 2024-03-27 22:58:51 */ using System.Collections.Generic;
/* @Lee SJ  - 2024-05-07 16:52:51 */ using Cysharp.Threading.Tasks;
/* @Lee SJ  - 2024-03-26 16:38:08 */ 
/* @Lee SJ  - 2024-03-26 16:38:08 */ namespace FullMoon.Interfaces
/* @Lee SJ  - 2024-03-26 16:38:08 */ {
/* @Lee SJ  - 2024-03-26 16:38:08 */     public interface IAttackable
/* @Lee SJ  - 2024-03-26 16:38:08 */     {
/* @Lee SJ  - 2024-03-27 22:58:51 */         void EnterViewRange(Collider unit);
/* @Lee SJ  - 2024-03-27 22:58:51 */         void ExitViewRange(Collider unit);
/* @Lee SJ  - 2024-05-07 16:52:51 */         UniTaskVoid ExecuteAttack(Transform location);
/* @Lee SJ  - 2024-03-26 16:38:08 */     }
/* @Lee SJ  - 2024-03-26 16:38:08 */ }