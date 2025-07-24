## :fire: Enum Type은 Presenter의 내부에 필드로 구현한다. <br> :fire: Enum Type은 Presenter와 View 그리고 Presenter와 Model끼리만 공유할 상수 약속이다.
- View와 Model에서는 using 문을 이용해서 Presenter가 제공하는 enum을 사용한다.

<br><br>

## :fire: Enum은 Static처럼 사용된다. <br> :fire: IL로 까보면 알 수 있다.
- ![alt text](./capture/20250724_1.png)
- ![alt text](./capture/20250724_2.png)