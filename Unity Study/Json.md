# 목차
- [목차](#목차)
- [Json에 관한 생각](#json에-관한-생각)
- [Json 기본](#json-기본)
- [JsonUtility](#jsonutility)
    - [1. 개요](#1-개요)
    - [2. 예제](#2-예제)
- [System.Text.Json](#systemtextjson)

# Json에 관한 생각
- 인턴 과정에서는 newtonsoft를 사용할 수 없는 환경이라 직접 파싱을 하는 함수를 만들었다.
- **Json을 파싱하는 것은 회사의 framework에 분명 존재할 것 이고, 그때는 이를 참고한다.**

# Json 기본
- ![image](https://user-images.githubusercontent.com/55792986/183588199-73975a4d-da6e-464b-8f77-a1cab49e4008.png)
- ![image](https://user-images.githubusercontent.com/55792986/183605050-091efb01-710c-4ba8-a398-b1e44b8ddb35.png)
- 중괄호로 감싸지면 하나의 객체고, 대괄호로 감싸지면 여러 객체를 담는 배열이다.
- 항상 중괄호로 먼저 시작해야 하나?

# JsonUtility
### 1. 개요
- 유니티에서 공식적으로 지원해주는 Json 파싱 클래스다.
- <img width="518" alt="20220809_164916" src="https://user-images.githubusercontent.com/55792986/183594565-69f1cffa-1b8b-45fd-bbe5-1187ce5e0aba.png">

### 2. 예제
(1) json 파일
~~~
{
    "minion":
    [
        {
            "name" : "warrior_minion" ,
            "ad" : 10,
            "ap" : 0     
        },
        {
            "name" : "magician_minion" ,
            "ad" : 5,
            "ap" : 10
        },
        {
            "name" : "cannon_minion" ,
            "ad" : 100,
            "ap" : 0
        }
    ]
}
~~~
- 보통 이렇게 배열 형식으로 사용하는 경우가 많을 것 이다.
- 현재는 배열 내부에 3개의 객체가 있다.
  - 객체가 1개인 경우는 배열을 사용하지 않는데 이런 경우 json 배열을 만들지 말고 아래의 JsonUtility 코드에서 Minions 클래스를 만들지 않으면 해결된다.
(2) JsonUtility 이용
~~~
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//데이터 매니저에서 모든 데이터를 관리할 필요가 없지만 현재는 작은 프로그램이므로 이렇게 진행.
//나중가면 미니언 데이터만 관리하는 스크립트를 따로 구성한다.

[System.Serializable]
public class Minion
{
    public string name;
    public int ad;
    public int ap;

    public void printDatas()
    {
        Debug.Log(name + " " + ad + " " + ap);
    }
}

public class Minions
{
    public Minion[] minion;
}

public class DataManager : MonoBehaviour
{
    void Start()
    {
        InitializeMinionDatas();        
    }

    private void InitializeMinionDatas()
    {
        //파일 경로
        string path = Application.dataPath + "/Datas/minion.json";
        var datas = File.ReadAllText(path);      

        //데이터 전체를 읽어온다.
        Minions minion_list = JsonUtility.FromJson<Minions>(datas);

        //데이터 배열의 원소를 순회한다.
        foreach(var i in minion_list.minion)
        {
            i.printDatas();
        }       
    }    
}
~~~
- **언제나 key와 클래스의 멤버 변수는 동일해야 한다. 그러므로 소문자로 작성한다.**

# System.Text.Json
- [Reference](https://docs.microsoft.com/ko-kr/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to?pivots=dotnet-6-0)
- .NET에서 공식적으로 지원하는 json parser다.
- 추후에 필요하다면 공부해보자.