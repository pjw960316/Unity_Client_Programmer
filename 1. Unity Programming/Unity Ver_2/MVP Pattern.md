## :fire: MVP Pattern을 써라. <br> :fire: Model = 순수 Data Class <br> :fire: View = UI 요소를 담당하는 MonoBehaviour Class <br> :fire: Presenter = Model 과 View를 1 : 1로 연결하는 Class <br> :fire: Manager = 여러 개의 Presenter class들을 관리하는 class.
- Model은 Data Class로도 구현이 되지만, PlayerPrefs or ScriptableObject로도 구현이 된다.
  - :link:[which methods should be in Model class except set/get members?](https://stackoverflow.com/questions/13550143/mvc-which-methods-should-be-in-model-class-except-set-get-members)
- 연결을 위해 presenter는 model 과 view를 멤버로 들고 있는다.
- 관리를 위해 manager는 presenter를 멤버로 들고 있는다.

#### [Sound System을 통한 예시]
![alt text](./captures/20250612.png)
- Presenter 끼리의 직접 소통은 금지하고 SoundManager를 통해 소통한다.
- SoundManager가 UIManager와 소통하기 위해서는 SoundManager가 들고 있는 Presenter 들을 통해 필요한 정보를 가져와서 전달해야 한다.

<br><br>

## :fire: Presenter가 명확하게 View와 Model을 분리해준다.
> Presenter: Model과 View 사이를 연결하는 중재자(mediator)입니다. View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
  - SoundManager가 필요한 정보를 UIManager에게 전달하려면 SoundManager가 Presenter를 참조해서 정보를 얻고, UIManager에게 전달한다.
- :link:[Unity에서 MVP 패턴으로 UI를 깔끔하게 관리하기](https://wolstar.tistory.com/73)

<br><br>

## :fire: :TODO: 그래서 MVP & Manager에 존재해야 할 Fields 와 Methods를 잘 정리하자.

<br><br>
## :fire: MVC는 Controller가 애매해서 Unity에서 사용하기 어렵다고 생각한다.
> M stands for Model (Which is a fancy name for **Data**)

> V stands for View (which is UI elements)

> C stands for Contoller, which is the logic **binding the two.**
  - 이 Controller가 Unity에서 일부는 Model class에 들어가고, 일부는 View class에 들어가서 모호한 개념이다.   
- :link:[What is a manager and controller?](https://www.reddit.com/r/Unity3D/comments/qe1s6f/what_is_a_manager_and_controller_in_beginner/)

<br><br>

## :fire: 잡설
- MVVM은 이전 프로젝트에서 Binding Hell을 겪어서 갈아 엎었다는 걸 들은 적이 있다.
- MVRP (R = Reactive) 
  - 지금 결국 구현하는 게 사실 MVRP?
- MVP를 챙기지 않아서 UI에서 모든 걸 처리하는 스파게티 코드 양산 개발자...
