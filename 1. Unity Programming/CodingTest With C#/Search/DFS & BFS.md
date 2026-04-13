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