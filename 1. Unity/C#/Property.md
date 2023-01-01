# 목차
- [목차](#목차)
- [개요](#개요)
- [Property를 사용해야 하는 이유](#property를-사용해야-하는-이유)
- [:star:캡슐화 : 멤버 변수(특히 데이터 필드)는 public이 아닌 private으로 사용해야 한다.](#star캡슐화--멤버-변수특히-데이터-필드는-public이-아닌-private으로-사용해야-한다)
- [그러면 이론상 모든 멤버의 필드는 프로퍼티인가?](#그러면-이론상-모든-멤버의-필드는-프로퍼티인가)
- [Property 기초](#property-기초)
- [getter와 setter의 특징](#getter와-setter의-특징)
- [자동속성](#자동속성)

# 개요
- 많이 배워봤고 들어봤지만 직접 써 본 경험은 적은 키워드다.

# Property를 사용해야 하는 이유
- :link:[Reference](https://developer-talk.tistory.com/39)
- 변수(프로퍼티)의 값을 변경하거나(set) 가져올 때(get) 조건을 걸어서 변수의 접근을 제어할 수 있기 때문입니다.
  - get, set을 만들 때 조건문으로 설정한다.
- get과 set이 간단한 경우 매우 간편하게 선언할 수 있다.
  - C#에서 자동으로 getter와 setter를 만들어 주는 편리한 property 문법이 있다.
  
# :star:캡슐화 : 멤버 변수(특히 데이터 필드)는 public이 아닌 private으로 사용해야 한다. 
- public으로 사용하면 '외부에 잘못된 사용'으로 객체의 상태가 잘못될 수 있다.
  - 접근이 간편하여 생기는 오류다.
  - '객체지향의 사실과 오해'를 읽으면 바로 이해할 수 있다.
    - 필드는 private로 하고 public 메서드(행동)로 해당 필드를 관리한다.
- 그래서 우리는 private로 선언하고 property를 이용한다.

# 그러면 이론상 모든 멤버의 필드는 프로퍼티인가?
- 멤버의 필드 중에 무엇이 데이터 필드일까? 기준이 있을까?
- 모든 데이터 필드는 프로퍼티로 이용해야 하는가?

# Property 기초 
- ![image](https://user-images.githubusercontent.com/55792986/207478777-de5bddc1-190f-4af7-a078-84fd85e8d9a0.png)
~~~c#
p.Age = 10; //좌변식이므로 자동으로 set이 호출.
int n2 = p.Age //우변식이므로 자동으로 get이 호출.
~~~
- 실제로는 메서드 이지만 사용시에는 필드처럼 보인다.

# getter와 setter의 특징
- getter와 setter 중에 하나만 있어도 상관은 없다.
- 접근지정자를 지정해서 사용할 수 있는 위치를 지정할 수 있다.

# 자동속성
![image](https://user-images.githubusercontent.com/55792986/207480187-face6cc1-3b8b-43ee-8ae3-3dc856e4eee2.png)
- 간단하게 만들고 싶을 때는 자동 속성을 이용하면 된다.
- 자동속성은 public과 동일한 데 굳이 사용하는 이유?
  - 나중에 변경 할 때 프로퍼티로 만들었다면 매우 쉽게 getter와 setter에 기능을 추가 할 수 있다.

