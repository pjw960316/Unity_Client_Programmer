# 목차
- [목차](#목차)
- [Layout Group](#layout-group)
- [Layout Element](#layout-element)
- [Content Size Fitter](#content-size-fitter)

<br/><br/><br/>

# Layout Group
- UI의 배치를 모두 수동으로 하면 상당히 많은 작업을 요구하며 시간을 많이 소모할 것 이다.
- **Layout Group으로 UI를 자동으로 정렬하여 배치할 수 있다.** 
- Horizontal, Vertical, Grid가 존재한다.
  - Grid는 바둑판이다.
- 세부 구성 요소
  - Spacing : 자식 요소들 간의 사이 거리
  - Child Alignment : 레이아웃 객체의 기준으로 자식들의 정렬 위치를 바꾼다.
  - Child Force Expand : 레이아웃(부모)에 자식의 정렬을 맞출 것 인가
- 각각의 자식은 Layout Element을 가질 수 있고, 이를 이용하여 자식 마다의 설정을 할 수 있다.
- https://wergia.tistory.com/178

<br/><br/><br/>

# Layout Element
- Layout Group에서 자식들의 배치에 대한 툴을 잡는다. 하지만 자식들 개개인의 옵션에 따라 배치를 커스텀하고 싶을 때가 존재한다.
- 이 때 Layout Element를 자식들에게 각각 부여하여 개별의 배치를 설정한다.
- 이름 그대로 상위 타입에 대한 자식 위젯(=element)의 크기를 조절한다.
  - OSA에서 스크롤 뷰를 만들 때 Content Size Fitter의 Preferred를 적용하려면 Layout Element의 preferred 값을 조절한다.

<br/><br/><br/>

# Content Size Fitter
- UI의 내용물에 따라서 UI의 크기를 알아서 조절해 주는 기능.
- 보통 Layout과 함께 사용 된다.