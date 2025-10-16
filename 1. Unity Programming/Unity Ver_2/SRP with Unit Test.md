## :fireworks: Unit Test에 대해 이해가 확 오는 article.
> As a metaphor for a proper software unit testing example, imagine a mad scientist who wants to build some supernatural chimera, with frog legs, octopus tentacles, bird wings, and a dog’s head. (This metaphor is pretty close to what programmers actually do at work). How would that scientist make sure that every part (or unit) he picked actually works? Well, he can take, let’s say, a single frog’s leg, apply an electrical stimulus to it, and check for proper muscle contraction. What he is doing is essentially the same Arrange-Act-Assert steps of the unit test; the only difference is that, in this case, unit refers to a physical object, not to an abstract object we build our programs from.

> <ins>The purpose of a unit test in software engineering is to verify the behavior of a relatively small piece of software, independently from other parts.</ins> Unit tests are narrow in scope, and allow us to cover all cases, ensuring that every single part works correctly.

<br>

- :link:[Unit Testing and Coding: Why Testable Code Matters](https://www.toptal.com/qa/how-to-write-testable-code-and-why-it-matters)

<br><br>

## :fireworks: Unit Test 하기 좋은 코드로 리팩터링 하는 방법
#### :one: method가 하나의 책임만 갖고 있도록 method를 최대한 작게 쪼갠다.
- 가독성 때문이라고 생각했지만, 이래야 작은 단위의 테스트를 하기 쉽다. 


유지보수가 잘 되는 프로그램일수록 테스트 대상이 작고 명확하며, 의도된 함수 하나만 확인하면 OK인 구조라는 뜻입니다. 정확한 표현입니다.