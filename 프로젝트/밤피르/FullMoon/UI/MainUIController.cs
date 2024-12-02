/* Git Blame Auto Generated */

/* @LiF     - 2024-04-11 01:43:00 */ using UnityEngine;
/* @LiF     - 2024-04-11 01:43:00 */ using UnityEngine.UIElements;
/* @Lee SJ  - 2024-04-12 20:53:01 */ using UnityEngine.SceneManagement;
/* @Lee SJ  - 2024-04-12 20:53:01 */ using FullMoon.Util;
/* @LiF     - 2024-04-11 01:43:00 */ 
/* @LiF     - 2024-04-11 01:43:00 */ namespace FullMoon.UI
/* @LiF     - 2024-04-11 01:43:00 */ {
/* @Lee SJ  - 2024-04-17 22:24:33 */     [DefaultExecutionOrder(-1)]
/* @LiF     - 2024-04-11 01:43:00 */     public class MainUIController : ComponentSingleton<MainUIController>
/* @LiF     - 2024-04-11 01:43:00 */     {
/* @Lee SJ  - 2024-04-17 22:24:33 */         public Button RetryButton { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         public Button ExitButton { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */ 
/* @LiF     - 2024-06-02 03:38:55 */         #region Phase
/* @LiF     - 2024-05-29 11:33:09 */         
/* @LiF     - 2024-06-01 18:09:32 */         public VisualElement BattlePhase { get; private set; }
/* @LiF     - 2024-06-01 18:09:32 */         public VisualElement RestPhase { get; private set; }
/* @LiF     - 2024-06-03 19:45:11 */         public VisualElement VictoryPhase { get; private set; }
/* @LiF     - 2024-06-01 18:09:32 */         public TextElement BattleDetailText { get; private set; }
/* @LiF     - 2024-06-01 18:09:32 */         public TextElement RestDetailText { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */ 
/* @LiF     - 2024-06-02 03:38:55 */         #endregion
/* @LiF     - 2024-06-02 03:38:55 */ 
/* @LiF     - 2024-06-02 03:38:55 */         #region Upper Bar
/* @LiF     - 2024-06-02 03:38:55 */ 
/* @LiF     - 2024-06-02 03:38:55 */         public VisualElement BattleIcon { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         public VisualElement RestIcon { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         public TextElement DayCountText { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         public TextElement CommonCountText { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         public TextElement WoodCountText { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         public TextElement IronCountText { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */         public TextElement EnemyCountText { get; private set; }
/* @LiF     - 2024-06-02 03:38:55 */ 
/* @LiF     - 2024-06-02 03:38:55 */         #endregion
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         public int CommonAmount { get; set; }
/* @LiF     - 2024-06-02 03:38:55 */         public int WoodAmount { get; set; }
/* @LiF     - 2024-06-02 03:38:55 */         public int IronAmount { get; set; }
/* @LiF     - 2024-06-02 03:38:55 */         public int EnemyAmount { get; set; }
/* @Lee SJ  - 2024-04-17 22:24:33 */         
/* @Lee SJ  - 2024-05-08 18:25:40 */         private void OnEnable()
/* @LiF     - 2024-04-11 01:43:00 */         {
/* @LiF     - 2024-04-11 01:43:00 */             VisualElement root = GetComponent<UIDocument>().rootVisualElement;
/* @LiF     - 2024-04-14 11:43:39 */             
/* @Lee SJ  - 2024-04-17 22:24:33 */             RetryButton = root.Q<Button>("RetryButton");
/* @Lee SJ  - 2024-04-17 22:24:33 */             RetryButton.RegisterCallback<ClickEvent>(Retry);
/* @LiF     - 2024-06-02 03:38:55 */             ExitButton = root.Q<Button>("ExitButton");
/* @LiF     - 2024-06-02 03:38:55 */             ExitButton.RegisterCallback<ClickEvent>(Exit);
/* @LiF     - 2024-05-29 11:33:09 */             
/* @LiF     - 2024-06-01 18:09:32 */             BattlePhase = root.Q<VisualElement>("BattlePhase");
/* @LiF     - 2024-06-01 18:09:32 */             RestPhase = root.Q<VisualElement>("RestPhase");
/* @LiF     - 2024-06-03 19:45:11 */             VictoryPhase = root.Q<VisualElement>("VictoryPhase");
/* @LiF     - 2024-06-01 18:09:32 */             BattleDetailText = root.Q<TextElement>("BattleDetailText");
/* @LiF     - 2024-06-01 18:09:32 */             RestDetailText = root.Q<TextElement>("RestDetailText");
/* @LiF     - 2024-06-01 18:09:32 */             BattlePhase.SetVisible(false);
/* @LiF     - 2024-06-01 18:09:32 */             RestPhase.SetVisible(false);
/* @LiF     - 2024-06-03 19:45:11 */             VictoryPhase.SetVisible(false);
/* @LiF     - 2024-06-02 03:38:55 */             
/* @LiF     - 2024-06-02 03:38:55 */             BattleIcon = root.Q<VisualElement>("BattleIcon");
/* @LiF     - 2024-06-02 03:38:55 */             RestIcon = root.Q<VisualElement>("RestIcon");
/* @LiF     - 2024-06-02 03:38:55 */             BattleIcon.SetVisible(false);
/* @LiF     - 2024-06-02 03:38:55 */             RestIcon.SetVisible(true);
/* @LiF     - 2024-06-02 03:38:55 */             
/* @LiF     - 2024-06-02 03:38:55 */             DayCountText = root.Q<TextElement>("DayCountText");
/* @LiF     - 2024-06-02 03:38:55 */             CommonCountText = root.Q<TextElement>("CommonCountText");
/* @LiF     - 2024-06-02 03:38:55 */             WoodCountText = root.Q<TextElement>("WoodCountText");
/* @LiF     - 2024-06-02 03:38:55 */             IronCountText = root.Q<TextElement>("IronCountText");
/* @LiF     - 2024-06-02 03:38:55 */             EnemyCountText = root.Q<TextElement>("EnemyCountText");
/* @LiF     - 2024-06-02 03:38:55 */         }
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         public void ChangeCommonAmount(int count)
/* @LiF     - 2024-06-02 03:38:55 */         {
/* @LiF     - 2024-06-02 03:38:55 */             CommonAmount += count;
/* @LiF     - 2024-06-02 03:38:55 */             CommonCountText.text = $"{CommonAmount}";
/* @LiF     - 2024-06-02 03:38:55 */         }
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         public void ChangeWoodAmount(int count)
/* @LiF     - 2024-06-02 03:38:55 */         {
/* @LiF     - 2024-06-02 03:38:55 */             WoodAmount += count;
/* @LiF     - 2024-06-02 03:38:55 */             WoodCountText.text = $"{WoodAmount}";
/* @LiF     - 2024-06-02 03:38:55 */         }
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         public void ChangeIronAmount(int count)
/* @LiF     - 2024-06-02 03:38:55 */         {
/* @LiF     - 2024-06-02 03:38:55 */             IronAmount += count;
/* @LiF     - 2024-06-02 03:38:55 */             IronCountText.text = $"{IronAmount}";
/* @LiF     - 2024-06-02 03:38:55 */         }
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         public void ChangeEnemyAmount(int count)
/* @LiF     - 2024-06-02 03:38:55 */         {
/* @LiF     - 2024-06-02 03:38:55 */             EnemyAmount += count;
/* @LiF     - 2024-06-02 03:38:55 */             EnemyCountText.text = $"{EnemyAmount}";
/* @LiF     - 2024-04-11 01:43:00 */         }
/* @LiF     - 2024-04-14 11:43:39 */         
/* @LiF     - 2024-04-14 11:43:39 */         private void Retry(ClickEvent evt)
/* @Lee SJ  - 2024-04-12 20:21:54 */         {
/* @Lee SJ  - 2024-04-12 20:21:54 */             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
/* @Lee SJ  - 2024-04-12 20:21:54 */         }
/* @LiF     - 2024-06-02 03:38:55 */         
/* @LiF     - 2024-06-02 03:38:55 */         private void Exit(ClickEvent evt)
/* @LiF     - 2024-06-02 03:38:55 */         {
/* @LiF     - 2024-06-02 03:38:55 */ #if UNITY_EDITOR
/* @LiF     - 2024-06-02 03:38:55 */             UnityEditor.EditorApplication.isPlaying = false;
/* @LiF     - 2024-06-02 03:38:55 */ #else
/* @LiF     - 2024-06-02 03:38:55 */             Application.Quit();
/* @LiF     - 2024-06-02 03:38:55 */ #endif
/* @LiF     - 2024-06-02 03:38:55 */         }
/* @LiF     - 2024-04-11 01:43:00 */     }
/* @LiF     - 2024-04-11 01:43:00 */ }
