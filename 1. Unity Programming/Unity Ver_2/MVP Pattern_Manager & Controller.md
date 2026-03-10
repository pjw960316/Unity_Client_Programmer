## :fire: Controller 와 Manager의 관계
#### 1. Controller는 Unity Scene에 존재하는 Object다. 

#### 2. Manager는 Unity Scene에 존재하는 Controller를 관리하는 C# 싱글턴 객체다.
- Manager는 MonoBehaviour를 상속받지 않고, 유니티 타입을 사용하지 않는다.
  - Ray, InputAction.CallbackContext같은 걸 사용할 수 없다.

#### 3. Controller는 다른 Controller를 절대 모르고 자신과 관련 없는 Manager도 절대 알면 안 된다. <br> Manager는 다른 Manager들끼리는 서로 알고 공유 할 수 있다. 

#### 4. Manager가 다른 Manager와 데이터를 공유 할 때는 Unity의 타입을 사용하지 않는다. <br> 그러므로 Controller에서 Unity 타입을 C# 타입으로 변환해서 Manager에게 전달한다.
- Controller가 게임을 통해 받는 Unity 데이터를 C# 데이터로 가공한다.
- Controller가 가공한 데이터를 통해 Manager는 C# 데이터를 갖고 이게 Manager가 외부와 공유하는 상태 데이터가 된다.

#### 5. 그러므로, Controller가 Manager를 들고 있고, Manager는 Controller를 들고 있지 않는다.

<br><br>

## :fire: Manager가 Controller에게 Request 하면 안 된다. <br> :fire: Controller가 미리 Manager가 다른 Manager와 공유해야 할 데이터를 가공해서 전달해놔야 한다.