## :fire: MVP에서 View에 Subject, Iobservable을 만들고 <br> Presenter에서 Iobservable에 SubScribe를 하는 것은 일회성이 아닐 때 <br> :fire: 일회성일 때는 Observable Static Class를 사용한다.
~~~c#
Observable.Timer(TimeSpan.FromSeconds(playingTime))
            .Subscribe(_ => RequestPlayingWakeUpSound())
            .AddTo(Disposable);
~~~