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
- Database : MYSQL
- Git : Github Desktop / SourceTree
- Environment : Window
- ETC : Notion / Excel / Google Drive

## 프로젝트
- "이상한 나라의 L" 2023-09-05 ~ 2023-11-29
    - 프로그래밍 2명, 기획 3명, 그래픽 7명으로 이루어진 팀 프로젝트
    - Unity로 개발한 2D 탄막 슈팅게임
    - 플레이어 동작, 적과의 상호작용 기능과 스킬을 주로 구현
      
    느낀점. 상황에 따른 애니메이션 조정, 시네머신에 대해서 알아가고 팀원들을 통해서 깃에서 몰랐던 부분을 더욱 충족시킬 수 있어서 좋았던 프로젝트
  
    https://youtu.be/iLkqFxFq6Ko 최종 빌드 영상
![mainbook](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/969d9273-eeb1-419e-807a-f7cd7fce1f99)
![스크린샷 2024-04-24 141225](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/a4fe6702-fc6a-4d7d-a14c-32752ab4ca6e)


- "밤피르" 2024-02-02 ~ 2024-06-07
    - 프로그래밍 2명, 기획 3명, 그래픽 9명, QA 1명으로 이루어진 팀 프로젝트
    - Unity로 개발중인 3D 디펜스 게임
    - 플레이어가 플레이하면서 사용할 수 있는 기능(건물 건설, 타일 설치, 유닛 생산 등등)을 주로 구현
    - 유니티의 Linq와 람다를 통한 코드 간결화, NavMesh에 대한 R&D로 플레이 중 베이크를 하면서 프레임이 떨어지지 않도록 한 경험이 있음.
    - DOTS를 적용시킬 계획이었으나 DOTS를 쓸 만큼 리소스가 많지 않아 적용 시키진 못함. DOTS에 대한 이론과 사용법만 배운 상태.
    - Tilemap을 이용하여 3D 환경에서도 타일을 설치하고 NavMesh와 연동하여 유닛들이 설치한 타일 위를 인식할 수 있도록 함.
 
      ![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/de9ae743-ec03-422a-b023-9e3547834c1f)
      ![image](https://github.com/rhtjdwns/Portfolio_T/assets/64015904/8a220b44-b6ce-4564-b972-0b2eea2e5813)

 
      https://youtu.be/vdxgS0z4MZg 기말 시연 영상

## 자료구조
- Stack, Queue, Linked List
- Tree, Graph
- DFS, BFS, 정렬 알고리즘(ETC. Heap, Quick...)
- Astar, Dijkstra
- LRU, 
### 디자인 패턴
- Observer, Strategy, Decoorator
- Factory, Singleton
- FSM >> "Facman" FSM을 활용한 간단한 팩맨 게임 구현

## 서버
- 소켓 프로그래밍 학습, TCP, UDP 두 가지의 통신을 활용하여 소켓 생성 -> 연결 -> 송수신 -> 종료 단계를 배우고 이를 이용하여 1대1 통신을 사용한 간단한 대화창을 구현함.
- 네트워크를 사용하여 두 플레이어간 먼저 골인지점을 차지하는 간단한 게임 구현 및 MYSQL를 사용하여 랭킹까지 구현 "MYSQL >> UnityServer3"
- 과제로 두명이서 MYSQL과 Unity를 사용하여 마인크래프트 처럼 블록을 제거 및 설치하며 서버에 저장, 불러올 수 있는 기능을 구현
