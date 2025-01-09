# 목차
- [목차](#목차)
- [:fire: Declared Type은 컴파일 시점의 타입이고, :fire: Instance Type은 런타임 시점의 타입이다.](#fire-declared-type은-컴파일-시점의-타입이고-fire-instance-type은-런타임-시점의-타입이다)

<br><br><br>

# :fire: Declared Type은 컴파일 시점의 타입이고, <br>:fire: Instance Type은 런타임 시점의 타입이다.
~~~c#
void Main()
{
	Fruit fruit = new Fruit();
	Fruit fruit2 = new Apple();
	Apple apple = new Apple();
	Animal animal = new Animal();
	
	//Test(fruit); //InvalidCastException
	Test(fruit2);
	Test(apple);
	//Test(animal); //InvalidCastException
}

public static void Test(object o)
{
	Apple apple = (Apple) o;
	apple.GetType().Dump();
}

public class Fruit{}
public class Apple : Fruit{}
public class Animal{}
~~~