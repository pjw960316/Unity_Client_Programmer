## :sparkles: 게임을 켜지 않고 물 알림에 응답하고 싶다

HabitGame의 물 알림은 기기에 예약하는 로컬 알림이다. 알림에 `[예]` 버튼을 두고, 게임 화면을 열지 않은 채 응답과 날짜를 기록하고 싶다.

> 게임이 실행되지 않는데 버튼은 누가 처리할까? 그 기록은 어디에 남고, 다음 게임 실행 때 어떻게 읽을까?

<br><br>

## :sparkles: 알림 버튼 클릭부터 기록 조회까지

1. **게임 실행 중 — C#에서 알림과 버튼 구성**
   Java의 `addConfirmButton()`을 호출해 `[예]` 버튼과 Android 수신기를 연결한다.

2. **게임이 꺼진 뒤 — Android에서 응답 저장**
   `[예]`를 누르면 Android가 수신기의 `onReceive()`를 호출한다. 여기서 날짜를 `SharedPreferences`에 저장한다. Unity 게임 화면은 열지 않는다.

3. **다음 게임 실행 — C#에서 기록 조회**
   Java의 `getLastConfirmedAt()`을 호출해 저장된 날짜를 읽는다. 이후 포인트 판단과 지급은 Unity의 C# 코드에서 처리하도록 연결할 수 있다.

<br><br>

## :sparkles: 수신기와 저장소 구분하기

- **BroadcastReceiver**: Android가 이벤트를 전달하는 수신기의 기본 클래스다. 이번 예제에서는 `WaterNotificationReceiver`가 상속한다.
- **SharedPreferences**: 작은 키·값 데이터를 파일에 저장하고 조회하는 Android API다. 이번 예제에서 말한 앱 전용 영구 저장소에 해당한다.
- 수신기는 클릭을 받아 저장 코드를 실행한다. 날짜는 수신기 객체가 아니라 저장소에 남긴다.
- 다음 실행 때는 저장소에서 데이터를 읽는다. 수신기 객체를 다시 로드하거나, 과거의 클릭 콜백이 자동으로 들어오기를 기다리는 구조가 아니다.
- **AndroidManifest.xml**에는 Android가 실행할 수신기를 등록한다.

<br><br>

## :sparkles: 오늘 추가한 학습용 예제

- C# 진입점: `AndroidPracticeManager.ShowWaterNotificationPractice()`
- Java 처리 코드: `WaterNotificationReceiver.java`
- 게임 시작 시 즉시 표시하는 테스트 알림이며, `[예]`를 누른 마지막 시각 하나를 저장한다.
- 다음 실행 때 기록을 읽어 로그로 출력한다. 포인트 처리는 아직 연결하지 않았다.
- Unity 빌드와 실제 기기에서의 동작 확인은 아직 필요하다.

- :airplane: [Android 공식 문서 - SharedPreferences](https://developer.android.com/training/data-storage/shared-preferences)
