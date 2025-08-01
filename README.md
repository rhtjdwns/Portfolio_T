# Portfolio

## 목차
- [개요](#개요)
- [프로젝트](#프로젝트)
    - [프로젝트 아치카](#프로젝트-아치카)
    - [이상한 나라의 L](#이상한-나라의-L)
    - [밤피르](#밤피르)
- [자료구조](#자료구조)
- [서버](#서버)
    - [유니티](#유니티)
    - [기타](#기타)




## 개요
- Name : 고성준
- Birth : 2001/04/09
- Email : ib041615@naver.com
- Language & Engine : C#(Main) / C++ / C / Unity
- Database & Server : MYSQL, MongoDB, IOCP, AWS
- Git Tool : Github Desktop / SourceTree
- Tooling : Visual Studio, Visual Studio Code
- Environment : Window, Linux(기초)
- ETC : Notion / Excel / Google Drive / Discord

### 인재상
1. 도전 - 새로운 것을 탐구하고 끊임 없이 부딪혀봅니다. 신기술을 받아들이고 적용해보며 장점을 접목시켜 볼려고 합니다.
2. 성실 - 주어진 일을 어떻게 해서든 반드시 마치려고 합니다. 일정 내에 자신의 일을 끝마치는 것에 대해 집착이 강하고 일에 대한 자긍심과 책임감이 강합니다.
3. 자기개발 - 모르는 것이 있다면 그것이 이해가 될 때까지 끊임 없이 찾아봅니다. 한 가지만을 아는 것이 아니라 그것에 관계된 다양한 것을 시도해보며 지식을 넓혀나갑니다.




## 프로젝트

### 프로젝트 아치카
![image](https://github.com/user-attachments/assets/f03fd005-493d-41aa-8db1-3a7b3b78a46d)

"아무 능력 없는 반 도꺠비, '첸'. 보름달이 뜨는 날 밤 이뤄지는 [도깨비 결전]. 아버지의 복수를 위해 첸은 신베이로 향한다."
- 2024-09-23 ~ 2024-11-29 3개월 소요
- 프로그래밍 1명, 기획 3명, 그래픽 13명, 사운드 1명으로 이루어진 팀 프로젝트
- Unity로 개발한 공격과 키 입력에 따른 커맨드를 사용하여 회피, 스킬을 사용하는 3D 횡스크롤 플랫포머 액션 게임
- 네오위즈 청강게임 크로니클 행사 출품
- 스토브 인디게임 출품 [주소 : https://store.onstove.com/ko/games/4513]
--------------------------------------------
- DOTween은 C#에 최적화되어 있고 기존 유니티의 복잡한 애니메이션 로직보다 DOTween의 API를 사용하면 더욱 간단하게 구현할 수 있어 이를 이용하여 이동과 스킬 관련 로직을 구현하였음
- 플레이어의 이동속도에 따라 발걸음 속도를 조절하거나, 각 장소마다 사운드를 따로 적용하기 위하여 기존 유니티의 사운드 시스템보다 FMOD를 사용하여 더 복잡한 사운드 시스템을 구현하였음
- 적의 종류가 많은 만큼 하나의 부모로 공통된 점을 작성하고 자식 클래스로 각 적 오브젝트만의 특별한 로직(EX. 적의 종류에 따라 달라지는 공격)을 작성함.


### 인게임

- 게임 플레이 영상
  
https://youtu.be/xOaz8Ckg8aY?si=lYkDuamOnC2qF-fY

- 인게임 스크린 샷

![image](https://github.com/user-attachments/assets/7afb6978-bda8-44ee-9f6f-aeaf16553677)
![image](https://github.com/user-attachments/assets/12958982-2c48-4161-bb71-da6679502b41)



### 주요 기능

- 보스
    1. 중간 보스 스킬 시스템
    2. 중간 보스 FSM
    3. 중간 보스 유도 미사일 패턴
- 몬스터
    1. 일반 몬스터 FSM
    2. 일반 몬스터 피격 에어본
- 플레이어
    1. 공격, 커맨드 시스템
- ETC
    1. 애니메이션 이벤트
    2. AWS DynamoDB 연동 

-------------------------------------------------------------------------------------------------------------------------------

- 중간 보스 스킬 시스템

몬스터의 패턴은 Scriptable를 통하여 데이터를 관리하고 Middle_Skill를 상속 받아 만들어지도록 설계

![image](https://github.com/user-attachments/assets/4e88564a-bdef-4f86-9403-cede74540e92)

중간 보스들은 해당 로직을 통해 현재 상태를 바꾸거나 패턴을 변경한다.

```
public void Enter()
{
    foreach (Middle_Skill s in _skillStorage)
    {
        s.Init(this);
    }

    _currentState = Define.MiddleMonsterState.NONE;

    ChangeCurrentState(Define.MiddleMonsterState.IDLE);
}

public void Stay()
{
    if (_currentState != Define.MiddleMonsterState.NONE)
    {
        _stateStroage[_currentState]?.Stay();
    }
}

public void ChangeCurrentState(Define.MiddleMonsterState state)
{
    if (_currentState != Define.MiddleMonsterState.NONE)
    {
        _stateStroage[_currentState]?.Exit();
    }
    _currentState = state;

    if (_currentState != Define.MiddleMonsterState.NONE)
    {
        _stateStroage[_currentState]?.Enter();
    }
}
```

중간 보스 스킬 클래스 다이어그램

![image](https://github.com/user-attachments/assets/f6956812-87da-4925-bbbc-70d62bab54a1)


----------------------------------------------------------------------------------------------------------------------

- 중간 보스 FSM

보스들은 해당 FSM에 따르며 HP가 0이 될 경우 다음 이벤트로 전환된다.

![image](https://github.com/user-attachments/assets/da6ffa3a-bbda-4159-8e9b-cac1453d726b)

----------------------------------------------------------------------------------------------------------------------

- 경채(중간보스 중 한명)의 유도 미사일 패턴을 위한 로직 코드

```

if (!isGrounded && !isNonAuto)
{
    // 일정 시간 후 유도 해제
    timer += Time.deltaTime;
    if (timer > 1.8f)
    {
        isNonAuto = true;
    }

    // 중력 적용
    rb.velocity += Vector3.up * Time.fixedDeltaTime;

    // 타겟 방향 계산
    Vector3 directionToTarget = (player.position - transform.position).normalized + new Vector3(0, 0.5f);

    // 유도 힘 계산 (목표 방향을 더 강하게 반영)
    Vector3 guidanceForce = directionToTarget * guidanceStrength;

    // 미사일 속도 조정 (유도 로직 추가)
    rb.velocity += guidanceForce * Time.fixedDeltaTime;

    // 최대 속도 제한
    rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);

    // 미사일의 앞 방향을 속도 벡터로 설정
    transform.up = -rb.velocity.normalized;
}

```

![유도미사일](https://github.com/user-attachments/assets/b2c51ee6-7099-41fd-95b0-c13923493941)

----------------------------------------------------------------------------------------------------------------------

- 일반 몬스터 FSM

NormalMonster.cs의 Start 부분이다. Dictionary로 현재 상태를 Define.cs에서 정의한 Enum 타입 값과 각각의 상태 클래스를 저장하여 변화한다.

```
// Define.cs
public enum PerceptionType
{
    IDLE, GUARD, HIT, DETECTIONM, SKILLATTACK, NORMALATTACK, TRACE, DEATH
}

// NormalMonster.cs
private void Start()
{
    _perceptionStateStorage.Add(Define.PerceptionType.IDLE, new Normal_IdleState(this));
    _perceptionStateStorage.Add(Define.PerceptionType.HIT, new Normal_HitState(this));
    _perceptionStateStorage.Add(Define.PerceptionType.TRACE, new Normal_TraceState(this));
    _perceptionStateStorage.Add(Define.PerceptionType.GUARD, new Normal_GuardState(this));
    _perceptionStateStorage.Add(Define.PerceptionType.DETECTIONM, new Normal_Detectionm(this));
    _perceptionStateStorage.Add(Define.PerceptionType.SKILLATTACK, new Normal_SkillAttackState(this));
    _perceptionStateStorage.Add(Define.PerceptionType.NORMALATTACK, new Normal_NormalAttackState(this));
    _perceptionStateStorage.Add(Define.PerceptionType.DEATH, new Normal_Death(this));

    CurrentPerceptionState = Define.PerceptionType.IDLE;

    _stat.Hp = _stat.MaxHp;

    _target = CharacterManager.Instance.GetCharacter(PlayerLayer.value)[0].transform;

    // 넉백 시 실행하는 이벤트
    OnKnockback += () =>
    {
        float dir = _player.position.x - transform.position.x;
        Direction = dir;
    };

    isAttack = true;
}
```

일반 몬스터 FSM에 따른 공격 플로우 차트

![image](https://github.com/user-attachments/assets/659e839d-650e-4422-9a44-218fb2ef8def)

- 일반 몬스터 피격 플로우 차트

![image](https://github.com/user-attachments/assets/2b3aff5b-cf7d-427e-ba35-0fec4460df0f)

![에어본](https://github.com/user-attachments/assets/f0dbf3ff-35bf-4201-8ecb-a43d39d193e0)


----------------------------------------------------------------------------------------------------------------------

- 플레이어의 커맨드 입력에 따른 공격을 위한 로직 코드

```
private bool CheckAttackCommand(List<KeyCode> keyList, int skillid, bool isBackDash)
{
    // 스킬 커맨드 검사하여 맞는 커맨드가 있다면 반환
    var matchingSkillId = _skillCommand.commandDatas
        .Where(cd => (isBackDash && cd.IsBack) || !cd.IsBack)
        .Where(cd => cd.PossibleSkillId.Contains(skillid))
        .Where(cd => ContainsSubsequence(keyList, cd.KeyCodes))
        .Select(cd => cd.SkillId)
        .FirstOrDefault(CheckUseSkill);

    if (matchingSkillId != default)
    {
        _player.Ani.SetInteger("CommandCount", matchingSkillId);
        return true;
    }

    return false;
}

private bool ContainsSubsequence(List<KeyCode> source, KeyCode[] target)
{
    if (target.Length == 0 || source.Count < target.Length)
        return false;

    // 플레이어가 왼쪽을 보고 있다면 오른쪽 키가 왼쪽을 입력하도록 변경
    bool isLeftDir = _player.IsLeftDirection();
    List<KeyCode> adjustedTarget = target.Select(key => isLeftDir ? 
                                   (key == KeyCode.RightArrow ? KeyCode.LeftArrow : key == KeyCode.LeftArrow ? KeyCode.RightArrow : key) : key).ToList();

    return Enumerable.Range(0, source.Count - adjustedTarget.Count + 1)
        .Any(i => source.Skip(i).Take(adjustedTarget.Count + 1)
        .SequenceEqual(adjustedTarget));
}
```

- 플레이어 커맨드 입력 플로우차트
![image](https://github.com/user-attachments/assets/95075cbc-eb42-4677-8b19-d6cf97a7577a)



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### 이상한 나라의 L
"앨리스에게 주인공 자리를 빼앗긴 L, 하트 여왕의 도움을 받아 앨리스를 물리치기 위해 모험을 떠난다."
- 2023-09-05 ~ 2023-11-29 4개월 가량 소요
- 프로그래밍 2명, 기획 3명, 그래픽 7명으로 이루어진 팀 프로젝트
- Unity로 개발한 2D 탄막 슈팅게임
- 플레이어 이동, 공격, 스킬 구현 및 관련된 리소스 적용.
      
- 담당 코드
    - 이상한 나라의 L 폴더 안 코드 전부 제가 담당한 코드입니다.
- 최종 빌드 영상 주소 [https://youtu.be/iLkqFxFq6Ko]  




### 인게임
    
![mainbook](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/969d9273-eeb1-419e-807a-f7cd7fce1f99)
![스크린샷 2024-04-24 141225](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/a4fe6702-fc6a-4d7d-a14c-32752ab4ca6e)


### 주요 기능
- 플레이어의 스킬 아이템 부모 클래스
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseSkill : MonoBehaviour
{
    [Header("Skill Info")]
    protected int SkillId;
    [SerializeField] protected float Damage;                // 데미지
    [SerializeField] protected float Speed;                 // 속도
    [SerializeField] protected float duration;              // 지속 시간
    protected float scope;                                  // 공격 범위

    protected Vector2 mouseDirection;                       // 마우스 방향

    protected virtual void Awake()
    {
    }

    protected virtual void LifeDuration()
    {
        duration -= Time.deltaTime;
    }

    public virtual void SetDamage(float damage)
    {
        Damage += damage;
    }
}
```

- 플레이어 스킬 아이템 클래스 다이어그램
  
![image](https://github.com/user-attachments/assets/d0e4a101-665e-4288-aec8-863d48c25bf8)



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### 밤피르
"평온하게 살고 있던 뱀파이어 밤피르. 자신들을 악으로 단정짓고 물리치러온 인간들을 상대하기 위해 오늘도 부하들을 이끈다."
- 2024-02-02 ~ 2024-06-07 4개월 소요
- 프로그래밍 2명, 기획 3명, 그래픽 9명, QA 1명으로 이루어진 팀 프로젝트
- Unity 3D 탑뷰 디펜스 게임
--------------------------------------------
- For이나 Foreach문 같은 경우 코드의 길이가 길어지고 가독성이 떨어져 Linq와 람다식을 통해 코드 구조를 간결화 하였음.
- 실시간으로 인게임에서 건물 건설, 타일 설치하는 것은 TileMap이 낫다고 판단하여 3D에서 TileMap을 적용하는 방법을 찾아보고 이를 적용하였음.
- 유닛이 자동으로 생성되어 이동, 공격, 정찰하기 때문에 이에 대한 로직으로 FSM을 채택하여 현재 유닛의 상태에 따라서 다른 유닛간의 상호작용을 하도록 구현함.
- MyBox(https://github.com/Deadcows/MyBox) 원문을 해석, 번역하여 기능을 사용하여 인스펙터 창에서 기획자들이 수치나 데이터 등을 조절하는 데 있어 직관성을 높이기 위해 프로퍼티를 정리하여 사용함.
- 기존 유니티의 코루틴 같은 경우에는 예외처리를 할 수 없고 리턴 타입에 제한이 있으며 C#의 Task 같은 경우 class를 기반한 힙 할당으로 인하여 가비지 생성이 많기 때문에 struct 기반인 UniTask를 채택하였음. 

- 담당 코드
    1. Camera > CameraController
    2. UI > TileController, CursorController
    3. Util > ObjectPoolManager
    4. ScriptableObject > BaseUnitData, BaseBuildingData
    5. NavMesh > NavMeshTag
    6. Entities > Building 폴더 안 전부
    코드에 rhtjdwns 커밋 부분이 제가 담당, 수정했던 코드입니다.


https://youtu.be/vdxgS0z4MZg 기말 시연 영상


### 인게임
 
![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/de9ae743-ec03-422a-b023-9e3547834c1f)
![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/8a220b44-b6ce-4564-b972-0b2eea2e5813)


### 주요 기능

- 플레이어 유닛 FSM 플로우 차트
![image](https://github.com/user-attachments/assets/a8ecbbf3-f99c-4d51-921c-d792f4350c93)

- NavMesh 함수 UpdateNavMeshDataAsync()

BuildNavMesh()는 런타임 중에서도 NavMesh를 빌드하여 갱신 시켜주지만 해당하는 프로젝트에서 이를 실행했을 경우 많은 프레임 저하가 나타나는 현상을 확인. UpdateNavMeshDataAsync을 사용하여 해결.

![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/5ce91407-4bc1-4cc4-b668-980593657b29)



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



## 자료구조
- 백준 문제풀이를 통한 자료구조에 대한 지식 습득
  
  https://github.com/rhtjdwns/Baekjoon

- FSM >> FSM을 활용하여 팩맨 게임을 모작하였다. 적이 주인공을 최단 거리 경로로 탐색하여 쫓아오고 주인공은 에너지를 전부 먹으면 클리어 되는 게임. (자료구조 >> Facman 경로에 해당 게임의 코드 설명이 있습니다.)

  
팩맨에서 활용된 적 FSM
![image](https://github.com/user-attachments/assets/f4538343-1310-4011-ba1d-62c51ed1ef60)





## 서버

### 유니티

- 유니티 Lobby를 이용하여 P2P 방식으로 호스트와 클라이언트를 나눠 방을 만들고 입장하는 기능을 제작
- 유니티 Relay 서버를 이용하여 플레이어 프리팹을 만들어 Rigidbody를 통한 이동 통신을 구현

### 기타

- 소켓 프로그래밍 학습, TCP, UDP 두 가지의 통신을 활용하여 소켓 생성 -> 연결 -> 송수신 -> 종료 단계를 배우고 이를 이용하여 1대1 통신을 사용한 간단한 대화창을 구현함.
  
- 소켓 프로그래밍을 사용하여 두 플레이어간 먼저 골인지점을 차지하는 간단한 게임 구현 (발판이 랜덤한 위치에 생성되며 이를 밟아 골인 지점까지 도달하는 게임)
  MYSQL를 사용하여 플레이어의 점수를 데이터베이스에 저장하여 랭킹을 구현하고 통신 부분은 플레이어의 위치를 주고 받도록 하였다. "MYSQL >> UnityServer3"
  ![image](https://github.com/user-attachments/assets/2dfc0330-bd62-4966-b767-fc1d911d275a)
  

- MYSQL과 Unity를 사용하여 마인크래프트 처럼 블록을 제거 및 설치하며 서버에 블록 개수를 저장, 불러올 수 있는 기능을 구현한 경험이 있음. "MYSQL >> FakeMinecraft에 코드 설명 파일"
  ![image](https://github.com/user-attachments/assets/d50e794c-cc38-4349-a8ff-cd9d2ac099e0)

