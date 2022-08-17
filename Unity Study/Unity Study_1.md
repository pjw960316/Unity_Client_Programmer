# 목차
- [목차](#목차)
- [Version : .Net, C#, Unity](#version--net-c-unity)
    - [1. .Net Version](#1-net-version)
    - [2. C# Version](#2-c-version)
    - [3. Unity Version](#3-unity-version)
- [Abstract](#abstract)
- [CallBack Function](#callback-function)
- [Property (속성)](#property-속성)
    - [Reference : MSDN](#reference--msdn)
    - [1. 학창시절에 왜 사용하지 않았을까?](#1-학창시절에-왜-사용하지-않았을까)
    - [2. Property를 사용해야 하는 이유](#2-property를-사용해야-하는-이유)
    - [3. 접근지정자는 어떻게 해야 할까?](#3-접근지정자는-어떻게-해야-할까)
- [Member Variable Tips](#member-variable-tips)
- [C# Dictionary](#c-dictionary)


# Version : .Net, C#, Unity
- 많이 정리해봤지만 unity의 버전과 연관지어서 정리해 보는 게 필요하다.
### 1. .Net Version
- Reference : [링크](https://s-core.co.kr/insight/view/%EC%95%8C%EC%95%84%EB%91%90%EB%A9%B4-%EC%93%B8%EB%AA%A8-%EC%9E%88%EB%8A%94-%EB%8B%B7%EB%84%B7-net-%EC%9D%B4%EC%95%BC%EA%B8%B0/)
- (1) .Net Framework 
   - <img width="720" alt="20220801_172301" src="https://user-images.githubusercontent.com/55792986/182106008-580997d5-eab8-4217-a5b6-4ab7c604c6aa.png">
   - <img width="574" alt="20220801_173239" src="https://user-images.githubusercontent.com/55792986/182107722-497748c4-cee1-4120-be22-4a072829a236.png">
   - 마지막 버전은 4.8이다. 이 다음 버전은 .Net 5.0 이다.
     - 일부 문서에서는 .Net 4.6, .Net 4.8로 표기하는데 이는 .NetFramework 4.6, .NetFramework 4.8과 동일하다.
  
- (2) .Net Core 
  - <img width="714" alt="20220801_173652" src="https://user-images.githubusercontent.com/55792986/182108414-96dea550-91b1-40de-a1a5-ef8f02aefa05.png">
  - 마지막 버전은 3.1이다. 이 다음 버전은 .Net 5.0 이다.

- (3) .Net Standard
  - 마지막 버전은 2.1이다.
  - Unity에서 사용되고 있다.
  
- (4) .Net
  - <img width="731" alt="20220801_173532" src="https://user-images.githubusercontent.com/55792986/182108154-58b9cecd-0929-4c3c-b2ba-fa8efb7c3419.png">
  - <img width="606" alt="20220801_180838" src="https://user-images.githubusercontent.com/55792986/182114491-9d58ef8f-b5f2-45fb-b593-3b498c6fcd55.png">
  - **.Net Framework와 .Net Core를 합쳐서 .Net으로 대동단결 하였다!**
  - .Net Standard도 이제는 .Net으로?

### 2. C# Version
- <img width="441" alt="20220801_181604" src="https://user-images.githubusercontent.com/55792986/182115871-428a9fec-b8ce-402e-b46d-7f54d1c755ec.png">
  
  - C# 11 은 .NET 7 이상 버전에서만 지원됩니다. C# 10 은 .NET 6 이상 버전에서만 지원됩니다. C# 9 는 .NET 5 이상 버전에서만 지원됩니다. C# 8.0 은 .NET Core 3.x 이상 버전에서만 지원됩니다.
  - .Net Framework 환경이라면 C# 7.3을 이용합니다.


### 3. Unity Version
- (1) LTS
  - 장기 지원
  - Live, 회사, 출시까지 목표를 하고 유지보수를 할 프로젝트

- (2) Tech Stream
  - 초심자
  - Unity 연습

- (3) 버전 업그레이드
  - 버전을 변경하면 일부 소스가 변경 될 수 있다.
  - 보수적으로 한다.

- (4) 현재 버전 및 생각
  - **2020.3.21f1 (LTS) / .Net Standard 2.0 / C# 7.3 (C# 8.0의 지원이 가능하게 할 수 있는 듯 하다.)**
    - ![image](https://user-images.githubusercontent.com/55792986/182115233-bd8106e4-11f4-4cab-8134-7b8654c1cf35.png)
    - <img width="963" alt="20220801_180630" src="https://user-images.githubusercontent.com/55792986/182114019-394e8efb-ee40-4cef-a13b-e617c77d6179.png">
    - .Net Standard 2.0은 .Net Framework 4.x보다 작은 범위라고 생각되며, 모든 Framework 4.x의 기능을 사용하지 못한다.
    - .Net Standard 2.0을 현재 버전에서 사용하고 있다. 그러므로 .Net API를 사용할 때 .Net Standard 2.0문서를 보면 될 것 이다.
    - 이전의 회사에서는 모두 예전 버전을 이용했다. Visual studio도 2017을 썼고, Cocos도 최신버전이 아니었다. 회사들은 새로운 버전이 나왔다고 바로 마이그레이션 하지 않기 때문에 다음에 갈 회사도 비슷할 것 이다. 그러므로 **현재 버전에서 어떤 .Net과 어떤 C# 버전이 이용되는지 파악하고 사용하는 것이 가장 중요할 것 이다.**

# Abstract
- abstract function이 1개라도 있으면 해당 클래스는 abstract class가 된다.
- 
# CallBack Function 

# Property (속성)
### Reference : [MSDN](https://docs.microsoft.com/ko-kr/dotnet/csharp/programming-guide/classes-and-structs/properties)
### 1. 학창시절에 왜 사용하지 않았을까?
- getter, setter도 제대로 사용하지 않았기 때문에 쓸 이유가 없었다.

### 2. Property를 사용해야 하는 이유
- 귀찮음이 해소 된다. 
  - 멤버 변수는 private로 선언하기 때문에 public으로 구현된 get,set을 이용해야 한다.
  - property는 이를 매우 간편하게 해준다.

### 3. 접근지정자는 어떻게 해야 할까?
- ![image](https://user-images.githubusercontent.com/55792986/182595224-ed28e2e9-14e7-47f8-8503-d45ec5f70ff6.png)
  - private로 선언하면 자식은 접근하지 못한다.

# Member Variable Tips
- ![image](https://user-images.githubusercontent.com/55792986/183585061-b53b3549-e031-4492-ab98-b6d663cc2c43.png)
- ![image](https://user-images.githubusercontent.com/55792986/183585096-2d2cc685-8d74-4682-a4e3-c5f0f0a9621a.png)

# C# Dictionary
- key,value에 클래스를 넣을 수 있다.
- dictionary도 객체이므로 항상 객체를 생성하고 이용한다.