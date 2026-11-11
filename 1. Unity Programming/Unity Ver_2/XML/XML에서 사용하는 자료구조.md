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

## :sparkles: DataContractSerializer로 Dictionary를 사용해보려고 했으나 <br> Xml에서는 List로 관리하는 게 맞다고 판단했다.

- 연월을 key로, 그 달에 물을 마셔서 포인트를 얻은 횟수를 value로 관리하고 싶었다. 예를 들면 `202610 → 3`처럼 key로 바로 접근하고 싶었다.
- XmlSerializer가 Dictionary를 직접 지원하지 않아, Dictionary를 지원하는 .NET 공식 API인 DataContractSerializer를 알아봤다.
- 내 의도는 기존 필드와 List는 XmlSerializer로 처리하고, Dictionary만 DataContractSerializer로 처리하는 것이었다. 저장 방식 전체를 교체하면서 기존 코드를 많이 바꾸고 싶지는 않았다.
- 같은 XML 안에서 둘을 혼용하는 코드를 보니, Dictionary를 XML 요소로 변환하고 다시 복원하는 연결 코드가 필요했다. 코드가 길어지고 직관적이지 않았으며, 클래스가 커지고 복잡해진다고 느꼈다.
- 이 처리를 XmlDataManager의 전문 메서드로 옮겨도, 저장·복원할 때 변환을 호출해야 하는 규칙을 계속 관리해야 한다는 점이 부담스러웠다. AI가 구현해 주더라도 결국 내가 이해하고 유지보수해야 하는 코드라는 생각이 들었다.
- 그래서 XML에서는 `List<class>`로 읽고 쓰기로 했다. 읽어 온 List를 public getter에서 Dictionary로 변환하는 정도는 짧고 이해하기 쉬웠다.
- 처음에는 Dictionary를 저장하려고 List까지 별도로 만드는 것이 싫었다. 이제는 List를 저장 원본으로 정하고, key 조회가 필요할 때 Dictionary 형태로 제공하기로 했다. Dictionary를 직접 저장하는 것보다 코드가 직관적이고 관리하기 쉬운지가 더 중요했다.

<br><br>

## :sparkles: public List로 저장하고, 필요한 자료구조는 [XmlIgnore]를 사용해서 public property에서 Dictionary로 만들어 사용하자.

- XML의 읽기·쓰기는 `List<class>`로 처리한다. 기록 하나에 함께 있어야 하는 정보는 class로 묶는다.
- key로 접근하고 싶을 때는 [XmlIgnore]가 붙은 public property에서 Dictionary로 변환해 사용한다.

현재 `MyCharacterData.cs`에서 물 보상 기록을 다루는 부분만 발췌했다. `YearMonth`에는 `"202610"` 같은 연월을, `RewardCount`에는 그 달의 보상 횟수를 담는다.

~~~c#
[XmlRoot]
public class MyCharacterData
{
    public class MonthlyWaterPointRecordData
    {
        public string YearMonth;
        public int RewardCount;
    }

    public List<MonthlyWaterPointRecordData> MonthlyWaterPointRecordList = new();

    [XmlIgnore]
    public ImmutableDictionary<string, int> MonthlyWaterPointRecordDictionary =>
        MonthlyWaterPointRecordList.ToImmutableDictionary(record => record.YearMonth, record => record.RewardCount);
}
~~~

**Codex 보완 설명**

- 현재 XmlSerializer가 저장하는 List는 `public` 필드다. `[XmlIgnore]`는 Dictionary를 반환하는 프로퍼티를 XML 저장 대상에서 제외하고, 실제 List → Dictionary 변환은 getter가 수행한다.
- getter는 호출할 때마다 조회용 ImmutableDictionary를 만든다. 기록 추가와 횟수 변경은 원본 List에 반영한다. 이렇게 하면 저장용 List와 별도의 Dictionary를 각각 갱신하는 동기화 코드를 둘 필요가 없다.
