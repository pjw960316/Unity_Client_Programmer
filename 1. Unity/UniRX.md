# 목차
- [목차](#목차)
- [UniRX 개념](#unirx-개념)
- [선행 개념](#선행-개념)
- [성능 및 장점](#성능-및-장점)
- [사용 하기 좋은 순간](#사용-하기-좋은-순간)
- [Iobservable, Iobserver, Idisposable](#iobservable-iobserver-idisposable)
- [unirx disposable](#unirx-disposable)
- [ReactiveProperty](#reactiveproperty)
- [MainThreadDisPatcher](#mainthreaddispatcher)
- [참고](#참고)

# UniRX 개념
- UniRX (=Reactive Extensions for Unity)
- 유니티에서 비동기적 처리를 더 효율적으로 하기 위한 도구이다. 
- 기존에 .NET Rx가 있었지만 UniRX만큼 Unity C#에 최적화되어 있지는 않았다.

# 선행 개념 
- Reactive Programming
  - 옵저버 패턴을 이용해서 비동기 이벤트를 처리하는 방식
- Observer Pattern
  - [My Github_1](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity/Design%20Pattern/Observer%20Pattern.md)
- Delegate & Event
  - [My Github_2](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity/C%23/Delegate%20%26%20Event.md)
  
# 성능 및 장점
- Update처럼 매 프레임 마다 검사할 필요가 없고 Awake 나 Start에 한 번 등록하면 되기 때문에 가독성이 뛰어나다.
- Event 보다 성능이 좋기 때문에 사용한다.
- Event는 함수를 직접 등록해야 하지만 UniRX는 subscribe에서 해당 데이터만 받아서 동작을 직접 정의할 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/207776982-a210bd9a-600e-4a7e-8e3c-6f25e480aeeb.png)

# 사용 하기 좋은 순간
- ![image](https://user-images.githubusercontent.com/55792986/207777381-c26351c9-e812-40ae-b726-e795ed073a88.png)
  - 플레이어가 마우스 클릭을 누르는 순간만 감지해서 해당 이벤트가 동작 할 수 있도록 한다.

# Iobservable, Iobserver, Idisposable
- 오늘 많이 이해했다.
~~~c#
private void TestUnirxDisposable()
    {
        Subject<int> sub = new Subject<int>();
        
        IObservable<int> a = sub;
        IObserver<int> b = sub;
        
        var disdis = a.Subscribe((x) => Debug.Log("1"));
        a.Subscribe((x) => Debug.Log("2"));
        a.Subscribe((x) => Debug.Log("3"));

        b.OnNext(5);
    }
~~~
- 실제로 이렇게 sub,a,b, 객체를 만들지는 않는다.
- 근데 **여기서 주목할 것은 Iobservable 객체는 subscribe만 할 수 있고 onnext는 못한다. Iobserver 객체는 onnext, oncomplete, onerror만 할 수 있다.** 이는 인터페이스 특성상 해당 메서드만 쓸 수 있기 때문이다.
- 그러면 어떤 메서드의 리턴 타입을 Iobservable로 하면 해당 리턴 타입에 대해서 subscribe를 할 수 있는 것 이다.
- Subject : ISubject : Iobservable & Iobserver
  - ![image](https://user-images.githubusercontent.com/55792986/215671326-0048592c-af76-4a67-89f1-36f0c3a9cf51.png)
  - ![image](https://user-images.githubusercontent.com/55792986/215671441-cc233445-9423-4029-9a70-46136f3da041.png)
- 이게 결국 분석하다보면 **Iobserver 형식으로는 상속관계 상 Onnext 3인방, Iobservable로는 subscribe를 이용할 수 있다.**
  - **인터페이스로 간편하게 하기 위해 Unirx는 대부분의 메서드의 리턴 타입을 Iobservable 또는 Iobserver로 하여 이들을 사용할 수 있도록 한다.**
  - 또한 위의 메서드에 대해 오버로딩과 확장을 구현하여 더 간단하게 사용할 수 있도록 만들어 줬다. 

# unirx disposable
- ![20230131_141547](https://user-images.githubusercontent.com/55792986/215671789-c79d7013-40c3-47f7-a840-d50c1e874e26.png)
- ![image](https://user-images.githubusercontent.com/55792986/215671941-71948207-6d98-4e8d-a7dc-b290b9dd11b7.png)

# ReactiveProperty
- ![image](https://user-images.githubusercontent.com/55792986/215650884-f4e3c4d6-6a35-4591-a0fe-470651c99ecb.png)

# MainThreadDisPatcher
- ![20230206_172626](https://user-images.githubusercontent.com/55792986/216921630-0891ed9a-ab14-45d0-8bdd-15ce5dbc09d2.png)
  - 결국 메인스레드에서 유니티 API를 동작하게 하는 기술이다.
  - 유니티 API가 메인스레드에서 동작해야 함은 변하지 않는다.

# 참고
- [티스토리](https://skuld2000.tistory.com/31)
  - 2번과 3번 강의가 핵심이다.
- [노는게 제일좋아](https://luv-n-interest.tistory.com/1268)
- [Mentum](https://mentum.tistory.com/525)



