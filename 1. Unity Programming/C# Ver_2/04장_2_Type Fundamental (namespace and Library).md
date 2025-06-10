## :fire: 하나의 DLL 파일 안에 여러 namespace가 있을 수 있고, <br> :fire: 하나의 namespace 안에는 여러 class or Inteface가 있을 수 있다.
- **ILSpy 기호**
  - ![alt text](./capture//20250606_1.png)

<br>

- **DLL 파일 경로**
  - ![alt text](./capture//20250606_2.png)

<br>

- **DLL 파일 하나에는 무수히 많은 NameSpace가 존재 할 수 있다.**
  - ![alt text](./capture//20250606_4.png)

<br>

- **NameSpace 하나에는 무수히 많은 Class가 존재 할 수 있다.**
  - ![alt text](./capture//20250606_3.png)
  - 우리가 사용하는 StringBuilder 클래스가 내가 구현하지 않았음에도 이 덕에 사용 할 수 있다.

- **NameSpace 하나에는 무수히 많은 Class가 존재 할 수 있다.**
  - ![alt text](./capture//20250606_5.png)

<br>

- 'using System' 과 'using System.Text'가 상속관계가 아니라 사실은 서로 다른 NameSpace이었다.

<br><br>

## :fire: Library는 class와 interface들이 모여있는 그룹이며 편하게 가져다가 쓸 수 있다. <br> Library에는 대표적으로 DLL과 BCL이 있다. <br> :fire: API는 <ins>a definition on how to write software to interface this thing</ins>로 이해한다.
- **Library**
  - **DLL** = Dynamic Link Library = 라이브러리
    - >	A DLL is a library that contains code and data that can be used by more than one program at the same time. 
  - **BCL** = Base Class Library = 기본 내장 라이브러리
    - Ex : System.IO , List<T>
<br>

- **API = Application Programming Interface**
  - 이 Interface가 C#의 Interface 문법은 아니다. Interface의 본질적인 의미인 설계서 느낌이다.
  - 그러므로 :star:API는 class가 될 수도 있고 library가 될 수도 있고:star:, 자기 멋대로다.
  - > An API is a specification; a library implements that specification. Theoretically, two different libraries could implement the same API. 

- #### [Reddit에서 찾은 답변]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

- ![alt text](./capture/20250606_6.png)
- ![alt text](./capture/20250606_7.png)
- [Reference](https://www.reddit.com/r/learnprogramming/comments/1l3ekbs/is_a_library_just_an_api/)

</details>

<br><br>

## :fire: Unity Project에서 팀원들이 Assets/Scripts 경로에 작성한 모든 C# 스크립트 파일들을 컴파일한다. <br> 그 결과로 하나의 DLL(='Assembly-CSharp.dll`)이 생성되며, <br> Assembly-CSharp.dll은 곧 하나의 .NET assembly이며, <br> DLL과 Assembly는 .NET 환경에서 사실상 같은 개념이다.
- > larger projects can be planned so that several developers can work on separate source code files or modules, which are combined to create a single assembly.
  - larger projects = 회사 게임 프로젝트, several developers = 클라이언트 팀, source code files = 업무시에 작성하는 스크립트 파일
  - single assembly = Assembly-Csharp dll 
  - 그러므로 Assembly == DLL로 인식해도 된다. (물론 엄밀히 말하면 좀 다르긴 한데 좀 애매하다.)
- > An assembly is a collection of types and resources that are built to work together and form a logical unit of functionality. Assemblies take the form of executable (.exe) or dynamic link library (.dll) files, and are the building blocks of .NET applications. DLL contains compiled code of functions stored in so called libraries. Programs call these functions found in the DLLs when needed from inside the program executable (or from another library).
  - MSDN과 Quora를 합친건데, 같다고 봐도 무방하다.
- :bangbang: .asmdef를 이용하면 하나의 'Assembly-Csharp.dll'이 아니라 여러 개의 dll로 나누어 진다.
  - internal keyword가 access 영역을 하나의 assembly(=dll)로 설정하는 키워드인데, .asmdef를 사용하지 않으면 클라이언트 개발팀에서 작성하는 모든 코드는 internal일 때 접근이 가능하다고 판단 할 수 있다.

<br><br>

## :fire: DLL도 Dependency가 생긴다는 단점은 있지만, 사용 할 수 밖에 없다고 생각한다.
> When a program or a DLL uses a DLL function in another DLL, a dependency is created. The program is no longer self-contained, and the program may experience problems if the dependency is broken. For example, the program may not run if one of the following actions occurs
- 버전을 올려서 이전 버전의 DLL을 사용할 때 생기는 충돌

<br><br>

## :fire: Using System 의 두 가지 의미 <br> :fire: 1. 이 파일에서 BCL의 System Namespace에 존재하는 class 들을 사용하겠다. <br> :fire: 2. System Namespace를 생략 할 수 있다.  
> C#에서 using 지시자를 사용할 것인지의 여부는 전적으로 여러분의 선택에 따르는 문제이며 필요하다면 namespace를 포함하는 전체 타입 이름을 매번 기술해주어도 상관 없다. C#의 using 지시자는 이 지시자로 선언한 namespace 참조를 각 타입 이름 앞에 자동으로 붙여서 적절한 타입을 찾아내도록 C# 컴파일러에게 지시하는 기능을 한다.

<br><br>

## :link: Reference
- [MSDN](https://learn.microsoft.com/en-us/troubleshoot/windows-client/setup-upgrade-and-drivers/dynamic-link-library)
- [WIKI](https://en.wikipedia.org/wiki/DLL_hell)

<br><br>

## :fire: namespace는 큰 게임 프로젝트 안에 존재하는 <br>:fire: 미니게임 프로젝트에서 독립성을 위해 사용하기 좋다고 생각한다. <br>:fire: 또한, 모호성을 해결하기 위해 명시적으로 using OOOOO = OOOOO;로 적어주자. 

#### [Ambiguous Code]
<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
using MiniGame; // 내가 2024년에 이렇게 적어놓고 사용했다고 가정하자. 아래 코드(using BigGame)가 없다면 잘 돌아간다.
using BigGame; // 2025년에 새로 들어온 개발자가 여기다가 이렇게 적으면 ambiguous가 발생한다. 

void Main()
{
	// 아래의 타입은 MiniGame.MusicController인지 BigGame.MusicController인지 알 수 없다.
	// CS0104: 'MusicController' is an ambiguous reference between 'MiniGame.MusicController' and 'BigGame.MusicController'
	MusicController musicController = new MusicController();
	musicController.Play();
}

namespace BigGame
{
	class MusicController
	{
		public void Play(){"BigGame".Dump();}
	}
}

namespace MiniGame
{
	class MusicController
	{
		public void Play(){"MiniGame".Dump();}
	}
}
~~~

</details>

#### [Explicit Code]
<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
using MusicController = MiniGame.MusicController; //Explicit

void Main()
{
	MusicController musicController = new MusicController();
	musicController.Play();
}

namespace BigGame
{
	class MusicController
	{
		public void Play(){"BigGame".Dump();}
	}
}

namespace MiniGame
{
	class MusicController
	{
		public void Play(){"MiniGame".Dump();}
	}
}
//MiniGame
~~~

</details>

<br><br>

## :fire: .Net Library인 String vs 내가 만든 String Class의 승리는 <br> :fire: 내가 만든 String Class이다.  
#### [코드 예제]
~~~c#
using System;

void Main()
{
	char[] charArray = { 'H', 'e', 'l', 'l', 'o' };
	String str = new String(charArray);

	if (str.GetType() == typeof(System.String))
	{
		"External Library Win".Dump();
	}
	else
	{
		"My Custom Library Win".Dump();
	}
}

public class String
{
	public String(char[] param) { }
}

// My Custom Library Win 
~~~
<br><br>

