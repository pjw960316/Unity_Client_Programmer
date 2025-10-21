## :fireworks: Constructor는 8장에 있지만 <br> Field와 함께 보는 것이 좋아 여기서 작성한다.

<br><br>

## :fireworks: 두 가지 Initialization 방법 <br> :one: Field Initializer (=Initialization At Declaration) <br> :two: Constructor <br> :fire: Field initializer가 Constructor 보다 먼저 호출된다. <br> :bangbang: 그러므로, Constructor가 Field Initializer를 덮어 씌울 수 있다. <br> :fire: readonly Field는 두 가지 Initialization에서만 할당이 가능하다. <br> Unity의 Awake()나 Start()에서 불가능해서 불편하다.
> Fields are initialized immediately before the constructor for the object instance is called. If the constructor assigns the value of a field, it overwrites any value given during field declaration.

> A read-only field can only be assigned a value during initialization or in a constructor

<br><br>

## :fire: DI 관점에서 <br> :fire: 외부 주입 없이 내부에서 할당이 가능하면 Field Initializer를 사용한다. <br> :fire: 외부에서 parameter로 주입 받아 할당이 필요하면 Constructor를 쓰도록 한다. 
> Prefer initialization in declaration if you don't have a constructor parameter that changes the value of the field.

> If the value of the field changes because of a constructor parameter put the initialization in the constructors.