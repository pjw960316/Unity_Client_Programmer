## :fireworks: 드디어 이해한 interface와 리모컨의 비유
## :fire: interface는 **외부와의 계약(=public contract)**으로 보통 설명된다. <br> 이는 public method로 해석 할 수 있다.
- 리모컨을 사용할 때 우리는 리모컨 내부가 어떻게 개발되었는지 전혀 모른다.
- 숫자 1을 누르면 화면에 1이 찍힌다. 실제로는 신호가 회로를 타고... 뭐 있겠지. 이걸 재해석하면 우리는 리모컨에게서 숫자 1을 선택하는 기능을 제공받는다.
- 외부와의 계약은 즉, 외부의 public한 객체에게 자신의 기능을 제공한다는 의미다. 
- 그러므로 제공받은 기능을 사용하기 위해서는 public method로 열어둬야 한다.

#### [C# 8.0에서는 private method도 가능은 한데?  ->  설계 관점에서는 쓰지 않는 게 맞아보인다.]
> private methods used to be prohibited in interfaces because interfaces are supposed to be contracts. They are a guarantee that "this class has the following methods and properties". Why would it be useful to guarantee that a class has a private method? It isn't useful, because no one else can call it!

> In C# 8, this changed. You can now specify private interface default methods. Note that it has to be a default method, not the ones that doesn't have an implementation. Here's the docs stating that fact:

> The syntax for an interface is relaxed to permit modifiers on its members. The following are permitted: private, protected, internal, public, virtual, abstract, sealed, static, extern, and partial.

> It is an error for a private or sealed function member of an interface to have no body. Here is a quote from the docs explaining why this is allowed: Static and private methods permit useful refactoring and organization of code used to implement the interface's public API.

<br><br>

## :fire: interface vs abstract class 공통점과 차이점
#### :one: 공통점
- 둘 다 **상속을 통해 하위 타입에서 구현을 하도록 강제**하는 구조이다.
- 객체의 설계도 특성을 갖고 있다.

#### :two: 차이점
- interface는 외부와의 소통을 위한 설계, abstract class는 외부와의 소통을 위해 내부 객체들의 구현 설계
- interface는 기본적으로 외부와의 계약이므로 public method로 구현되어 있다. abstract class는 접근지장자에 대해서 자유롭다.
  - 보통 protected method를 abstract method로 선언해서 구현했던 것 같다. -> 코드의 중복 해결 및 내부 캡슐화 
- interface의 method는 모두 하위 타입에서 구현해야 한다. 하지만 abstract class에서는 일반 메서드를 통해 공통 로직을 구현할 수 도 있다.
- interface는 field를 통해 state를 갖지 않는 게 설계 원칙이다. abstract class는 상속구조 내에서 공통된 상태를 갖는다.

#### :three: 그래서 Interface가 최상단, Abstract Class가 중간, Concrete Class가 하단으로 구성되는 구조가 종종 보인다.