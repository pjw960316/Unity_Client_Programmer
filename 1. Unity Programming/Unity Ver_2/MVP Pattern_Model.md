## :fire::one: Model의 역할 및 책임
#### 1. Presenter 또는 Manager가 데이터를 요청 할 때 get, set, update를 담당할 책임이 있다.
- :star: Get, Set, Update는 매우 단순한 동작을 처리해야 한다. 
- 그러므로, Presenter 또는 Manager에서 Get, Set, Update를 단순하게 처리 할 수 있게 가공하는 로직을 구현해야 한다.

<br><br>

## :fire::two: Model이 멤버로 들고 있을 것
#### 1. private으로 캡슐화 시킨 일반 타입의 데이터 필드
- 외부에서 접근을 public getter property 또는 public getter method로 구현해서 Property에게 제공한다.
#### 2.Container 
- private 형태의 외부 접근 불가한 기본 Container(List, Dictionary) 
- public 형태의 외부 접근 가능한 ImmutableContainer의 Getter Property
- :link:[06장_Type and Member Basics (=Class).md](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/06%EC%9E%A5_Type%20and%20Member%20Basics%20(%3DClass).md)
#### 3. Enum
- Class 외부에 선언하지 않도록 주의한다. (Scope)
- ![alt text](./captures/20250715.png)

<br><br>

## :fire::three: Model의 특징
#### 1. 절대로 View를 멤버로 갖지 않는다.