# :deciduous_tree: 코딩테스트 문제 해결 전략

<br>

## :one:문제를 처음부터 끝까지 순서대로 천천히 읽는다. (문제 조건 놓치면 디버깅으로도 찾을 수 없다.)
- 어디가 중요한지 알 수 없기에 띄엄띄엄 읽지 않는다.
- n번째, i번째 이렇게 적혀서 이해가 되지 않으면 직접 몇 개 넣어본다.

<br><br>

## :two: 종이노트에 다음의 세 가지 항목을 적는다. <br> :white_check_mark: 기능(메서드) 구분  <br> :white_check_mark: 알맞은 자료구조 <br> :white_check_mark: 자료구조를 구성할 DTO (데이터 클래스)
- **체감상 쉬운 문제는 메서드 1 ~ 2개로, 어려운 문제는 메서드 3개 이상으로 구성되는 것 같다.**
- **메서드로 기능 분리와 알맞은 자료구조로 해결되는 문제는 난이도가 높아도 잘 푼다.**
- **그러나 DTO를 유니티의 concrete class처럼 필드를 상세히 만들어야 하는 문제는 잘 못 풀었다.**
  - 아마도 시간의 압박과 코딩테스트가 복잡한 게임과는 다르다고 생각했기 때문이다. 그러나 충분히 DTO 구현은 필수적이다.
  - primitive 타입의 List, Dictionary, 배열이 계속 늘어나고, “이 데이터들이 사실상 하나의 대상을 설명하고 있다”는 느낌이 들면 반드시 DTO로 묶는다.

<br><br>

## :three: 복잡도 검사
### :triangular_flag_on_post: 해야 할 것
1. **대강의 완전탐색 Big-O를 빠르게 파악한다.**
2. **상수까지 포함해서 제대로 복잡도를 계산한다.**
   - Big-O는 상수를 무시하지만 실전 복잡도에서는 포함하는 게 더 확실하다.
    - 이차원 배열 (N * N)이 나오고 N이 50이라고 가정한다.
    - Vertex 탐색은 50 * 50이고, Edge 탐색은 4방향이니까 50 * 50 * 4다.
    - 그러므로 50 * 50 * (1+4) 니까 12,500이 나오고, 이건 2,500(Big-O)와 큰 차이다.
3. **메모리 초과가 일어나는지 계산한다.**
   - 재귀 또는 무한루프를 주의한다. 

<br>

### :bangbang: 주의 사항
- **재귀는 특히 조심한다.**
  - 재귀의 경우 100회 순회를 재귀로 4번만 타도 10^8이다.
- **nlogn에서 logn은 밑이 2인 logn이다.**
- **문제는 보통 10^9에서 터지도록 설계된다. 너무 보수적으로 시도를 주저하지는 말자.**
- **이 값이 10^8 이하고, for문 내부에 O(N) 급(ex : LINQ)연산이 없다면 진행한다.**

<br><br>

## :four: 완전탐색이 시간초과가 난다면 아래의 방식들을 고려해본다.
### :white_check_mark: Binary Search
- :airplane:[Binary Search](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/CodingTest%20With%20C%23/Search/Binary%20Search.md)

<br>

### :white_check_mark: BackTracking
- :airplane:[BackTracking](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/CodingTest%20With%20C%23/Search/%5BDFS%20%26%20BFS%5D%20with%20Backtracking%20and%20Pruning.md)

<br>

### :white_check_mark: DP
- :airplane:[DP](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/CodingTest%20With%20C%23/Dynamic%20Programming.md)

<br>

### :white_check_mark: Greedy
- 하지만 나는 보통 그리디 문제라고 느껴지면 일단 넘어감  ->  잘못 빠지면 큰일남
- 최후의 방식

<br><br>

## :five: 구현
- **주석을 적으며 구현한다.**
  - TODO : 실수할 여지가 있는 부분
  - 디버깅 시에 도움 줄 내용

<br>

- **데이터의 상태와 의미가 헷갈리는 문제라면 필드와 컨테이너의 네이밍을 제대로 짓는다.**
  - 대충 지은 이름은 디버깅시에 다시 해석해야 한다.

<br>

- **Instance Field를 적극적으로 사용하고, Field Initializer 방식으로 초기화한다.**
  - Initialize()를 통해 복잡한 구현이 필요하다면 수정한다.

<br>

- **기능이 복잡하면 무조건 Method로 만든다.** 
  - **Method로 만드는 조건**
    - 일단 구현할 때 복잡하고 실수 할 것 같으면 만든다. (실수는 쉬운 기능에서 한다.)
      - 어려운 기능이 여러 개일 때는 자연스럽게 기능을 나누어 구현한다.
      - 쉬운 기능은 한 번에 처리하려는 경향이 생기고, 이 과정에서 필연적으로 실수한다.
    - 매개변수에 다양한 인자의 값을 넣어 호출해야 하는 경우는 반드시 메서드로 만든다.
    - 쪼개서 보는 게 가독성 및 디버깅에 유리하면 만든다.  (ex  :  Print Method)
  - **Method 대신 if-else 사용하는 조건**
    - 기능이 2~3줄 정도로 간단하다면 if-else를 사용한다.
    - 두 메서드의 흐름이 중요하다면 하나의 메서드로 통합한다.
  - **예시 문제**
    - [BOJ_17281](https://github.com/pjw960316/Algorithm-Habit/blob/main/%EB%B0%B1%EC%A4%80/Gold/17281.%E2%80%85%E2%9A%BE/%E2%9A%BE.cs)
      - 복잡하면 Method (백준 시절은 local function)
      - 야구 게임 시뮬은 쉽지만 거슬려서 method로 뺀다.
    - [BOJ_21922](https://github.com/pjw960316/Algorithm-Habit/blob/main/%EB%B0%B1%EC%A4%80/Gold/21922.%E2%80%85%ED%95%99%EB%B6%80%E2%80%85%EC%97%B0%EA%B5%AC%EC%83%9D%E2%80%85%EB%AF%BC%EC%83%81/%ED%95%99%EB%B6%80%E2%80%85%EC%97%B0%EA%B5%AC%EC%83%9D%E2%80%85%EB%AF%BC%EC%83%81.cs)

<br><br>

## :six: 구현 코드 검증
  - 구현하면서 논리적으로 100% 확신이 있지 않은 부분이 있다면 반드시 검증한다.
  - 검증할 때 가독성이 떨어져서 이해가 되지 않는다면 적절히 리팩토링을 진행한다.

<br><br>

## :seven: **실패 → 주석 + 로그와 같이 디버깅**
1. **틀렸습니다.**
    - Print() 메서드를 만든다.
      - Console.WriteLine(”===============================”);
      - 로그

<br>

2. **시간 초과 = 불 필요한 순회**
    - 다시 한 번 시간복잡도를 구해본다.
    - 순회 과정에서 중복이거나, 굳이 해 볼 필요 없는 연산은 제거한다.
      - 순열과 조합의 차이
      - 조건문으로 early break로 종료시킨다.

<br>

3. **메모리 초과**
    - C#은 힙 할당만 확인한다.
      - LINQ의 ToList() , ToDictionary()가 자주 호출되지 않은지 확인한다.
    - 재귀적으로 할당하는 부분을 확인한다.
      - 1mb가 128번 할당되어 128Mb이 되는 건 바로 파악한다.
      - 그러나 4Bytes의 작은 데이터가 재귀를 25번 돌면서 Add되는 경우는 파악이 어렵다.
      - 4Bytes * 2^25 = 128Mb가 된다.
