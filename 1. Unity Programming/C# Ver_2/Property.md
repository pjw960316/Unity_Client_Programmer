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

<br>

#### [여러 줄 Property]
~~~c#
private int capacity; 
public int Capacity
{
    get
    {
        ShowToastMessage();
        
        return capacity;
    }
    private set
    {
        if (capacity < 100)
        {
            "drink all".Dump();
            capacity = 0;
        }
        else
        {
            capacity = value;
        }
    }
}
~~~

<br>

#### [Property 사용 예제 - 커피 마시기]
~~~c#
public class Coffee
{
	private int DEFAULT_COFFEE_CAPACITY = 300;
	
	public Coffee()
	{
		capacity = DEFAULT_COFFEE_CAPACITY;
	}
	
	private int capacity; 
	public int Capacity
	{
		get
		{
			ShowToastMessage();
			
			return capacity;
		}
		private set
		{
			if (capacity < 100)
			{
				"drink all".Dump();
				capacity = 0;
			}
			else
			{
				capacity = value;
			}
		}
	}
	
	private void ShowToastMessage()
	{
		"GetCapacity".Dump();
	}
	
	public void DrinkedCoffee()
	{
		Capacity -= 100; //여기서도 Get이 호출된다.
	}
}

void Main()
{
	Coffee americano = new Coffee();
	americano.DrinkedCoffee();

	int myAmericanCapacity = americano.Capacity;
	myAmericanCapacity.Dump(); 
}
/*
RESULT

GetCapacity
GetCapacity
200
*/
~~~
- DrinkedCoffee() 메서드에서도 Get이 호출되는 것을 주의한다.