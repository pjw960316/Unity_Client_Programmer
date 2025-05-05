## :fire: abstract & virtual 과거 문서
- [Github -C#](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23/Abstract%20%26%20Virtual.md)

<br><br>

## :fire: '메서드 단위의 추상화'는 메서드를 작게 많이 쪼개고, 메서드의 같은 추상화 레벨 순서로 적는 것. 
![alt text](./capture/20250505.png)
~~~c#
// 모든 메서드의 바디에는 구현이 있다고 가정한다.
void Main()
{
	DailyRoutinePlanner myDailyRoutinePlanner = new DailyRoutinePlanner();
	myDailyRoutinePlanner.DoDailyRoutine();
}

public class DailyRoutinePlanner
{
	public void DoDailyRoutine()
	{
		PrepareMorning();
		StudyProgramming();
		TakeRest();
	}
	public void PrepareMorning()
	{
		TakeShower();
		DrinkCoffee();
		MoveToLibrary();
	}

	public void TakeShower() { }
	public void DrinkCoffee() { }
	public void MoveToLibrary() { }
	
	public void StudyProgramming()
	{
		OpenBook();
		StudyUnityProgramming();
		DrinkWater();
	}

	public void OpenBook() { }
	
	public void StudyUnityProgramming()
	{
		StudyWithStackOverFlow();
		StudyJefferyBook();
		MakeGame();
	}

	public void StudyWithStackOverFlow() { }
	public void StudyJefferyBook() { }
	public void MakeGame() { }

	public void DrinkWater(){}
	
	public void TakeRest()
	{
		TurnOffLight();
		GoToBed();
		PlayGame();
	}
	
	public void TurnOffLight(){}
	public void GoToBed(){}
	public void PlayGame(){}
}
~~~

- :star: *“메서드 단계에서의 추상화”는 하나의 기능만 하는 작은 메서드를 많이 만들어 복잡한 로직을 계속 쪼개고, 세부 구현을 또 다른 메서드로 숨겨 흐름을 단순하고 명확하게 만드는 작업이다.*
- 메서드가 짧다 → 하나의 기능을 한다 → 좋은 이름 짓기가 쉽고 빠르다 → 메서드를 이름만 보고 이해 가능

<br><br>

## :fire: '클래스 단위의 추상화' : 클래스 쪼개기?????
~~~c#
void Main()
{
	MyDailyRoutine myDailyRoutinePlanner = new MyDailyRoutine();
	myDailyRoutinePlanner.DoDailyRoutine();
}

public class MyDailyRoutine
{
	private MorningRoutine morningRoutine;
	private StudyRoutine studyRoutine;
	private NightRoutine nightRoutine;
	
	public MyDailyRoutine()
	{
		morningRoutine = new MorningRoutine();
		studyRoutine = new StudyRoutine();
		nightRoutine = new NightRoutine();
	}
	
	public void DoDailyRoutine()
	{
		morningRoutine.DoRoutine();
		studyRoutine.DoRoutine();
		nightRoutine.DoRoutine();
	}
}

public abstract class Routine()
{
	public abstract void DoRoutine();
}

public class MorningRoutine : Routine
{
	public override void DoRoutine()
	{
		PrepareMorning();
	}
	
	public void PrepareMorning()
	{
		TakeShower();
		DrinkCoffee();
		MoveToLibrary();
	}

	public void TakeShower() { }
	public void DrinkCoffee() { }
	public void MoveToLibrary() { }
}

public class StudyRoutine : Routine
{
	public override void DoRoutine()
	{
		OpenBook();
		StudyUnityProgramming();
		DrinkWater();
	}
	public void OpenBook() { }

	public void StudyUnityProgramming()
	{
		StudyWithStackOverFlow();
		StudyJefferyBook();
		MakeGame();
	}

	public void StudyWithStackOverFlow() { }
	public void StudyJefferyBook() { }
	public void MakeGame() { }

	public void DrinkWater() { }
}

public class NightRoutine : Routine
{
	public override void DoRoutine()
	{
		TakeRest();
	}

	public void TakeRest()
	{
		TurnOffLight();
		GoToBed();
		PlayGame();
	}

	public void TurnOffLight() { }
	public void GoToBed() { }
	public void PlayGame() { }
}
~~~

<br><br>

## :fire: 참고 서적
- 클린 코드
- 객체 지향의 사실과 오해