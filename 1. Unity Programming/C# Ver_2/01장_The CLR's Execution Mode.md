## :fire: [유니티에서 스크립트가 기계어가 되는 과정] <br> :point_right: C# script -> Roslyn compiler compile -> IL code (Assembly-Csharp.dll) -> play Button Click -> JIT compiler compile -> Runtime -> 01010101001(기계어)
- **Assembly-Csharp.dll 파일은 IL Code** + Meta file로 구성되어 있다.
> 관리 어셈블리는 메티데이터와 IL을 같이 포함한다고 하였다. IL은 CPU에 독립적인 기계어 코드로 MS가 외부의 몇몇 상용 및 학술용 언어/컴파일러 제작자들과의 상의하에 만든 것 이다. IL은 대다수의 CPU 기계어보다 더욱 고차원의 언어다.

> IL은 네이티브 CPU 명령어(기계어)로 변환된다. 이 작업은 CLR의 JIT(Just-in-time) 컴파일러에 의하여 실행된다.

<br><br>

## :fire: Unity Rebuild (Ctrl + R) == Rider에서 작성한 코드 변경사항 반영
- Rebuild == Roslyn Compiler로 모든 script file들을 <br> Recompile해서 기존의 Assembly-Csharp.dll을 지우고 새로 만드는 기능.
    - DLL이 갱신 되므로 코드가 반영된다.

<br><br>

## :Link: [04장_2_Type Fundamental (namespace and Library)](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/04%EC%9E%A5_2_Type%20Fundamental%20(namespace%20and%20Library).md)