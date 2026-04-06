## :fireworks: 좀 더 깊게 정리해도 좋을 것 같다.
- 한동안 Observable, UniTask, 이벤트와 콜백을 이용해서 시간 관련 구현을 처리했었다.
- 그러다보니 코루틴을 쓰지 않았던 것 같다.
- 근데 유니티 생명주기를 보니 생각이 조금 달라졌다.

<br><br>

## :fire: 유니티는 사실 내부에서 매 프레임마다 콜이 되고 있고, 일정한 시간마다도 콜이 되고 있다. <br> 그리고 이 주기 안에 코루틴이 들어간다. <br> :fire: 다시 말해, 코루틴을 위해 새로운 시간 흐름을 만드는 게 아니라 이미 하고 있는 것에 추가하는 개념이다. <br> 이는 곧 최적화 된 시간 흐름 구현이 아닐까?
~~~c#
Coroutine StartCoroutine(IEnumerator routine)

public void StopCoroutine(IEnumerator routine)
~~~
- StartCoroutine()은 내가 실행 시킬 코루틴을 코루틴 리스트에 등록한다.
- 그러면 유니티 생명주기에서 이 코루틴 리스트의 코루틴들을 지속적으로 추적하게 된다.  
- StopCoroutine()은 타겟 코루틴을 코루틴 리스트에 제거한다.