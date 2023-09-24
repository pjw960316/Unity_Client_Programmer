# 목차
- [목차](#목차)
- [개요](#개요)
- [A is B에서 A는 상위도 하위도 모두 가능](#a-is-b에서-a는-상위도-하위도-모두-가능)

<br/><br/><br/>

# 개요
- 많이 사용하는 캐스팅 문법은 정리가 필요하다.

<br/><br/><br/>

# A is B에서 A는 상위도 하위도 모두 가능
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/275c0001-0696-4d0d-8e7c-d6e8bf010463)
~~~c#
class Animal
{
    public void Eat()
    {
        Console.WriteLine("Animal is eating.");
    }
}

class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine("Dog is barking.");
    }
}

class Program
{
    static void Main(string[] args)
    {
		//1. 내가 생각한 일반적으로 쓰는 다운 캐스팅
        Animal animal = new Dog();

        if (animal is Dog dog)
        {
            dog.Bark();  // 'dog'는 Dog 타입으로 캐스팅되었으므로 Dog 클래스의 메서드 사용 가능
            dog.Eat();   // 'dog'는 Animal 타입도 되므로 Animal 클래스의 메서드 역시 사용 가능
        }
		
		//2. 이런 코드도 된다.
		Dog dog2 = new Dog();
		if(dog2 is Animal animal2)
		{
			//animal2.Bark(); //결국에 animal2는 Animal 타입이므로 Dog Class의 Bark를 사용할 수 없다.
			animal2.Eat();
		}
    }
}
~~~