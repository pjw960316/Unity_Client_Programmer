# Notion 연결하기

## :airplane: Official Docs

- [Notion — Personal access tokens](https://developers.notion.com/guides/get-started/personal-access-tokens)
- [Notion — API Introduction](https://developers.notion.com/reference/intro)
- [Notion — Retrieve your token's bot user](https://developers.notion.com/reference/get-self)
- [Unity — UnityWebRequest.SendWebRequest](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Networking.UnityWebRequest.SendWebRequest.html)

<br><br>

## :fire: PAT은 내 권한으로 Notion API에 접근하기 위한 개인용 토큰이다.

- PAT은 **Personal Access Token**의 약자다.
- PAT은 하나의 워크스페이스에 속하며, 발급한 사용자의 접근 권한을 사용한다.
- 내가 접근할 수 있는 비공개 문서도 API로 읽을 수 있다. 문서를 웹에 공개할 필요는 없다.
- 내부 연결의 봇에게 페이지를 공유하는 방식과 달리, PAT은 발급한 사용자의 페이지 권한을 사용한다.
- API 요청에 토큰을 붙여 접근 권한을 전달한다.

~~~http
Authorization: Bearer <PAT>
~~~

<br><br>

## :fire: Unity에서 Notion에 연결하기 위해 별도 SDK를 반드시 설치해야 하는 것은 아니다.

- Notion API는 HTTPS 요청을 받고 JSON 형식으로 응답한다.
- Unity의 기본 기능인 `UnityWebRequest`로 요청할 수 있다.
- SDK는 요청과 응답 처리를 편하게 해주는 라이브러리다. 직접 HTTP 요청을 보내도 된다.
- 이번 연습에서는 기존 프로젝트의 UniTask와 UnityWebRequest를 사용한다.
- 별도의 내 서버 없이 PC에서 Notion 서버에 직접 요청한다.

<br><br>

## :fire: 공개 프로젝트에서는 PAT 파일을 Git 저장소 밖에 둔다.

- 이번에는 PC의 특정 경로에 PAT을 텍스트 파일로 저장하고, 실행 중 해당 파일을 읽기로 했다.
- 예시: `C:\HabitGameLocal\notion-pat.txt`
- 파일에는 PAT 문자열만 저장한다.
- 공개하는 것은 파일을 읽는 코드와 경로 정보다. 실제 토큰 파일은 저장소 밖에 둔다.
- `Resources`는 프로젝트 내부이므로 파일이 함께 커밋될 수 있고, 빌드에도 포함된다.
- 외부 파일은 `Resources.Load()` 대신 `File.ReadAllText()`로 읽는다.
- 파일을 읽은 뒤 `Trim()`으로 앞뒤 공백과 줄바꿈을 제거한다.

~~~c#
var pat = File.ReadAllText(patFilePath).Trim();
~~~

<br><br>

## :fire: 첫 요청은 PAT으로 내 사용자 정보를 읽는 것으로 시작한다.

- 첫 요청 주소: `https://api.notion.com/v1/users/me`
- PAT으로 요청하면 발급한 사용자 정보를 반환한다.
- 사용자 정보를 읽는 요청이 성공해도 개별 문서 조회까지 검증된 것은 아니다.
- 요청에는 `Authorization`과 `Notion-Version` 헤더를 넣는다.
- 이번 코드의 API 버전은 `2026-03-11`이다.

~~~text
로컬 PAT 파일 읽기
    → 토큰을 요청 헤더에 넣기
    → Notion 서버에 GET 요청
    → HTTP 상태와 원본 JSON 확인
~~~

### HabitGame에서 준비한 코드

- `NotionManager.RequestNotionUserAsync(string patFilePath)` 메서드를 추가했다.
- 파일 존재 여부와 빈 토큰을 확인하고, 요청 후 HTTP 상태와 원본 JSON을 Console에 출력한다.
- HTTP 실패 시에도 오류 응답 내용을 관찰할 수 있도록 원본 응답을 반환한다.
- `PracticeNotion()`에서 실제 파일 경로를 전달하여 호출할 수 있다.

~~~c#
RequestNotionUserAsync(@"C:\HabitGameLocal\notion-pat.txt").Forget();
~~~

<br><br>

## :fire: 현재는 첫 연결 코드를 준비한 단계다.

- 아직 실제 Notion 통신 성공과 Unity 컴파일 여부는 확인하지 않았다.
- 다음에는 로컬 토큰 파일을 준비하고, Unity Play 모드에서 응답을 확인한다.
