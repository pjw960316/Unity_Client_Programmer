## :fireworks: XML에서 Dictionary를 사용할 수 있을까? 에 대한 고민

#### :white_check_mark: 경험 (고민)

- xml에서도 Dictionary를 사용하고 싶다.
- 그런데 XmlSerializer로 저장하기 위해 List를 별도로 만들고 Dictionary와 동기화해야 하는 방식이 불만이다. 
- 가독성이 떨어지고 메모리도 추가로 사용하는 것 같다고 느꼈다.
- 저장 형식에 맞추기 위해 별도의 자료구조와 코드를 만드는 것이 싫다. 
- Dictionary를 쓰고 싶지만, 이를 유지하는 과정이 번거롭다면 꼭 고집하려는 것은 아니다.

<br>

#### :white_check_mark: 핵심 검토 사항

- XmlSerializer는 왜 Dictionary를 직접 지원하지 않는가?
- 대부분은 XmlSerializer로 처리하면서 Dictionary만 다른 API나 라이브러리로 저장·복원할 수 있는가? List를 별도로 유지하는 것보다 간단한가?
- Dictionary와 List를 함께 유지할 때 메모리와 동기화 부담은 실제로 어느 정도인가?
- List 하나를 사용한다면 원소를 class와 tuple 중 무엇으로 묶을 수 있고, 어떤 방식이 읽기 쉬운가?

<br><br>

#### :fire: 기존에 사용하던 XmlSerializer는 Dictionary를 지원하지 않는다.
~~~c#
if (typeof(IDictionary).IsAssignableFrom(type))
{
    if (memberInfo == null)
    {
        throw new NotSupportedException(SR.Format(SR.XmlUnsupportedIDictionary, type.FullName));
    }
    else
    {
        throw new NotSupportedException(SR.Format(SR.XmlUnsupportedIDictionaryDetails, memberInfo, type.FullName));
    }
}
~~~
- :airplane:[XmlSerializer와 Dictionary 타입](https://blog.naver.com/techshare/222633166069)

<br><br>

#### :fire: DataContractSerializer로 Dictionary를 사용해보려고 했으나 <br> Xml에서는 List로 관리하는 게 맞다고 판단했다.

<br><br>

#### :fire: private List로 저장하고, 필요한 자료구조는 [xmlIgnore]를 사용해서 public property Dictionary처럼 만들자. 