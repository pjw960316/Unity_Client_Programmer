## :fire: Awake와 Start는 둘 다 초기화를 수행하는 unity event method다. <br> :fire: Awake는 다른 instance와 상관 없이 나 자신이 스스로 초기화 하는 데 문제가 없는 멤버들을 초기화 한다. <br> :fire: Start는 다른 instance의 행동이 완료되었을 때 초기화를 하여 다른 instance에게 의존적인 멤버들을 초기화 한다.
- Awake가 Start보다 빠름. -> 그럼 모든 Awake는 Start보다 빠르냐?
- 초기화에 대한 Dependency Injection은 아래에서 공부한다.
![alt text](./captures/20250520.png)
- [Reference](https://artoonsolutions.com/unity-awake-vs-start/)

## Dependency Injection (=DI =의존성 주입)은 현재 나의 지식으로 이해한 것은 클래스 A가 클래스 B를 필드로 들고 있고, B의 행동으로 인해 A의 행동을 수행할 수 있는 의존적인 상태를 만드는 것 이다.
- 