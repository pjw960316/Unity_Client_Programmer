# using은 언제 쓸까?

## :sparkles: 짧은 메서드에서도 using이 필요한 이유

#### :white_check_mark: 경험

`AndroidManager`에서 Java Set을 읽고 C# `HashSet<string>`으로 변환하는 코드를 보다가 의문이 생겼다.

> 메서드가 이렇게 짧으면, 어차피 메서드를 탈출할 때 지역변수도 해제되는 것 아닌가? 굳이 using이 필요한가?

나는 지역변수는 함수가 끝나면 정리된다고 배웠다. 그래서 지역변수 `javaSet`의 스코프가 끝나면, 그 변수가 가리키는 객체와 자원도 함께 정리될 것이라고 생각했다. **지역변수 자체의 수명과 참조하는 객체·자원의 수명을 구분하지 못했던 것이다.**

설명을 듣고 나서는 `using`이 GC에게 다른 참조까지 전부 끊으라고 전달하는 것인지 질문했다. 이후 `AndroidJavaObject.Dispose(bool)`에서 `GlobalJavaObjectRef.Dispose()`와 `QueueDeleteGlobalRef()`까지 직접 따라갔다.

이 흐름을 확인하면서, **짧은 메서드에서도 using이 필요한 이유는 객체가 가진 자원을 정리할 시점을 명확하게 하기 위해서**라는 것을 이해했다.

<br>

#### :white_check_mark: 대화에서 정리한 이해

- 참조형 지역변수의 사용 범위가 끝나는 것과, 그 변수가 가리키던 객체의 메모리가 회수되는 것은 별개다.
- 프로그램에서 객체에 접근할 수 있는 참조가 남아 있으면 객체는 유지된다. 접근할 수 없게 되어도 GC가 즉시 회수한다고 보장되지는 않는다.
- `IDisposable`은 정리 메서드의 계약을 정의하는 인터페이스이고, 실제 정리 작업은 해당 클래스의 `Dispose()` 구현에 들어 있다.
- `using`은 GC에게 정리를 요청하는 명령이 아니다. 블록을 벗어날 때 `Dispose()`를 호출하도록 한다. `return`이나 예외로 나가는 경우도 포함한다.
- `Dispose()`는 파일 닫기, 연결 종료, JNI 참조 해제처럼 **보유한 자원을 정리하는 작업**이다. C# 객체 자체의 메모리 회수는 GC가 담당한다.

:airplane: [IDisposable 객체의 사용과 자원 정리](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/using-objects)

<br>

#### :white_check_mark: 직접 따라간 Dispose 구현

```text
using 블록 종료
→ AndroidJavaObject.Dispose()
→ AndroidJavaObject.Dispose(bool)
→ 내부 GlobalJavaObjectRef.Dispose()
→ m_disposed로 중복 처리 방지
→ QueueDeleteGlobalRef(m_jobject) 호출
```

직접 확인한 C# 구현에서는 JNI 전역 참조의 삭제 요청을 전달한다. `AndroidJavaObject`는 내부의 `m_jobject`, `m_jclass` 필드도 `null`로 바꾼다.

**Codex 보완:** 필드를 `null`로 바꾸는 것은 현재 래퍼가 가진 참조를 비우는 것이다. 다른 변수의 참조까지 끊거나 C# 객체 자체를 즉시 삭제하는 동작은 아니다. `QueueDeleteGlobalRef()`를 호출한 코드만으로 실제 JNI 참조 삭제가 완료되는 시점까지 단정하지 않는다.

JNI 참조가 해제되어도 Java 쪽에서 객체를 계속 참조한다면 객체는 남을 수 있다. Java Set 객체의 회수는 Java GC의 영역이며, 저장된 물 섭취 기록을 삭제하는 것과도 다르다.

:airplane: [Android JNI의 지역 참조와 전역 참조](https://developer.android.com/ndk/guides/jni-tips#local-and-global-references)

<br>

#### :white_check_mark: Codex 보완 — scope와 stack·heap을 연결할 때의 주의점

아래는 이번 혼동을 이해하기 위한 일반적인 동기 메서드의 설명이다. 실제 저장 위치는 런타임과 최적화에 따라 달라질 수 있다.

| 구분 | 이번 경험과 연결되는 의미 |
| --- | --- |
| scope | 코드에서 변수 이름을 사용할 수 있는 범위다. 범위를 벗어난다는 사실만으로 객체나 자원이 즉시 정리되지는 않는다. |
| stack | 보통 메서드 호출에 필요한 정보와 지역변수의 저장 공간을 설명할 때 등장한다. 함수가 끝나 호출 프레임이 정리되는 것과 heap 객체의 회수는 별개다. |
| managed heap | C# 객체가 저장되고 GC가 메모리 회수를 관리하는 영역이다. |

`javaSet`은 객체 자체를 담는 값이 아니라, C# `AndroidJavaObject` 객체에 대한 참조다. 이 래퍼는 다시 JNI 전역 참조를 통해 Java Set에 접근한다.

```text
지역변수 javaSet의 참조
    ↓
C# AndroidJavaObject 객체 (managed heap)
    ↓ JNI 전역 참조
Java Set 객체 (Java heap)
```

처음 정리한 **“값 타입은 scope를 벗어나면 바로 해제된다”는 표현은 보완이 필요하다.** 값 타입이 언제나 stack에 있는 것은 아니다. 클래스의 값 타입 필드는 객체 내부에 저장되고, 값 타입을 `object`로 박싱하면 그 값을 담은 객체가 managed heap에 생긴다.

따라서 `int` 같은 단순한 지역변수의 저장 공간이 정리되는 상황을, 참조 객체나 JNI 자원 전체의 해제에 그대로 적용하지 않는다. **변수의 사용 범위, 객체의 메모리 회수, 객체가 보유한 자원의 정리를 구분해서 본다.**

:airplane: [GC와 managed heap](https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/fundamentals), [값 타입의 박싱](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing)

<br><br>

## :sparkles: using을 선택하는 기준

#### :white_check_mark: 자원의 정리 책임과 사용 범위

- `Dispose()`를 지원하고, 내가 정리할 책임이 있으며, 블록이 끝나면 더 쓰지 않는 객체에 쓴다.
- 클래스가 유지되는 동안 필요한 자원은 종료 시점의 `Terminate()`나 `Dispose()`에서 정리한다.
- 다른 곳에서도 쓰는 공유 객체는 정리 책임을 가진 쪽에 맡긴다.
- `Dispose()`는 자원 정리다. C# 객체 자체의 메모리 회수는 GC가 담당한다.
- 이번 알림의 Context·Builder 래퍼는 구성·예약 요청까지만 쓰므로 `using`을 쓴다.
- :airplane: [C# using 공식 문서](https://learn.microsoft.com/dotnet/csharp/language-reference/statements/using)

<br><br>

## :sparkles: 같은 using으로 두 가지 일을 시키다니.

#### :white_check_mark: 경험

- 네임스페이스의 `using`과 자원 정리의 `using`을 오래 헷갈렸다.

<br>

#### :white_check_mark: 용도 구분

- `using UnityEngine;` → 타입 이름을 짧게 쓰기.
- `using (...) { ... }` → 이 범위에서 자원을 쓰고, 벗어날 때 `Dispose()` 호출 보장.
