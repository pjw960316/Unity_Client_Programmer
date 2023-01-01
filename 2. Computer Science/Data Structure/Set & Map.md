# 목차
- [목차](#목차)
- [Associative Container의 역순 순회](#associative-container의-역순-순회)
    - [1. Library](#1-library)
    - [2. Associative Container의 Iterator 분석](#2-associative-container의-iterator-분석)
- [Associative Container의 Erase](#associative-container의-erase)
- [value가 자료구조라면?](#value가-자료구조라면)
- [:star:Unordered Map(Hash)의 Find](#starunordered-maphash의-find)
    - [1. 복잡도는 상수 시간이다.](#1-복잡도는-상수-시간이다)
    - [2. key로 직접 찾으면 아래의 위험성이 발생하니 find를 사용하자.](#2-key로-직접-찾으면-아래의-위험성이-발생하니-find를-사용하자)
- [:star:operator[]로 map에 원소를 추가한다.](#staroperator로-map에-원소를-추가한다)
    - [1. 기존 생각](#1-기존-생각)
    - [2. Official](#2-official)

# Associative Container의 역순 순회
### 1. Library
- 정방 순회를 이용한 후 stack, vector로 가공해서 하는 방법도 있다. 하지만 매우 간단한 걸 library에서 제공한다.
~~~c++
for(auto iter = m.rbegin(); iter != m.rend(); iter++)
    {
        auto tmp = *iter;
        for(auto i : tmp.second)
        {
            answer.push_back(i);
        }
    }
~~~
  - 기존의 방식에서 begin() -> rbegin() , end() -> rend()를 이용하면 된다.
  - m.end() 자체가 자료구조의 맨 끝을 가리키는 포인터가 아니므로 위의 방식을 사용해야 한다.

### 2. Associative Container의 Iterator 분석
~~~c++
map<int,int> m;
    for(int i=0; i<3; i++)
    {
        m.insert({i*10 , i*11});
    }

    auto iter = m.begin(); //auto를 컴파일러가 판별하도록 제시.

    //1. 일반적인 map 순회
    for(iter = m.begin(); iter != m.end(); ++iter)
    {
        printf("%p " , iter);
    }
    // 결과 : 006C1BA8 006C1BD8 006C1BF8 
    printf("\n\n");
 
    //2. map의 begin() 부터 증가해보기
    iter = m.begin();
    printf("%p\n" , iter);
    printf("%p\n" , ++iter);
    printf("%p\n" , ++iter);
    printf("%p\n" , ++iter);
    /* 결과
    006C1BA8
    006C1BD8
    006C1BF8
    0061FEDC
    */

    printf("\n\n");

    //3. map의 end() 부터 증가해보기
    iter = m.end();
    printf("%p\n" , iter);
    printf("%p\n" , --iter);
    printf("%p\n" , --iter);
    printf("%p\n" , --iter);

    /* 결과
    0061FEDC
    006C1BF8
    006C1BD8  
    006C1BA8
    */
    printf("\n\n");

    //4. map의 begin() 부터 감소해보기
    iter = m.begin();
    printf("%p\n" , iter);
    printf("%p\n" , --iter);
    printf("%p\n" , --iter);
    printf("%p\n" , --iter);

    /* 결과
    006C1BA8
    0061FEDC
    006C1BF8
    006C1BD8
    */

    printf("\n\n");

    //5. map의 end() 부터 증가해보기
    iter = m.end();
    printf("%p\n" , iter);
    printf("%p\n" , ++iter);
    printf("%p\n" , ++iter);
    printf("%p\n" , ++iter);  

    /* 결과
    0061FEDC
    006C1BF8
    0061FEDC
    006C1BF8
    */

    printf("\n\n");

    //6. 위의 결론을 통해 map을 역순으로 순회 하는 방법
    for(auto iter = --m.end(); iter != --m.begin(); --iter)
    {
        cout << (*iter).first << " " << (*iter).second << "\n";
    }
    /* 결과
    20 22
    10 11
    0 0
    */
~~~
  - map의 begin ~ 실제 마지막 원소까지는 일정한 바이트 만큼의 차이를 두고 연속적으로 저장된다.
  - map의 end만은 관련 없는 주소값으로 저장된다.
  - **그러나 map의 실제 마지막 원소에서 증가를 시키면 map의 end 주소로 이동한다.**
    - 그렇기 때문에 이를 생각하면 iterator를 이용해서 역순으로 출력하는 것은 어렵지 않다.
  - 4번 5번의 결과를 보면 begin에서 감소시키고, end에서 증가시키면 신기하게 다시 map의 iterator들을 참고하지만 이는 무시하자.
  - **우리가 기억해야 할 것은 map의 begin은 map에서 실제로 이용되는 값의 주소를 가리키므로 연속적인 주소의 첫 주소다. 하지만 map의 end는 map에서 실제로 이용되는 값의 주소가 아니라 map이 특이하게 저장하는 end flag의 주소다. 그러나 map의 실제마지막 값의 주소에서 iterator를 증가시키면 map의 end flag의 주소로 이동한다!**

# Associative Container의 Erase
- **Set, Map의 erase는 iterator와 key를 이용하여 모두 지울 수 있다.**
- key의 경우 값이므로 해당 값을 erase의 인자로 넣어 지운다.
- iterator로 지우는 방법은 실수 할 여지가 크다.
~~~c++
#include <set>
#include <map>
#include <iostream>

using namespace std;

set<int> s;
map<int,int> m;

void printSetAndMap()
{
    cout << "set : ";
    for(auto i : s)
    {
        cout << i << " ";
    }
    cout << "\nmap : ";
    for(auto i : m)
    {
        cout << i.first << " " << i.second << " / ";
    }
    cout << "\n";
}

int main()
{
    //1. set, map 선언    
    for(int i=1; i<=5; i++)
    {
        s.insert(i*10);
    }
    
    for(int i=1; i<=5; i++)
    {
        m.insert({i*2,i*10});
    }

    printSetAndMap();
    //set : 10 20 30 40 50 
    //map : 2 10 / 4 20 / 6 30 / 8 40 / 10 50

    //2. key로 지우기
    s.erase(30);
    m.erase(4);

    printSetAndMap();
    //set : 10 20 40 50 (30이 제거된다!)
    //map : 2 10 / 6 30 / 8 40 / 10 50 (key가 4인 원소가 제거된다! 4번째 원소를 제거하지 않는다.)

    
    //3. iterator로 지우기
    //2번 예제에서 지운 것들 복구
    s.insert(30);
    m.insert({4,20});

    int cnt = 0;
    for(auto i = s.begin(); i != s.end(); i++)
    {
        cnt++;
        auto tmp = i++; //아래에서 iterator i를 지워버리므로 segment fault가 일어난다. 그러므로 주소를 임시저장 한다.
        if(cnt == 3)
        {
            s.erase(i);
        }
        i = tmp;
    }

    cnt = 0;
    for(auto i = m.begin(); i != m.end(); i++)
    {
        cnt++;
        auto tmp = i++; //아래에서 iterator i를 지워버리므로 segment fault가 일어난다. 그러므로 주소를 임시저장 한다.
        if(cnt == 3)
        {
            m.erase(i);
        }
        i = tmp;
    }

    printSetAndMap();
    //set : 10 20 30 50
    //map : 2 10 / 4 20 / 6 30 / 10 50
    //iterator는 0부터이므로 3번째 인덱스의 원소를 지운다.
}
~~~
- iterator 순회를 이용해서 제거한다.
- **iterator는 주소이므로 지워버리면 해당 메모리가 해제 된다. 그러므로 반복문에서 널 포인터에 i++를 하므로 에러가 난다. 반드시 i에 다른 iterator 값을 저장해준다.**

# value가 자료구조라면?
~~~c++
//map의 value를 vector로 할 때.
    vector<int> v; // 빈 벡터 형성

    m.insert({1,v});
    m.insert({2,v});
    
    m[1].push_back(2);
    m[1].push_back(3);
    m[1].push_back(4);

    m[2].push_back(12);
    m[2].push_back(13);
    m[2].push_back(14);
    
    for(auto i : m)
    {
        cout << i.second[0] << " " << i.second[1] << " " << i.second[2] << "\n";
    }
    // 2 3 4
    // 12 13 14
~~~
- 빈 자료구조를 선언한다.
- 해당 자료구조를 key와 함께 insert 한다.

# :star:Unordered Map(Hash)의 Find
### 1. 복잡도는 상수 시간이다.
- 이전에 공부했지만 모호한 부분이 있어서 정리한다.
- Find() 함수가 O(1)이고 이전에 작성한 로직
- 이전 로직
  - ![image](https://user-images.githubusercontent.com/55792986/187898039-f773c236-2a39-44f2-80ad-bfdef0d656e8.png)
- Reference
  - ![image](https://user-images.githubusercontent.com/55792986/187898202-b5550d17-4218-42c3-ab21-28e041bd2dc4.png)
  - ![image](https://user-images.githubusercontent.com/55792986/187898248-5efb5cab-9dcf-4087-b37e-486c1bd4940e.png)
### 2. key로 직접 찾으면 아래의 위험성이 발생하니 find를 사용하자.
~~~c++
#include <iostream>
#include <unordered_map>
#include <string>

using namespace std;

int main()
{
    unordered_map<string,int> m;
    m.insert({"aaa" , 1});
    m.insert({"bbb" , 2});
    m.insert({"ccc" , 3});

    cout << m["aaa"] << "\n"; // 1
    cout << m["ddd"] << "\n"; // 0

    cout << m.size() << "\n"; // 4

    if(m.find("aaa") != m.end())
    {
        cout << "aaa find\n";
    }
    // "aaa find"를 출력한다.
    if(m.find("ddd") != m.end())
    {
        cout << "ddd find\n";
    }
    // "ddd find"를 출력한다.
    
    cout << m["ddd"]; // 0
    return 0;
}
~~~
- **Key가 존재하든 말든 m[key]로 검색을 할 때 오류는 나지 않고 return 을 한다.**
- 없는 key에 대해서 m[key]를 하면 0(항상 0인지는 모르겠다. 아마 type에 맞추지 않을까)을 value로 하여 원소로 자동으로 추가한다.
  - 그래서 위의 예제를 보면 m["ddd"]를 출력할 때 자동으로 원소를 추가시켜 4개의 원소가 m에 저장된다.
- 가장 안전한 것은 **find를 사용**하는 것 이라고 생각한다. 

# :star:operator[]로 map에 원소를 추가한다.
~~~c++
 map<string,int> m;
    
    //원소 추가 방식_1 : insert
    m.insert({"jiwon" , 111});

    //원소 추가 방식_2 : operator []
    m["jitwo"] = 222;

    cout << m["jiwon"] << " " << m["jitwo"] << " " <<m["jithree"]; 
    // 111 222 0
    // jithree는 존재하지 않지만 오류를 나타내지 않는다.
~~~
### 1. 기존 생각
- 기존에는 원소 추가 방식_1만 이용했다. (set과 혼동하지 않기 위함)
  - 원소 추가 방식_2는 에러가 날 것 이라고 생각했다.
- 하지만 **operator[]을 사용하는 원소 추가 방식_2 또한 올바르게 동작**한다.
  - 그래도 set과 통일하기 위해 원소 추가 방식_1을 사용하자.

### 2. Official
- [Official Reference](https://cplusplus.com/reference/map/map/operator[]/)
- ![20221010_154527](https://user-images.githubusercontent.com/55792986/194810816-7292cf8a-c121-4cf7-a698-8c1f0ac44f30.png)
  - 원소가 없다면 insert를 한다.
    - 그러면 당연히 map의 size가 증가한다.
    - 값도 지정하지 않았다면 default value를 넣는다.
  - int value의 default = 0
  - string value의 default = ""
~~~c++
    //1. 선언한 map의 크기
    cout << m.size() << "\n"; // 0

    //2. insert의 크기
    m.insert({1,1});
    cout << m.size() << "\n"; // 1

    //3. []으로 선언할 때
    cout << m[2] << "\n"; // m[2]를 출력시키게 하면 0을 자동으로 넣는다. (int의 default value = 0)
    cout << m.size() << "\n"; // 2

    m[3] = 1;
    cout << m.size() << "\n"; // 3

    //4. string의 default value
    map<int, string> m_string;
    cout << m_string[1]; 
    if(m_string[1] == "")
    {
        cout << "YES"; //YES를 출력한다. 그러므로 string 의 default value는 ""이다.
    }
~~~
