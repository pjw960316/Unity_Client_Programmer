## :fire: MVP Pattern을 써라. <br> :fire: Model = Data Class <br> :fire: View = UI Class <br> :fire: Presenter = 1 : 1로 Model 과 View를 연결하는 Class <br> :fire: Manager = 여러 개의 Presenter class들을 관리하는 class. 
#### [MVC는 Controller가 애매해서 Unity에서 사용하기 어렵다고 판단했다.]
> M stands for Model (Which is a fancy name for **Data**)

> V stands for View (which is UI elements)

> C stands for Contoller, which is the logic **binding the two.**
  - 이 Controller가 Unity에서 일부는 Model class에 들어가고, 일부는 View class에 들어가서 모호한 개념이다.   
- [reddit](https://www.reddit.com/r/Unity3D/comments/qe1s6f/what_is_a_manager_and_controller_in_beginner/)

<br><br>

#### [MVP는 Presenter가 명확하게 View와 Model을 분리해준다.]
> Presenter: Model과 View 사이를 연결하는 중재자(mediator)입니다. View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
  - SoundManager가 필요한 정보를 UIManager에게 전달하려면 SoundManager가 Presenter를 참조해서 정보를 얻고, UIManager에게 전달한다.
- [국내 블로그](https://wolstar.tistory.com/73)