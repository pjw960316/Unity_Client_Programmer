## :orange_book: 작가가 은유한 예시를 실제 Unity Programming 구현과 연관 짓는다. <br> :orange_book: 작가의 용어를 이해하고 :star:로 시작하는 문단을 이해한다.

<br><br>

## :fire: '요청' = '호출' = Method Call = Message Send
> 객체가 어떤 행동을 하는 유일한 이유는 다른 객체로부터 요청을 수신했기 때문이다.
- Method Call에는 적절한 argument와 함께 할 수 있다.

<br><br>

## :fire: '책임' = '행동' = Method Signature = Method Head = Interface의 존재 이유

<br><br>

## :fire: '책임 수행' = Method Body = Method 구현

<br><br>

## :fire: '역할' = Class = Type
> 어떤 객체가 수행하는 책임의 집합은 객체가 협력 안에서 수행하는 역할을 암시한다.

> 역할은 협력 안에서 구체적인 객체로 대체될 수 있는 추상적은 협력자다. 따라서 본질적으로 역할은 다른 객체에 의해 대체 가능함을 의미한다.
- 하나의 class를 만들면 해당 type으로 여러 instance를 생성 할 수 있다. (not singleton) 

<br><br>

## :fire: '협력' = Assembly = Unity Project 

<br><br>

## :fireworks: Interface 와 Abstract를 책을 통해 이해한 내용을 스스로 정리했다. <br> 이상한 엘리스의 예시 대신 나의 예시로 변경했다. <br> 아래까지 다 읽는다. 
### :one: Interface / Abstract / concrete <br> :fire: 한국 군대 조직을 생각해보면, 국군의 날 행사가 있다. <br> :fire: Interface는 대통령이다. <br> :fire: Abstract class와 Concrete class 모두 대통령 산하 장성들이다. <br> Abstract class는 국방부 장관이다. <br> Concrete Class는 육군대장, 해군대장, 공군대장, 이하 장성들이다. <br> Abstract class 와 abstract method를 엄밀히 구분할 필요는 없다. 둘이 의미하는 건 동일하다.
- 대통령은 세계 정상회의에서 앞으로의 군대 정책을 외부(public) 정상들에게 발표할 책임이 있다. 
- 국방부 장관은 육군,해군,공군의 군대 정책을 각각의 대장들로부터 대통령의 지침을 바탕으로 세부 내용 준비를 강제시킬 책임이 있다.
  - 국방부 장관 : "각 군(육·해·공)은 대통령의 지침을 바탕으로 세부 내용을 준비하라". 
- 육군대장, 해군대장, 공군대장은 각자 군대 정책의 실제 발표 내용을 구현할 책임이 있다. (이 구현 또한 별 3개 -> 별 2개 -> 별 1개한테 계속 책임을 부여할 수 있다.) 
- 그리고 장성이 아닌 계급에서는 일급 비밀에 관여 할 수 없으므로 장성 이하에서는 sealed 처리로 block 한다.
- 대통령은 정상회의에 가서 당연히 군대 정책을 발표할 책임이 있는데 수행하지 못하면? 한국은 망한다. 
- 국방부 장관이 대통령에게 세부 내용 지침을 전달하지 못하면? 국방부 장관은 책임을 다하지 못하고 사퇴한다.
- 대장들이 국방부 장관에게 세부 내용 지침을 전달하지 못하면? 대장들은 책임을 다하지 못하고 사퇴한다.
  - method body를 구현하지 않은 것은 무시하도록 한다.
> Interfaces are about exposing a contract. “You can use this thing this way”. Abstract classes are used for shared functionality. “Here’s a toolkit in building this class, implement one or two methods and you’re set.” They have a bit of overlap. Many abstract classes also implement interfaces. It’s not uncommon to see where something that takes in the interface, but there’s a base class that provides a lot of common functionality.

> 행동은 결국 객체가 협력에 참여하면서 완수해야 하는 책임을 의미한다.

> 크레이그 라만 : 객체지향 개발에서 가장 중요한 능력은 책임을 능숙하게 소프트웨어 객체에 할당하는 것

> A return type of a method isn't part of the signature of the method for the purposes of method overloading. However, it's part of the signature of the method when determining the compatibility between a delegate and the method that it points to.

### :two::fire: Abstract method는 국방부 장관이 아무것도 정해주지 않고 <br> '정책 세부 내용 준비하라'는 책임만 강제한 method이다. <br>:fire: virtual method는 국방부 장관이 '정책 세부 내용 준비'를 하고, <br> '정책 세부 내용 준비'를 대장들이 하지 않으면 <br> 자신이 준비한 '정책 세부 내용'을 전달하는 것 이다. <br> 대장들은 자신의 '정책 세부 내용'국방부 장관의 '정책 세부 내용'을 포함시킬 수도 있고, 포함시키지 않을 수도 있다.

### :three: 코드
~~~c#
internal interface IPresenter // 대통령
{
    public void Initialize(IView view);

    public void SetView();

    public void BindEvent();
}

public abstract class PresenterBase : IPresenter // 국방부 장관
{
    public virtual void Initialize(IView view)
    {
        _soundManager = SoundManager.Instance;
        _uiToastManager = UIToastManager.Instance;
        _uiManager = UIManager.Instance;
        _myCharacterManager = MyCharacterManager.Instance;
        _scriptableObjectManager = ScriptableObjectManager.Instance;
        _stringManager = StringManager.Instance;
        _presenterManager = PresenterManager.Instance;
        _serverManager = MockServerManager.Instance;
        _view = view;

        InitializeView();
        InitializeModel();
    }

    public abstract void SetView();

    public abstract void BindEvent();

    protected abstract void InitializeView();

    protected abstract void InitializeModel();
}
~~~

<br><br>

## :fire: Method Call('요청')은 Unirx의 Subject 와 Observable로 강제하거나 <br> Event(+Unity Event) System을 통해 강제 시킬 수 있다.
- 책임도 강제가 되고, 요청도 강제가 되면 설계자가 다른 프로그래머에게 내 의도를 강제 시킬 수 있다. 