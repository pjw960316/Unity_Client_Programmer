## :fireworks: 유니티에서 내가 작성한 C# 스크립트가 기계어가 되는 과정 <br> :fire: C# script -> Roslyn compiler compile -> IL code (Assembly-Csharp.dll) -> <br> play Button Click -> JIT compiler compile -> Runtime -> 01010101001(기계어)
- **Assembly-Csharp.dll 파일은 IL Code** + Meta file로 구성되어 있다.
> 관리 어셈블리는 메티데이터와 IL을 같이 포함한다고 하였다. IL은 CPU에 독립적인 기계어 코드로 MS가 외부의 몇몇 상용 및 학술용 언어/컴파일러 제작자들과의 상의하에 만든 것 이다. IL은 대다수의 CPU 기계어보다 더욱 고차원의 언어다.

> IL은 네이티브 CPU 명령어(기계어)로 변환된다. 이 작업은 CLR의 JIT(Just-in-time) 컴파일러에 의하여 실행된다.

<br><br>

## :fire: Unity Rebuild (Ctrl + R) == Rider에서 작성한 코드 변경사항 반영
- Rebuild는 Roslyn Compiler로 모든 script file들을 Recompile해서 기존의 Assembly-Csharp.dll을 지우고 새로 만드는 기능이다.
- DLL이 갱신 되므로 코드가 반영된다.