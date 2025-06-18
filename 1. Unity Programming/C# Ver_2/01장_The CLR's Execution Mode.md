## :fire: [유니티에서 스크립트가 기계어가 되는 과정] <br> :point_right: C# script -> Roslyn compiler Compile -> IL code (Assembly-Csharp.dll) -> play Button Click -> JIT compiler compile -> Runtime -> 01010101001
- **Assembly-Csharp.dll 파일은 IL Code** + Meta file로 구성되어 있다.

<br><br>

## :fire: Unity Rebuild (Ctrl + R) == Roslyn Compiler로 모든 script file들을 <br> Recompile해서 기존의 Assembly-Csharp.dll을 지우고 새로 만드는 기능.

<br><br>

## :Link: [04장_2_Type Fundamental (namespace and Library)](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/04%EC%9E%A5_2_Type%20Fundamental%20(namespace%20and%20Library).md)