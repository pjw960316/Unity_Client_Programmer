## :fireworks: 자주 사용하는 uniRx 메서드를 가볍게 정리한다.

<br><br>

## :fire: Concat() <br> 2개 이상의 시간 기반 Observable을 순차적으로 실행할 때 사용한다.
~~~c#
Observable.Concat
(
    Observable.Timer(TimeSpan.FromSeconds(1))
        .Do(_ => Debug.Log("A 실행")),
    Observable.Timer(TimeSpan.FromSeconds(2))
        .Do(_ => Debug.Log("B 실행"))
)
.Subscribe()
.AddTo(this);
~~~