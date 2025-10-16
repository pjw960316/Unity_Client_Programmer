## :fire: Unit Test는 코드의 가장 작은 단위(주로 잘게 쪼갠 메서드)가 <br> 기대한 대로 작동하는지 독립적으로 검증하는 테스트다. <br> :fireworks: 키메라를 이용해서 예시를 들어준 Unit Test Article를 한 번 읽어보자.
> As a metaphor for a proper software unit testing example, imagine a mad scientist who wants to build some supernatural <ins>chimera</ins>, with frog legs, octopus tentacles, bird wings, and a dog’s head. (This metaphor is pretty close to what programmers actually do at work). How would that scientist make sure that every part (or unit) he picked actually works? Well, he can take, let’s say, a single frog’s leg, apply an electrical stimulus to it, and check for proper muscle contraction. What he is doing is essentially the same Arrange-Act-Assert steps of the unit test; the only difference is that, in this case, unit refers to a physical object, not to an abstract object we build our programs from.

> <ins>The purpose of a unit test in software engineering is to verify the behavior of a relatively small piece of software, independently from other parts.</ins> Unit tests are narrow in scope, and allow us to cover all cases, ensuring that every single part works correctly.

<br>

- 개구리 다리, 문어 촉수, 새 날개, 개 머리는 각각 메서드라고 생각하고, 각각이 제 역할을 제대로 수행하는 지 확인하면 키메라라는 게임이 제대로 동작한다.
- :link:[Unit Testing and Coding: Why Testable Code Matters](https://www.toptal.com/qa/how-to-write-testable-code-and-why-it-matters)

<br><br>

## :fire: 좋은 Unit Test는 하나의 책임만 가진 메서드를 대상으로 할 때 <br> 가장 쉽고 효과적이다. <br> :fire: 그러므로 메서드를 쪼개고 쪼개며 1개의 책임만 갖는 SRP를 철저히 지킬 때 <br> Unit Test도 하기 쉬울 것이다.

<br><br>

## :fireworks: UnitTest의 종류
#### :one: 자동으로 실행되고, 특정 조건이 만족되지 않으면 실패(또는 경고) 로그를 출력하는 코드
#### :two: Assert
