# Notion 연결하기

## :airplane: Official Docs

- [Notion — Personal access tokens](https://developers.notion.com/guides/get-started/personal-access-tokens)
- [Notion — API Introduction](https://developers.notion.com/reference/intro)
- [Notion — Search by title](https://developers.notion.com/reference/post-search)
- [Notion — Retrieve your token's bot user](https://developers.notion.com/reference/get-self)
- [Unity — UnityWebRequest.SendWebRequest](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Networking.UnityWebRequest.SendWebRequest.html)

<br><br>

## :fire: 토큰 관리
- 환경 변수란 무엇인가? 똑똑하면서도 어리버리한 컴퓨터를 위해서 미리 경로나 값 등을 지정해 둔 것을 환경 변수(env)라고 할 수 있다. 
- 로컬 환경 변수를 이용해서 관리하는 게?

<br><br>

## :fire: PAT은 내 권한으로 Notion API에 접근하기 위한 개인용 토큰이다.
- PAT은 **Personal Access Token**의 약자다.
- PAT은 하나의 워크스페이스에 속하며, 발급한 사용자의 접근 권한을 사용한다. <br> 다시 말해, PAT이 있으면 내 노션에 접근 할 수 있다.
- 내가 접근할 수 있는 비공개 문서도 API로 읽을 수 있다. 
  - 문서를 웹에 공개할 필요는 없다.

<br><br>

## :sparkles: Unity에서 Notion에 연결하기 위해 별도 SDK를 반드시 설치해야 하는 것은 아니다.

- Notion API는 HTTPS 요청을 받고 JSON 형식으로 응답한다.
- Unity의 기본 기능인 `UnityWebRequest`로 요청할 수 있다.
- SDK는 요청과 응답 처리를 편하게 해주는 라이브러리다. 직접 HTTP 요청을 보내도 된다.
- 이번 연습에서는 기존 프로젝트의 UniTask와 UnityWebRequest를 사용한다.
- 별도의 내 서버 없이 PC에서 Notion 서버에 직접 요청한다.

<br><br>

## :sparkles: PAT은 Windows 사용자 환경 변수에서 읽는다.

- 현재는 Windows 사용자 환경 변수 `NotionToken`에 PAT을 저장한다.
- `EnvironmentVariableTarget.User`를 지정해 사용자 영역의 값을 읽는다.
- 읽은 값을 private 필드 `_notionToken`에 보관하고 요청마다 사용한다.
- 토큰 자체는 소스 코드나 Resources에 넣지 않는다.

~~~c#
_notionToken = Environment.GetEnvironmentVariable("NotionToken", EnvironmentVariableTarget.User);
~~~

<br><br>

## :sparkles: UnityWebRequest에 목적에 맞는 API URL과 HTTP 메서드를 지정한다.

- `UnityWebRequest`는 한 번의 HTTP 요청을 처리하는 객체다.
- 노트 화면을 여는 링크와 데이터를 요청하는 API 주소는 구분한다.
- `GET /v1/users/me`는 PAT을 발급한 사용자 정보를 확인하는 요청이다.
- 이번에 필요한 페이지 정보는 `POST /v1/search`로 검색해서 받아왔다.
- URL과 HTTP 메서드를 지정하고, PAT을 헤더에 넣은 뒤 `SendWebRequest()`로 전송한다.

~~~c#
using (var request = new UnityWebRequest(NOTION_SEARCH_URL, "POST"))
{
    // 인증 헤더와 검색 조건 JSON을 설정한 뒤 요청한다.
}
~~~

<br><br>

## :sparkles: 페이지 정보는 Search API로 제목을 검색해서 찾는다.

- API 주소: `https://api.notion.com/v1/search`
- HTTP 메서드: `POST`
- `query`에 검색할 제목을 넣고, `filter`로 페이지만 검색하도록 지정한다.
- 공식 문서에서 요청 주소, HTTP 메서드, 헤더, JSON 본문, 응답 형식을 확인할 수 있었다.
- 검색 조건 JSON을 `RequestNotionPage(string requestJson)`에 전달했다.

### 예시: 제목에 2026년 10월이 포함된 페이지 검색

~~~json
{
  "query": "2026년 10월",
  "filter": {
    "property": "object",
    "value": "page"
  }
}
~~~

~~~c#
var requestJson = "{\"query\":\"2026년 10월\",\"filter\":{\"property\":\"object\",\"value\":\"page\"}}";
RequestNotionPage(requestJson).Forget();
~~~

- `query`는 페이지의 제목을 검색한다. 본문이나 날짜 속성을 검색하는 조건은 아니다.
- 검색 결과의 페이지 정보에서 `id`와 속성 등을 확인할 수 있다.
- 페이지 본문은 블록으로 구성되며, 본문을 읽으려면 검색 결과의 페이지 ID로 별도 블록 조회를 해야 한다.

<br><br>

### :sparkles: `query`를 생략했을 때

> If no query param is provided, then the response contains all pages or data_sources that have been shared with the connection.

- `query`를 생략하면 제목 조건 없이, 해당 연결에 공유된 페이지 또는 데이터 소스를 조회한다.
- `filter`를 `"page"`로 지정했다면 그중 페이지만 반환한다.
- :airplane: [Notion — Search by title](https://developers.notion.com/reference/post-search)
