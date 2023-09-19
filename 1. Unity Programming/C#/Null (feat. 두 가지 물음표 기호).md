# 목차
- [목차](#목차)
- [개요](#개요)
- [? 연산자](#-연산자)
- [Nullable 연산자 : ? 연산자와 ??연산자 사용법](#nullable-연산자---연산자와-연산자-사용법)
- [Nullable 연산자 사용 이유](#nullable-연산자-사용-이유)
- [Null 검사의 빈도](#null-검사의-빈도)

# 개요
- Null을 정복한다.
- '?' 연산자를 이용해서 null을 검사한다.
- '??' 연산자를 이용해서 코드의 총량을 줄인다.
<br/><br/><br/>

# ? 연산자
~~~c#
string y = x?.ToString().ToString();

string y = ((x != null) ? x.ToString ().ToString () : null);
~~~
- 두 식은 같은 것을 의미한다. (LINQPAD7)
- ![20230919_114911](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/994a3d0f-719f-4b57-9605-ba8a29e5d7f8)
- ![20230919_115011](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/a446db15-ec43-481c-95c0-2784fb21a377)
  - ? 이전의 구문이 null이면 뒤에 어떤 구문이 와도 null을 리턴한다.
<br/><br/><br/>

# Nullable 연산자 : ? 연산자와 ??연산자 사용법
~~~c#
void Test(String str, DataNode dataNode)
{
    /*
    myStr은 매개변수로 들어온 str이 
    null이 아니면 str을 참조하고
    null이면 defaultString을 참조한다.
    */
    String defaultString = "Your Input str is null"
    String myStr = str ?? defaultString;

    /*
    dataNode가 null로 들어오면 dataNode?.Amount도 null 이 된다.
    그러므로 amount의 값은
    dataNode?.Amount가 null이 아니면 dataNode?.Amount로 초기화하고
    dataNode?.Amount가 null이면 5로 초기화 한다.
    */
    int amount = dataNode?.Amount ?? 5;  
}
~~~
<br/><br/><br/>

# Nullable 연산자 사용 이유
- 참조형 객체에 대해서 null을 검사하고 그 결과에 따라 이어지는 변수와 함수를 간단하게 처리하기 위함.
~~~c#
  dataNode?.Amount;
  dataNode?.TestSomething();
~~~ 
- 대게 사용하는 상황
  - 유니티에서 게임오브젝트를 읽어오고 그에 대해 null을 검사하여 null exception을 검사한다.
    - 에디터에서 게임 오브젝트와 바인딩이 되지 않으면 NullReferenceException 발생.
  - 데이터 시트에서 값을 LINQ를 이용해서 찾을 때 찾지 못하는 경우도 존재한다. 
    - 반복문에서 LINQ가 수행된다고 가정한다. 어떤 인덱스에서는 LINQ에서 찾지 못해 null을 반환하고 아무것도 수행하지 않길 의도하는 경우도 존재한다. 이런 경우 nullable 연산자가 매우 유용하다.  
- 코드 예제
~~~c#
//1. 게임오브젝트가 null 인 경우
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public GameObject targetObject;

    private void Start()
    {
        // 게임 오브젝트가 null인 경우에 대한 처리
        if (targetObject != null)
        {
            // 게임 오브젝트가 존재하는 경우에 작업 수행
            targetObject.SetActive(true);
            targetObject.transform.position = Vector3.zero;
        }
        else
        {
            // 게임 오브젝트가 null인 경우에 대한 처리
            Debug.LogWarning("Target object is null!");
        }

        // nullable 연산자를 사용한 게임 오브젝트 접근
        GameObject otherObject = targetObject?.GetComponent<GameObject>();
        if (otherObject != null)
        {
            // otherObject가 존재하는 경우에 작업 수행
            otherObject.SetActive(false);
        }
    }
}
~~~

~~~ c#
//2. 반복문에서 nullable 처리
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        foreach (GameObject obj in objects)
        {
            // nullable 연산자를 사용한 게임 오브젝트 접근
            Transform objTransform = obj?.transform;
            
            if (objTransform != null)
            {
                // objTransform이 존재하는 경우에 작업 수행
                objTransform.position = Vector3.zero;
            }
        }
    }
}
~~~
- 매우 좋은 예제는 아니지만 obj가 null인 경우를 의도하는 구현은 충분히 자주 있다.

<br/><br/><br/>

# Null 검사의 빈도
- ![20230528_222433](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/28e6c741-a61e-45e1-84ca-03179f8daa82)



