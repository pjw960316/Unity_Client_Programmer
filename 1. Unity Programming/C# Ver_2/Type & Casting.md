## :fire: 용어정리 <br>:fire: Base Type - Derived Type : 상위 타입과 하위 타입 <br>:fire: Declared Type - Instance Type : Complie Type & Runtime Type <br>:fire: () : Explicit Casting

<br><br>

## :fire: 컴파일 시점에는 타입 검사시에 Declared Type으로 한다.<br>:fire: 런타임 시점에는 타입 검사시에 Instance Type으로 한다.
~~~c#
void Main()
{
	Fruit fruit = new Fruit();
	Fruit fruit2 = new Apple(); //Fruit = Declared Type , Apple = Instance Type
	Apple apple = new Apple();
	GreenApple greenApple = new GreenApple();
	Animal animal = new Animal();
	
	//Test(fruit); //InvalidCastException
	//Test(animal); //InvalidCastException
	Test(fruit2); //Success
	Test(apple); //Success
	Test(greenApple); //Success
}

public static void Test(object o)
{
	Apple apple = (Apple) o;
	apple.GetType().Dump();
}

public class Fruit{}
public class Apple : Fruit{}
public class GreenApple : Apple{}
public class Animal{}
~~~
- **컴파일 단계**
  - 컴파일러는 <ins>컴파일 단계에서 명시적 캐스팅이 성공할지 실패할지를 검증하지 않는다.</ins> 대신, 이 책임을 런타임에 넘긴다.
  - 컴파일러는 "캐스팅 구문이 문법적으로 유효하다"고 판단하여 에러 없이 컴파일을 허용합니다.
- **런타임 단계**
  - 런타임 단계에서 Test 메서드에 있는 Apple apple = (Apple) o;이 성공하려면, <ins>instance type이 Apple 타입이거나 Apple의 derived Class</ins>이어야 한다.
  - 그러므로 fruit2와 apple은 instance type이 Apple 타입이고, greenApple은 Apple 타입의 Dervied Class이므로 캐스팅에 성공한다.

<br><br>

## :fire: Is는 런타임 시점에 <br>:fire: 검사 대상의 Instance Type과 검사 타겟의 Instance Type을 비교한다. <br>:fire: 검사 타겟의 Instance Type과 같거나 <br>:fire: 검사 타겟의 Derived Instance Type이면 <br>:fire: TRUE를 리턴한다.
~~~c#
void Main()
{
	Fruit fruit = new Fruit();
	Fruit apple_1 = new Apple();
	Apple apple_2 = new Apple();
	GreenApple apple_3 = new GreenApple();
	
	Test(fruit);
	Test(apple_1);
	Test(apple_2);
	Test(apple_3);
}

static void Test(Fruit inputObj)
{
	//inputObj(=검사 대상)가 Apple(=검사 타겟)과 같은 타입이거나
	//Apple의 Derived Type이면 TRUE를 리턴한다.
	if(inputObj is Apple) 
	{
		(inputObj.ToString() + " is Apple").Dump();
	}
	else
	{
		(inputObj.ToString() + " is not Apple").Dump();
	}
}

class Fruit {}
class Apple : Fruit{}
class GreenApple : Apple{}

/*
UserQuery+Fruit is not Apple
UserQuery+Apple is Apple
UserQuery+Apple is Apple
UserQuery+GreenApple is Apple
*/
~~~

<br><br>

## :fire: Declared Type <= Instance Type일 때만 암시적 할당 가능하다. <br>:fire: Declared Type > Instance Type인 경우, 명시적 캐스팅 필요하다.
![alt text](./capture/0117_1.png)
- Parent는 Object의 모든 걸 갖고, Derived Type은 Base Type의 모든 걸 갖기 때문에 포함 관계를 위의 그림처럼 이해한다.
- 여기서 업캐스팅과 다운 캐스팅의 개념이 나오지만 굳이 기록 X (지울거)
- 아마 P120 ~ P127이 이걸 깊게 이해하는 내용일 테니 5장 공부 후 그 다음에 읽고 여기에도 정리하자.
- Type Safety 개념도 함께 정리
~~~c#
void Main()
{
	Object o1 = new Base(); // SUCCESS
	//Parent o2 = new Object(); // Compile ERROR : cannot implicitly convert type 'object' to 'Base'
	Object o3 = (Base) o1; // SUCCESS -> 명시적 캐스팅 (Explicit Casting)
	
	if(o3 is Base) {"TRUE".Dump();} // TRUE
	if(o3 is Object) {"TRUE".Dump();} // TRUE
}

class Base { }
~~~
<br><br>

## :fire: Instance Type이 Base Type인 객체를<br>:fire: Derived Type으로 Expicit Casting을 시도할 때<br>:fire: Runtime Exception과 함께 실패한다.

#### [예외 발생 코드]
~~~c#
void Main()
{
    Object obj1 = new Object();
	Base obj2 = (Base)obj1; //Invalid_Cast_Exception
}

public class Base { }
public class Derived : Base { }
~~~
- Base Type 객체는 Dervied Type에서 정의한 고유 멤버를 포함하지 않으므로, Derived Type으로 변환 할 수 없다.
- 그러므로 is 또는 as를 이용해서 검사해야 한다.
  
#### [예외 처리 코드]
~~~c#
void Main()
{
    Object obj1 = new Object();
	
	if(obj1 is Base)
	{
		Base obj2 = (Base)obj1;
	}
	else
	{
		Base obj2 = null;
	}
}

public class Base { }
public class Derived : Base { }
~~~