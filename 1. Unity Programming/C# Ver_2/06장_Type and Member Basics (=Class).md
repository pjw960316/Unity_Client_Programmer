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

## :fire: Private 필드는 외부에서 접근에 완벽히 안전하지 않다. 그러므로 다음의 두 가지 방식을 사용한다. <br> :one: private field에 readonly + Declaration을 쓰도록 한다. <br> :two: Container는 Immutable Type으로 구현한다.
#### [private의 한계 : property나 public Method라 그냥 뚫린다.]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
public class TestManager
{
	private PrivateTestSubject _privateTestSubject;
	
	public TestManager(PrivateTestSubject arg)
	{
		//DI
		this._privateTestSubject = arg;
	}
	
	public void TestFirstQuestion()
	{
		//Compile ERROR
		//_privateTestSubject._privateNumber = 1;
	}
	
	public void TestSecondQuestion()
	{
		_privateTestSubject.ChangePrivateNumber();
	}
	public void PrintPrivateNumber()
	{
		_privateTestSubject.PrintPrivateNumber();
	}
}

public class PrivateTestSubject
{
	private int _privateNumber = 0;
	
	public void ChangePrivateNumber()
	{
		_privateNumber = 2;
	}
	
	public void PrintPrivateNumber()
	{
		_privateNumber.Dump();
	}
	
}

void Main()
{
	PrivateTestSubject privateTestSubject = new PrivateTestSubject();
	TestManager testManager = new TestManager(privateTestSubject);
	
	testManager.TestSecondQuestion();
	testManager.PrintPrivateNumber();
}
// Result
// 2
~~~

</deatails>

- TestFirstQuestion()에서 DI로 받은 Instance 내부의 private Field는 접근이 불가능함을 보여준다.
- TestSecondQuestion()에서 Public Method로 바꿀 수 있다.

#### [:one: readonly]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
public class PrivateTestSubject
{
	private readonly int _privateNumber = 0;
	
	public void ChangePrivateNumber()
	{
		// 불가능
		// _privateNumber = 2;
	}
}
~~~

</deatails>

- :link:[Readonly 키워드가 붙은 멤버의 Set은 declaration 또는 constructor에서만 가능하다.](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/06%EC%9E%A5_Type%20and%20Member%20Basics%20(%3DClass).md)
  - 그러므로 public Method로 private Field를 변경하는 방식을 막을 수 있다.

#### [:two: Immutable]