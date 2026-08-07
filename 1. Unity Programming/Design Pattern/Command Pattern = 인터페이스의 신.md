매번 노트에 추상적으로 이들이 결국 같은 걸 하는 거 같은데... 근데 인터페이스가 과하지 않나? 왜냐면 내 기억속 인터페이스는 막 엄청 재사용 되어야 하는데. 근데 그냥 이 싱글턴 매니저 자체에서만 쓰는 인터페이스 만드는 거에 두려워 하지 말자. 지금도 어쨌든 갱신과 저장이란 추상 개념은 앞으로도 계속 동일할거임. 그러면 인터페이스로 우선 만들어 버려. 그리고 MyChacarterManager아래에 여러 nested class를 만들어버려. 걔들은 모두 내가 만든 인터페이스를 상속받는 거지. 이게 전형적인 commandpattern인 거 같구나. 회사로 비유하면, 회사가 mychacratermanager고, 뭐 routine 부서 이렇게 있음. 근데 루틴 부서든 알람 부서든 모두 뭐 6시에 퇴근하면 퇴근 도장 찍어야 된다. 이런 건 니들이 어떻게 찍는 지 관심은 없다만 찍어야함. 이걸 인터페이스로 만드는 거지?

인터페이스는 여러 프로젝트나 시스템에서 널리 재사용되어야만 가치가 있다는 생각에서 벗어난 게 중요합니다.
인터페이스의 가치는 재사용 횟수보다 다음에 있습니다.
서로 다른 구현을 상위 객체가 하나의 책임으로 취급하게 만드는 것

그니까 내가 지금 만든 게 사실 facade에서 비슷하게 구현한
거대한 MYCharcarterManager 회사에서 다양한 직원인거지.
지금 어쨌든 데이터 바꾸고, xml 갱신은 같지만 각각의 부서에서 하는 행동의 specific은 다름. 그러면 이걸 인터페이스로 만들고, nested class에서 상속받아서 구현하는 거지
누가 이럴 수 잇지? 어차피 메서드 다양하게 만드는 거면 큰 의미 없는 거 아닌가요? 근데 상위 타입에서 메서드 이름을 고정하면 100개의 하위 타입도 사실 데이터를 갱신한다는 추상화로 묶는 거지. 그래서 오! 아 이래서 interface를 하는 구나. interface로 지금 추상화를 적으면 다양한 이름의 메서드도 같은 이름을 쓴다. 이게 의미하는 건 같은 책임이다. 당연히 내부 구조는 다르다.

<br>

## :Fireworks: 프롬프트 넣고 CODEX로 생성
~~~markdown
## :fireworks: 경험만 적자.

<br><br>

## :fire: 인터페이스는 많이 재사용해야만 가치가 있는 것이 아니다.
- 예전에는 여러 프로젝트에서 재사용해야 인터페이스를 만들 가치가 있다고 생각했다.
- 중요한 것은 재사용 횟수가 아니라, 서로 다른 구현을 상위 객체가 하나의 책임으로 취급하게 만드는 것이다.
- 루틴 기록과 낮잠 기록은 데이터와 내부 동작이 다르지만, 결국 `MyCharacterData를 변경한다`는 동일한 책임을 가진다.
- 한 Manager 내부에서만 사용하더라도 공통 책임이 분명하면 인터페이스로 묶는 것을 두려워하지 말자.

<br><br>

## :fire: 구체적인 행동이 달라도 추상적인 책임이 같으면 Command로 묶을 수 있다.
- `RoutineRecordCommand`와 `SiestaRecordCommand`의 내부 구현은 다르다.
- 하지만 둘 다 `ApplyTo()`를 통해 `MyCharacterData`에 변경을 적용한다.
- 새로운 변경이 100개 생겨도 상위 객체는 모두 `IMyCharacterDataCommand`로 취급할 수 있다.
- 클래스는 인터페이스를 상속받는다고 표현하기보다 인터페이스를 구현한다고 표현한다.

<br><br>

## :fire: MyCharacterManager는 Facade이자 Command를 실행하는 본사다.
- 각 MVP Presenter는 서로 다른 일을 하는 부서다.
- Command는 각 부서가 제출하는 업무 처리서다.
- `MyCharacterManager`는 처리서를 받아 `MyCharacterData`에 적용하고, 완료되면 공통으로 XML 저장 도장을 찍는다.
- 각 Command는 자신의 구체적인 데이터 변경만 알고, XML 저장은 몰라야 한다.
- 처음에는 Command를 `MyCharacterManager`의 nested class로 두고, 너무 많아지면 별도 파일로 분리한다.

```c#
public interface IMyCharacterDataCommand
{
    void ApplyTo(MyCharacterData data);
}

public void ApplyChange(IMyCharacterDataCommand command)
{
    command.ApplyTo(_myCharacterData);
    SaveMyCharacterDataXml();
}
```

- Command Pattern은 서로 다른 변경 행동을 같은 인터페이스로 묶는다.
- Facade는 외부에서 `MyCharacterData`와 XML 구조를 몰라도 하나의 진입점으로 사용할 수 있게 감춘다.
- 구체적인 작업은 달라도 `데이터 변경 → 저장`이라는 공통 흐름은 `MyCharacterManager`가 끝까지 보장한다.
~~~