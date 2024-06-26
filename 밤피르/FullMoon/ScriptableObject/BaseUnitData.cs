/* Git Blame Auto Generated */

/* @Lee SJ    - 2024-03-26 16:38:08 */ using MyBox;
/* @Lee SJ    - 2024-03-26 16:38:08 */ using UnityEngine;
/* @Lee SJ    - 2024-03-26 16:38:08 */ 
/* @Lee SJ    - 2024-03-31 16:27:54 */ namespace FullMoon.ScriptableObject
/* @Lee SJ    - 2024-03-26 16:38:08 */ {
/* @LiF       - 2024-06-02 01:42:09 */     [System.Flags]
/* @LiF       - 2024-06-02 01:42:09 */     public enum UnitClassFlag
/* @LiF       - 2024-06-02 01:42:09 */     {
/* @LiF       - 2024-06-02 01:42:09 */         None = 0,
/* @LiF       - 2024-06-02 01:42:09 */         Sword = 1 << 0, // 1
/* @LiF       - 2024-06-02 01:42:09 */         Spear = 1 << 1, // 2
/* @LiF       - 2024-06-02 02:43:11 */         Crossbow = 1 << 2, // 4
/* @rhtjdwns  - 2024-06-02 13:58:03 */         Main = 1 << 3 // 8
/* @LiF       - 2024-06-02 01:42:09 */     }
/* @LiF       - 2024-06-02 01:42:09 */     
/* @Lee SJ    - 2024-03-26 16:38:08 */     [System.Serializable]
/* @Lee SJ    - 2024-03-31 16:27:54 */     public class BaseUnitData : UnityEngine.ScriptableObject
/* @Lee SJ    - 2024-03-26 16:38:08 */     {
/* @Lee SJ    - 2024-03-26 16:38:08 */         [Separator("Base Unit Settings")]
/* @Lee SJ    - 2024-04-12 20:21:54 */         
/* @Lee SJ    - 2024-04-12 20:21:54 */         [SerializeField, OverrideLabel("유닛 코드")] private string unitCode = "000";
/* @Lee SJ    - 2024-04-12 20:21:54 */         public string UnitCode => unitCode;
/* @Lee SJ    - 2024-04-12 20:21:54 */         
/* @Lee SJ    - 2024-04-12 20:21:54 */         [SerializeField, OverrideLabel("유닛 이름")] private string unitName = "";
/* @Lee SJ    - 2024-04-12 20:21:54 */         public string UnitName => unitName;
/* @Lee SJ    - 2024-04-12 20:21:54 */         
/* @Lee SJ    - 2024-04-12 20:21:54 */         [Separator]
/* @Lee SJ    - 2024-04-12 20:21:54 */         
/* @Lee SJ    - 2024-04-12 20:21:54 */         [SerializeField, OverrideLabel("유닛 타입"), DefinedValues("Player", "Enemy")]
/* @Lee SJ    - 2024-04-12 20:21:54 */         private string unitType = "Player";
/* @Lee SJ    - 2024-04-12 20:21:54 */         public string UnitType => unitType;
/* @Lee SJ    - 2024-04-12 20:21:54 */         
/* @Lee SJ    - 2024-05-21 16:02:38 */         [SerializeField, OverrideLabel("유닛 클래스"), DefinedValues("Main", "Common", "Hammer", "Sword", "Spear", "Crossbow")]
/* @Lee SJ    - 2024-05-08 18:25:40 */         private string unitClass = "Main";
/* @Lee SJ    - 2024-04-12 20:21:54 */         public string UnitClass => unitClass;
/* @Lee SJ    - 2024-04-12 20:21:54 */         
/* @Lee SJ    - 2024-04-12 20:21:54 */         [Separator]
/* @Lee SJ    - 2024-03-26 16:38:08 */     
/* @Lee SJ    - 2024-04-24 16:49:30 */         [SerializeField, OverrideLabel("최대 체력")] private int maxHp = 100;
/* @Lee SJ    - 2024-03-26 16:38:08 */         public int MaxHp => maxHp;
/* @Lee SJ    - 2024-03-26 16:38:08 */         
/* @Lee SJ    - 2024-04-12 19:33:46 */         [SerializeField, OverrideLabel("이동 속도")] private float movementSpeed = 5f;
/* @Lee SJ    - 2024-03-26 16:38:08 */         public float MovementSpeed => movementSpeed;
/* @Lee SJ    - 2024-04-03 23:06:27 */         
/* @Lee SJ    - 2024-04-03 23:06:27 */         [Separator]
/* @Lee SJ    - 2024-04-03 23:06:27 */     
/* @Lee SJ    - 2024-04-12 19:33:46 */         [SerializeField, OverrideLabel("회피율 (%)")] private float missRate = 50f;
/* @Lee SJ    - 2024-04-03 23:06:27 */         public float MissRate => missRate;
/* @LiF       - 2024-04-14 15:23:17 */         
/* @LiF       - 2024-04-14 15:23:17 */         [Space(5)]
/* @LiF       - 2024-04-14 15:23:17 */         
/* @Lee SJ    - 2024-05-06 19:08:19 */         [SerializeField, ConditionalField(nameof(unitType), false, "Enemy"), OverrideLabel("유닛 리스폰 오브젝트")] 
/* @Lee SJ    - 2024-05-06 19:08:19 */         private GameObject respawnUnitObject;
/* @Lee SJ    - 2024-05-06 19:08:19 */         public GameObject RespawnUnitObject => respawnUnitObject;
/* @Lee SJ    - 2024-03-26 16:38:08 */     
/* @Lee SJ    - 2024-06-02 20:40:10 */         [SerializeField, ConditionalField(nameof(unitType), false, "Enemy"), OverrideLabel("자원 유닛 획득량")] 
/* @Lee SJ    - 2024-06-02 20:40:10 */         private int unitDrop = 5;
/* @Lee SJ    - 2024-06-02 20:40:10 */         public int UnitDrop => unitDrop;
/* @Lee SJ    - 2024-06-02 20:40:10 */         
/* @Lee SJ    - 2024-03-26 16:38:08 */         [Separator]
/* @Lee SJ    - 2024-03-26 16:38:08 */     
/* @Lee SJ    - 2024-04-12 19:33:46 */         [SerializeField, OverrideLabel("공격 가능 여부")] private bool attackEnabled = true;
/* @Lee SJ    - 2024-03-26 16:38:08 */         public bool AttackEnabled => attackEnabled;
/* @Lee SJ    - 2024-03-26 16:38:08 */     
/* @Lee SJ    - 2024-04-15 23:12:39 */         [SerializeField, ConditionalField(nameof(attackEnabled)), OverrideLabel("공격 당 데미지")]
/* @Lee SJ    - 2024-04-24 16:49:30 */         private int attackDamage = 10;
/* @Lee SJ    - 2024-03-27 20:08:19 */         public int AttackDamage => attackDamage;
/* @Lee SJ    - 2024-03-26 16:38:08 */         
/* @Lee SJ    - 2024-04-15 23:12:39 */         [SerializeField, ConditionalField(nameof(attackEnabled)), OverrideLabel("첫 공격 딜레이")]
/* @Lee SJ    - 2024-03-26 16:38:08 */         private float attackDelay = 1f;
/* @Lee SJ    - 2024-03-26 16:38:08 */         public float AttackDelay => attackDelay;
/* @Lee SJ    - 2024-03-26 16:38:08 */     
/* @Lee SJ    - 2024-04-22 20:03:35 */         [SerializeField, ConditionalField(nameof(attackEnabled)), OverrideLabel("공격 쿨타임")]
/* @Lee SJ    - 2024-04-22 20:03:35 */         private float attackCoolTime = 1f;
/* @Lee SJ    - 2024-04-22 20:03:35 */         public float AttackCoolTime => attackCoolTime;
/* @Lee SJ    - 2024-03-27 20:08:19 */         
/* @Lee SJ    - 2024-04-15 23:12:39 */         [SerializeField, ConditionalField(nameof(attackEnabled)), OverrideLabel("공격 반경")] 
/* @Lee SJ    - 2024-04-15 23:12:39 */         private float attackRadius = 10f;
/* @Lee SJ    - 2024-04-12 19:33:46 */         public float AttackRadius => attackRadius;
/* @rhtjdwns  - 2024-04-25 11:19:59 */ 
/* @Lee SJ    - 2024-03-26 16:38:08 */         [Separator]
/* @Lee SJ    - 2024-04-02 04:24:46 */         
/* @Lee SJ    - 2024-04-12 19:33:46 */         [SerializeField, OverrideLabel("시야 반경")] private float viewRadius = 10f;
/* @Lee SJ    - 2024-04-02 04:24:46 */         public float ViewRadius => viewRadius;
/* @Lee SJ    - 2024-04-24 16:49:30 */         
/* @Lee SJ    - 2024-04-24 16:49:30 */         
/* @Lee SJ    - 2024-04-24 16:49:30 */         [SerializeField, OverrideLabel("상태 전이 반경")] private float stateTransitionRadius = 10f;
/* @Lee SJ    - 2024-04-24 16:49:30 */         public float StateTransitionRadius => stateTransitionRadius;
/* @rhtjdwns  - 2024-05-28 12:04:17 */ 
/* @rhtjdwns  - 2024-05-28 12:04:17 */         [Separator]
/* @LiF       - 2024-06-02 01:42:09 */         
/* @LiF       - 2024-06-02 01:42:09 */         [SerializeField, OverrideLabel("상성 상 유리한 클래스")]
/* @LiF       - 2024-06-02 01:42:09 */         private UnitClassFlag unitAdvance = UnitClassFlag.None;
/* @LiF       - 2024-06-02 01:42:09 */         public UnitClassFlag UnitAdvance => unitAdvance;
/* @rhtjdwns  - 2024-05-28 12:04:17 */ 
/* @LiF       - 2024-06-02 01:42:09 */         [SerializeField, OverrideLabel("상성 상 불리한 클래스")]
/* @LiF       - 2024-06-02 01:42:09 */         private UnitClassFlag unitCounter = UnitClassFlag.None;
/* @LiF       - 2024-06-02 01:42:09 */         public UnitClassFlag UnitCounter => unitCounter;
/* @rhtjdwns  - 2024-05-28 12:04:17 */ 
/* @rhtjdwns  - 2024-06-02 16:08:39 */         [SerializeField, OverrideLabel("카운터에게 입는 데미지(%)")]
/* @rhtjdwns  - 2024-05-28 12:04:17 */         private float counterDamage = 100;
/* @rhtjdwns  - 2024-05-28 12:04:17 */         public float CounterDamage => counterDamage;
/* @rhtjdwns  - 2024-05-28 12:04:17 */ 
/* @rhtjdwns  - 2024-05-28 12:04:17 */         [SerializeField, OverrideLabel("상성에 따른 넉백(m)")]
/* @Lee SJ    - 2024-06-02 20:40:10 */         private float counterKnockBack = 0;
/* @Lee SJ    - 2024-06-02 20:40:10 */         public float CounterKnockBack => counterKnockBack;
/* @rhtjdwns  - 2024-05-28 12:04:17 */ 
/* @rhtjdwns  - 2024-05-28 12:04:17 */         [SerializeField, OverrideLabel("상성에 따른 방어 확률(%)")]
/* @rhtjdwns  - 2024-05-28 12:04:17 */         private float counterGuard = 0;
/* @rhtjdwns  - 2024-05-28 12:04:17 */         public float CounterGuard => counterGuard;
/* @Lee SJ    - 2024-03-26 16:38:08 */     }
/* @Lee SJ    - 2024-03-26 16:38:08 */ }