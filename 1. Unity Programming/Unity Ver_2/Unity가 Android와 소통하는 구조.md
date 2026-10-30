## :fireworks: 어떻게 해야 게임이 메모리에 로드되지 않은 상태에서도 <br> 무언가를 동작시킬 수 있을까? <br> 안드로이드를 이용하자! <br> 그러면 Java를 써서 구현해야한다.
- 과거에는 이 기능이 Android Studio + Java를 잘 다루어야 가능했다.
- 하지만 이제는 이 부분이 Codex를 통해 쉽게 된다.

<br><br>

## :fireworks: Unity에서 Android를 이용하기 위해 필요한 요소들을 설명한다. <br> 이해하고 생성은 Codex로. <br> 되도록 Android 디렉터리 하단에 모아두자.
- :airplane:[unity docs](https://docs.unity3d.com/6000.0/Documentation/Manual/android-library-project-import.html)
- **`.androidlib` 폴더**
  - Android 기능의 소스와 설정을 묶는 폴더다. Unity는 이 폴더를 Android 빌드에 포함한다.
  - 아래의 `AndroidManifest.xml`과 `.java` 파일은 **이 폴더 안에 들어간다.**

```text
Assets/Android/Test.androidlib/
└─ src/main/
   ├─ AndroidManifest.xml
   └─ java/com/habitgame/test/
      └─ TestPlugin.java
```
<br>

- **`AndroidManifest.xml`**
  - Android에 필요한 설정을 선언하는 파일이다.
  - 이번 알림 기능에서는 버튼 클릭을 처리할 수신기 클래스를 등록했다.

<br>

- **`.java` 파일**
  - Android에서 실행할 기능을 작성하는 소스 파일이다.
  - 빌드할 때 컴파일되어 앱에 포함되고, Unity C#에서는 `AndroidJavaClass` API로 해당 클래스에 접근한다.
    - 내부 분석하면 별거 없다.
    - 클래스나 객체를 잡고
    - 이름으로 메서드를 호출하거나 필드를 읽고
    - 결과를 C# 타입으로 받는다.
  - Java는 내가 미숙하므로 AI 생성을 이용한다.

<br><br>

## :fire: Java를 통해 Android를 다루게 되면 <br> 게임을 로드하지 않고 안드로이드 기기의 기능을 사용할 수 있다.
<br><br>

## :sparkles: 오늘의 핵심 : C#에서 Java 메서드를 호출하려면 세 가지 정보가 필요하다.

- 내가 이해한 핵심은 **Java 클래스, 그 클래스의 메서드, 그 메서드를 위한 인자들**이다.
- 현재 구현한 것은 Java 클래스의 **static 메서드를 호출하는 구조**다.

| 필요한 정보 | 의미 | 이번 알림 코드의 예시 |
|---|---|---|
| Java 클래스 이름 | 어느 클래스에 접근할 것인가? | `com.habitgame.notificationpractice.WaterNotificationReceiver` |
| 메서드 이름 | 그 클래스에서 무엇을 호출할 것인가? | `addConfirmButton`, `getLastConfirmedAt` |
| 인자 목록 | 메서드 실행에 어떤 값을 전달할 것인가? | Context, 알림 Builder, 알림 ID 등을 담은 `object[]` |

- 인자의 **개수, 순서, 타입**은 호출할 Java 메서드의 선언과 맞아야 한다.
- 반환값이 있다면 `CallStatic<long>`처럼 Java 반환형에 맞는 C# 반환형도 지정한다.
- Java 쪽에도 실제 메서드 구현이 있어야 한다. 직접 만드는 기능은 현재 Codex에게 구현을 맡기고 있다.
- 이 과정은 Java 개발자에게 기능을 받아서 호출 규격을 맞추는 협업처럼 느껴졌다. Android나 라이브러리가 이미 제공하는 메서드는 직접 구현하지 않고 호출할 수 있다.

<br><br>

## :sparkles: 다른 언어의 타입을 어떻게 연결할까? JNI와 object[]

- 오늘은 C#과 Java가 서로 다른 언어인데 어떻게 인자를 주고받는지 제대로 모르고 있었다는 것을 알게 됐다.
- **C#과 Java 사이에는 정해진 타입 변환 규칙이 있고, Unity가 그 변환을 수행한다.** 모두 비트로 표현된다는 이유만으로 타입이 저절로 맞는 것은 아니다.
- 연결에 사용하는 기술은 **JNI(Java Native Interface)**다. Unity에서는 `AndroidJavaClass`와 `AndroidJavaObject`가 호출 API를 제공한다.
- `AndroidJavaObject`는 Java 객체를 C#에서 참조하기 위한 래퍼다. Java의 Context나 Builder를 일반 C# 데이터로 복사해서 전달하는 것은 아니다.
- :airplane:[Unity CallStatic API](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/AndroidJavaObject.CallStatic.html)
- :airplane:[JNI 타입 변환 규칙](https://docs.oracle.com/en/java/javase/17/docs/specs/jni/types.html)

<br>

- Java 메서드마다 매개변수의 개수와 타입이 다르다. 그래서 이번 공통 호출 API는 **`params object[] args`로 열었다.**
- `object[]`는 서로 다른 타입의 인자를 순서대로 담는 C# 컨테이너다. Java 메서드가 이 배열 하나를 받는다는 뜻은 아니다.
- `params` 덕분에 호출할 때 인자를 나열해도 된다. Unity가 각 값을 Java 호출용으로 변환한다.
- 다만 임의의 C# 객체가 모두 Java 객체로 자동 변환되는 것은 아니다. Unity가 지원하는 값이나 Java 객체 래퍼를 전달해야 한다.

```csharp
// javaClassName과 필요한 Java 객체 참조가 이미 준비되었다고 가정한다.
// Java: addConfirmButton(Context context, Notification.Builder builder, int notificationId)
AndroidManager.Instance.CallStatic(javaClassName, "addConfirmButton", context, builder, WATER_NOTIFICATION_ID);

// Java: long getLastConfirmedAt(Context context)
long confirmedAt = AndroidManager.Instance.CallStatic<long>(javaClassName, "getLastConfirmedAt", context);
```

- `WATER_NOTIFICATION_ID = 1001`은 세 인자 중 알림 ID다. Context와 Builder는 별도로 확보하거나 생성해야 한다.
- `object[]`를 사용할 때 배열 할당과 값 타입의 박싱 비용이 생길 수 있다. 현재처럼 호출 빈도가 낮은 흐름은 먼저 구현하고, 필요하면 실제 비용을 확인한 뒤 최적화한다.

<br><br>

## :fire: Java Method의 경우는 Codex로 자동 생성했다. <br> 시니어 Java 개발자의 코드를 내 C#에서 호출한다고 생각하면 된다.
- 안드로이드 푸쉬앱의 경우 C#과 Unity로 깊게 제어하기가 어렵다.
- 그러므로 여기서는 반드시 Java를 이용해야 한다.
- 아래의 코드를 깊게 이해할 필요는 없다. 
- 여기서 알아야 할 거는 이렇게 앱 푸쉬를 제어하는 건 결국 자바코드고 우리는 이를 c#에서 호출만 하면 된다.

~~~java
// C#에서 호출합니다. [예]를 누르면 이 수신기를 실행하도록 연결합니다.
public static void addConfirmButton(Context context, Notification.Builder builder, int notificationId)
{
    Intent intent = new Intent(context, WaterNotificationReceiver.class);
    intent.setAction(ACTION_CONFIRM);
    intent.putExtra(EXTRA_NOTIFICATION_ID, notificationId);

    PendingIntent pendingIntent = PendingIntent.getBroadcast(context, notificationId, intent, PendingIntent.FLAG_UPDATE_CURRENT | PendingIntent.FLAG_IMMUTABLE);
    builder.addAction(new Notification.Action.Builder(null, "예", pendingIntent).build());
}

// [예]를 누르면 Android가 호출합니다. Unity의 실행 여부와 관계없이 처리합니다.
@Override
public void onReceive(Context context, Intent intent)
{
    if (!ACTION_CONFIRM.equals(intent.getAction()))
    {
        return;
    }

    // 앱 전용 저장소에 마지막 클릭 시각 하나를 보관합니다. 값의 단위는 Unix 밀리초입니다.
    context.getSharedPreferences(PREFERENCES_NAME, Context.MODE_PRIVATE).edit().putLong(CONFIRMED_AT_KEY, System.currentTimeMillis()).apply();

    NotificationManager notificationManager = context.getSystemService(NotificationManager.class);
    if (notificationManager != null)
    {
        notificationManager.cancel(intent.getIntExtra(EXTRA_NOTIFICATION_ID, -1));
    }
}
~~~

<br><br>

## :fire: Android의 작은 기록 저장소 — SharedPreferences
- Unity와 Android가 소통하기 위해서는 서로 주고받을 데이터가 필요하다. <br> 그 데이터를 SharedPreferences에 저장한다.
  - `SharedPreferences`는 Android가 관리하는 파일에 작은 데이터를 키·값으로 저장하고 읽는 API다.
- :airplane: [Android 공식 문서 — SharedPreferences](https://developer.android.com/training/data-storage/shared-preferences)

<br>

#### SharedPreferences 구조
- 데이터를 저장하는 공간은 어떤 구조로 데이터를 저장하고 있는가?

> 저장 구조는 '이름으로 고르는 저장소 안에 key - value 구조'가 있다.

- 저장소 이름: habit_water_practice
└─ 키: last_confirmed_at
   └─ 값: 마지막 클릭 시각 (long)

<br>

#### Write
- 이건 JAVA 코드를 codex 자동완성으로 구현한다.

<br>

#### READ
- C#에서 게임을 로드 할 때 read 하도록 한다.
- AndroidManager 같이 가장 상위 타입에서 게임이 로드되면 현재 sharedPreferences에 저장된 데이터를 읽도록 한다.
  - 여기서 읽은 데이터를 처리하는 기법은 기존 C#과 동일하기 때문에 따로 신경 쓸 것은 없다.
  - 오히려 읽은 데이터를 저장하는 방식, 읽지 못했을 때 처리가 훨씬 중요하다고 생각한다.