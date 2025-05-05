## :fire: 메서드 단위의 추상화를 자주 생각하고 -> 클래스 단위의 추상화를 가끔 생각하고.

## :fire: 추상화 1단계 : 메서드 단위의 추상화
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

## :fire: 추상화 2단계 : 클래스 단위의 추상화
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