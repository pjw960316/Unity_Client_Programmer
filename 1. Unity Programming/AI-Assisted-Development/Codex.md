## :fireworks: CODEX는 4단계 스펙트럼을 도와줄 도구
- 2026 개발 조합  :  **Unity + Rider + GPT + Codex (Rider Official Plugins)**
  - :airplane: [Codex Official Docs](https://learn.chatgpt.com/docs)
  - :airplane: [Codex Is Now Integrated Into JetBrains IDEs](https://blog.jetbrains.com/ai/2026/01/codex-in-jetbrains-ides/?utm_source=chatgpt.com)
  - ![alt text](./captures/codex.png)

<br><br>

## :fireworks: CODEX 전용 관리 문서
- **AGENTS.md를 만들어서 세션마다 진행할 영구 프롬프트를 구성한다.**
  - 경로는 .codex 하단에 만들면 된다.
- **config.toml을 만들어서 CODEX에게 권한을 부여한다.**
  - 더 이상 권한을 묻지 않으므로 인터럽트가 걸리지 않는다.
  - :airplane:[config.toml](https://learn.chatgpt.com/docs/sandboxing?surface=app)
- :airplane:[CODEX 설정 팁](https://blog.naver.com/jhc9639/224186400602)

<br><br>

## :sparkles: 추론 수준 — 내가 언제 사용할까?
- 모델명과 분리하여, 작업의 복잡도와 AI에 맡길 범위를 기준으로 선택한다.
- **나의 기본값은 Medium. 구조 판단이나 어려운 오류에서 High 이상을 사용한다.**

| 수준 | 직관적인 선택 기준 | 내 프로젝트에서 사용할 때 |
| --- | --- | --- |
| Low | 해야 할 일이 명확하고 간단할 때 | :x: |
| Medium | 코드를 이해하며 작은 단위로 개발할 때 — 기본값 | 일반적인 구현 |
| High | 여러 흐름을 연결하고 설계를 판단할 때 | 복잡한 설계 |
| xHigh | 원인을 찾기 어렵거나 여러 조건이 얽혔을 때 | High에서도 해결되지 않고, 더 기다려서라도 풀 가치가 있을 때만 |
| Max | 시간을 들여 넓은 범위를 종합 검토할 때 | :x: |
| Ultra | 지원 환경에서 가장 깊은 검토가 필요할 때 | :x: |

- 위 예시는 나의 작업 선택 기준이며, 단계별로 가능한 작업을 제한하는 규칙은 아니다.
- 추론 수준을 높이면 응답 시간과 사용량이 늘어날 수 있다. 지원 단계와 의미는 모델·앱·버전에 따라 달라질 수 있으므로, 이 6개가 영구 고정된다고 보지는 않는다.
- Ultra와 병렬 에이전트 사용은 구분한다. ChatGPT Work의 Ultra는 자율적인 작업 분담을 지원하지만, 로컬 Codex에서는 명시적인 요청이나 AGENTS.md·스킬 지침에 따라 분담한다.

<br>

- :airplane: [OpenAI 추론 수준 안내](https://developers.openai.com/api/docs/guides/reasoning)
- :airplane: [Codex 서브에이전트와 추론 설정](https://learn.chatgpt.com/docs/agent-configuration/subagents)
