/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-06-02 20:59:55 */ using MyBox;
/* @Lee SJ    - 2024-05-25 15:34:29 */ using System;
/* @Lee SJ    - 2024-05-25 15:34:29 */ using System.Linq;
/* @Lee SJ    - 2024-06-02 20:59:55 */ using System.Threading;
/* @Lee SJ    - 2024-06-02 20:59:55 */ using System.Collections.Generic;
/* @Lee SJ    - 2024-05-06 22:26:59 */ using Cysharp.Threading.Tasks;
/* @LiF       - 2024-06-03 16:41:20 */ using FullMoon.Camera;
/* @Lee SJ    - 2024-06-02 20:59:55 */ using UnityEngine;
/* @Lee SJ    - 2024-06-02 20:59:55 */ using UnityEngine.UI;
/* @LiF       - 2024-05-29 11:33:09 */ using FullMoon.UI;
/* @Lee SJ    - 2024-05-06 22:26:59 */ using FullMoon.Util;
/* @Lee SJ    - 2024-06-02 20:59:55 */ using FullMoon.Entities.Unit;
/* @Lee SJ    - 2024-05-06 22:26:59 */ 
/* @Lee SJ    - 2024-05-25 16:45:42 */ namespace FullMoon.Entities
/* @Lee SJ    - 2024-05-06 22:26:59 */ {
/* @Lee SJ    - 2024-05-25 15:34:29 */     [Serializable]
/* @Lee SJ    - 2024-05-25 15:34:29 */     public class EnemyDetail
/* @Lee SJ    - 2024-05-25 15:34:29 */     {
/* @Lee SJ    - 2024-05-25 15:34:29 */         public GameObject enemy;
/* @Lee SJ    - 2024-05-25 15:34:29 */         public int count = 1;
/* @Lee SJ    - 2024-05-29 14:59:18 */         public float spawnInterval;
/* @Lee SJ    - 2024-05-25 15:34:29 */         [DefinedValues("Random", "Left", "Right", "Up", "Down")] public string location = "Random";
/* @Lee SJ    - 2024-05-25 15:34:29 */     }
/* @Lee SJ    - 2024-05-25 15:34:29 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */     [Serializable]
/* @Lee SJ    - 2024-05-25 15:34:29 */     public class Wave
/* @Lee SJ    - 2024-05-25 15:34:29 */     {
/* @Lee SJ    - 2024-05-25 15:34:29 */         public int level = 1;
/* @Lee SJ    - 2024-05-25 15:34:29 */         public List<EnemyDetail> enemyDetails;
/* @Lee SJ    - 2024-05-25 15:34:29 */     }
/* @Lee SJ    - 2024-05-25 15:34:29 */     
/* @Lee SJ    - 2024-06-02 20:59:55 */     [Serializable]
/* @Lee SJ    - 2024-06-02 20:59:55 */     public class ButtonUnlock
/* @Lee SJ    - 2024-06-02 20:59:55 */     {
/* @rhtjdwns  - 2024-06-03 12:16:21 */         public string buttonName;
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         [Separator]
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         public bool changeButtonText;
/* @LiF       - 2024-06-03 16:41:20 */         [ConditionalField(nameof(changeButtonText))] public string enableText;
/* @LiF       - 2024-06-03 16:41:20 */         [ConditionalField(nameof(changeButtonText))] public string disableText;
/* @LiF       - 2024-06-03 16:41:20 */ 
/* @LiF       - 2024-06-03 16:41:20 */         [Separator]
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         public Color selectedColor = Color.green;
/* @LiF       - 2024-06-03 16:41:20 */         public Color unselectedColor = Color.white;
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         [Separator]
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         public BuildingType buildingType;
/* @Lee SJ    - 2024-06-02 20:59:55 */         public int unlockWave = 1;
/* @Lee SJ    - 2024-06-02 20:59:55 */         public Button unlockButton;
/* @Lee SJ    - 2024-06-02 20:59:55 */     }
/* @Lee SJ    - 2024-06-02 20:59:55 */     
/* @Lee SJ    - 2024-05-25 16:45:42 */     public class WaveManager : MonoBehaviour
/* @Lee SJ    - 2024-05-06 22:26:59 */     {
/* @Lee SJ    - 2024-05-25 15:34:29 */         [ReadOnly] private int currentLevel;
/* @LiF       - 2024-05-29 11:33:09 */         [SerializeField] private float spawnDistance = 20f;
/* @LiF       - 2024-05-29 11:33:09 */         [SerializeField] private float spawnInterval = 15f;
/* @Lee SJ    - 2024-06-02 23:52:11 */ 
/* @Lee SJ    - 2024-06-02 23:52:11 */         [Separator] 
/* @Lee SJ    - 2024-06-02 23:52:11 */         
/* @Lee SJ    - 2024-06-02 23:52:11 */         [SerializeField] private GameObject CraftingButton;
/* @Lee SJ    - 2024-06-02 23:52:11 */         
/* @Lee SJ    - 2024-06-02 23:52:11 */         [Separator]
/* @Lee SJ    - 2024-06-02 23:52:11 */         
/* @Lee SJ    - 2024-06-02 20:59:55 */         [SerializeField] private List<ButtonUnlock> buildingUnlock;
/* @Lee SJ    - 2024-05-25 15:34:29 */         [SerializeField] private List<Wave> waves;
/* @LiF       - 2024-06-02 17:19:19 */ 
/* @LiF       - 2024-06-02 17:19:19 */         private CancellationTokenSource cancellationTokenSource;
/* @Lee SJ    - 2024-05-25 16:45:42 */         
/* @Lee SJ    - 2024-05-25 16:45:42 */         private readonly List<BaseUnitController> enemyWaitList = new();
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         private CameraController cameraController;
/* @Lee SJ    - 2024-05-08 18:25:40 */ 
/* @Lee SJ    - 2024-05-06 22:26:59 */         private void Start()
/* @Lee SJ    - 2024-05-06 22:26:59 */         {
/* @LiF       - 2024-06-02 17:19:19 */             cancellationTokenSource = new CancellationTokenSource();
/* @LiF       - 2024-06-03 16:41:20 */             cameraController = FindObjectOfType<CameraController>();
/* @LiF       - 2024-06-03 16:41:20 */             DisableAllUnlockButton();
/* @LiF       - 2024-06-03 16:41:20 */             buildingUnlock.ForEach(b =>  b.unlockButton.onClick.AddListener(() => OnCraftingButtonClicked(b)));
/* @LiF       - 2024-06-02 17:19:19 */             SpawnWaveAsync(cancellationTokenSource.Token).Forget();
/* @LiF       - 2024-06-02 17:19:19 */         }
/* @LiF       - 2024-06-02 17:19:19 */ 
/* @LiF       - 2024-06-02 17:19:19 */         private void OnDestroy()
/* @LiF       - 2024-06-02 17:19:19 */         {
/* @LiF       - 2024-06-02 17:19:19 */             cancellationTokenSource?.Cancel();
/* @LiF       - 2024-06-02 17:19:19 */             cancellationTokenSource?.Dispose();
/* @Lee SJ    - 2024-05-06 22:26:59 */         }
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         public void DisableAllUnlockButton()
/* @LiF       - 2024-06-03 16:41:20 */         {
/* @LiF       - 2024-06-03 16:41:20 */             cameraController.CreateTileSetting(false, BuildingType.None);
/* @LiF       - 2024-06-03 16:41:20 */             foreach (var button in buildingUnlock)
/* @LiF       - 2024-06-03 16:41:20 */             {
/* @LiF       - 2024-06-03 16:41:20 */                 button.unlockButton.GetComponent<Image>().color = button.unselectedColor;
/* @LiF       - 2024-06-03 16:41:20 */             }
/* @LiF       - 2024-06-03 16:41:20 */         }
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         private void OnCraftingButtonClicked(ButtonUnlock buttonUnlock)
/* @LiF       - 2024-06-03 16:41:20 */         {
/* @LiF       - 2024-06-03 16:41:20 */             DisableAllUnlockButton();
/* @LiF       - 2024-06-03 16:41:20 */             if (buttonUnlock.unlockWave > currentLevel + 1)
/* @LiF       - 2024-06-03 16:41:20 */             {
/* @LiF       - 2024-06-03 18:58:33 */                 ToastManager.Instance.ShowToast($"{buttonUnlock.buttonName} 건설은 웨이브 <size=54>{buttonUnlock.unlockWave}</size>에서 해제됩니다", "#FF7C7F");
/* @LiF       - 2024-06-03 16:41:20 */                 return;
/* @LiF       - 2024-06-03 16:41:20 */             }
/* @LiF       - 2024-06-03 16:41:20 */             buttonUnlock.unlockButton.GetComponent<Image>().color = buttonUnlock.selectedColor;
/* @LiF       - 2024-06-03 16:41:20 */             cameraController.CreateTileSetting(true, buttonUnlock.buildingType);
/* @LiF       - 2024-06-03 16:41:20 */         }
/* @LiF       - 2024-06-03 16:41:20 */         
/* @LiF       - 2024-06-03 16:41:20 */         private void UpdateCraftingButton()
/* @LiF       - 2024-06-03 16:41:20 */         {
/* @LiF       - 2024-06-03 16:41:20 */             foreach (var button in buildingUnlock)
/* @LiF       - 2024-06-03 16:41:20 */             {
/* @LiF       - 2024-06-03 16:41:20 */                 if (button.unlockWave <= currentLevel + 1)
/* @LiF       - 2024-06-03 16:41:20 */                 {
/* @LiF       - 2024-06-03 16:41:20 */                     if (button.changeButtonText)
/* @LiF       - 2024-06-03 16:41:20 */                     {
/* @LiF       - 2024-06-03 16:41:20 */                         button.unlockButton.GetComponentInChildren<Text>().text = $"{button.buttonName}\n{button.enableText}";
/* @LiF       - 2024-06-03 16:41:20 */                     }
/* @LiF       - 2024-06-03 16:41:20 */                 }
/* @LiF       - 2024-06-03 16:41:20 */                 else
/* @LiF       - 2024-06-03 16:41:20 */                 {
/* @LiF       - 2024-06-03 16:41:20 */                     if (button.changeButtonText)
/* @LiF       - 2024-06-03 16:41:20 */                     {
/* @LiF       - 2024-06-03 16:41:20 */                         button.unlockButton.GetComponentInChildren<Text>().text = $"{button.buttonName}\n<color=\"red\">{button.disableText}</color>";
/* @LiF       - 2024-06-03 16:41:20 */                     }
/* @LiF       - 2024-06-03 16:41:20 */                 }
/* @LiF       - 2024-06-03 16:41:20 */             }
/* @LiF       - 2024-06-03 16:41:20 */         }
/* @Lee SJ    - 2024-05-06 22:26:59 */ 
/* @LiF       - 2024-06-02 17:19:19 */         private async UniTaskVoid SpawnWaveAsync(CancellationToken cancellationToken)
/* @Lee SJ    - 2024-05-06 22:26:59 */         {
/* @LiF       - 2024-06-02 17:19:19 */             await UniTask.NextFrame(cancellationToken);
/* @Lee SJ    - 2024-05-25 15:34:29 */             while (currentLevel < waves.Max(w => w.level))
/* @Lee SJ    - 2024-05-06 22:26:59 */             {
/* @Lee SJ    - 2024-06-02 20:59:55 */                 if (cancellationToken.IsCancellationRequested)
/* @Lee SJ    - 2024-06-02 20:59:55 */                 {
/* @Lee SJ    - 2024-06-02 20:59:55 */                     break;
/* @Lee SJ    - 2024-06-02 20:59:55 */                 }
/* @LiF       - 2024-06-02 17:19:19 */ 
/* @LiF       - 2024-05-29 11:33:09 */                 bool enemyAlive = enemyWaitList.Any(e => e.Alive);
/* @LiF       - 2024-05-29 11:33:09 */                 
/* @LiF       - 2024-05-29 11:33:09 */                 if (enemyAlive)
/* @LiF       - 2024-05-29 11:33:09 */                 {
/* @LiF       - 2024-06-02 17:19:19 */                     await UniTask.DelayFrame(1, cancellationToken: cancellationToken);
/* @LiF       - 2024-05-29 11:33:09 */                     continue;
/* @LiF       - 2024-05-29 11:33:09 */                 }
/* @LiF       - 2024-05-29 11:33:09 */                 
/* @LiF       - 2024-05-29 11:33:09 */                 enemyWaitList.Clear();
/* @LiF       - 2024-05-29 11:33:09 */                 
/* @Lee SJ    - 2024-06-02 23:52:11 */                 CraftingButton.SetActive(true);
/* @LiF       - 2024-06-03 19:45:11 */                 
/* @LiF       - 2024-06-03 16:41:20 */                 UpdateCraftingButton();
/* @Lee SJ    - 2024-06-02 20:59:55 */                 
/* @LiF       - 2024-06-03 19:45:11 */                 if (currentLevel == 10)
/* @LiF       - 2024-06-03 19:45:11 */                 {
/* @LiF       - 2024-06-03 19:45:11 */                     MainUIController.Instance.VictoryPhase.SetVisible(true, 1f);
/* @LiF       - 2024-06-03 19:45:11 */                     await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: cancellationToken);
/* @LiF       - 2024-06-03 19:45:11 */                     MainUIController.Instance.VictoryPhase.SetVisible(false, 1f);
/* @LiF       - 2024-06-03 19:45:11 */                     await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellationToken);
/* @LiF       - 2024-06-03 19:45:11 */                 }
/* @LiF       - 2024-06-03 19:45:11 */                 
/* @LiF       - 2024-06-03 19:45:11 */                 MainUIController.Instance.DayCountText.text = $"{currentLevel + 1}";
/* @LiF       - 2024-06-03 19:45:11 */                 
/* @LiF       - 2024-06-02 17:19:19 */                 await DisplayCountdown(spawnInterval, cancellationToken);
/* @LiF       - 2024-05-29 11:33:09 */                 
/* @Lee SJ    - 2024-06-02 23:52:11 */                 CraftingButton.SetActive(false);
/* @LiF       - 2024-06-03 18:58:33 */                 DisableAllUnlockButton();
/* @Lee SJ    - 2024-06-02 23:52:11 */                 
/* @Lee SJ    - 2024-05-25 15:34:29 */                 currentLevel++;
/* @Lee SJ    - 2024-05-25 15:34:29 */                 var currentWave = GetRandomWave();
/* @Lee SJ    - 2024-06-02 23:52:11 */                 SpawnWaveTextAsync(5f, cancellationToken).Forget();
/* @LiF       - 2024-06-02 17:19:19 */                 await SpawnEnemies(currentWave, cancellationToken);
/* @Lee SJ    - 2024-05-06 22:26:59 */             }
/* @Lee SJ    - 2024-05-06 22:26:59 */         }
/* @LiF       - 2024-05-29 11:33:09 */         
/* @LiF       - 2024-06-02 17:19:19 */         private async UniTaskVoid SpawnWaveTextAsync(float displayTime, CancellationToken cancellationToken)
/* @LiF       - 2024-05-29 11:33:09 */         {
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.BattleIcon.SetVisible(true);
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.RestIcon.SetVisible(false);
/* @Lee SJ    - 2024-06-02 23:52:11 */             MainUIController.Instance.BattlePhase.SetVisible(true, 1f);
/* @LiF       - 2024-06-02 00:25:34 */             MainUIController.Instance.BattleDetailText.text = $"WAVE {currentLevel:00}";
/* @LiF       - 2024-06-02 17:19:19 */             await UniTask.Delay(TimeSpan.FromSeconds(displayTime), cancellationToken: cancellationToken);
/* @Lee SJ    - 2024-06-02 23:52:11 */             MainUIController.Instance.BattlePhase.SetVisible(false, 1f);
/* @LiF       - 2024-05-29 11:33:09 */         }
/* @LiF       - 2024-05-29 11:33:09 */ 
/* @LiF       - 2024-06-02 17:19:19 */         private async UniTask DisplayCountdown(float interval, CancellationToken cancellationToken)
/* @LiF       - 2024-05-29 11:33:09 */         {
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.BattleIcon.SetVisible(false);
/* @LiF       - 2024-06-02 03:38:55 */             MainUIController.Instance.RestIcon.SetVisible(true);
/* @Lee SJ    - 2024-06-02 23:52:11 */             MainUIController.Instance.RestPhase.SetVisible(true, 1f);
/* @LiF       - 2024-06-01 18:09:32 */             MainUIController.Instance.RestDetailText.text = $"다음 전투까지 {interval:F1}초";
/* @LiF       - 2024-06-02 17:19:19 */             await UniTask.Delay(TimeSpan.FromSeconds(3f), cancellationToken: cancellationToken);
/* @Lee SJ    - 2024-06-02 23:52:11 */             MainUIController.Instance.RestPhase.SetVisible(false, 1f);
/* @Lee SJ    - 2024-05-29 22:29:57 */             
/* @Lee SJ    - 2024-05-29 22:29:57 */             float remainingTime = interval;
/* @LiF       - 2024-05-29 11:33:09 */ 
/* @LiF       - 2024-05-29 11:33:09 */             while (remainingTime > 0)
/* @LiF       - 2024-05-29 11:33:09 */             {
/* @LiF       - 2024-06-02 17:19:19 */                 if (cancellationToken.IsCancellationRequested) break;
/* @LiF       - 2024-06-02 17:19:19 */ 
/* @Lee SJ    - 2024-05-29 22:29:57 */                 if (remainingTime > 5f)
/* @Lee SJ    - 2024-05-29 22:29:57 */                 {
/* @LiF       - 2024-06-02 17:19:19 */                     await UniTask.DelayFrame(1, cancellationToken: cancellationToken);
/* @Lee SJ    - 2024-05-29 22:29:57 */                     remainingTime -= Time.deltaTime;
/* @Lee SJ    - 2024-05-29 22:29:57 */                     continue;
/* @Lee SJ    - 2024-05-29 22:29:57 */                 }
/* @Lee SJ    - 2024-06-02 23:52:11 */ 
/* @Lee SJ    - 2024-06-02 23:52:11 */                 MainUIController.Instance.RestPhase.SetVisible(!(remainingTime <= 1f), 1f);
/* @Lee SJ    - 2024-06-02 23:52:11 */ 
/* @LiF       - 2024-06-01 18:09:32 */                 MainUIController.Instance.RestDetailText.text = $"다음 전투까지 {remainingTime:F1}초";
/* @LiF       - 2024-06-02 17:19:19 */                 await UniTask.DelayFrame(1, cancellationToken: cancellationToken);
/* @LiF       - 2024-05-29 11:33:09 */                 remainingTime -= Time.deltaTime;
/* @LiF       - 2024-05-29 11:33:09 */             }
/* @LiF       - 2024-05-29 11:33:09 */         }
/* @Lee SJ    - 2024-05-06 22:26:59 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */         private Wave GetRandomWave()
/* @Lee SJ    - 2024-05-25 15:34:29 */         {
/* @Lee SJ    - 2024-05-25 15:34:29 */             var currentLevelWaves = waves.Where(w => w.level == currentLevel).ToList();
/* @Lee SJ    - 2024-05-25 15:34:29 */             return currentLevelWaves[UnityEngine.Random.Range(0, currentLevelWaves.Count)];
/* @Lee SJ    - 2024-05-25 15:34:29 */         }
/* @Lee SJ    - 2024-05-25 15:34:29 */ 
/* @LiF       - 2024-06-02 17:19:19 */         private async UniTask SpawnEnemies(Wave wave, CancellationToken cancellationToken)
/* @Lee SJ    - 2024-05-06 22:26:59 */         {
/* @LiF       - 2024-06-03 19:45:11 */             int enemyCount = wave.enemyDetails.Sum(e => e.count);
/* @LiF       - 2024-06-03 19:45:11 */             MainUIController.Instance.ChangeEnemyAmount(enemyCount);
/* @LiF       - 2024-06-03 19:45:11 */             
/* @Lee SJ    - 2024-05-25 15:34:29 */             foreach (var enemyDetail in wave.enemyDetails)
/* @Lee SJ    - 2024-05-06 22:26:59 */             {
/* @Lee SJ    - 2024-05-25 15:34:29 */                 for (int i = 0; i < enemyDetail.count; i++)
/* @Lee SJ    - 2024-05-25 15:34:29 */                 {
/* @LiF       - 2024-06-02 17:19:19 */                     if (cancellationToken.IsCancellationRequested) break;
/* @LiF       - 2024-06-02 17:19:19 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */                     Vector3 spawnPosition = GetSpawnPosition(enemyDetail.location);
/* @Lee SJ    - 2024-05-25 15:34:29 */                     var unit = ObjectPoolManager.Instance.SpawnObject(enemyDetail.enemy, spawnPosition, Quaternion.identity).GetComponent<BaseUnitController>();
/* @Lee SJ    - 2024-05-25 16:45:42 */                     enemyWaitList.Add(unit);
/* @LiF       - 2024-06-02 17:19:19 */                     await UniTask.NextFrame(cancellationToken);
/* @Lee SJ    - 2024-05-25 16:45:42 */                     unit.MoveToPosition(transform.position);
/* @Lee SJ    - 2024-05-25 16:45:42 */                 }
/* @LiF       - 2024-06-03 21:40:38 */                 await UniTask.Delay(TimeSpan.FromSeconds(enemyDetail.spawnInterval), cancellationToken: cancellationToken);
/* @Lee SJ    - 2024-05-08 18:25:40 */             }
/* @Lee SJ    - 2024-05-25 15:34:29 */         }
/* @Lee SJ    - 2024-05-08 18:25:40 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */         private Vector3 GetSpawnPosition(string location)
/* @Lee SJ    - 2024-05-25 15:34:29 */         {
/* @Lee SJ    - 2024-05-25 15:34:29 */             Vector3 direction = Vector3.zero;
/* @Lee SJ    - 2024-05-08 18:25:40 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */             float randomXY = UnityEngine.Random.Range(-spawnDistance, spawnDistance);
/* @Lee SJ    - 2024-05-25 15:34:29 */             Vector3 randomSpawnPosition = Vector3.zero;
/* @Lee SJ    - 2024-05-25 15:34:29 */             
/* @Lee SJ    - 2024-05-25 15:34:29 */             switch (location)
/* @Lee SJ    - 2024-05-08 18:25:40 */             {
/* @Lee SJ    - 2024-05-25 15:34:29 */                 case "Left":
/* @Lee SJ    - 2024-05-25 15:34:29 */                     direction = Vector3.left;
/* @Lee SJ    - 2024-05-25 15:34:29 */                     randomSpawnPosition = new Vector3(0f, 0f, randomXY);
/* @Lee SJ    - 2024-05-25 15:34:29 */                     break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                 case "Right":
/* @Lee SJ    - 2024-05-25 15:34:29 */                     direction = Vector3.right;
/* @Lee SJ    - 2024-05-25 15:34:29 */                     randomSpawnPosition = new Vector3(0f, 0f, randomXY);
/* @Lee SJ    - 2024-05-25 15:34:29 */                     break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                 case "Up":
/* @Lee SJ    - 2024-05-25 15:34:29 */                     direction = Vector3.forward;
/* @Lee SJ    - 2024-05-25 15:34:29 */                     randomSpawnPosition = new Vector3(randomXY, 0f, 0f);
/* @Lee SJ    - 2024-05-25 15:34:29 */                     break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                 case "Down":
/* @Lee SJ    - 2024-05-25 15:34:29 */                     direction = Vector3.back;
/* @Lee SJ    - 2024-05-25 15:34:29 */                     randomSpawnPosition = new Vector3(randomXY, 0f, 0f);
/* @Lee SJ    - 2024-05-25 15:34:29 */                     break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                 default:
/* @Lee SJ    - 2024-05-25 15:34:29 */                     int randomDirection = UnityEngine.Random.Range(0, 4);
/* @Lee SJ    - 2024-05-25 15:34:29 */                     switch (randomDirection)
/* @Lee SJ    - 2024-05-25 15:34:29 */                     {
/* @Lee SJ    - 2024-05-25 15:34:29 */                         case 0:
/* @Lee SJ    - 2024-05-25 15:34:29 */                             direction = Vector3.left;
/* @Lee SJ    - 2024-05-25 15:34:29 */                             randomSpawnPosition = new Vector3(0f, 0f, randomXY);
/* @Lee SJ    - 2024-05-25 15:34:29 */                             break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                         case 1:
/* @Lee SJ    - 2024-05-25 15:34:29 */                             direction = Vector3.right;
/* @Lee SJ    - 2024-05-25 15:34:29 */                             randomSpawnPosition = new Vector3(0f, 0f, randomXY);
/* @Lee SJ    - 2024-05-25 15:34:29 */                             break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                         case 2:
/* @Lee SJ    - 2024-05-25 15:34:29 */                             direction = Vector3.forward;
/* @Lee SJ    - 2024-05-25 15:34:29 */                             randomSpawnPosition = new Vector3(randomXY, 0f, 0f);
/* @Lee SJ    - 2024-05-25 15:34:29 */                             break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                         case 3:
/* @Lee SJ    - 2024-05-25 15:34:29 */                             direction = Vector3.back;
/* @Lee SJ    - 2024-05-25 15:34:29 */                             randomSpawnPosition = new Vector3(randomXY, 0f, 0f);
/* @Lee SJ    - 2024-05-25 15:34:29 */                             break;
/* @Lee SJ    - 2024-05-25 15:34:29 */                     }
/* @Lee SJ    - 2024-05-25 15:34:29 */                     break;
/* @Lee SJ    - 2024-05-06 22:26:59 */             }
/* @Lee SJ    - 2024-05-25 15:34:29 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */             return (transform.position + direction * spawnDistance) + randomSpawnPosition;
/* @Lee SJ    - 2024-05-06 22:26:59 */         }
/* @Lee SJ    - 2024-05-06 22:26:59 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */ 
/* @Lee SJ    - 2024-06-02 22:56:15 */ #if UNITY_EDITOR
/* @Lee SJ    - 2024-05-06 22:26:59 */         private void OnDrawGizmos()
/* @Lee SJ    - 2024-05-06 22:26:59 */         {
/* @Lee SJ    - 2024-05-06 22:26:59 */             Gizmos.color = Color.red;
/* @Lee SJ    - 2024-05-25 15:34:29 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */             Vector3 center = transform.position;
/* @Lee SJ    - 2024-05-25 15:34:29 */             Vector3 size = new Vector3(spawnDistance * 2, 1, spawnDistance * 2);
/* @Lee SJ    - 2024-05-25 15:34:29 */ 
/* @Lee SJ    - 2024-05-25 15:34:29 */             Gizmos.DrawWireCube(center, size);
/* @Lee SJ    - 2024-05-06 22:26:59 */         }
/* @Lee SJ    - 2024-06-02 22:56:15 */ #endif
/* @Lee SJ    - 2024-05-06 22:26:59 */     }
/* @LiF       - 2024-06-02 17:19:19 */ }