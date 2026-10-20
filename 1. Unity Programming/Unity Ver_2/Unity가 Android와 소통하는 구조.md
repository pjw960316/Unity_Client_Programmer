## :sparkles: Unity가 Android와 소통하는 구조

- Unity의 C# 스크립트에서 Android 기능을 설정하고 호출할 수 있다.
- Unity는 C#에서 Java 클래스와 메서드에 접근할 수 있도록 연결 API를 제공한다.
- 현재 Unity 패키지에 필요한 기능이 없다면, Java로 구현해서 C#에서 호출하는 방법도 있다.

```mermaid
flowchart LR
    A[Unity의 C# 스크립트] --> B[Unity가 제공하는 연결 API]
    B --> C[Java 클래스의 메서드]
    C --> D[Android 기능]
```

<br><br>

## :sparkles: Java 파일도 Unity 프로젝트에 포함할 수 있다

- `.java` 파일을 `Assets` 안에 Android 플러그인으로 포함하면 Unity의 Android 빌드 과정에서 함께 컴파일된다.
- 이 작업을 위해 Android Studio 프로젝트를 별도로 만들 필요는 없다.
- 이번 예제는 `Assets/Android/NotificationPractice.androidlib` 안에 Java 코드, `AndroidManifest.xml`, `build.gradle`을 함께 두었다.

<br><br>

## :sparkles: C#에서 Java 메서드 호출하기

- `AndroidJavaClass`: Java 클래스에 접근한다.
- `CallStatic()`: Java 클래스의 정적 메서드를 호출한다.
- `AndroidJavaObject.Call()`: Java 객체의 메서드를 호출한다. 모든 Java 메서드를 static으로 만들 필요는 없다.

이번 학습 코드에서 Java의 `addConfirmButton()`을 호출하는 부분이다.

```csharp
using (var receiverClass = new AndroidJavaClass("com.habitgame.notificationpractice.WaterNotificationReceiver"))
{
    receiverClass.CallStatic("addConfirmButton", currentActivity, builder, notificationId);
}
```

- 필요한 Android 기능을 Java로 구현하고, C#에서는 그 기능을 호출해서 사용할 수 있다.
- 다만 Unity 런타임이 종료된 동안의 처리는 Android에서 실행할 코드가 담당해야 한다.

- :airplane: [Unity 공식 문서 - C#에서 Java 코드 호출하기](https://docs.unity3d.com/6000.0/Documentation/Manual/android-plugins-java-code-from-c-sharp.html)
