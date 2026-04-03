## :fire: 유틸적인(=사용자의 편리성을 향상하는 유용하고 실용적인) 메서드는 <br> static class에 확장 메서드(Extension Method)로 구현한다.

#### [Generic Extension Method]
~~~c#
void Main()
{
	//External Library
	StringBuilder sb = new StringBuilder();
	
	//Custom Class
	MyStringManager stringManager = new MyStringManager();
	
	//Generic으로 둘 다 사용 가능
    //Static Method를 Instance Method 처럼 사용하는 부분
	sb.DoNotImportantThing();
	stringManager.DoNotImportantThing();
}

public class MyStringManager()
{
	public void DoImportantThing()
	{
		"Important".Dump();
	}
}

static class StringExtension
{
	//1. Using Generic
	public static void DoNotImportantThing<T>(this T sb)
	{
		"Not Important".Dump();
	}
	
	//2. Not Using Generic
	/*public static void DoNotImportantThing(this StringBuilder sb)
	{
		"Something".Dump();
	}

	public static void DoNotImportantThing(this MyStringManager sb)
	{
		"Something".Dump();
	}*/
}
~~~
- External Library의 기본 기능에 회사 프로젝트에 알맞은 기능을 추가해서 강화시킬 수 있다. (== 외부 라이브러리 커스터 마이징이 된다!)
- 클래스를 중요한 메서드와 중요하지 않은 메서드로 구분해서 하나의 일반 클래스와 static 클래스(확장 메서드의 모임인 Extension)로 쪼갤 수 있다.
  > Extension methods don't appear directly in the class definition, meaning that they can be harder to discover by developers who are unfamiliar with the available extensions.
- 확장 메서드를 담고 있는 static class에서는 기존 class의 private | protected member에는 접근하지 못하는 단점도 있다.
- 굳이 기존 클래스에 포함되지 않아도 될 유틸을 괜히 확장 메서드로 포함시킨다면 class의 응집도를 떨어뜨릴 수 있다.

<br><br>

## :fire: 확장메서드 구현 3요소를 아래에 적어 놓았다.
- 확장메서드는 :star:반드시:star: Static Class 내부에 구현해야 한다. 
- 확장메서드는 :star:반드시:star: Static Method 여야 한다.
- 확장메서드는 :star:반드시:star: 첫 params에 this 키워드를 이용해서 수신자의 타입을 정해야 한다.

<br><br>

## :fire: 참고자료
- [FreeCodeCamp](https://www.freecodecamp.org/news/how-to-write-extension-methods-in-csharp/)