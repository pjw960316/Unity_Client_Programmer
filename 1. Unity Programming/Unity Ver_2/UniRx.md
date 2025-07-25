## :fireworks: 자주 쓰는 UniRx의 Method 뜯어 보기

<br><br>

## :fire: Observable.Timer 해석

#### [Observable.Timer 예제]
~~~c#
Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ => { MainCanvas.ToastMessage.SetActive(false); });
~~~
- ![alt text](./captures/20250725_1.png)
  - Static으로 편하게 받고, 내부에서 Instance를 생성한다. (쉽게 사용 가능)
- ![alt text](./captures/20250725_2.png)
  - 내부적으로는 생성자가 이렇게 동작하는데 이것까지는 알 필요 없다.
- ![alt text](./captures/20250725_3.png)
- ![alt text](./captures/20250725_4.png)
  - BaseType이 SubScribe를 구현하고 있다.
- ![alt text](./captures/20250725_5.png)
- ![alt text](./captures/20250725_6.png)
  - 하지만 Action이 있어서 ObservableExtension을 이용하고 있다.
- 


