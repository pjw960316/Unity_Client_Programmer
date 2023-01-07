# 목차
- [목차](#목차)
- [Delegate와 관련이 있는 Design Pattern : Observer Pattern (Listener \& Callback)](#delegate와-관련이-있는-design-pattern--observer-pattern-listener--callback)
    - [1. 개념](#1-개념)
    - [2. C# 예시](#2-c-예시)
    - [3. 결론](#3-결론)
- [한 번 더 공부하는 기본 개념](#한-번-더-공부하는-기본-개념)
    - [1. 완전 기초](#1-완전-기초)
    - [2. 다음 단계](#2-다음-단계)

# Delegate와 관련이 있는 Design Pattern : Observer Pattern (Listener & Callback)
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

# 한 번 더 공부하는 기본 개념
- ![image](https://user-images.githubusercontent.com/55792986/209259652-8b19ea06-fa46-41c5-b398-e4cdb6bf10ff.png)
- 하나의 데이터가 변경될 때 연관된 객체가 반응해서 변경되는 기법
- 테이블과 그래프의 연관 예시로 설명
- :star:subject는 알림을 보내주는 녀석 (notify)

### 1. 완전 기초
  - 테이블이 변경될 때 원형 그래프와 막대 그래프가 변경되는 걸 구현하고 싶다.
  - 테이블에서 뭐 setdata라는 메서드가 있겠다. 그러면 setdata를 하는 순간에 그래프를 변경시키는 코드를 넣으면 된다. 각각의 그래프의 변화에 대한...
  - 하지만 문제가 있을 것
### 2. 다음 단계
- ![image](https://user-images.githubusercontent.com/55792986/209261331-b5a5853e-e5a7-45f5-81d6-98dcbcc41a27.png)
- ![image](https://user-images.githubusercontent.com/55792986/209261772-eaf5cdb8-67ba-4d46-b8ca-f1f15b268bde.png)
  - 테이블이 주체(=subject =값을 변화시키는 녀석)
  - 그래프들이 관찰자(=observer =주체가 변화시키는 것을 관찰하고 있음)
  - 근데 결국에 테이블이랑 그래프가 실제로 보여주는 데이터만 거기에 저장하고 그들의 subject , observer의 기능을 담당하는 것은 각각의 부모 클래스로 구현하면 된다.
  - subject 클래스에 subject로써의 기능을 모두 메서드로 구현한다.(관찰자들에게 알리는 기능도 포함) Observer는 subject한테 변화의 소식을 듣고 그에 따른 행동을 한다.
