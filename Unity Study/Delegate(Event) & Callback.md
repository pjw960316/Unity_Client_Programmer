# 목차
- [목차](#목차)
- [개요](#개요)
- [Design Pattern : Observer Pattern](#design-pattern--observer-pattern)
- [Delegate & Event](#delegate--event)
    - [1. 이전 공부](#1-이전-공부)
    - [2. 사용 이유](#2-사용-이유)
    - [2. 막 고민](#2-막-고민)
    - [3. 참고](#3-참고)
    - [생각](#생각)
- [EventHandler Class](#eventhandler-class)
- [연습](#연습)
    - [1. 구현](#1-구현)
    - [2. EventHandler와 Delegate](#2-eventhandler와-delegate)


# 개요
- 라이브 게임 개발 당시 가장 어려웠던 것은 회사 코드의 콜백 구조를 이해하는 것 이었다.
- 유니티에서 콜백을 잘 사용하려면 어떻게 설계해야 할지 공부해본다.

# Design Pattern : Observer Pattern
- [Link_1](https://velog.io/@haero_kim/%EC%98%B5%EC%A0%80%EB%B2%84-%ED%8C%A8%ED%84%B4-%EA%B0%9C%EB%85%90-%EB%96%A0%EB%A8%B9%EC%97%AC%EB%93%9C%EB%A6%BD%EB%8B%88%EB%8B%A4)
- [Link_2](https://www.codeproject.com/Articles/1084848/Implementing-Observer-Pattern-with-Events-Csharp)

# Delegate & Event
- [Link](https://gamedevbeginner.com/events-and-delegates-in-unity/)
### 1. 이전 공부
- [Link](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Unity%20Study/Previous%20Unity%20Study_2.pdf)
- 기본적인 개념을 공부했다.
- 이론만 공부를 한 상태에서 실무를 해보니 어려웠다.
- 실무에서 고민한 내용을 지금 연습해보자.

### 2. 사용 이유
- 이벤트를 사용하지 않으면 메서드들을 호출 시킬 때 메서드를 보유한 객체를 메서드들을 호출시키는 스크립트에서 선언을 해야 한다.
  - 스크립트가 복잡해진다.
  - 스크립트(클래스)간에 연결이 필요 없다.
- 이벤트에 어떤 메서드들이 등록되어 있는지 알 필요가 없다.  
- [Reference](https://daebalstudio.tistory.com/entry/%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%99%84%EB%B2%BD%ED%95%98%EA%B2%8C-%EC%9D%B4%ED%95%B4%ED%95%98%EA%B8%B0)
### 2. 막 고민
- 2.1 하나의 Delegate에 다수의 Event를 등록하는가?
- 질문 : ![image](https://user-images.githubusercontent.com/55792986/185571375-0feb7d4b-2e2c-4a02-a867-8866e3010231.png)
- 답변 : ![image](https://user-images.githubusercontent.com/55792986/185571638-7dc0041b-f475-401a-9394-f8bc7c668068.png)
  - 가능하다. 그래서 EventHandler Class를 .Net에서 구현해줬다.

- 2.2 이벤트는 하나의 스크립트에 몰아서 구현하는가?

- 2.3 참고참고
- https://daebalstudio.tistory.com/entry/%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%99%84%EB%B2%BD%ED%95%98%EA%B2%8C-%EC%9D%B4%ED%95%B4%ED%95%98%EA%B8%B0

- 2.4 이벤트는 static ? private ? public?
  - 이벤트는 모든 스크립트에서 참고 해야 하지 않는가?
  - 이벤트의 범용성이 궁금한 것.

- 2.5 이 메소드는 반드시 상속받아서 사용해야 한다를 표기하는 것? 근데 구현은 위에서 해줌.
  - 다시 말 해 구현은 위에서 하는 데 위의 스크립트는 쓰지 않고 무조건 자식에서만 쓰임을 표기
### 3. 참고
- [Unity Official Youtube](https://www.youtube.com/watch?v=k4JlFxPcqlg)

### 생각
- delegate에 다양한 타입
- 하나의 delegate에 여러가지 이벤트
- eventhandler api

# EventHandler Class
- 다른 스크립트의 함수 포인터를 추가 할 수 있다.
  - 게임내에 존재하는 모든 함수를 하나의 EventHandler 객체에 등록 시킬 수 있음을 의미한다.
# 연습
### 1. 구현
- 미니언이 죽었을 때 죽인 캐릭터에서 메서드가 호출되고, 게임매니저에서 관련 메서드가 호출되도록 구현한다.

### 2. EventHandler와 Delegate
- .Net의 EventHandler를 이용하여 delegate를 만들지 않는다. (delegate 만드는 건 귀찮다!)