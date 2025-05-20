## awake와 start
- 비교 다 하고 최후에 awake는 이때 쓰고, start는 이때 쓴다
- 일단 awake가 start보다 빠름.
- 이걸 제대로 이해하기 위해서는 의존성 주입을 알아야 하는데 의존성 주입은 쉬움
- Instance의 메서드가 동작을 할 때 외부의 instance에서 계산한 값이 필요한 것.
- 그러니까 초기화를 할 때 awake에는 외부 의존성이 없는 초기화 , start는 외부 의존성이 있는 초기화를 하는 게 좋지 않을까?
- 둘 다 초기화
- awake는 다른 객체의 초기화 상태와 상관없는 초기화를 진행하고, start는 다른 객체의 초기화 상태와 상관있는 초기화를 진행한다. awake은 의존적이지 않고 start는 의존적인 초기화를 하자.
- ![alt text](./captures/20250520.png)
- [Reference](https://artoonsolutions.com/unity-awake-vs-start/)

## Dependency Injection (의존성 주입)은 현재 나의 지식으로 이해한 것은 클래스 A가 클래스 B를 필드로 들고 있고, B의 행동으로 인해 A의 행동을 수행할 수 있는 의존적인 상태를 만드는 것 이다.
- 