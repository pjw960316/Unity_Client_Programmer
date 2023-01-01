# 목차
- [목차](#목차)
- [Priority Queue](#priority-queue)
    - [1. 3개의 인자](#1-3개의-인자)
    - [2. Heap을 이용한 시간복잡도 (내가 이해한 것으로 정리)](#2-heap을-이용한-시간복잡도-내가-이해한-것으로-정리)
    - [3. 추가 사항](#3-추가-사항)


# Priority Queue
### 1. 3개의 인자
- ![20220919_163918](https://user-images.githubusercontent.com/55792986/190970634-22ddbf4c-b10f-4dad-9963-54694aa11d5e.png)
  - container는 반드시 vector, list, deque을 이용한다.
~~~c++
#include <iostream>
#include <queue>
using namespace std;

int main()
{
    priority_queue<pair<int,int>, vector<pair<int,int>>, greater<pair<int,int>>> pq;
    pq.push({30,1});
    pq.push({20,2});
    pq.push({10,-1});

    auto tmp = pq.top();
    cout << tmp.first << " " << tmp.second << "\n"; // 10 -1
    //오름차순 힙으로 정렬되어 있다. 그러므로 10 -1이 가장 먼저 나온다.
}
~~~
### 2. Heap을 이용한 시간복잡도 (내가 이해한 것으로 정리)
- Max Heap or Min Heap일 테니 가장 큰 값이나 작은 값을 찾을 때는 O(1)이다.
  - Root 니까
- Insert, Erase 과정도 부모나 자식과 순차적으로 비교하기 때문에 트리의 깊이가 된다.
  - 트리의 깊이는 log(n)

### 3. 추가 사항
- 기존의 queue와는 다르게 front()와 back()을 이용하지 않고 top()만을 이용한다.
- 원하는 순서대로 정렬하고 이 순서로 출력할 수 있는 매우 훌륭한 자료구조다.
- greater는 오름차순, less는 내림차순이다.
- **greater 나 less는 사용하는 컨테이너의 첫 번째 원소의 값으로 비교한다.**


