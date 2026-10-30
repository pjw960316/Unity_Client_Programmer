# :sparkles: using은 언제 쓸까?

- `Dispose()`를 지원하고, 내가 정리할 책임이 있으며, 블록이 끝나면 더 쓰지 않는 객체에 쓴다.
- 클래스가 유지되는 동안 필요한 자원은 종료 시점의 `Terminate()`나 `Dispose()`에서 정리한다.
- 다른 곳에서도 쓰는 공유 객체는 정리 책임을 가진 쪽에 맡긴다.
- `Dispose()`는 자원 정리다. C# 객체 자체의 메모리 회수는 GC가 담당한다.
- 이번 알림의 Context·Builder 래퍼는 구성·예약 요청까지만 쓰므로 `using`을 쓴다.
- :airplane: [C# using 공식 문서](https://learn.microsoft.com/dotnet/csharp/language-reference/statements/using)

<br><br>

## :sparkles: 같은 using으로 두 가지 일을 시키다니.
- 네임스페이스의 `using`과 자원 정리의 `using`을 오래 헷갈렸다.
- `using UnityEngine;` → 타입 이름을 짧게 쓰기.
- `using (...) { ... }` → 이 범위에서 자원을 쓰고, 벗어날 때 `Dispose()` 호출 보장.
