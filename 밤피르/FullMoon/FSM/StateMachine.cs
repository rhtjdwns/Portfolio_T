/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-05-03 19:25:37 */ using Unity.Burst;
/* @Lee SJ  - 2024-05-03 19:25:37 */ 
/* @Lee SJ  - 2024-03-26 16:01:50 */ namespace FullMoon.FSM
/* @Lee SJ  - 2024-03-26 16:01:50 */ {
/* @Lee SJ  - 2024-05-03 19:25:37 */     [BurstCompile]
/* @Lee SJ  - 2024-03-26 16:01:50 */     public class StateMachine
/* @Lee SJ  - 2024-03-26 16:01:50 */     {
/* @Lee SJ  - 2024-03-26 16:01:50 */         public IState CurrentState { get; private set; }
/* @Lee SJ  - 2024-03-26 16:01:50 */ 
/* @Lee SJ  - 2024-03-26 16:01:50 */         public void ChangeState(IState newState)
/* @Lee SJ  - 2024-03-26 16:01:50 */         {
/* @Lee SJ  - 2024-03-26 16:01:50 */             CurrentState?.Exit();
/* @Lee SJ  - 2024-03-26 16:01:50 */             CurrentState = newState;
/* @Lee SJ  - 2024-03-26 16:01:50 */             CurrentState?.Enter();
/* @Lee SJ  - 2024-03-26 16:01:50 */         }
/* @Lee SJ  - 2024-03-26 16:01:50 */ 
/* @Lee SJ  - 2024-03-26 16:01:50 */         public void ExecuteCurrentState()
/* @Lee SJ  - 2024-03-26 16:01:50 */         {
/* @Lee SJ  - 2024-03-26 16:01:50 */             CurrentState?.Execute();
/* @Lee SJ  - 2024-03-26 16:01:50 */         }
/* @Lee SJ  - 2024-03-26 16:01:50 */         
/* @Lee SJ  - 2024-03-26 16:01:50 */         public void FixedExecuteCurrentState()
/* @Lee SJ  - 2024-03-26 16:01:50 */         {
/* @Lee SJ  - 2024-03-26 16:01:50 */             CurrentState?.FixedExecute();
/* @Lee SJ  - 2024-03-26 16:01:50 */         }
/* @Lee SJ  - 2024-03-26 16:01:50 */     }
/* @Lee SJ  - 2024-03-26 16:01:50 */ }