# 목차
- [목차](#목차)
- [가볍게 용어부터 정리하고 가자.](#가볍게-용어부터-정리하고-가자)
- [Coupling](#coupling)
- [Coupling의 종류](#coupling의-종류)
- [Tight Coupling (=강한 결합)](#tight-coupling-강한-결합)
- [Loose Coupling (=약한 결합)](#loose-coupling-약한-결합)

# 가볍게 용어부터 정리하고 가자.
- ![image](https://user-images.githubusercontent.com/55792986/197937691-a9bdf6f3-0afd-478e-8e87-5ebcd3c7c992.png)
    - :star:(Coupling = 결합도) = (Dependency = 의존성) != (Cohesion = 응집도)
  
# Coupling 
- **유니티에서 모듈을 스크립트**라고 생각하면 스크립트간에 연관된 정도가 많으면 결합도가 높다고 한다.
- 응집도와는 반대 되는 개념이다. 
- 결합도가 높으면 다른 모듈을 찾아가며 유지 보수해야 하기 때문에 유지 보수 측면에서 좋지 않다.

# Coupling의 종류
![image](https://user-images.githubusercontent.com/55792986/197939252-ea8700b8-2bc3-4180-bb8e-569f53b4b14e.png)
   - 종류는 중요하지 않아 보인다.
   - 결국 하나의 모듈을 여러 모듈이 동시에 공유하기 때문에 값의 변경에 민감하다.
   - 멀티스레드의 공유 자원과 비슷하다고 생각한다.


# Tight Coupling (=강한 결합)
-![image](https://user-images.githubusercontent.com/55792986/184570254-c0c64600-8688-483d-96f5-fa08551de7f4.png)
  - 두 객체가 강하게 결합되어 있다면 하나의 객체에서 다른 객체를 알고 있어야 한다.
  - 작은 프로그램에서는 괜찮지만 큰 프로그램이라면 문제가 된다. 다른 개발자가 작성한 코드를 완벽하게 이해하지 않은 상태에서 모두 검사해 보아야 한다.
- 팀장님이 하셨던 말 중에 '드래곤이 없다면 이 코드는 동작하지 않을 텐데?' 라는 것이 기억난다.
  - 모듈이나 객체가 없더라도 코드가 동작해야 한다.
- 아래의 예시를 확인해 본다.
~~~C#
class A {
   public int a = 0;
   public int getA() {
      System.out.println("getA() method");
      return a;
   }
   public void setA(int aa) {
      if(!(aa > 10))
         a = aa;
   }
}
public class B {
   public static void main(String[] args) {
      A aObject = new A();
      aObject.a = 100; // Not suppose to happen as defined by class A, this causes tight coupling.
      System.out.println("aObject.a value is: " + aObject.a);
   }
}
~~~
  - 만약 개발자가 클래스 A의 변수 a를 private로 변경한다면 클래스 B는 정상 동작하지 않게 된다.

# Loose Coupling (=약한 결합)
- 결합도를 줄이는 것 이다.
- 개발을 하면서 이 방식을 채워 나간다.