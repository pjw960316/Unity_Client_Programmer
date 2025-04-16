## MSDN Link
- [LINK](https://learn.microsoft.com/ko-kr/dotnet/standard/garbage-collection/)

<br><br>

## :fire: 결론부터 적으면, GC는 내가 생각보다 컨트롤 하기 쉽지 않으므로, <br> GC 친화적인 코딩 방식 정도만 알고 넘어가자.
- null로 끊는다고 메모리가 확보되지는 않는다는 것
- GC 친화적인 방법 코딩이 그나마 내가 실천할 수 있다
  - object pooling 
  - [LINK](https://www.reddit.com/r/dotnet/comments/1b95sam/how_to_write_c_to_avoid_any_gc_pauses/)

<br><br>


## 핵심 개요 [흐름 파악]
- 왜 GC가 필요한지를 Managed Heap에서 메모리를 관리하는 방식을 통해 이해한다.
- GC 알고리즘을 가볍게 이해한다.
- GC에게 부담을 주는 코딩이 무엇이고 그걸 하지 않는 방식을 유니티와 연관해서
- GC가 관리하지 못할 것으로 생각되는 부분을 내가 강제로 해제하는 구현
- GC.Collect() 하면 순간 멈춘 경험

<br><br>

## :fire: Managed Heap을 CLR이 관리하는 기법을 알면 GC의 필요성을 이해할 수 있다. <br> :fire: GC가 죽여야 할 객체를 판단하는 근거.
- **구시대 방식 : Reference Counting**
  > 객체는 자신이 참조 되는 횟수를 기록하는 필드를 가지고 있어서 프로그램 내에 <ins>얼마나 많은 부분이 해당 객체를 참조</ins>하고 있는지를 기록한다.
  - 자식 클래스의 멤버로 부모 클래스를 갖고 있으면 참조가 +1이 되므로 0이 되지 않아 Circular Reference가 일어나서 메모리에서 해제되지 않는 문제가 있다
- **C# 방식 : Reference Tracking**
  - Root
    - GC가 객체 생존 여부를 판단하는 최초의 기준점이 되는 Reference Type의 변수.
    - 무조건 reference Type이다.
    - 새로운 변수가 아닌 내가 작성한 코드에 있는 Reference Type 변수이다.
  - Mark
    - 아래의 예제에서 unreachable을 이해하면 된다.

<br><br>

## :fire: Heap Memory에서 해제되는 정밀한 시점 : GC가 동작했을 때
#### [예제]
~~~c#
class Test
{
	public int num;
}

class Program
{
	static void Main()
	{
		Test obj = new Test();
		obj.num = 10;

		obj = null;  //1번 시점 : 참조 끊기
		
		GC.Collect(); //2번 시점 : GC 동작
	}
}
~~~
- Test obj = new Test()에서 주소는 사실 2개가 존재한다.
  - 첫 번째 주소 = heap 주소 : obj의 인스턴스가 실제로 저장된 Heap 메모리의 주소값. (예제의 0x77)
  - 두 번째 주소 = stack 주소 : 첫 번째 주소의 값을 stack의 변수에 저장하는 데, 이 때 stack에 생기는 주소 저장 필드의 주소값. (예제의 0x11)
  - ![alt text](./capture/20250404.png) 
- 예제 코드의 1번 시점에 obj는 **unreachable(=접근 불가)** 상태가 되지만, 아직 managed heap에 obj의 인스턴스 정보가 저장되어 있다. 
- 예제 코드의 2번 시점이 되면 heap에서 해제된다. 

<br><br>

## :fire: '= null'과 'unreachable'은 명백히 다른 개념이다. <br> unreachable은 인스턴스에 대한 '모든' 참조가 null이 되어야 한다. <br> 참조가 100개 되어 있는데, 고작 1개를 null로 초기화 한다고 unreachable이 되지 않는다.
#### [참조가 2개인 힙에 올라간 1개의 AAA 인스턴스]
~~~c#
void Main()
{
	AAA obj_1 = new AAA();
	AAA obj_2 = obj_1; //프로젝트에서는 협업이므로 이렇게 직관적으로 참조가 보이지 않는다. 
	
	obj_1 = null; // Dispose() 방식을 쓰지 않은 예제
}

public class AAA
{
	public int a;
	
	public AAA()
	{
		a = 12;
	}
}
~~~
- :bangbang: obj_1의 참조를 끊었으니 힙에 있는 AAA인스턴스는 GC가 수집되어 메모리가 해제되겠다고 생각하지만, 절대 그렇지 않다.
- AAA 인스턴스는 아직 obj_2로 reachable 하기 때문에 개발자가 'obj_1 = null'을 한다고 힙에서 AAA 인스턴스가 GC로 인해 해제 되지는 않는다.

<br><br>

## :fire: GC는 Managed Heap에 있는 ReferenceType만 관리한다. <br> :fire: 하지만... Class의 멤버로 있는 ValueType도 함께 관리된다.
  - ReferenceType인 인스턴스가 제거되면 당연히 인스턴스 전체가 메모리에서 사라지기 때문에, 내부의 valueType 멤버들(int, struct)도 **같이 제거** 된다.
    - Class의 valueType 멤버들도 managed heap에 있다.
  - 다시 말해, 클래스의 valueType 멤버가 독립적으로 제거되는 경우는 알 수 없으나, 인스턴스가 삭제될 때 valueType 멤버는 당연히 같이 해제된다.

<br><br>

## :fire: [경험을 해봐야 할 것] GC Collect를 사용해도 괜찮다는 두 가지 순간
> 1. 반복 발생할 가능성이 매우 희박한 이벤트를 처리한 후 상당히 많은 객체가 제거되어야 하는 경우
> 2. 게임 프로젝트의 초기화 완료 직후나 사용자가 데이터 파일을 저장한 직후와 같은 경우에 모든 세대에 걸친 GC 작업은 괜찮아 보인다.
  
<br><br>

## :fire: unsafe 코드 블록 안에서는 C#의 안전한 메모리 관리 환경을 벗어나 <br> C++과 비슷하게 포인터를 사용하여 메모리 주소를 직접 다룰 수 있다. <br> :fire: fixed 키워드를 이용하면 GC에 의해 인스턴스가 이동되지 않도록 고정한다.
- > unsafe 컨텍스트에서 코드는 포인터를 사용하고, 메모리 블록을 할당 및 해제하고, 함수 포인터를 사용하여 메서드를 호출할 수 있습니다.
- Static Utill Class에서 valueType의 주소를 찾을 때 두 키워드를 사용했다. 
