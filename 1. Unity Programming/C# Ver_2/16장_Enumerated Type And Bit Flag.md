## :fire: Enum Type은 Presenter의 내부에 필드로 구현한다. <br> :fire: Enum Type은 Presenter와 View 그리고 Presenter와 Model끼리만 공유할 상수 약속이다.
- View와 Model에서는 using 문을 이용해서 Presenter가 제공하는 enum을 사용한다.