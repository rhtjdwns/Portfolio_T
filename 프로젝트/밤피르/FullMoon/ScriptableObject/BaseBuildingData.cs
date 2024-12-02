/* Git Blame Auto Generated */

/* @rhtjdwns  - 2024-05-30 11:28:19 */ using MyBox;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ using UnityEngine;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */ namespace FullMoon.ScriptableObject
/* @rhtjdwns  - 2024-05-30 11:28:19 */ {
/* @rhtjdwns  - 2024-05-30 11:28:19 */     [System.Serializable]
/* @rhtjdwns  - 2024-05-30 11:28:19 */     public class BaseBuildingData : UnityEngine.ScriptableObject
/* @rhtjdwns  - 2024-05-30 11:28:19 */     {
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Separator("Base Building Settings")]
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("건물 코드")] private string buildingCode = "000";
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public string BuildingCode => buildingCode;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("건물 이름")] private string buildingName = "";
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public string BuildingName => buildingName;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("건물 타입"), DefinedValues("LumberMill", "SwordArmy", "SpearArmy", "CrossbowArmy")] 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         private string buildingType = "";
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public string BuildingType => buildingType;
/* @Lee SJ    - 2024-06-02 20:22:27 */         
/* @Lee SJ    - 2024-06-02 20:22:27 */         [Separator]
/* @Lee SJ    - 2024-06-02 20:22:27 */         
/* @Lee SJ    - 2024-06-02 20:22:27 */         [SerializeField, OverrideLabel("건물 생성 시간")] private float buildTime = 5f;
/* @Lee SJ    - 2024-06-02 20:22:27 */         public float BuildTime => buildTime;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [Separator]
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("건물 최대 체력")] private int maxHp = 1000;
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public int MaxHp => maxHp;
/* @rhtjdwns  - 2024-05-30 11:28:19 */ 
/* @rhtjdwns  - 2024-05-30 11:28:19 */         [SerializeField, OverrideLabel("건물 방어력")] private int defAmount = 0;
/* @rhtjdwns  - 2024-05-30 11:28:19 */         public int DefAmount => defAmount;
/* @rhtjdwns  - 2024-05-30 11:28:19 */     }
/* @rhtjdwns  - 2024-05-30 11:28:19 */ }
