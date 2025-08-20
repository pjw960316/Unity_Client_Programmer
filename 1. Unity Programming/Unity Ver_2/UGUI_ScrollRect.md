## :fire: 계층 구조 <br> :one: ScrollRect Component를 넣을 Panel <br> :two: ViewPort를 담당할 Panel <br> :three: Content를 담당할 Panel <br> :four: Content 아래에 재사용 될 Widget 들 5개 정도
- ViewPort는 **고정된** 액자 
- Content는 스크롤이 될 공간

<br><br>

## :fire: ViewPort는 RectMask 2D를 넣어서 전체 중 일부 영역만 보이게 한다. 없으면 ViewPort 뚫고 Content가 모두 보인다. <br> :fire: Content는 Vertical Layout을 넣어서 Widget 들이 정렬되도록 한다.