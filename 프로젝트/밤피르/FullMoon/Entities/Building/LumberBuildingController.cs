/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-05-30 11:28:19 */ using Unity.Burst;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */ namespace FullMoon.Entities.Building
/* @rhtjdwns  - 2024-05-30 11:28:19 */ {
/* @rhtjdwns  - 2024-05-30 11:28:19 */     [BurstCompile]
/* @rhtjdwns  - 2024-05-30 11:28:19 */     public class LumberBuildingController : BaseBuildingController
/* @rhtjdwns  - 2024-05-30 11:28:19 */     {
/* @rhtjdwns  - 2024-05-30 11:28:19 */         protected override void OnEnable()
/* @rhtjdwns  - 2024-05-30 11:28:19 */         {
/* @rhtjdwns  - 2024-05-30 11:28:19 */             base.OnEnable();
/* @Lee SJ    - 2024-06-02 20:22:27 */             ShowFrame(buildingData.BuildTime).Forget();
/* @rhtjdwns  - 2024-05-30 11:28:19 */         }
/* @rhtjdwns  - 2024-05-30 11:28:19 */     }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ }
