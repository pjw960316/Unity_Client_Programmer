## :fire: Class 내부의 Static Member는 모든 instance 간에 공유해야 하는 값을 저장한다.
> 정적 멤버는 클래스의 인스턴스가 없는 경우에도 클래스에서 호출할 수 있습니다

> 생성된 클래스의 인스턴스 수에 관계없이 정적 멤버의 복사본은 하나만 존재합니다.

> 전체 클래스를 정적으로 선언하는 것보다 일부 정적 멤버를 사용하여 비정적 클래스를 선언하는 것이 더 일반적입니다.

<br><br>

## :fire: Readonly 키워드가 붙은 멤버의 Set은 declaration 또는 constructor에서만 가능하다.
> 'readonly' indicates that assignment to the field can only occur as part of the declaration or in a constructor in the same class. 
- static member 같은 global 한 변수는 유지보수 관점에서는 readonly로 선언하는 게 좋다고 생각한다. 물론, 런타임에서 동적으로 바뀌어야 하면 예외지만.

#### [예제]
~~~c#
void Main()
{
	Test obj = new Test();
	
	obj.instanceField.Dump();
	Test.staticField.Dump();
}

public class Test
{
    // declaration
	public readonly int instanceField = 1;
	public readonly static int staticField = 1;
	
    // constructor
	public Test()
	{
		instanceField = 2;
	}

	static Test()
	{
		staticField = 3;
	}
}
// 2 3
~~~

<br><br>
