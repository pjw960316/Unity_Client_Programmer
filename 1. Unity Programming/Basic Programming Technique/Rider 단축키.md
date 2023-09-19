# 목차
- [목차](#목차)
- [공식 문서 사용법](#공식-문서-사용법)
- [F12 : Go to Declaration or Usages](#f12--go-to-declaration-or-usages)
- [Shift + F12 : Find Usages](#shift--f12--find-usages)
- [Ctrl + F12 : Go To Implementation](#ctrl--f12--go-to-implementation)
- [:star:Shift + Shift : Search Everywhere](#starshift--shift--search-everywhere)
- [Ctrl + Shift + F : Find In Files](#ctrl--shift--f--find-in-files)
- [F3 : Find Next/ Move to Next Occurrence](#f3--find-next-move-to-next-occurrence)
- [Ctrl + G : Go to Line/Column](#ctrl--g--go-to-linecolumn)
- [Ctrl + R + R : Rename Refactoring](#ctrl--r--r--rename-refactoring)
- [Alt + Enter : Show Intention Actions](#alt--enter--show-intention-actions)
- [Alt + Home : 부모 클래스의 메서드로 이동](#alt--home--부모-클래스의-메서드로-이동)
- [Ctrl + Alt + B : 디버깅 포인트 목록 보기](#ctrl--alt--b--디버깅-포인트-목록-보기)
  
 <br/><br/><br/>
  
# 공식 문서 사용법
- :link:[Rider Official Docs](https://www.jetbrains.com/help/rider/Reference_Keyboard_Shortcuts_Index.html#top_shortcuts)
  - 단축키 옆에 적은 내용을 해당 링크에서 검색하면 찾아갈 수 있다.
<br/><br/><br/>

# F12 : Go to Declaration or Usages
- **메서드 정의문으로 바로 이동**한다.
- 정의를 개발자가 스스로 다른 스크립트나 현재 스크립트에 만들었다면 그 곳으로 순간이동 한다.
<br/><br/><br/>

# Shift + F12 : Find Usages
- **메서드의 호출 위치를 모두 보여준다.** (=참조된 모든 위치를 정리해서 보여준다.)
- :star:만약 형식이 다양하다면 형식을 select 해줘야 하므로 Shift + F12 + Enter(Select)를 해주면 어디서 호출하고 있는지 파악이 가능하다.
  - 이 함수가 어디서 호출되는지 파악하면 코드 흐름을 파악하기 용이하다.
<br/><br/><br/>

# Ctrl + F12 : Go To Implementation
- 어떤 메서드가 인터페이스의 메서드를 호출하는 경우, 해당 메서드의 직접적인 구현부를 봐서 어떻게 동작하는 지 이해할 필요가 있다.
- 예를 들어, 어떤 넘이 Dispose()를 호출하는 데 이 넘의 정의부를 F12로 가면  Interface의 정의(정의도 없지)만 나온다.
~~~c#
public interface IDisposable
  {
    /// <summary>
    ///   관리되지 않는 리소스의 확보, 해제 또는 다시 설정과 관련된 응용 프로그램 정의 작업을 수행합니다.
    /// </summary>
    void Dispose();
  }
~~~
  - 여기서 Dispose에 Ctrl + F12를 하고 직접 구현한 클래스의 구현부를 본다. 내가 F12를 누른 곳이 CompositeDispose 클래스의 객체에 대한 Dispose였다.
~~~c#
public void Dispose()
        {
            var currentDisposables = default(IDisposable[]);
            lock (_gate)
            {
                if (!_disposed)
                {
                    _disposed = true;
                    currentDisposables = _disposables.ToArray();
                    _disposables.Clear();
                    _count = 0;
                }
            }

            if (currentDisposables != null)
            {
                foreach (var d in currentDisposables)
                    if (d != null)
                        d.Dispose();
            }
        }
~~~
<br/><br/><br/>

# :star:Shift + Shift : Search Everywhere
- 전체 검색
- ![image](https://user-images.githubusercontent.com/55792986/213972762-c5ae0099-ba36-46f3-8bdc-36fdb01135f2.png)
  - 검색한 문자열이 포함된 파일(.cs), 클래스, 등에 대해 전체 프로젝트에서 검색한다.
  - **:star: 스크립트 파일 찾을 때 유용하다.**
  - **코드를 작성한 분들이 이전에 어떻게 사용했는 지 찾아볼 수 있다.**
<br/><br/><br/>

# Ctrl + Shift + F : Find In Files
- 정확한 명칭은 Find and Replace Text in Solution
- Shift + Shift는 간략하게 나오지만 얘는 코드의 일부도 보여준다.
  - **:star: 협력 코드에서 다른 사람이 어떻게 썼는지 찾기에 유용하다.**
    - 예를 들어, Unirx의 OnNext()를 어떻게 썼는지 궁금할 때 OnNext()를 검색한다.
<br/><br/><br/>

# F3 : Find Next/ Move to Next Occurrence
- 찾은 내용에 대해서 다음으로 이동할 때 사용한다.
<br/><br/><br/>

# Ctrl + G : Go to Line/Column 
- 줄 번호 이동
<br/><br/><br/>

# Ctrl + R + R : Rename Refactoring
- 바꾸고 싶은 애의 이름을 더블클릭하고 이 기능을 사용하면 해당 이름과 같은 모든 이름을 원하는 이름으로 한 번에 변경할 수 있다.
<br/><br/><br/>

# Alt + Enter : Show Intention Actions
- 라이더가 알려주는 더 좋은 코드로 바꾸는 기술.
<br/><br/><br/>

# Alt + Home : 부모 클래스의 메서드로 이동
<br/><br/><br/>

# Ctrl + Alt + B : 디버깅 포인트 목록 보기
- ![20230907_153123](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/8c4a23ab-3775-4f15-adc7-13e70a3be3b5)
  - 전체 선택 하고 '-'를 누르면 디버깅 포인트 모두 날릴 수 있다.





