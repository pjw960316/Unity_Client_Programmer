## :Fire: 일단
- 우선 FieldObjectManager와 FieldObjectPresenter 객체들의 관계
- FieldObjectManager는 모든 FieldObjectPresenter를 관리한다. -> 이건 필드로 들고 있고 접근이 가능함을 의미한다.
- 그러므로 둘은 양방향에 강한 의존이다. 하지만 괜찮다고 본다.
- 대신 이렇게 하면 장점이 FieldObjectManager는 View에 접근하지 못한다. 즉, View와 FieldObjectManager는 의존성이 zero다. Model도.
- 그 결과, Manager와 Presenter 간의 소통만이 존재하므로 View와 Model이 안전하게 유지된다.