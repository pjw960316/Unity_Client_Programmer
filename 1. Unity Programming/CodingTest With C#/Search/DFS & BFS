## :fireworks: DFS & BFS 기초 팁
- input의 변수명은 무시하고 반드시 row와 col로 변수명을 선언해서 사용하고 Arr [row+1 , col+1] 배열을 이용한다.
- Local Function을 만들고 visit 갱신, 값 갱신은 모두 메서드에서 진행한다.
- Enqueue 할 때 visit을 true로 한다.
- 모든 문제가 DFS로 풀리지는 않는다. BFS가 유리한 문제도 존재한다.
  - :airplane:[BOJ_7576](https://www.acmicpc.net/problem/7576)

<br><Br>

## :fire: 재귀가 코드에서 읽히지 않는다면, 종이노트에 트리를 그려라!
- 쉬운 탐색은 ‘Depth 마다의’ 또는 ‘Node 마다의’ 상태를 몰라도 풀린다.
- 하지만 어려운 탐색은 ‘Depth 마다의’ 또는 ‘Node 마다의’ 상태를 자세하게 알아야 한다.  ->  Backtracking
  - 필요시에는 Node 별로 현재 state를 관리하기 위해 node 별 struct 나 class가 요구된다.
- 재귀는 멈춰야 한다. 보통 그 조건은 Depth로 한다. 그러므로 모든 Node마다 Depth는 저장해야 한다.
- for문으로는 해결이 불가능한, **깊이가 달라지는 문제 구조**


## BackTracking_DFS (Prunning)

---



# Binary Search

---

- **이진 탐색은 큰 범위에서 큰 범위(Half)를 가지치는 방식**
    - 탐색 범위를 가지친다.
    - 가지치는 범위가 타당한지 검사하는 게 이진 탐색의 핵심이다.
        - 이 때 숫자 몇 개를 넣어본다.
        - ✔️ P(1)이 성립함을 확인하고
        ✔️ P(n) → P(n+1)이 ‘반드시’ 성립할 수밖에 없음을 증명하면
        ✔️ 모든 n에 대해 P(n)이 성립합니다.
- **탐색 범위는 mid를 제외하고 개신한다.**
    - left  =  mid - 1
    - right  =  mid + 1
    - DFS / BFS 에서 자기 자신은 제외하고 탐색을 진행하는 것과 동일하다.
- **Binary Search API 내부 구조**
    
    [MSDN  :  List_Binary Search API](https://www.notion.so/MSDN-List_Binary-Search-API-2f029daafd66808abb31f77c7e6e7cf9?pvs=21)