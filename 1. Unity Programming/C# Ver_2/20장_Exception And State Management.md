## :fire: 개발자 간 논리 오류를 전파하고 싶을 때는 Debug.Assert를 사용한다.
> It should NEVER be possible to produce a test case which causes an assertion to fire. If an assertion fires, either the code is wrong or the assertion is wrong; either way, something needs to change in the code.
<br><br>

## :fire: 기획 데이터가 null 이거나, 논리 흐름에서 예외가 발생해서 게임을 터뜨려야 하는 순간이 있다. <br> 터뜨려야 고친다. <br> :fire: 이런 순간에는 throw new 를 이용하여 Exception을 발생시킨다.
> It should ALWAYS be possible to produce a test case which exercises a given throw statement. If it is not possible to produce such a test case then you have a code path in your program which never executes, and it should be removed as dead code.