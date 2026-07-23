## :fireworks: 아직 크게 기록 방향성은 못 잡았으나.
- :airplane:[docs](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/AndroidJavaClass.html)
- project setting → players → android
- Unity Android 빌드 → Android Studio 없어도 가능

<br><br

## :fireworks: Unity Package Manager와 Gradle은 외부 기능을 받아오는 역할을 한다.
- Unity Package Manager는 Unity/C#에서 사용할 Package를 다운로드하고 관리한다.
- Gradle은 Android/Java/Kotlin에서 사용할 Library를 다운로드하고 관리한다.
- Gradle은 Library 관리뿐만 아니라 Android 앱 빌드도 담당한다.

```text
Unity Package Manager ≈ Gradle의 의존성 관리 기능
```

<br><br>

## :fire: UniRx와 Health Connect Client는 미리 만들어진 외부 기능이다.
- UniRx는 Unity에서 Reactive 기능을 사용하도록 만들어진 Package다.
- Health Connect Client는 Android의 Health Connect 기능을 사용하도록 Google에서 만든 Library다.

```text
Unity Package Manager로 UniRx를 다운로드한다.
≈
Gradle로 Health Connect Client를 다운로드한다.
```

> UniRx가 Unity용 외부 기능이라면 Health Connect Client는 Android용 외부 기능이다.  
> Package Manager와 Gradle은 각각의 개발 환경에서 이러한 외부 기능을 받아오고 관리한다.>

<br><br>


## :fireworks 나의 이해 프롬프트
- [ ]  유니티의 package Manager가 안드로이드에서는 Gradle임
    - 
    
    Unity Package Manager
    → Unity/C#용 패키지를 프로젝트에서 쓸 수 있게 해줌
    
    Gradle
    → Android/Java/Kotlin용 패키지를 Android 앱에서 쓸 수 있게 해줌
    
    그니까 android studio에서 gradle을 이용해서 어떤 플러그인 다운로드 받는 거가 유니티에서 package manager를 통해서 내가 uniRx 다운로드 받는 거랑 똑같은건데
    unity도 apk를 만들어내니. unity를 위해 android 환경에서 gradle을 사용할 수 있게 unity에서 연결해줌
    
    Android Studio
    → Gradle을 호출
    → Android 라이브러리 다운로드
    → APK/AAB 빌드
    
    Unity
    → Android용 Gradle 프로젝트를 자동 생성
    → Gradle을 호출
    → Android 라이브러리 다운로드
    → APK/AAB 빌드
    
    UniRx                    ≈ Health Connect Client 라이브러리
    Unity Package Manager    ≈ Gradle의 의존성 관리 기능