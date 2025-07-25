## :fireworks: 자주 쓰는 UniRx의 Method 뜯어 보기

<br><br>

## :fire: Observable.Timer로 이해하는 자동으로 Dispose()까지 불리는 과정

#### [Observable.Timer 예제]
~~~c#
Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ => { MainCanvas.ToastMessage.SetActive(false); });
~~~
- :one: **Static으로 편하게 받고, 내부에서 Instance를 생성한다. (쉽게 사용 가능)**
  - ![alt text](./captures/20250725_1.png)
- :two: **내부적으로는 생성자가 이렇게 동작하는데 이것까지는 알 필요 없다.**
- ![alt text](./captures/20250725_2.png)
- :three: **BaseType이 SubScribe를 구현하고 있다.**
  - ![alt text](./captures/20250725_3.png)
  - ![alt text](./captures/20250725_4.png)
- :four: **Action이 있어서 ObservableExtension을 이용하고 있다.**
  - ![alt text](./captures/20250725_5.png)
  - ![alt text](./captures/20250725_6.png)
- :five: **내가 적은 1f 시간 만큼 Time이 걸린다.**
  - ![alt text](./captures/20250725_7.png)
  - ![alt text](./captures/20250725_8.png)
- wkfy
  - ![alt text](./captures/20250725_9.png)
  - ![alt text](./captures/20250725_10.png)
  - ![alt text](./captures/20250725_11.png)
  - ![alt text](./captures/20250725_12.png)
  - ![alt text](./captures/20250725_13.png)
  
<br>

- **결론**
  - Observable.Timer가 반환하는 TimerObservable은 OperatorObservableBase<long>를 상속받는다. 
  - ObservableExtensions.Subscribe는 IObservable<T>.Subscribe를 호출하지만, 이 인터페이스의 실제 구현은 OperatorObservableBase<T> Subscribe이다.
  - 따라서 결과적으로 Observable.Timer의 Subscribe는 OperatorObservableBase의 Subscribe가 된다.


