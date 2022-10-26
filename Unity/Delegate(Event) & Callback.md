# 목차
- [목차](#목차)
- [개요](#개요)
- [이전 공부](#이전-공부)
- [Design Pattern : Observer Pattern (Listener & Callback)](#design-pattern--observer-pattern-listener--callback)
    - [1. 개념](#1-개념)
    - [2. C# 예시](#2-c-예시)
    - [3. 결론](#3-결론)
- [Delegate & Event의 사용 이유](#delegate--event의-사용-이유)
- [이벤트 주도적 프로그래밍](#이벤트-주도적-프로그래밍)
- [Event에 대해 좀 더 깊게 공부해 본다.](#event에-대해-좀-더-깊게-공부해-본다)
    - [1. Event를 이용할 때는 .Net의 EventHandler를 이용하는 방법이 좋다.](#1-event를-이용할-때는-net의-eventhandler를-이용하는-방법이-좋다)
    - [2. public으로 선언해도 오직 해당 클래스 내부에서만 호출할 수 있습니다.](#2-public으로-선언해도-오직-해당-클래스-내부에서만-호출할-수-있습니다)
    - [3. Event(==EventHandler)는 static으로 선언해야 하는가?](#3-eventeventhandler는-static으로-선언해야-하는가)
- [고민](#고민)
- [.Net API : EventHandler](#net-api--eventhandler)
- [연습](#연습)
    - [1. 구현](#1-구현)
    - [2. EventHandler와 Delegate](#2-eventhandler와-delegate)


# 개요
- 라이브 게임 개발 당시 가장 어려웠던 것은 회사 코드의 콜백 구조를 이해하는 것 이었다.
- :star:유니티에서 **콜백**을 잘 사용하려면 어떻게 설계해야 할지 공부해본다.

# 이전 공부
- :link:[Link](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Unity/Unity%20Study%20(2022.04%20~%202022.06).pdf)
- 기본적인 개념을 공부했다.
- 이론만 공부를 한 상태에서 실무를 해보니 어려웠다.
- 실무에서 고민한 내용을 지금 연습해보자.

# Design Pattern : Observer Pattern (Listener & Callback)
### 1. 개념
- :link:[Link_1 : 전반적인 개념](https://velog.io/@haero_kim/%EC%98%B5%EC%A0%80%EB%B2%84-%ED%8C%A8%ED%84%B4-%EA%B0%9C%EB%85%90-%EB%96%A0%EB%A8%B9%EC%97%AC%EB%93%9C%EB%A6%BD%EB%8B%88%EB%8B%A4)
- **내 생각 : 어떤 이벤트가 발생하면 이벤트 처리기에 등록된 메서드들이 호출되는 패턴이 옵저버 패턴이다.** 
- 문제 상황
  - ![image](https://user-images.githubusercontent.com/55792986/186142782-682ee612-9601-44f2-85c1-6974f9cfcb22.png)
- 해결 방법 : 종 = 인터페이스 = 이벤트(C#)
  - ![image](https://user-images.githubusercontent.com/55792986/186142895-16bc60d2-609a-4ff7-97fd-6a9a200444de.png)
  - ![image](https://user-images.githubusercontent.com/55792986/186142945-43b38284-b326-4a37-91af-5b0ab9116389.png)
  
### 2. C# 예시
- [Link_2 : C#에서 Event를 사용하면서 구현한 예시](https://www.codeproject.com/Articles/1084848/Implementing-Observer-Pattern-with-Events-Csharp)
- Subject(주체) 
  - ![image](https://user-images.githubusercontent.com/55792986/186143915-5b6c17aa-8ab9-4b94-a680-052be8b94a2f.png)
    - 이벤트를 처리하는 처리기를 선언한다.
    - 처리기가 이벤트에 등록되는 메서드들과 이들을 호출하는 주체를 묶어준다.
    - 이벤트가 발생해야 함을 알려주는 주체다. 다시 말해 어떤 상황이 도래해서 호출되어야 하는 메시지 묶음를 호출하는 인스턴스.
- Observer(관찰자, 리스너)
  - ![image](https://user-images.githubusercontent.com/55792986/186144376-be4ede98-6ea8-4f2d-bc40-abde766bb9c3.png)
    - 주체에서 발생한 이벤트에 등록할 메서드들이 여기서 등록된다.

### 3. 결론
- **내 생각 : 콜백 메서드들을 EventHandler에 등록하면, EventHandler에 신호가 오는지 귀를 기울인다. 신호가 오면 EventHandler에서 콜백 메서드들이 호출된다.**
  

# Delegate & Event의 사용 이유
- :star:사용 이유 : 이벤트를 사용하지 않으면 메서드들을 호출 시킬 때 메서드를 보유한 객체를 메서드들을 호출시키는 스크립트에서 선언을 해야 한다.
  - 다시 말해 **스크립트가 복잡**해진다.
- **이벤트를 사용하면 스크립트(클래스)간에 연결이 필요 없다.**
  - 이벤트에 어떤 메서드들이 등록되어 있는지 알 필요가 없다.  
  - :link:[Reference](https://daebalstudio.tistory.com/entry/%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%99%84%EB%B2%BD%ED%95%98%EA%B2%8C-%EC%9D%B4%ED%95%B4%ED%95%98%EA%B8%B0)

# 이벤트 주도적 프로그래밍
- 면접 때 잘못 이해하고 있어서 면접관님에게 지적 받은 부분이다. 제대로 이해하고 다시 작성해본다.
- 
# Event에 대해 좀 더 깊게 공부해 본다.
### 1. Event를 이용할 때는 .Net의 EventHandler를 이용하는 방법이 좋다.
- [Reference](https://docs.microsoft.com/ko-kr/dotnet/api/system.eventhandler?view=net-6.0)
- .Net에서 이벤트를 일관된 패턴으로 사용하도록 구현해놓은 표준 객체
  - (object와 EventArgs)를 인자로 갖는 메서드를 등록한다.
### 2. public으로 선언해도 오직 해당 클래스 내부에서만 호출할 수 있습니다.
- 참고 자료_1
  - ![image](https://user-images.githubusercontent.com/55792986/186148873-168b521e-799c-41d9-8a5d-69990264e4e6.png)
  - ![image](https://user-images.githubusercontent.com/55792986/186146881-9ceb03ad-b7b9-44cc-babf-7bfd6a844e4f.png)
    - 다른 클래스에서 이벤트를 호출하려 하면 에러가 난다.
    
- 참고 자료_2 : 다른 클래스에서 이벤트를 호출하는 방법
  - <img width="659" alt="20220823_203316" src="https://user-images.githubusercontent.com/55792986/186147650-682687d7-f30c-437a-b223-c1626c33974e.png">
  - <img width="666" alt="20220823_203357" src="https://user-images.githubusercontent.com/55792986/186147772-512623d6-8e8e-4c4b-bf1b-e675f817d8df.png">

### 3. Event(==EventHandler)는 static으로 선언해야 하는가?
- 이건 실제로 코딩하면서 결정해보자. static으로 하면 이 이벤트처리기는 프로그램 전체에서 공유.
  
# 고민
- 2.1 하나의 Delegate에 다수의 Event를 등록하는가?
- 질문 : ![image](https://user-images.githubusercontent.com/55792986/185571375-0feb7d4b-2e2c-4a02-a867-8866e3010231.png)
- 답변 : ![image](https://user-images.githubusercontent.com/55792986/185571638-7dc0041b-f475-401a-9394-f8bc7c668068.png)
  - 가능하다. 그래서 EventHandler Class를 .Net에서 구현해줬다.

- 2.5 이 메소드는 반드시 상속받아서 사용해야 한다를 표기하는 것? 근데 구현은 위에서 해줌.
  - 다시 말 해 구현은 위에서 하는 데 위의 스크립트는 쓰지 않고 무조건 자식에서만 쓰임을 표기
- 2.6 delegate에 다양한 타입
- 2.7 하나의 delegate에 여러가지 이벤트
- 2.8 eventhandler api
  
# .Net API : EventHandler 
- 다른 스크립트의 함수 포인터를 추가 할 수 있다.
  - 게임내에 존재하는 모든 함수를 하나의 EventHandler 객체에 등록 시킬 수 있음을 의미한다.

# 연습
### 1. 구현
- 미니언이 죽었을 때 죽인 캐릭터에서 메서드가 호출되고, 게임매니저에서 관련 메서드가 호출되도록 구현한다.

### 2. EventHandler와 Delegate
- .Net의 EventHandler를 이용하여 delegate를 만들지 않는다. (delegate 만드는 건 귀찮다!)