/* Git Blame Auto Generated */

/* @LiF     - 2024-03-27 02:08:23 */ using FullMoon.Entities.Unit;
/* @Lee SJ  - 2024-03-26 16:38:08 */ 
/* @Lee SJ  - 2024-03-26 16:38:08 */ namespace FullMoon.Interfaces
/* @Lee SJ  - 2024-03-26 16:38:08 */ {
/* @Lee SJ  - 2024-03-26 16:38:08 */     public interface IDamageable
/* @Lee SJ  - 2024-03-26 16:38:08 */     {
/* @Lee SJ  - 2024-03-26 16:38:08 */         int Hp { get; set; }
/* @LiF     - 2024-03-27 02:08:23 */         void ReceiveDamage(int amount, BaseUnitController attacker);
/* @LiF     - 2024-04-11 01:52:04 */         void Die();
/* @Lee SJ  - 2024-03-26 16:38:08 */     }
/* @Lee SJ  - 2024-03-26 16:38:08 */ }