## :fire::one: Model의 역할 및 책임
#### 1. Presenter 또는 Manager가 데이터를 요청 할 때 get, set, update를 담당할 책임이 있다.
- :star: Get, Set, Update는 매우 단순한 동작을 처리해야 한다. 
- 그러므로, Presenter 또는 Manager에서 Get, Set, Update를 단순하게 처리 할 수 있게 가공하는 로직을 구현해야 한다.

<br><br>

## :fire::two: Model이 멤버로 들고 있을 것
#### 1. 일반 필드
- :question: Set을 Property로 사용하기 때문에 Manager에서 사용해도 문제 없다고 판단한다. 하지만 이 것도 Model에서 진행 할 지 수칙을 정해야 한다.
#### 2.Container 
- Container(List, Dictionary)의 Set, Update 종류의 데이터 Setter Method
- ImmutableContainer의 Get 종류의 데이터 Getter Method
- :link:[06장_Type and Member Basics (=Class).md](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/06%EC%9E%A5_Type%20and%20Member%20Basics%20(%3DClass).md)

<br><br>

## :fire::three: Model의 특징
#### 1. 절대로 View를 멤버로 갖지 않는다.