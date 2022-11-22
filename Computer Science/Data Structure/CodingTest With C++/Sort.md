# 목차
- [목차](#목차)
- [Sort Algorithm에서 사용하는 사용자 정의 함수 : bool cmp()](#sort-algorithm에서-사용하는-사용자-정의-함수--bool-cmp)
    - [1. 이전 필기에 대해서](#1-이전-필기에-대해서)
    - [2. 간단한 예시 : 개념 이해](#2-간단한-예시--개념-이해)
    - [3. 실전 예시](#3-실전-예시)
    - [4. 복잡도](#4-복잡도)
- [역순으로 벡터 정렬하기](#역순으로-벡터-정렬하기)

# Sort Algorithm에서 사용하는 사용자 정의 함수 : bool cmp()
### 1. 이전 필기에 대해서
- 이전 필기에서 해당 내용에 대한 오류가 있어서 다시 공부하고 정리한다.
- 일반 함수와 사용 방식이 다르다.
- **조건문에서 순서를 결정하지 않고 Return 문에서 순서를 결정한다.**
  - <img width="547" alt="20220907_204716" src="https://user-images.githubusercontent.com/55792986/188871084-b34a8035-bc59-4eb6-9ed5-3ec250269414.png">
  - **'return a > b' 는 'a와 b의 값에 대해 내림차순으로 정렬하시오'와 의미가 동일하고 이게 핵심이다.**
  - 아래의 예시를 자세히 읽어보자.
  
### 2. 간단한 예시 : 개념 이해
~~~c++
#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>
#include <algorithm>

using ll = long long;
using namespace std;

vector<string> v;

//문자열 길이 대로 오름차순으로 나열하고 싶다! -> 결과부터 말하면 이는 실패한다.
bool compareBad(string a , string b)
{
    // a보다 b가 길면 b를 오른쪽에 놓아야겠다.
    if(a.length() < b.length())
    {
        return a.length() < b.length();
    }
    // a가 b보다 길면 a를 왼쪽에 놓아야겠다.
    else
    {
        return a.length() >= b.length();
    }
    // 하지만 결과는 제대로 나오지 않는다.
}

bool compareGood(string a , string b)
{
    // a와 b의 길이에 대해서 오름차순으로 정렬하겠다는 의미.
    return a.length() < b.length(); 
}

int main()
{
    v.push_back("apple");
    v.push_back("ant");
    v.push_back("sand");
    v.push_back("apple");
    v.push_back("append");
    v.push_back("sand");
    v.push_back("sand");

    //1. 잘못된 정렬
    sort(v.begin(), v.end() , compareBad);
    for(auto i : v)
    {
        cout << i << " "; //sand sand append apple sand ant apple 결과가 올바르지 않음
    }

    //2. 올바른 정렬
    sort(v.begin(), v.end() , compareGood);
    for(auto i : v)
    {
        cout << i << " "; //ant sand sand sand apple apple append 
    }
}
~~~
- 오름차순으로 정렬할지 내림차순으로 정렬할지에 대해서는 return에서 결정한다.
- 추가적으로 조건이 필요할 때 if문을 이용한다.

### 3. 실전 예시
- [아래 예시는 boj_20920의 cmp 함수](https://www.acmicpc.net/problem/20920)
~~~c++
bool cmp(pair<string,int> a , pair<string,int> b)
{
    //두 단어의 출현 횟수가 다르면 내림차순으로 정렬
    if(a.second != b.second)
    {
        return a.second > b.second; //정렬_1 : 출현 횟수로 내림차순
    }
    //두 단어의 출현 횟수가 다르면 추가로 고려
    else
    {
        //두 단어의 길이가 다르면 길이에 따라 내림차순 정렬
        if(a.first.length() != b.first.length())
        {
            return a.first.length() > b.first.length(); //정렬_2 : 길이로 내림차순!
        }
        else
        {
            return a.first < b.first; //정렬_3 : 문자열의 순서로 오름차순! == 알파벳 순서
        }
    }
}
~~~
- [프로그래머스 문제_문자열 내 마음대로 정렬하기](https://school.programmers.co.kr/learn/courses/30/lessons/12915)
### 4. 복잡도
- Sort 알고리즘은 O(nlogn)의 정말 좋은 복잡도를 갖고 있다.
- 여기에 사용자 정의 비교 함수를 만들고 정렬을 시킨다면 해당 복잡도에 곱을 할 것 이다. 
  - n * logn * 사용자 정의 함수의 복잡도
  - 예를 들어 간단하게 3가지를 비교하는 (분기문 3개)것이라면 n * logn * 3일 것 이다.


# 역순으로 벡터 정렬하기
- 라이브러리를 암기할 필요가 없지만 이를 요구하는 시험들이 있다.
- 정렬된 벡터에 대해서 reverse(v.begin() , v.end()) 를 진행하자.
  - sort(v.begin() , v.end() , greater<int>()) 도 있다.