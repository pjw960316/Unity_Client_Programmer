## :fireworks: Nullable Value Type 과 Nullable Reference Type은 모두 type에 ?을 붙이고, 이는 null을 허용함을 의미한다.

<br><br>

## :fire: Nullable Value Type은 <br> 2개의 필드 (bool hasValue 와 T value)를 들고 있는 struct다.

#### [실제 구현]
<details>
  <summary> :point_up_2: 누르면 코드가 나옵니다.  </summary>

- ![alt text](./capture/20250828_2.png)

</details>

<br><br>

## :fire: int? == Int32? == Nullable<int> == Nullable<Int32> 

<br><br>

## :fire: MSDN Nullable Attribute
- ![alt text](./capture/20250828.png)

<br><br>

## :fire: Nullable Reference Type은 NullReferenceException을 줄여준다.
> Nullable reference types are a group of features that minimize the likelihood that your code causes the runtime to throw System.NullReferenceException.
- 그러나 줄이는 게 과연 좋을까?

