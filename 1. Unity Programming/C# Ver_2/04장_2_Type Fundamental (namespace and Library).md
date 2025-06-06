## :fire: namespace는 큰 게임 프로젝트 안에 존재하는 <br>:fire: 미니게임 프로젝트에서 독립성을 위해 사용하기 좋다고 생각한다. <br>:fire: 또한, 모호성을 해결하기 위해 명시적으로 using OOOOO = OOOOO;로 적어주자. 

#### [Ambiguous Code]
<details>
  <summary> :point_up_2: 눌러서 코드를 합시다  </summary>

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
  <summary> :point_up_2: 눌러서 코드를 합시다  </summary>

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

## :fire: 하나의 DLL 안에 여러 네임스페이스가 있을 수 있고, <br> :fire: 하나의 네임스페이스 안에는 여러 클래스가 있을 수 있다.
- ILSpy 기호
  - ![alt text](./capture//20250606_1.png)
- DLL 파일 경로
  - ![alt text](./capture//20250606_2.png)
- DLL 파일 하나에는 무수히 많은 NameSpace가 존재 할 수 있다.
  - ![alt text](./capture//20250606_4.png)
- NameSpace 하나에는 무수히 많은 Class가 존재 할 수 있다.
  - ![alt text](./capture//20250606_3.png)