# 목차
- [목차](#목차)
- [Ref](#ref)
    - [1. 기본 개념](#1-기본-개념)
    - [2. 메모리 절약 관점의 궁금증](#2-메모리-절약-관점의-궁금증)
- [Out](#out)
- [Ref와 Out 정리](#ref와-out-정리)

<br/><br/><br/>

# Ref
### 1. 기본 개념
~~~c#
            int n3 = 10;
            ref int n4 = ref n3;
            n3 = 20;
            Console.WriteLine(n3 + " " + n4); //20 20

            int n5 = 30;
            ref int n6 = ref n5;
            n6 = 40;
            Console.WriteLine(n5 + " " + n6); //40 40
~~~
- ![image](https://user-images.githubusercontent.com/55792986/207465568-8e3bb21e-ed15-4edf-bd1b-7e04a3a82d5b.png)
  - n4는 n3의 값을 가리키고 n3의 값이 변경 되었으므로 모두 20으로 출력된다.
  - n6는 n5의 값을 가리키고 n6의 값이 가리키는 값을 변경시키게 했으므로 모두 40으로 출력된다.
- 참조 타입은 언제나 스택의 변수가 힙의 실제 데이터(예제의 20과 40의 값 들)의 주소를 갖고 있는다고 생각했다. 하지만 ref의 참조 타입은 스택의 변수가 스택에 존재하는 실제 데이터를 참조하는 방식이다. 
- :star: Value Type을 Reference Type처럼 이용하는 키워드이다.
- 함수의 인자로 넘겨줄 때 call-by-reference처럼 되려면 매개변수와 인자에 모두 ref를 붙인다. 

<br/><br/>

### 2. 메모리 절약 관점의 궁금증
- ref로 참조하면 메모리도 절약되는가?
  - 예를 들어 함수의 매개변수로 값을 받는다고 가정하자. 그러면 매개변수를 전달하면서 하나의 복사본을 만들게 된다. 그리고 인자로 넘겨 준 복사본과는 독립적인 관계가 된다. 이 때 복사본을 만들면서 메모리 낭비?가 발생한다. 하지만 ref로 전달해주면 복사본을 생성하지 않기 때문에 메모리가 절약 된다.

<br/><br/><br/>

# Out
- ref와 비슷한 키워드다.
- 전달할 인자를 전달 받은 내부 함수에서 계산해서 출력만을 하고 싶을 때 사용하는 키워드이다.
  - :star:내가 out을 포함한 인자로 함수로 보내줬을 때 나를 읽어서 계산에 사용하지 말고 (계산에 사용은 우변식) 나를 가공만 해줘라 (좌변식)
- 무슨 짓을 하든지 내부 함수에서 우변식에 들어가면 에러가 나온다.
~~~c#
            int outValue = 1;
            UseOutKeyword(out outValue);
            Console.WriteLine(outValue);

            public static void UseOutKeyword(out int justUse)
            {   
                //justUse = justUse + 1; //ERROR
                justUse = 3;
            }
~~~

<br/><br/><br/>

# Ref와 Out 정리
- ![image](https://user-images.githubusercontent.com/55792986/207471350-ac5444c7-3738-48c8-9a8c-943153aeddb9.png)
- 내 생각 : ref와 out은 결국 value type과 관련이 있는 키워드다.