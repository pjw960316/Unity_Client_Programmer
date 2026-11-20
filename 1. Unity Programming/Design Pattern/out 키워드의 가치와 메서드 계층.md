## :fireworks: 메서드를 나눌 때 out으로 지역변수 유지하기

#### :white_check_mark: 회사에서의 경험

- 회사에서는 클래스에서 2군데만 쓰여도 귀찮아서 멤버 변수로 선언했다.
- 협업을 하기 때문에 어쩔 수 없이 코드에는 god-manager가 있었다. 
  - 3000줄 이상
- 그런 곳에도 멤버 변수를 선언하니 scope는 말도 안되게 넓었다.

<br>

#### :white_check_mark: 현재의 경험

- 위의 구조가 문제라고 여겼고 개선사항을 찾기 시작했다. 그게 out 키워드
- 나는 상위 메서드에서 호출 순서만 읽어도 전체 흐름이 보이는 구조를 좋아한다.

  ```mermaid
  flowchart LR
      subgraph reward["물 보상 처리 생성자 · 메서드 호출 구조 예시"]
          direction LR
          subgraph dates["날짜 가져오기()"]
              direction TB
              last["마지막 보상일 읽기()"] --> confirmed["물 섭취 확인 날짜 읽기()"]
          end
          subgraph compare["날짜 비교하기()"]
              direction TB
              selectDates["마지막 보상일 이후 날짜 고르기()"] --> countDates["보상 대상 일수 세기()"]
          end
          subgraph update["보상 기록 갱신하기()"]
              direction TB
              addPoints["보상 포인트 더하기()"] --> updateDate["마지막 보상일 갱신하기()"]
          end
          dates --> compare --> update
      end
  ```

- 이런 구조에서 많은 scope가 발생한다.
- 이 때 상위 메서드에서 지역변수를 생성하고, out 키워드를 구하는 메서드를 만드는 방식이 좋지 않을까 싶었다.

<br>

#### :white_check_mark: 값을 가져오는 단계에서 out을 쓴 이유

- `GetRewardDates`가 두 값을 채우고, 생성자는 그 값을 다음 메서드에 인자로 넘길 수 있다.

  ```csharp
  GetRewardDates(out var latestRewardDate, out var confirmedDates);
  ```

- 지역 변수는 상위 메서드의 scope를 갖는다. <br> 그 값의 계산은 하위 메서드에서 담당한다.
  - 나는 값은 상위 메서드의 지역변수로 두고, 그 값을 준비하는 일만 하위 메서드에 맡기는 구조를 좋아한다.
  - 상위 메서드에서는 그 값이 어느 단계에 쓰이는지 읽을 수 있고, <br> 하위 메서드에서는 그 값을 어떻게 구하는지에만 집중할 수 있다.
- 멤버 변수보다 scope가 작기 때문에 유지보수에 훨씬 좋다.

> 한 번의 처리에 필요한 값은 그 흐름을 지휘하는 메서드에 둔다

<br><br>

## :fire: 한 번의 처리에 필요한 값은 우선 지역변수로 둔다. <br> 처리가 끝난 뒤에도 객체가 그 값을 기억해야 하면 멤버 변수로 올린다.

- YAGNI를 따른다.
- 격상 자체는 CODEX에게 맡기면 된다.
