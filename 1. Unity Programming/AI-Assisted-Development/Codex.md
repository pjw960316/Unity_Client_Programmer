## :fireworks: 4단계 스펙트럼을 도와줄 도구
- 2026 개발 조합  :  **Unity + Rider + GPT + Codex (Rider Official Plugins)**
  - :airplane: [Codex Is Now Integrated Into JetBrains IDEs](https://blog.jetbrains.com/ai/2026/01/codex-in-jetbrains-ides/?utm_source=chatgpt.com)
  - ![alt text](./captures/codex.png)

<br><br>

## :fireworks: Codex에게 명령을 내리고 개발자가 할 수 있는 일
- 제일 좋은 것은 결국 코드를 읽는 것 이다. Codex라는 시니어 개발자가 코드를 생성한 걸 내가 해석하고 분석 할 줄 알아야 한다.

<br><br>

## :fireworks: 일단 적기 CODEX 잘 쓰기 -> 이거가 핵심일 거 같다.
- 메모리 정리 잘 됐는지 확인해줘. 보다는 현재 씬이 바뀌면 이전 씬에서 필요없는 메모리를 내려야 하는데, 그거 체크해 줘.
  - 예를 들면, 현재 scene change의 책임을 부여한 클래스가 없음. 근데 그 클래스의 책임을 정확히 명시는 했으나, 보수적임.
- AI가 만든 결과물에 감탄하는 단계에서 끝나면 위험함
- AI가 만든 결과물을 읽고 판단하면 성장함
- AI가 만든 결과물을 내 기준으로 고치면 실력이 됨

<br><br>

## :fireworks: CODEX 적용 규칙
- **AGENTS.md를 만들어서 세션마다 진행할 영구 프롬프트를 구성한다.**
  - 경로는 .codex 하단에 만들면 된다.
- **config.toml을 만들어서 CODEX에게 권한을 부여한다.**
  - 더 이상 권한을 묻지 않으므로 인터럽트가 걸리지 않는다.
  - :airplane:[config.toml](https://learn.chatgpt.com/docs/sandboxing?surface=app)
- :airplane:[CODEX 설정 팁](https://blog.naver.com/jhc9639/224186400602)

<br><br>

## :memo: 메모
- 새 세션 == 새 채팅
- 결국 CODEX의 원리와 구성방식을 잘 알아야 개발력도 올라간다. 