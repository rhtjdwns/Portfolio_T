/* Git Blame Auto Generated */

/* @Lee SJ  - 2024-04-17 16:41:43 */ using MyBox;
/* @Lee SJ  - 2024-04-17 16:41:43 */ using UnityEngine;
/* @Lee SJ  - 2024-04-17 16:41:43 */ using UnityEngine.UI;
/* @LiF     - 2024-04-22 19:32:04 */ using DG.Tweening;
/* @Lee SJ  - 2024-04-17 16:41:43 */ using FullMoon.Entities.Unit;
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */ namespace FullMoon.UI
/* @Lee SJ  - 2024-04-17 16:41:43 */ {
/* @Lee SJ  - 2024-04-17 16:41:43 */     [RequireComponent(typeof(Canvas))]
/* @Lee SJ  - 2024-04-17 16:41:43 */     public class UnitUIController : MonoBehaviour
/* @Lee SJ  - 2024-04-17 16:41:43 */     {
/* @Lee SJ  - 2024-04-17 16:41:43 */         [Separator("HP")]
/* @Lee SJ  - 2024-04-17 16:41:43 */         [SerializeField] private Slider hpSlider;
/* @Lee SJ  - 2024-04-17 16:41:43 */         [SerializeField] private Image hpFillImage;
/* @Lee SJ  - 2024-04-17 16:41:43 */         [SerializeField] private Color playerColor = Color.blue;
/* @Lee SJ  - 2024-04-17 16:41:43 */         [SerializeField] private Color enemyColor = Color.red;
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */         private BaseUnitController Unit { get; set; }
/* @LiF     - 2024-04-22 19:32:04 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */         private void Start()
/* @Lee SJ  - 2024-04-17 16:41:43 */         {
/* @Lee SJ  - 2024-04-17 16:41:43 */             GetComponent<Canvas>().worldCamera = UnityEngine.Camera.main;
/* @Lee SJ  - 2024-04-17 16:41:43 */             Unit = GetComponentInParent<BaseUnitController>();
/* @LiF     - 2024-04-22 19:32:04 */ 
/* @Lee SJ  - 2024-04-17 16:41:43 */             hpSlider.gameObject.SetActive(true);
/* @Lee SJ  - 2024-04-17 16:41:43 */             hpFillImage.color = Unit.unitData.UnitType == "Player" ? playerColor : enemyColor;
/* @Lee SJ  - 2024-04-17 16:41:43 */             hpSlider.maxValue = Unit.unitData.MaxHp;
/* @LiF     - 2024-04-22 19:32:04 */             hpSlider.value = Unit.unitData.MaxHp;
/* @Lee SJ  - 2024-04-17 16:41:43 */         }
/* @Lee SJ  - 2024-04-17 16:41:43 */ 
/* @LiF     - 2024-04-22 19:32:04 */         private void Update()
/* @Lee SJ  - 2024-04-17 16:41:43 */         {
/* @LiF     - 2024-04-22 19:32:04 */             if (Unit.Hp != (int)hpSlider.value)
/* @LiF     - 2024-04-22 19:32:04 */             {
/* @LiF     - 2024-04-22 19:32:04 */                 hpSlider.DOValue(Unit.Hp, 0.3f).SetEase(Ease.OutQuad);
/* @LiF     - 2024-04-22 19:32:04 */             }
/* @Lee SJ  - 2024-04-17 16:41:43 */         }
/* @Lee SJ  - 2024-04-17 16:41:43 */     }
/* @LiF     - 2024-04-22 19:32:04 */ }