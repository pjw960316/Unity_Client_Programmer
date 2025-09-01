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
<details>
  <summary> :point_up_2: 누르면 매우 큰 이미지가 나옵니다...  </summary>

![alt text](./captures/20250612.png)

</details>

- Presenter 끼리의 직접 소통은 금지하고 SoundManager를 통해 소통한다.
- SoundManager가 UIManager와 소통하기 위해서는 SoundManager가 들고 있는 Presenter 들을 통해 필요한 정보를 가져와서 전달해야 한다.

<br><br>

## :earth_asia: Model, View, Present, Manager에 대한 역할과 <br> 무엇을 적어야 하는 지 적어 놓았다. <br> 제목 말고 아래의 글 까지 읽어야 한다. 

<br><br>






## :fireworks: MVP Pattern을 사용하니 체감되는 생산성
- View, Presenter, Model을 분리해서 개발하니 View 구현 시간이 현저히 줄었다. View는 의도적으로 멍청하게 만들어 두고, Presenter에서 모든 로직을 처리한 뒤 View에는 최소한의 데이터만 전달(SetText 정도) 하면 된다. 이 구조 덕분에 View 개발은 단순 작업 수준으로 떨어지고, 핵심은 Presenter 로직 설계에만 집중하면 되므로 전체 생산성이 크게 향상됨을 체감했다.

<br>

- 전 직장에서는 View와 Presenter 로직이 뒤섞여 있어 View 수정도 로직 파악이 필요했고, 결과적으로 작업 시간이 오래 걸렸다. 역할을 명확히 분리하자, 반복적인 View 작업이 단순화되고 유지보수도 쉬워졌다.

<br><br>

## :fire: Scene에서 큰 View(Popup)는 작은 View들(Button, Image, ScrollView)을 들고 있다. <br> :fire: 작은 View를 구현할 때 View Struct 와 View Widget 중 선택한다.

#### :one: 거대해졌는데 아직 로직은 없다 -> Presenter가 쉽게 이용하도록 <ins>**public Struct로 묶기**</ins>

<br>

~~~c#
// Ebuttons 하나 추가하려고 Widget으로 빼서 Script를 만드는 것 도 낭비다.
public struct ButtonData
{
  public Button button;
  public EButtons buttonType;
}
~~~

<br>

#### :two: 거대해졌는데 로직도 필요하다 -> <ins>**Widget Script로 빼기**</ins>   
- :link:[Do you create a component for every element? ](https://www.reddit.com/r/reactjs/comments/kp356z/do_you_create_a_component_for_every_element/)
> extracting the button into a component that lets you control these different variants with props would save a lot of time of creating it on the fly each time. same with inputs/titles, there maybe the repeated variants across your app that would be easier to manage by extracting a component. Everything is a trade off though. I'm not saying doing this every time is the right way, for example I also think that trying to make every single thing reusable can overcomplicate things if you take it too far.
- Popup 안에 있는 Button을 Widget화 시켜서 스크립트를 만들어 관리한다. 크게 볼 때 많은 중복을 줄일 수 있다.

<br>

#### [회사에서 적어 놓은 내용]
<details>
  <summary> :point_up_2: 눌러서 이미지를 확인 합니다.  </summary>

- ![alt text](./captures/20250711.png)

</details>

<br><br>

## :fire: Presenter가 View와 1대1 대응을 하면 view를 Implemented Type으로 들고 있어도 된다. <br> :fire: Presenter가 View와 1대다 대응을 하면 view를 Interface Type으로 들고 있는 게 좋다. <br> :question: 유지 보수를 생각하면 언제나 Interface로 들고 있는 게 맞는 듯 하지만, 아직 명확한 답을 내리지 못했다.

<br><br>

## :question: 무수히 다양한 추상화 레벨의 View와 Presenter가 Project에 있지만, <br> 어떤 Presenter와 View가 추상화 레벨이 같다면, 해당 Presenter가 자신과 같은 추상화 레벨의 View Type을 명시적으로 멤버로 들고 있자???
- 어떤 View의 상속 단계가 4단계 중 2단계이다. 이 View와 연결된 Presenter가 있다. 그러면 이 Presenter는 IView 타입으로 view를 자신의 필드로 들고 있는 게 아니라, View의 상속 2단계 타입으로 들고 있는다.  
- :bangbang:한 동안 무조건 Iview로 들고 있어야 한다고 생각을 했고, 이 위에도 그런데. 좀 더 구현하면서 두 개념을 다듬어 보자
- :link:[내가 적은 Abstract Programming](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/%EB%B2%88%EC%99%B8_Abstract%20Programming.md)

<br><br>

## :fire: View는 필드로 다른 view를 들고 있는 경우가 대부분이다. <br> (예를 들면, Popup 내부에는 여러 개의 Button이 있다.) <br> :fire:구현 초기에는, Popup에서 모든 하위 View들을 필드로 관리하는 데 어려움이 없다. <br> 그러나 추후에 하위 View들이 거대해지면서 코드의 중복이 생기기 시작한다. <br> :fire::star:이 때가 필드로 들고 있던 view를 독립적인 새로운 View Script를 빼서 관리해야 할 때다. <br> 회사에서는 이걸 widget화 한다고 배웠었다.



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
