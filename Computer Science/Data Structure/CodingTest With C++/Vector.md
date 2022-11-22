# 목차
- [목차](#목차)
- [Vector에서 특정 값을 가진 원소를 지우는 방식](#vector에서-특정-값을-가진-원소를-지우는-방식)
- [Vector를 인자로 넘길 때](#vector를-인자로-넘길-때)
  
# Vector에서 특정 값을 가진 원소를 지우는 방식
1. **Vector의 원소를 map의 키로 저장하고, value를 bool이나 int로 저장하기**
     - 원소의 삽입 및 삭제가 매우 간편해지고 정렬도 되어 진다.
     - vector의 3이라면 map에서는 (3,true)
     - vector와 map의 장점을 얼마나 쉽게 마이그레이션 할 수 있는지가 코테에서 중요하다.

2. **정렬 되지 않은 순서가 중요한 경우 vector의 erase를 이용한다.**
     - ![image](https://user-images.githubusercontent.com/55792986/181666647-80d7c0b6-9639-4957-af98-0cf447058f3b.png)


# Vector를 인자로 넘길 때
~~~c++
#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>

using ll = long long;
using namespace std;

int callByValue(vector<int> v)
{
    printf("cbv : %p\n" ,v); //0061FF00 (다름)
    v[3] = 77;
}

int callByRefer(vector<int> &v)
{
    printf("cbr : %p\n" ,v); // 0061FEF0
    v[2] = 88;
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    vector<int> v;
    for(int i=0; i<5; i++)
    {
        v.push_back(i*10);
    }

    printf("local : %p\n" ,v); //0061FEF0

    callByValue(v);
    callByRefer(v);

    for(auto i : v)
    {
        cout << i << " "; //0 10 88 30 40
    }
   return 0;
}
~~~
- callByValue는 vector의 값을 **복사해서** 전달하기 때문에 새로운 벡터가 생성되는 것과 같다.
- callByRefer는 vector의 주소를 전달하기 때문에 main에서 선언한 vector와 동일하다. 
  - 매개변수에서 주소 값을 받아야 한다.
- 알고리즘의 경우 전역변수를 이용하면 참조할 대상을 전역에 놓기 때문에 매개변수가 필요 없어서 지금까지 전역변수로 이용했다. 하지만 지역변수로 전달해야 하는 문제도 종종 있었기에 한 번 정리했다.
  - call-by-value는 복사하기 때문에 오버헤드가 클 것으로 예상된다.
  - call-by-reference도 언제나 포인터 만큼의 매개변수 메모리를 할당하긴 한다.
- 재귀함수도 동일하게 받아온 주소를 인자로 넘겨주면 된다.