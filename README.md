# Portfolio

## 목차
- [개요](#개요)
- [프로젝트](#프로젝트)
- [자료구조](#자료구조)
- [서버](#서버)




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
1. 배려 - 원활한 커뮤니케이션을 위해서 항상 타인의 입장을 생각하면서 말을 합니다. 갈등이 일어나지 않게 하기 위해서 먼저 양보, 배려하는 습관이 있습니다.
2. 성실 - 주어진 일을 어떻게 해서든 반드시 마치려고 합니다. 일정 내에 자신의 일을 끝마치는 것에 대해 집착이 강하고 일에 대한 자긍심과 책임감이 강합니다.
3. 호기심 - 모르는 것이 있다면 그것이 이해가 될 때까지 끊임 없이 찾아봅니다. 한 가지만을 아는 것이 아니라 그것에 관계된 다양한 것을 시도해보며 지식을 넓혀나갑니다.




## 프로젝트

### "프로젝트 아치카" 2024-09-23 ~ 진행중
    - 프로그래밍 1명, 기획 3명, 그래픽 13명, 사운드 1명으로 이루어진 팀 프로젝트
    - Unity로 개발한 공격과 커맨드를 사용하여 회피, 스킬을 사용하는 3D 횡스크롤 플랫포머 액션 게임
    - 전반적인 게임 기능을 전부 구현
    - DOTween을 사용하여 이동, 스킬 로직 구현
    - FMOD를 사용하여 사운드 적용

경채(중간보스 중 한명)의 유도 미사일 패턴을 위한 이동 코드
![image](https://github.com/user-attachments/assets/b6f33a6d-7956-4aaa-92bd-f78ede0ba130)

플레이어의 커맨드 입력에 따른 공격을 위한 코드
![image](https://github.com/user-attachments/assets/fda421a5-3add-4d9a-845e-611ff80712ba)


참고 영상
https://www.youtube.com/watch?v=_td_gm923iA

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### 게임 스크린샷

![image](https://github.com/user-attachments/assets/fb1598b9-a4d2-46ae-a6cb-bebae2e5d7f4)
![image](https://github.com/user-attachments/assets/c5df18c7-f1f3-4de5-a734-52eb3d48aa8f)

### "이상한 나라의 L" 2023-09-05 ~ 2023-11-29
    - 프로그래밍 2명, 기획 3명, 그래픽 7명으로 이루어진 팀 프로젝트
    - Unity로 개발한 2D 탄막 슈팅게임
    - 플레이어 이동, 공격, 스킬 구현 및 관련된 리소스 적용.
      
    - 담당 코드
        - 이상한 나라의 L 폴더 안 코드 전부 제가 담당한 코드입니다.
  
https://youtu.be/iLkqFxFq6Ko 최종 빌드 영상

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### 게임 스크린샷
    
![mainbook](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/969d9273-eeb1-419e-807a-f7cd7fce1f99)
![스크린샷 2024-04-24 141225](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/a4fe6702-fc6a-4d7d-a14c-32752ab4ca6e)


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### "밤피르" 2024-02-02 ~ 2024-06-07
    - 프로그래밍 2명, 기획 3명, 그래픽 9명, QA 1명으로 이루어진 팀 프로젝트
    - Unity 3D 디펜스 게임
    - 건물 건설, 타일 설치, 유닛 생산 기능 구현, 카메라 움직임과 마우스 클릭 관련 Raycast 구현
    - 유니티의 Linq와 람다를 통한 코드 간결화
    - NavMesh를 이용, 유닛 간에 이동, 공격, 정찰 FSM 구현
    - Tilemap 사용. 타일맵을 이용하여 실시간으로 지형을 설치할 수 있는 기능 구현
    - MyBox(https://github.com/Deadcows/MyBox) 원문을 해석, 번역하여 기능을 사용해 프로퍼티 정리
    - UniTask를 사용한 코드 최적화.

    - 담당 코드
        1. Camera > CameraController
        2. UI > TileController, CursorController
        3. Util > ObjectPoolManager
        4. ScriptableObject > BaseUnitData, BaseBuildingData
        5. NavMesh > NavMeshTag
        6. Entities > Building 폴더 안 전부
        코드에 rhtjdwns 커밋 부분이 제가 담당, 수정했던 코드입니다.
        
플레이어 유닛 FSM 플로우차트
    ![image](https://github.com/user-attachments/assets/a8ecbbf3-f99c-4d51-921c-d792f4350c93)

https://youtu.be/vdxgS0z4MZg 기말 시연 영상

### 기능
[UpdateNavMeshDataAsync]

NavMeshBuilder에 있는 메소드인인 BuildNavMesh()는 런타임 중에서도 NavMesh를 빌드하여 갱신 시켜주지만 해당하는 프로젝트에서 이를 실행했을 경우 많은 프레임 저하가 나타나는 현상을 확인했다.

이를 해결하기 위해 방법을 찾던 도중 NavMeshBuilder에 있는 또 다른 메소드인 UpdateNavMeshDataAsync를 발견했는데 이는 NavMesh 데이터를 업데이트 하는 동안 메인 스레드가 멈추지 않도록 할 수 있도록 하는 역할을 한다. 또한 이 메소드에는 Bound 라는 영역 값을 지정해줄 수 있는 인자가 있는데 이를 이용하여 원하는 특정 구간만 빌드를 해줄 수도 있다.

![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/5ce91407-4bc1-4cc4-b668-980593657b29)


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### 게임 스크린샷
 
![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/de9ae743-ec03-422a-b023-9e3547834c1f)
![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/8a220b44-b6ce-4564-b972-0b2eea2e5813)


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



## 자료구조
- FSM >> FSM을 활용하여 팩맨 게임을 모작하였다. 적이 주인공을 최단 거리 경로로 탐색하여 쫓아오고 주인공은 에너지를 전부 먹으면 클리어 되는 게임. (자료구조 >> Facman 경로에 해당 게임의 코드 설명이 있습니다.)
  팩맨에서 활용된 적 FSM
![image](https://github.com/user-attachments/assets/f4538343-1310-4011-ba1d-62c51ed1ef60)





## 서버
- 소켓 프로그래밍 학습, TCP, UDP 두 가지의 통신을 활용하여 소켓 생성 -> 연결 -> 송수신 -> 종료 단계를 배우고 이를 이용하여 1대1 통신을 사용한 간단한 대화창을 구현함.
  
- 소켓 프로그래밍을 사용하여 두 플레이어간 먼저 골인지점을 차지하는 간단한 게임 구현 (발판이 랜덤한 위치에 생성되며 이를 밟아 골인 지점까지 도달하는 게임)
  MYSQL를 사용하여 플레이어의 점수를 데이터베이스에 저장하여 랭킹을 구현하고 통신 부분은 플레이어의 위치를 주고 받도록 하였다. "MYSQL >> UnityServer3"
  ![image](https://github.com/user-attachments/assets/2dfc0330-bd62-4966-b767-fc1d911d275a)
  

- MYSQL과 Unity를 사용하여 마인크래프트 처럼 블록을 제거 및 설치하며 서버에 블록 개수를 저장, 불러올 수 있는 기능을 구현한 경험이 있음. "MYSQL >> FakeMinecraft에 코드 설명 파일"
  ![image](https://github.com/user-attachments/assets/d50e794c-cc38-4349-a8ff-cd9d2ac099e0)

