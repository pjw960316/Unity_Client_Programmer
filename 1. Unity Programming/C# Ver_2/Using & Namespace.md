## :fire: namespace는 큰 게임 프로젝트 안에 존재하는 <br>:fire: 미니게임 프로젝트에서 독립성을 위해 사용하기 좋다고 생각한다. <br>:star: 또한, 모호성을 해결하기 위해 명시적으로 using OOOOO = OOOOO;로 적어주자. 

#### [Ambiguous Code]
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

#### [Explicit Code]
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



<br><br>

## :fire: using과 namespace 안에 직접 선언한 클래스의 우선순위는 직접 선언이 이긴다.
#### [System.String Class 와 내가 만든 String Class의 싸움]
~~~c#
using System;

void Main()
{
	char[] charArray = { 'H', 'e', 'l', 'l', 'o' };
	String str = new String(charArray);
	
	if(str.GetType() == typeof(System.String))
	{
		//str.Any();
		"My Custom class's priority is higher than using System".Dump();
	}
	else
	{
		str.Test();
	}
}

public class String
{
	public String(char[] param) {}
	
	public void Test()
	{
		"My Custom String's Test Method Called".Dump();
	}
}
// My Custom String's Test Method Called
~~~
<br><br>

## :question: using을 쓰면 컴파일러가 모든 using을 매번 검사하는 건 아니지만, <br>:question: 어쨌든 불필요한 검사가 있을 거임.