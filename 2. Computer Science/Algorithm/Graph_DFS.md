# 목차
- [목차](#목차)
- [왜 DFS를 BFS보다 잘하지 못하는가?](#왜-dfs를-bfs보다-잘하지-못하는가)
- [이전 필기에서 발견한 잘못된 것 들을 해결한다.](#이전-필기에서-발견한-잘못된-것-들을-해결한다)
    - [1. DFS에서 필요한 조건을 3가지 -> **2가지**로 변경한다.](#1-dfs에서-필요한-조건을-3가지---2가지로-변경한다)
    - [2. DFS에서 'Depth 0 vs Depth 1'에 대해 명확히 정리한다.](#2-dfs에서-depth-0-vs-depth-1에-대해-명확히-정리한다)
- [:star:DFS에서 visit 배열을 만들 때 마다 드는 불안감](#stardfs에서-visit-배열을-만들-때-마다-드는-불안감)
    - [예시](#예시)
- [DFS 예제](#dfs-예제)
- [순열과 조합](#순열과-조합)

# 왜 DFS를 BFS보다 잘하지 못하는가?
- 대부분의 문제를 BFS로 풀다보니 DFS로 문제를 해결한 경험이 적다.
  - 특히, 그래프 문제
- 순열과 조합의 경우는 DFS로 해결해야 한다.
  - 간단한 문제의 경우는 그래도 푼다.
  - 하지만 조금만 DFS(순열과 조합)로 해결하는 것을 문제에서 숨긴다면 아예 생각이 도달하지 않는다.
- **코드의 기억을 더듬어서 풀려고 하지말고, 아래의 핵심 조건 2가지를 기억하고 이해하며 푼다.**

# 이전 필기에서 발견한 잘못된 것 들을 해결한다.
- [이전 학습](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Computer%20Science/Study%20In%20College/Algorithm%20(Coding%20Test).pdf)
### 1. DFS에서 필요한 조건을 3가지 -> **2가지**로 변경한다.
- 이전 필기에서 필요한 3가지 조건을 '재귀함수, visit 배열, index가 depth인 배열'로 필기했다.
- BFS에서는 visit 배열이 필요하다. 하지만 **DFS의 경우 visit 배열이 index가 depth인 배열과 결국 동일하다!**
- 그러므로 **:star:재귀함수, index 가 depth인 배열:star:** 이 2가지만 필요하다.

### 2. DFS에서 'Depth 0 vs Depth 1'에 대해 명확히 정리한다.
- **가상의 루트 노드를 Depth 0로 이용한다.**
- DFS에서 언제나 Depth 1 부터 시작시킨다.
- 아래 예제에서 index가 depth인 배열은 1부터 데이터를 저장한다.

# :star:DFS에서 visit 배열을 만들 때 마다 드는 불안감
- 항상 "visit 배열(depth가 인덱스)은 재귀함수를 돌다보면 depth 마다 계속 overwrite 되지 않을까?"
- 예를 들어, **visit[1] = 3이라고 하면 depth가 1일 때 3을 방문했다. 그런데 재귀를 돌다보면 depth가 1일 때 3이 아닌 다른 값으로 덮어 씌워질 것 같다.**
- 하지만 **DFS(재귀) 특성상 그럴일은 없다.**
  - **depth[1] = 3을 만족시키면 바로 재귀호출을 하며 depth[2]의 단계를 수행**한다. 해당 재귀 스레드는 조건을 만족하지 않는 단계를 진행할 때 까지 depth[1] = 3을 만족한다.
- 예시 코드를 보며 이해해보자.
### 예시
~~~c++
    if(pirodo > dungeons[i][0])
    {
        if(depth > answer)
        {
            answer = depth;
        }
        visit[depth] = i; // 핵심 구문_1 : 이번 depth에서 방문한 지역을 배열에 초기화
        Dfs(pirodo - dungeons[i][1] , depth+1 , dungeons); // 핵심 구문_2 : 바로 다음 단계의 depth로 DFS를 수행        
    }
~~~
  - 재귀함수의 일부분을 발췌했다.
  - 조건문은 무시한다. 
  - 이번 depth에서 해당 조건을 만족했기 때문에 채택되었고, 방문 했으니 visit[depth] = i를 수행한다.
  - 그리고 바로 'Dfs(pirodo - dungeons[i][1] , depth+1 , dungeons)' 를 통해 다음 depth를 수행한다.
    - visit[depth] 배열들은 올바르게 유지된다.
# DFS 예제
~~~c++
#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using ll = long long;
using namespace std;

string coffee[4] = {"americano", "latte", "mocha", "water"};
string arr[5]; //2번 조건 : index가 depth인 배열

//1번 조건 : 재귀 함수
void Dfs(int depth)
{
    // Depth가 오버되면 종료한다.
    if(depth == 5)
    {
        for(int i=1; i<=4; i++)
        {
            cout << arr[i] << " ";
        }
        cout << "\n";
        return;
    }

    // 현재 Depth에 원소를 넣을 때, 이전에 사용한 원소가 있는지 검사한다.
    // 이 때 visit 배열을 따로 만들 게 아니라 이미 넣고 있던 배열을 검사하면 된다.
    // Depth는 언제나 1부터 채워 나가고, 0번 depth가 root로 가상으로 존재한다고 가정한다.
    for(int i=0; i<4; i++)
    {
        bool is_visit = false;
        for(int j=0; j<depth; j++) //이전 depth에서 해당 커피를 마셨는지 검사한다.
        {
            if(arr[j] == coffee[i])
            {
                is_visit = true;
                break;
            }
        }
        if(is_visit == false)
        {
            arr[depth] = coffee[i];
            Dfs(depth+1);
        }
    }

}
int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    Dfs(1);

   return 0;
}
~~~

# 순열과 조합
- for문이 중첩되는 나열이다? -> 대부분 DFS로 나열
- c++ Algorithm에 next_permutation이 존재하지만 기억이 나지 않을 수 있으므로 지양한다.