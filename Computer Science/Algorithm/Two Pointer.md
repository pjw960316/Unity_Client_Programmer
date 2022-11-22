# 로직
### 1. 두개의 포인터
- start, end pointer를 이용하여 순회한다.
- start를 우측으로 이동시켜서 합을 줄이고 end를 좌측으로 이동시켜서 합을 늘리며 순회하는 로직이다.

### 2. 종료 시점
- 순회단계에서 start가 end와 같아지거나 커지면 종료한다.
  - 로직의 의도를 상실한다.
- 합의 값이 목표치보다 작아서 증가를 시킨다. 그 때 end pointer가 배열의 인덱스를 넘어가면 종료한다.
  - 더 증가시켜야 하지만 배열이 종료되므로 투 포인터 로직에서 더 이상 합을 증가시킬 방법이 없기 때문에 종료한다.
  - start를 우측으로 옮기는 것은 값을 줄이기만 할 뿐이다.

# 복잡도
- 완전 탐색이라면 이중 for 문을 수행하므로 O(n^2)이다.
- 하지만 투 포인터 로직을 이용하면 O(n)이다.
    - 복잡도가 이해가 가지 않는다면 아래의 reference를 확인한다.
# Reference
- [Reference](https://github.com/WooVictory/Ready-For-Tech-Interview/blob/master/Algorithm/%ED%88%AC%ED%8F%AC%EC%9D%B8%ED%84%B0%20%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98.md)
  - 완벽한 설명
- [BOJ_2003](https://www.acmicpc.net/problem/2003)
- [프로그래머스_구명보트](https://school.programmers.co.kr/learn/courses/30/lessons/42885)