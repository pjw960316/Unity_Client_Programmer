
# :fire: Property 사용 규칙
#### [한 줄 Property]
~~~c#
private int capacity; 
public int Capacity
{
    get => capacity;
    private set => capacity = value;
}
~~~

#### [Property 사용 예제 - 커피 마시기기]
~~~c#
public class Coffee
{
	private int DEFAULT_COFFEE_CAPACITY = 300;
	
	public Coffee()
	{
		Capacity = DEFAULT_COFFEE_CAPACITY;
	}
	
	private int capacity; 
	public int Capacity
	{
		get => capacity;
		private set => capacity = value;
	}
	
	public void DrinkedCoffee()
	{
		Capacity -= 100;
	}
}

void Main()
{
	Coffee americano = new Coffee();
	americano.DrinkedCoffee();

	int myAmericanCapacity = americano.Capacity;
	myAmericanCapacity.Dump(); 
}
// RESULT : 200
~~~