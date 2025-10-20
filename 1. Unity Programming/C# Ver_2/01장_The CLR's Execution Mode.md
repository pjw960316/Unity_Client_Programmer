## :fireworks: 유니티에서 내가 작성한 C# 스크립트가 기계어가 되는 과정 <br> :fire: C# script -> Roslyn compiler compile -> IL code (Assembly-Csharp.dll) -> <br> play Button Click -> JIT compiler compile -> Runtime -> 01010101001(기계어)
- **Assembly-Csharp.dll 파일은 IL Code** + Meta file로 구성되어 있다.
> 관리 어셈블리는 메티데이터와 IL을 같이 포함한다고 하였다. IL은 CPU에 독립적인 기계어 코드로 MS가 외부의 몇몇 상용 및 학술용 언어/컴파일러 제작자들과의 상의하에 만든 것 이다. IL은 대다수의 CPU 기계어보다 더욱 고차원의 언어다.

> IL은 네이티브 CPU 명령어(기계어)로 변환된다. 이 작업은 CLR의 JIT(Just-in-time) 컴파일러에 의하여 실행된다.
- :link:[실전 예시](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/04%EC%9E%A5_2_Type%20Fundamental%20(namespace%20and%20Library).md#fire-%EC%9C%A0%EB%8B%88%ED%8B%B0-%EA%B2%8C%EC%9E%84-%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8%EC%97%90%EC%84%9C-%ED%8C%80%EC%9B%90%EB%93%A4%EC%9D%B4-%EC%9E%91%EC%84%B1%ED%95%9C--%EB%AA%A8%EB%93%A0-c-%EC%8A%A4%ED%81%AC%EB%A6%BD%ED%8A%B8-%ED%8C%8C%EC%9D%BC%EB%93%A4%EC%9D%80-%EB%B3%B4%ED%86%B5-assetscripts%EC%97%90-%EC%A0%80%EC%9E%A5%EB%90%9C%EB%8B%A4--%EC%9D%B4-%ED%8C%8C%EC%9D%BC%EB%93%A4%EC%9D%84-%EC%BB%B4%ED%8C%8C%EC%9D%BC-%ED%95%98%EB%A9%B4-%EA%B7%B8-%EA%B2%B0%EA%B3%BC%EB%A1%9C-%ED%95%98%EB%82%98%EC%9D%98-dllassembly-csharpdll%EC%9D%B4-%EC%83%9D%EC%84%B1%EB%90%9C%EB%8B%A4--fire-assembly-csharpdll%EC%9D%80-%EA%B3%A7-%ED%95%98%EB%82%98%EC%9D%98-net-assembly%EC%9D%B4%EB%A9%B0--dll%EA%B3%BC-assembly%EB%8A%94-net-%ED%99%98%EA%B2%BD%EC%97%90%EC%84%9C-%EC%82%AC%EC%8B%A4%EC%83%81-%EA%B0%99%EC%9D%80-%EA%B0%9C%EB%85%90%EC%9D%B4%EB%8B%A4)

<br><br>

## :fire: Unity Rebuild (Ctrl + R) == Rider에서 작성한 코드 변경사항 반영
- Rebuild는 Roslyn Compiler로 모든 script file들을 Recompile해서 기존의 Assembly-Csharp.dll을 지우고 새로 만드는 기능이다.
- DLL이 갱신 되므로 코드가 반영된다.