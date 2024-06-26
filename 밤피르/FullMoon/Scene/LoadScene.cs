/* Git Blame Auto Generated */

/* @LiF  - 2024-06-03 14:20:46 */ using System;
/* @LiF  - 2024-06-03 14:20:46 */ using Cysharp.Threading.Tasks;
/* @LiF  - 2024-06-03 14:20:46 */ using FullMoon.Util;
/* @LiF  - 2024-06-03 14:20:46 */ using UnityEngine;
/* @LiF  - 2024-06-03 14:20:46 */ using UnityEngine.SceneManagement;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */ namespace FullMoon.Scene
/* @LiF  - 2024-06-03 14:20:46 */ {
/* @LiF  - 2024-06-03 14:20:46 */     public class LoadScene : ComponentSingleton<LoadScene>
/* @LiF  - 2024-06-03 14:20:46 */     {
/* @LiF  - 2024-06-03 14:20:46 */         public string loadingSceneName = "LoadingScene";
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         private string sceneToLoad;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         public void StartLoading(string sceneName)
/* @LiF  - 2024-06-03 14:20:46 */         {
/* @LiF  - 2024-06-03 14:20:46 */             sceneToLoad = sceneName;
/* @LiF  - 2024-06-03 14:20:46 */             LoadLoadingScene().Forget();
/* @LiF  - 2024-06-03 14:20:46 */         }
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         private async UniTaskVoid LoadLoadingScene()
/* @LiF  - 2024-06-03 14:20:46 */         {
/* @LiF  - 2024-06-03 14:20:46 */             await SceneManager.LoadSceneAsync(loadingSceneName);
/* @LiF  - 2024-06-03 14:20:46 */             await LoadSceneAsync();
/* @LiF  - 2024-06-03 14:20:46 */         }
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */         private async UniTask LoadSceneAsync()
/* @LiF  - 2024-06-03 14:20:46 */         {
/* @LiF  - 2024-06-03 14:20:46 */             AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
/* @LiF  - 2024-06-03 14:20:46 */             operation.allowSceneActivation = false;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */             while (operation.progress < 0.9f)
/* @LiF  - 2024-06-03 14:20:46 */             {
/* @LiF  - 2024-06-03 14:20:46 */                 float progress = Mathf.Clamp01(operation.progress / 0.9f);
/* @LiF  - 2024-06-03 14:20:46 */                 ConsoleProDebug.Watch("Loading Progress", progress.ToString("P0"));
/* @LiF  - 2024-06-03 14:20:46 */                 await UniTask.Yield();
/* @LiF  - 2024-06-03 14:20:46 */             }
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */             ConsoleProDebug.Watch("Loading Progress", "100%");
/* @LiF  - 2024-06-03 14:20:46 */             await UniTask.Delay(TimeSpan.FromSeconds(3f));
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */             operation.allowSceneActivation = true;
/* @LiF  - 2024-06-03 14:20:46 */ 
/* @LiF  - 2024-06-03 14:20:46 */             while (!operation.isDone)
/* @LiF  - 2024-06-03 14:20:46 */             {
/* @LiF  - 2024-06-03 14:20:46 */                 await UniTask.Yield();
/* @LiF  - 2024-06-03 14:20:46 */             }
/* @LiF  - 2024-06-03 14:20:46 */         }
/* @LiF  - 2024-06-03 14:20:46 */     }
/* @LiF  - 2024-06-03 14:20:46 */ }