## :fire: GC 친화적 개발 방식은 거시적으로 보면 힙에 불필요한 객체를 할당시키지 않는 게 전부다.. <br> :fire: 관점 자체를 힙 객체가 생성되느냐 아니냐로 바라본다. 
- 이게 스택에 생성되냐 힙에 생성되냐를 따지는 것은 생각보다 중요하지 않다.

<br><br>

## :fire: GC는 managed heap(=관리 힙)을 관리하고, managed object만 추적한다. <br> 그리고 managed object는 managed heap에만 존재한다. <br> 즉, gc가 추적하는 대상이 오버헤드를 만들어낸다. <br> :fire: 그러나 ValueType은 managed Object가 아니다. 
> it allocates a segment of memory to store and manage objects. This memory is called the managed heap

> As an application developer, you work only with virtual address space and never manipulate physical memory directly. The garbage collector <ins>allocates and frees virtual memory for you on the managed heap.</ins>

- 모든 struct는 valueType이므로 managed object가 아니다. 그러므로 GC를 발생시키지 않는다.

<br><br>

## :fire: valueType이 박싱되면, managed object가 되기 때문에 주의한다.

<br><br>

## :fireworks: struct로 구성된 List와 class로 구성된 List를 GC 관점에서 정리한다. 
- struct로 구성된 List에서 원소를 추가할 때는 managed object가 생성되지 않기 때문에 GC가 추적해야 할 객체 수가 증가하지 않는다. 반면 class로 구성된 List에서 원소를 추가할 때 new로 생성된 각 인스턴스는 managed object가 되어 GC 추적 대상이 늘어난다. GC 관점에서 struct로 구성된 List는 List 자체의 오버헤드는 존재하지만 내부 원소 추가에 대해서는 0이다.
- struct로 구성된 List는 대신 내부 원소 수정이나 과하게 캐싱을 하면 계속 복사가 일어난다. struct는 그래서 16bytes 이하를 권장되는 것 이다. 만약 struct를 16bytes 이하로 만들었다면 100번의 struct 복사는 1번의 new class보다 빠르다. 
- struct로 구성된 List는 struct 원소들이 연속적으로 저장되어 있다. class로 구성된 List도 class들이 연속적으로 저장되어 있긴 하나, 포인터를 따라가면 실제로는 그렇지 않다. 그러므로 캐시 히트에서 훨씬 유리하다.
	> Structs are allocated contiguously, whereas classes are all over the place in heap, so they should cause more cache misses

#### [예제_1]
~~~c#
public void Main()
{
	// instance는 T 타입이고, 어디선가 만들어졌다고 가정한다.
	Test(instance); 
}

public void Test(T obj) where T : class
{}
~~~
- 여기서 obj라는 변수는 8bytes이고, instance의 주소를 갖고 있다. 그리고 GC는 발생하지 않는다.
- 그러므로 이런 방식은 오히려 struct보다 빠를 수 있다.
- 하지만 예제_2를 보자

#### [예제_2]
~~~c#
public void Main()
{
	// 생성해서 넣는다.
	var instance = new T();
	Test(instance); 
}

public void Test(T obj) where T : class
{}
~~~
- 결국, 참조 타입 매개변수를 갖는다면 힙에 할당하는 new를 허용하게 된다. 힙 객체 생성이 GC의 원인이 된다.