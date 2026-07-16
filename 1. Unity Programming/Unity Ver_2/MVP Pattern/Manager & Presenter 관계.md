## :fire: Manager는 Presenter와만 대화하도록 구현한다.
- Manager가 MVP 구조의 GameObject와 대화를 하기 위해서는 결국 누군가와는 의존을 가져야한다.
- Presenter의 가장 큰 책임은 결국 연결 통로기 때문에 Presenter와 대화를 한다.
- View와 Model은 Manager를 알지 못하며 의존성이 분리된다.