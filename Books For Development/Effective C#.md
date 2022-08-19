# 목차
- [목차](#목차)
- [0. 서론](#0-서론)
- [1. 지역 변수를 선언할 때는 var를 사용하는 것이 낫다. (동의하지 않음!)](#1-지역-변수를-선언할-때는-var를-사용하는-것이-낫다-동의하지-않음)
- [2. Const(컴파일 타임 상수) 보다는 readonly(런타임 상수)가 좋다. (동의하지 않음!)](#2-const컴파일-타임-상수-보다는-readonly런타임-상수가-좋다-동의하지-않음)
    - [2-1. Const](#2-1-const)
    - [2-2. Readonly](#2-2-readonly)
    - [2-3. 좀 더 깊게 이해하려면 Static Class와 Singleton의 차이를 이해해야 한다.](#2-3-좀-더-깊게-이해하려면-static-class와-singleton의-차이를-이해해야-한다)
    - [2-4. 객체를 이용한 연습](#2-4-객체를-이용한-연습)
# 0. 서론
- 이미 C#을 사용하고 있는 개발자라면 상당히 도움될 만한 흥미로운 팁들로 잔뜩 채워져있다. 특히 성능과 효율을 고려하면서 우아한 코딩을 하고 싶은 개발자라면 이 책에서 설명하는 다양한 기법이 좋은 팁이 될 것 이다.
- 천천히 여러 번 보시기를...
- 개발자가 일상에서 접하는 문제을 해결하는 데 실질적인 도움이 되는 기능을 중심으로 소개하고 있다.


# 1. 지역 변수를 선언할 때는 var를 사용하는 것이 낫다. (동의하지 않음!)
- ![20220819_120839](https://user-images.githubusercontent.com/55792986/185534422-1f09c058-701a-44b7-80a2-b82f52509997.png)
    - 이런 문제는 개발자가 직접 찾기 매우 어렵기 때문에 유연성을 포기하고 안정성을 택하는 것이 맞다고 생각한다.

# 2. Const(컴파일 타임 상수) 보다는 readonly(런타임 상수)가 좋다. (동의하지 않음!)
### 2-1. Const
- 컴파일 타임 상수는 성능이 매우 중요하고 상수의 값이 절대로 바뀌지 않는 경우에만 제한적으로 사용하는 것이 좋다.
- Const는 언제나 Static 이다.
  - ![image](https://user-images.githubusercontent.com/55792986/185535340-ef0e5406-48e1-45a3-b5ee-dc9403fa5bc4.png)

### 2-2. Readonly
- 런타임에 값이 평가 된다.
- 메서드에서 선언할 수 없다.
- 런타임 상수는 선언 시에 초기화 하지 않아도 되고, 생성자를 통해 초기화 할 수 있다.

### 2-3. 좀 더 깊게 이해하려면 Static Class와 Singleton의 차이를 이해해야 한다.
- Static Class
  - 객체를 만들 수 없다.
  - 모든 멤버는 static이어야 한다.
    - const도 가능하다.
  - 객체가 없기 때문에 객체 지향을 위반한다?
    - Lua 언어에서는 어떤 테이블이 Static Class임과 동시에 객체를 만들 수 있었다.
      - 어떤 값을 넣었을 때 이게 객체에 들어가는 건지 정적 클래스에 들어가는 건지 매우 모호했다.
      - 하지만 이렇게 Static Class를 만들고 클래스 변수로만 사용하면 나름의 이점이 있는 것 아닌가?
  - 객체를 만들지 않기 때문에 **속도가 빠르다.**
  - Static class의 메서드에는 static 변수만 사용되어야 하기 때문에 어렵고 싱글턴 메서드가 더 좋다고 생각한다.
    - 변수나 상수만 저장하는 클래스 -> 상수만 저장하는 클래스 
  - **결론 : 상수만 저장하는 클래스로 이용하면 장점만 있지 않다!**

- Singleton
  - 객체를 하나 만들고 이를 static으로 사용하여 어디서나 접근할 수 있도록 한다.
  - 객체를 추가로 만드려 할 때 제한을 둔다.
  
### 2-4. 객체를 이용한 연습
~~~
public static class Constant 
{    
    public const int CONST_VALUE = 10;
    public static readonly int READONLY_VALUE = 20;
}

public class ManyInstancesConstant
{
    public readonly int VALUE;

    public ManyInstancesConstant(int value)
    {
        VALUE = value;
    }
}
~~~
~~~
public void practiceConst()
{
        // static class
        Debug.Log(Constant.CONST_VALUE); //10
        Debug.Log(Constant.READONLY_VALUE); //20

        // non-static class
        ManyInstancesConstant obj1 = new ManyInstancesConstant(100);
        ManyInstancesConstant obj2 = new ManyInstancesConstant(200);
        ManyInstancesConstant obj3 = new ManyInstancesConstant(300);
        Debug.Log(obj1.VALUE + " " + obj2.VALUE + " " + obj3.VALUE); //100 200 300
}        
~~~   
- 객체마다 다른 상수 값을 선언해주니까 유연성은 갖는다.
  - 근데 이런 유연성을 갖는 건 상수보다 변수 느낌아닌가?
- **Const 와 Readonly 마다 어울리는 쓰임새가 있으므로 적절히 분배하는 게 맞지 않은가**
