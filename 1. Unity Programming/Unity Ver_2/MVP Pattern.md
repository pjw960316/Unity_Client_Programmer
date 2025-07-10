## :fireworks: MVP + Manager + User

<details>
  <summary> :point_up_2: 누르면 매우 큰 이미지가 나옵니다...  </summary>

![alt text](./captures/20250618.png)

</details>

<br><br>

## :fire: MVP+Manager Pattern을 써라. <br> :fire: Model = 순수 Data Class <br> :fire: View = UI 요소를 담당하는 MonoBehaviour Class <br> :fire: Presenter = Model 과 View를 1 : 1로 연결하는 Class <br> :fire: Manager = 여러 개의 Presenter class들을 관리하는 class.
- Model은 Data Class로도 구현이 되지만, PlayerPrefs or ScriptableObject로도 구현이 된다.
  - :link:[which methods should be in Model class except set/get members?](https://stackoverflow.com/questions/13550143/mvc-which-methods-should-be-in-model-class-except-set-get-members)
- 연결을 위해 presenter는 model 과 view를 멤버로 들고 있는다.
- 관리를 위해 manager는 presenter를 멤버로 들고 있는다.
- OOOO이 XXXX를 들고 있다 or 관리하고 있다 = 멤버로 저장하고 있다.
- OOOO이 XXXX를 모른다 = 멤버로 저장하고 있지 않다.

#### [Sound System을 통한 예시]
![alt text](./captures/20250612.png)
- Presenter 끼리의 직접 소통은 금지하고 SoundManager를 통해 소통한다.
- SoundManager가 UIManager와 소통하기 위해서는 SoundManager가 들고 있는 Presenter 들을 통해 필요한 정보를 가져와서 전달해야 한다.

<br><br>

## :earth_asia: Model, View, Present, Manager에 대한 역할과 <br> 무엇을 적어야 하는 지 적어 놓았다. <br> 제목 말고 아래의 글 까지 읽어야 한다. 

<br><br>

## :fire: Model
#### 1. 역할 및 책임

<br>

#### 2. 멤버로 들고 있을 것

<br>

#### 3. 특징
- 절대로 View를 멤버로 갖지 않는다.
- 1개의 Model로 여러개의 Presenter와 연결 할 수 있다?? 
- 게임 로드시에 모두 로딩한다??

<br><br>

## :fire: View
#### 1. 역할 및 책임
> the View is responsible for handling user input.
  - View는 User의 Input을 받고 Presenter에게 알리는 역할만 하면 된다.
  - 이를 UniRx를 이용해서 구현한다.

<br>

#### 2. 멤버로 들고 있을 것
- **의견이 갈리지만 Presenter**
  - :link:[Model-View-Presenter implementation thoughts](https://softwareengineering.stackexchange.com/questions/60774/model-view-presenter-implementation-thoughts?utm_source=chatgpt.com)
    - 3가지 Choice가 있다.
  - :link:[The Model-View-Presenter pattern and its implementation in ASP.NET](https://www.codeproject.com/Articles/5388787/The-Model-View-Presenter-pattern-and-its-implement)
    - view가 presenter를 class Type으로 들고 있다.
- **private Subject & public IObservable**
  - View에서는 event를 감지만 하고, event handle logic은 Presenter에서 처리하도록 rx를 제공만 한다. 
~~~c#
private readonly Subject<Unit> _onSoundButtonClicked = new();
public IObservable<Unit> OnSoundButtonClicked => _onSoundButtonClicked;
~~~
- **Presenter가 MonoBehaviour 관련 데이터(ex : transform)를 요청 할 때, 그걸 줄 수 있는 Public Get Method**

<br>

#### 3. 특징
- 절대로 Model을 멤버로 갖지 않는다.

<br><br>

## :fire: Presenter
#### 1. 역할 및 책임
> Presenter: Model과 View 사이를 연결하는 **중재자(mediator)**입니다. View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
  - SoundManager가 필요한 정보를 UIManager에게 전달하려면 SoundManager가 Presenter를 참조해서 정보를 얻고, UIManager에게 전달한다.
> The presenter receives events from the view, retrieves data from the model and updates the view with the data.

<br>

#### 2. 멤버로 들고 있을 것
- **View 와 Model을 멤버로 갖는다.**
- **View에서 전달 받은 event의 <ins>Handle Method</ins>**
~~~c#
 _view.OnSoundButtonClicked.Subscribe(unit => OpenPopup()).AddTo(_disposable);
 ~~~
- **Manager에게 Request 하는 Method**

<br>

#### 3. 기타 사항
> Model does not know the View or the Presenter. Presenter knows both Models and Views, but only through their interfaces.
- :link:[Unity에서 MVP 패턴으로 UI를 깔끔하게 관리하기](https://wolstar.tistory.com/73)

<br><br>

## :fire: Manager
#### 1. 역할 및 책임
- 좀 더 실력이 늘면 factory class와 분리하는 게 맞지만 지금은 factory class의 역할을 manager에서 해도 좋을 것 같다. (factory class에서 presenter에 model과 view의 interface를 argument로 전달해서 DI를 진행한다.)

<br>

#### 2. 멤버로 들고 있을 것

<br>

#### 3. 기타 사항
:question: :link:[여러 개의 view와 1개의 model을 대응할 때 presenter?](https://chatgpt.com/c/68501688-00ec-8004-af44-6a66c19db681)
  - 나는 이런 걸 Manager로 해버리려 했다. 예를 들어 StringManager 1개가 모든 string을 관리하는 것.
  - 그러나 토론에서는 1:1로 presenter를 만들라는데, 일단 stringManager를 구현하면서 여기를 수정한다.

<br><br>

## :fire: Presenter가 View와 1대1 대응을 하면 view를 Implemented Type으로 들고 있어도 된다. <br> :fire: Presenter가 View와 1대다 대응을 하면 view를 Interface Type으로 들고 있는 게 좋다. <br> :question: 유지 보수를 생각하면 언제나 Interface로 들고 있는 게 맞는 듯 하지만, 아직 명확한 답을 내리지 못했다.

<br><br>

## :question: 무수히 많은 View와 무수히 많은 Presenter가 Project에 있지만, <br> 그들의 추상화 레벨이 같다면 Presenter가 자신과 같은 추상화 레벨의 View Type을 명시적으로 갖도록 구현할 수 있다?
- 어떤 View가 상속 단계 4단계 중 2단계인데 이 View와 연결된 Presenter가 있다. 그러면 이 Presenter는 View를 IView 타입으로 자신의 필드로 들고 있는 게 아니라, View의 상속 2단계 타입으로 들고 있는다.  
- :bangbang:한 동안 무조건 Iview로 들고 있어야 한다고 생각을 했고, 이 위에도 그런데. 좀 더 구현하면서 두 개념을 다듬어 보자
- :link:[내가 적은 Abstract Programming](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/%EB%B2%88%EC%99%B8_Abstract%20Programming.md)

<br><br>

## :earth_asia: MVP + Manager의 역할을 위에서 알아보았고 각각 생성을 할 수 있다. <br> 이제는 Dependency Injection을 통해 <br> 서로를 연결 시켜주어야 한다.

<br><br>

## :fire: Dependency Injection은 필요한 instance를 직접 new로 생성하지 않고, <br> 외부(다른 class or interface)에서 제공 받는 것 이다. <br><br> :fire: 만약 필요한 instance의 field가 100개라면 <br> 직접 new 할 때 100개의 field를 모두 초기화해줘야 하는 끔찍한 일이 벌어진다. <br> :fire: 흔히 Dependency를 다른 class를 '알아야 한다'고 표현하는데 <br> new 할 때 100개의 field를 다 초기화 해줘야 하니 '알아야 한다'와 일맥상통 한다.

#### [DI 없음 : 직접 new로 생성하는 쓰레기 코드]
~~~c#
public class Employee 
{
    private ComplexClass oneHundredFieldInstance; // 필드 100개인 instance

    public Employee() 
    {
        this.oneHundredFieldInstance = new ComplexClass(arg0, arg1, arg2, ... , arg99); // 직접 생성
    }
}
~~~

<br>

#### [DI 있음 : 외부로 주입 받는 좋은 코드]
~~~c#
public class Employee 
{
    private IComplexClass oneHundredFieldInstance; // 필드 100개인 instance + Interface로 받는다.

    public Employee(IComplexClass oneHundredFieldInstance) 
    { 
      // 외부에서 생성된 인스턴스를 받음
        this.oneHundredFieldInstance = oneHundredFieldInstance;
    }
}
~~~
- :link:[How to explain dependency injection to a 5-year-old? - what about this? 형님 글_추천수 92](https://stackoverflow.com/questions/1638919/how-to-explain-dependency-injection-to-a-5-year-old)
> The main problem comes when you need to test one particular object, you need to create an instance of other object, and most likely you need to create an instance of yet other object to do that. The chain may become unmanageable.
- 테스트 할 때 외부로 주입을 받는 다면 MockAddress instance를 만들어서 쉽게 테스트가 가능하지만, 내가 직접 생성해야 하면 내가 직접 다 만들어야 하는 괴로움이 있다. 

<br><br>

## :zzz: MVC는 Controller가 애매해서 Unity에서 사용하기 어렵다고 생각한다.
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
