# 목차
- [목차](#목차)
- [개요](#개요)
- [Set 과 Vector에 구조체를 넣어보자](#set-과-vector에-구조체를-넣어보자)
    - [1. 문제 코드 : set에 구조체를 넣는 경우](#1-문제-코드--set에-구조체를-넣는-경우)
    - [2. 해결 방법](#2-해결-방법)

# 개요
- 파트를 마땅히 나누기 어려운 내용은 이 곳에 정리한다.
  
# Set 과 Vector에 구조체를 넣어보자
### 1. 문제 코드 : set에 구조체를 넣는 경우
~~~c++
struct str
{
    int a;
    int b;
    string c;
};

set<str> s;
vector<str> v;

set<pair<int,int>> s_pair;
vector<pair<int,int>> v_pair;

int main()
{
    //1. struct test
    v.push_back({3,2,"bca"});
    v.push_back({2,25,"aaa"});
    v.push_back({4,1,"ewerwer"});

    ** 에러가 발생한다! **
    //s.insert({3,2,"bca"});
    //s.insert({2,25,"aaa"});
    //s.insert({4,1,"ewerwer"});

    for(auto i : v)
    {
        cout << i.a << " " << i.b << " " << i.c << "\n";
    }
    /* 올바른 출력
    3 2 bca
    2 25 aaa
    4 1 ewerwer
    */

    //2. pair
    v_pair.push_back({3,2});
    v_pair.push_back({2,4});
    v_pair.push_back({4,-1});

    s_pair.insert({3,2});
    s_pair.insert({2,4});
    s_pair.insert({4,-1});

    for(auto i : v_pair)
    {
        cout << i.first << " " << i.second << "\n";
    }
    /* 올바른 출력
    3 2
    2 4
    4 -1
    */

    for(auto i : s_pair)
    {
        cout << i.first << " " << i.second << "\n";
    }
    /* 올바른 출력
    2 4
    3 2
    4 -1
    */
	return 0;
}
~~~
- pair의 경우 다음 자료구조들에서는 모두 안전하게 출력이 된다.
- **:star:Set과 map에서 구조체를 타입으로 하는 경우 에러가 발생**한다.
  
### 2. 해결 방법
- 문제 상황
  - ![image](https://user-images.githubusercontent.com/55792986/194299728-c37a3adf-5246-42a6-a3ae-840ca35b4752.png)
- 해결 방법
  - ![image](https://user-images.githubusercontent.com/55792986/194299959-6168a600-ff63-4887-b729-d1ba16cff6b2.png)
  - 구조체를 만들 때 operator를 반드시 구현해야 한다.
- 다른 자료
  - ![image](https://user-images.githubusercontent.com/55792986/194300280-50ebb8d0-60e5-4021-8db9-831b8888a0fb.png)
- 결론
  - operator를 추가하는 것은 절대 코딩테스트에서 기억하지 못한다.
  - 그러므로 Vector를 이용한다.
  - Vector를 이용해서 해결하는 방식이 복잡도가 오버되면 다른 알고리즘으로 구현한다. 

