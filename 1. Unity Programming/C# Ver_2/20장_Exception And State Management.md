## :fireworks: 논리판정 기초 내용도 추가한다.

<br><br>

## :fire: 판별식에서 && 와 ||은 좌변식의 결과만으로도 조건의 True / False를 판별가능하다.
- **Short-Circuiting Evaluation**
> Yes. In C# && and || are short-circuiting and thus evaluates the right side only if the left side doesn't already determine the result. The operators & and | on the other hand don't short-circuit and always evaluate both sides.
~~~c#
if(0 < queue.Count && queue.Peek() == 10)
{}
~~~
- queue가 비어 있다면 좌변식이 false가 되므로 전체 논리는 무조건 False가 된다. 
- 그러므로 우변식을 검사하지 않기에, queue가 비어 있어도 예외가 발생하지 않는다.

<br><br>

## :fire: 조건이 명확하다면 early-continue 또는 early-return으로 <br> 분기를 일찍 종료하는 방식이 좋다고 생각한다. 

<br><br>

## :fire: 개발자 간 논리 오류를 전파하고 싶을 때는 Debug.Assert를 사용한다.
> Assertions are used to check the programmer's understanding of the world. An assertion should fail only if the **programmer has done something wrong.** For example, never use an assertion to check user input.

> It should NEVER be possible to produce a test case which causes an assertion to fire. If an assertion fires, either the code is wrong or the assertion is wrong; either way, something needs to change in the code.
<br><br>

## :fire: 기획 데이터가 null 이거나, 논리 흐름에서 예외가 발생해서 게임을 터뜨려야 하는 순간이 있다. <br> 터뜨려야 고친다. <br> :fire: 이런 순간에는 throw new 를 이용하여 Exception을 발생시킨다.
> It should ALWAYS be possible to produce a test case which exercises a given throw statement. If it is not possible to produce such a test case then you have a code path in your program which never executes, and it should be removed as dead code.