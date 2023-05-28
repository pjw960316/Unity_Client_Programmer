# 목차
- [목차](#목차)
- [개요](#개요)
- [? 연산자 = Nullable 연산자](#-연산자--nullable-연산자)

# 개요
- '?' 연산자를 이용해서 null을 검사한다.

# ? 연산자 = Nullable 연산자
- 정의
  - 어떤 객체에 대해서 null을 검사하고 null 이 아니라면 이어지는 statement를 수행하지만 null이라면 수행하지 않고 null을 반환한다.
  - 변수와 함수 모두 null 객체에 대해서는 null을 리턴한다.
- 필요한 이유와 상황
  - 유니티에서 게임오브젝트를 읽어오고 그에 대해 null을 검사하여 null exception을 검사한다.
    - 게임 오브젝트가 연결되지 않아 null이 발생하고 이에 대한 예외처리가 이루어지지 않으면 에러가 나타난다.
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

