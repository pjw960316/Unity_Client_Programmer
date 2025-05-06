## :fire: Property 사용 규칙
#### [한 줄 Property]
~~~c#
private int capacity; 
public int Capacity
{
    get => capacity;
    private set => capacity = value;
}
~~~
- Rider에서 Reformat & Clean-Up 해버리면 알아서 Auto-Property로 변경해준다.

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
	private const int DEFAULT_COFFEE_CAPACITY = 300;
	
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
> 만약 속성을 정의한다면, get과 set 접근자를 모두 정의하는 것이 좋다.
- DrinkedCoffee() 메서드에서도 Get이 호출되는 것을 주의한다.

<br><br>

## :fire: 생성자에서 private 변수를 초기화 할까? 아니면 그 변수의 property를 초기화할까? <br> :fire: 유니티로 개발하면서 뭐가 맞는 지 정하고 여기에 적자.

<br><br>

## :fire: Property는 Field가 아닌 Method다. <br> :fire: Field 보다 조금이지만 overhead가 있을 수 밖에 없다. <br> :fire: 그러므로 property를 남용하지 말고 field로 충분하면 field로 사용한다.
> 저자는 생각보다 많은 사람들이 property를 필요 이상으로 남용한다는 것에 개인적으로 많이 놀랐다.

> Property는 메서드를 호출하는 것과 비교했을 때 성능상의 이점이 있는 것도 아니다.

<br><br>

## :fire: Field 대신 Property를 사용할 때 얻는 이점