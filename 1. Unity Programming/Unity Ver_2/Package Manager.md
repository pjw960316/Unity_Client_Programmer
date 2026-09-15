## :airplane: Official Docs
- :airplane: [Unity Docs](https://docs.unity3d.com/kr/current/Manual/Packages.html)

<br><br>

## :fireworks: 필요한 Package를 찾는 순서
> **Built-in → Unity Registry → My Assets → 외부 Package 순서로 찾는다.**

- Unity에 내장되었거나 Unity가 공식 관리하는 기능을 먼저 확인하고, 원하는 기능이 없을 때 Asset Store와 외부 Package까지 범위를 넓힌다.
- `+` 외부 Package는 Git URL이나 Local Package처럼 Unity Registry와 Asset Store 밖에서 직접 가져오는 Package를 뜻한다.

### Unity Registry Package는 주기적으로 Update를 확인한다.
- Bug 수정과 안정성 개선을 적용할 수 있다.
- 새로운 Unity·Android·iOS 버전과의 호환성을 유지할 수 있다.
- 폐기된 API를 피하고 현재 Unity가 지원하는 기능을 사용할 수 있다.

<br><br>

## :fire: Package Manager 분류
- **In Project** : 출처와 관계없이 현재 프로젝트에 설치되어 있는 Package 목록이다.

<br>

- **Unity Registry** : Unity가 직접 제공하고 관리하는 **공식 Package Store**이다.
  - Unity에 모든 기능을 기본으로 넣지 않고, 프로젝트에 필요한 기능만 선택해서 설치하게 한다.
  - 프로젝트를 가볍게 유지하고 Package별 버전과 업데이트를 따로 관리할 수 있다.
  - 주기적으로 Update를 확인한다.
    - Bug 수정과 안정성 개선을 적용할 수 있다.
    - 새로운 Unity·Android·iOS 버전과의 호환성을 유지할 수 있다.
    - 폐기된 API를 피하고 현재 Unity가 지원하는 기능을 사용할 수 있다.

<br>

- **Built-in** : Unity Editor에 이미 포함되어 있는 기본 기능이다. <br> 내가 관리 할 필요가 없다!  이미 거의 엔진처럼 자동으로 사용하고 있다.
  - Unity Registry처럼 새로 다운로드하는 기능이 아니라, 프로젝트에 필요한 기능을 켜거나 끈다.
  - Unity Editor에 포함 → **Unity 버전을 올릴 때 함께 업데이트** → 프로젝트에서 사용 여부 선택

<br>

- **My Assets** : 내가 **Asset Store** 다운로드한 Asset(package라고 볼 수도) 목록이다.

<br>

- ** 외부 Package**
  - Github
    - '+'를 누르고 'install package by github' 
    - '+'를 누르고 'install package by name' 
  - NuGet
    - C#·.NET Library를 배포하고 설치하는 Package Manager이다.
    - Unity 전용이 아니므로 Unity의 .NET 버전과 Platform·IL2CPP 호환성을 확인해야 한다.
    - 순수 C# 기능이 필요할 때 공식 배포자가 관리하는 Package를 우선 검토한다.

<br>

- **Services** : Analytics, Ads처럼 Unity가 서버를 통해 제공하는 온라인 기능이다.
  - Package Manager에서는 프로젝트와 해당 Service를 연결하는 데 필요한 Package를 설치하거나 제거한다.
