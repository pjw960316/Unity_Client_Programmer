# 목차
- [목차](#목차)
- [UniRX 개념](#unirx-개념)
- [선행 개념](#선행-개념)
- [성능 및 장점](#성능-및-장점)
- [사용 하기 좋은 순간](#사용-하기-좋은-순간)
- [참고](#참고)

# UniRX 개념
- UniRX (=Reactive Extensions for Unity)
- 유니티에서 비동기적 처리를 더 효율적으로 하기 위한 도구이다. 
- 기존에 .NET Rx가 있었지만 UniRX만큼 Unity C#에 최적화되어 있지는 않았다.

# 선행 개념 
- Reactive Programming
  - 옵저버 패턴을 이용해서 비동기 이벤트를 처리하는 방식
- Observer Pattern
  - [My Github](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Unity/Delegate%20%26%20Event.md#delegate%EC%99%80-%EA%B4%80%EB%A0%A8%EC%9D%B4-%EC%9E%88%EB%8A%94-design-pattern--observer-pattern-listener--callback)
- Delegate & Event
  - [My Github](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Unity/Delegate%20%26%20Event.md)
# 성능 및 장점
- Update처럼 매 프레임 마다 검사할 필요가 없고 Awake 나 Start에 한 번 등록하면 되기 때문에 가독성이 뛰어나다.
- Event 보다 성능이 좋기 때문에 사용한다.
- Event는 함수를 직접 등록해야 하지만 UniRX는 subscribe에서 해당 데이터만 받아서 동작을 직접 정의할 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/207776982-a210bd9a-600e-4a7e-8e3c-6f25e480aeeb.png)

# 사용 하기 좋은 순간
- ![image](https://user-images.githubusercontent.com/55792986/207777381-c26351c9-e812-40ae-b726-e795ed073a88.png)
    - 플레이어가 마우스 클릭을 누르는 순간만 감지해서 해당 이벤트가 동작 할 수 있도록 한다.

# 참고
- [노는게 제일좋아](https://luv-n-interest.tistory.com/1268)
- [티스토리](https://skuld2000.tistory.com/31)