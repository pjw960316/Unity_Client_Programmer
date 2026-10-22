## :sparkles: Codex Skill
- 반복하는 작업의 절차를 `SKILL.md`에 저장하고, 필요할 때 불러오는 기능이다.
- `AGENTS.md`에는 항상 지킬 규칙을, Skill에는 특정 작업을 할 때 따를 절차를 적는다.
- 지금은 `AGENTS.md`만으로 충분하다. 반복 작업의 절차가 길어지면 Skill로 분리하면 된다.

<br><br>

## :sparkles: 만드는 방법
- 파일 형식은 Markdown이며, 파일 이름은 `SKILL.md`로 작성한다.
- 사용자 전역 경로에 두면 프로젝트와 무관하게 사용할 수 있다.
- 스킬마다 폴더를 만들고, 그 안에 `SKILL.md`를 둔다.

```text
C:\Users\pjw96\.agents\skills\
├─ quick-answer\
│  └─ SKILL.md
└─ study-review\
   └─ SKILL.md
```

- `SKILL.md`의 최소 작성 예시. 폴더 이름과 `name`을 같게 맞추면 관리하기 편하다.

```markdown
---
name: quick-answer
description: 사용자가 간결한 답변을 요청할 때 사용한다.
---

- 결론을 먼저 말한다.
- 필요한 설명만 짧게 덧붙인다.
- 정확한 답변에 필요한 확인은 생략하지 않는다.
```

<br><br>

## :sparkles: 사용하는 방법
- Codex에서 `$quick-answer`를 지정해 호출한다.
- 요청이 `description`과 맞으면 Codex가 자동으로 선택할 수도 있다.
- 목록에 나타나지 않으면 Codex를 재시작해 확인한다.
- 간단한 지침은 `SKILL.md` 하나로 충분하며, 참고 자료나 스크립트는 필요할 때 추가한다.

:airplane: [OpenAI 공식 문서 - Build skills](https://learn.chatgpt.com/docs/build-skills)
