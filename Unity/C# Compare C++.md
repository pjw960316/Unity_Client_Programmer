# 목차
- [목차](#목차)
- [개요](#개요)
- [자료구조](#자료구조)
    - [1. 개요](#1-개요)
    - [2. C++ Vector == C# List](#2-c-vector--c-list)
    - [3. C++ Deque == C# LinkedList](#3-c-deque--c-linkedlist)
    - [4. Set 계열](#4-set-계열)
  - [5. Map 계열](#5-map-계열)

# 개요
- 인턴 시절 기억이 난다.
  - JAVA 개발자들이 C계열 개발자들을 위해 C계열에서 이 기능이 JAVA에서는 이런 것이다 라는 컨플루언스 문서가 있었다.
  - 나 스스로도 C#과 C++의 마이그레이션을 자유롭게 하기 위한 문서를 만들어야 한다.
  
# 자료구조
### 1. 개요
- 요즘은 C++ 자료구조가 더 익숙하니 C++ -> C#으로 생각해보자.
- 어차피 언어만 다를 뿐 자료구조 로직이라는 것은 동일할테니 중요한 메서드를 비교해보며 같음을 증명한다.
- 모두 직접 코드로 테스트 했지만 굳이 첨부하지는 않았다.

### 2. C++ Vector == C# List 
- ![image](https://user-images.githubusercontent.com/55792986/197687880-f6a67e22-7cab-4bcd-a33f-8e0da6449882.png)
  - 보편적으로 자료를 담고 싶을 때 이용한다.
- ![image](https://user-images.githubusercontent.com/55792986/197765048-10c9eef2-f312-4ddd-b40b-3d4fb4ffb6dc.png)
  - Sort의 경우 QuickSort를 이용하는 로직 또한 c++과 동일하다.

### 3. C++ Deque == C# LinkedList 
- ![image](https://user-images.githubusercontent.com/55792986/197766237-09467f36-054a-433f-8c8d-29f02e352b0e.png)
  - 기능적으로 완벽히 동일하고 Deque보다 좀 더 쓸만한 메서드도 포함하고 있다.
  - [Reference](https://www.tutorialspoint.com/A-Deque-Class-in-Chash)

### 4. Set 계열
~~~c#
        SortedSet<int> s = new SortedSet<int>();
        s.Add(3);
        s.Add(2);
        s.Add(1);

        foreach(var i in s)
        {
            Debug.Log("sortedset" + i); // 1 2 3 정렬이 된다.
        }

        HashSet<int> h_s = new HashSet<int>();
        h_s.Add(6);
        h_s.Add(5);
        h_s.Add(4);

        foreach (var i in h_s)
        {
            Debug.Log("HashSet" + i); // 6 5 4 정렬이 되지 않는다.
        }
~~~
![image](https://user-images.githubusercontent.com/55792986/197771069-d9a32630-6e74-46a8-8aee-04d91d63f7a2.png)
  - C++ set -> C# SortedSet 
  - C++ unordered_set -> C# HashSet
    - Hash 기반으로 find가 O(1)이다.
  - C++의 multi_set은 공식적으로 지원하지 않는다.
    - 회사에 구현한 모듈이 있을 것 이다.

## 5. Map 계열
~~~c#
        SortedDictionary<int, string> m = new SortedDictionary<int, string>();
        m.Add(3, "ccc");
        m.Add(2, "bbb");
        //m.Add(2, "adfasdfasdf"); //중복된 키는 넣을 수 없다.
        m.Add(1, "aaa");
        m.Add(4, "bbb");
        m[3] = "ttt";
        m[5] = "zzz";

        foreach(var i in m)
        {
            Debug.Log("SortedDictionary" + i.Key + i.Value); 
        }
        /*
         * 1 aaa
         * 2 bbb
         * 3 ttt
         * 4 bbb
         * 5 zzz
         */

        Dictionary<int, string> m2 = new Dictionary<int, string>();
        m2.Add(3, "ccc");
        m2.Add(2, "bbb");
        //m2.Add(2, "adfasdfasdf"); //중복된 키는 넣을 수 없다.
        m2.Add(1, "aaa");
        m2.Add(4, "bbb");
        m2[3] = "ttt";
        m2[5] = "zzz";

        foreach (var i in m2)
        {
            Debug.Log("Dictionary" + i.Key + i.Value);
        }
~~~
  - C++ Map -> C# SortedDictionary 
  - C++ unordered_Map -> C# Dictionary 
  - key의 중복이 있다면 에러가 납니다. value의 중복은 허용합니다.
  - HashTable이라는 것이 Generic library가 아닌 system library에 존재합니다.
    - C++의 자료구조와 유사한 것은 Dictionary에 가까우므로 일단은 참고만 합니다.
    - [HashTable vs Dictionary](https://hongjinhyeon.tistory.com/87)

