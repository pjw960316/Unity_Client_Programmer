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