## :sparkles: 게임을 켜지 않고 물 알림에 응답하고 싶다

HabitGame의 물 알림은 기기에 예약하는 로컬 알림이다. 알림에 `[예]` 버튼을 두고, 게임 화면을 열지 않은 채 응답과 날짜를 기록하고 싶다.

> 게임이 실행되지 않는데 버튼은 누가 처리할까? 그 기록은 어디에 남고, 다음 게임 실행 때 어떻게 읽을까?

<br><br>

## :sparkles: 알아야 할 개념

- **Context**: Android의 앱 기능과 저장소에 접근할 때 사용하는 객체다. 현재 코드에서는 `GetCurrentActivity()`로 얻은 Activity를 사용한다.
- **Builder**: 기본 알림 데이터로 만든 Android 알림 구성 객체다. Java에서 같은 Builder에 ‘예’ 버튼을 붙인 뒤 예약한다.
- **저장소**: 다음 앱 실행 때도 읽을 수 있도록 데이터를 기기에 보관하는 곳이다. 이번에는 `SharedPreferences`를 사용한다.
- :airplane: 저장소의 사용 방식은 [Unity가 Android와 소통하는 구조](./Unity가%20Android와%20소통하는%20구조.md)에 정리한다.

