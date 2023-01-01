# 목차
- [목차](#목차)
- [개요](#개요)
- [문자열 슬라이싱](#문자열-슬라이싱)
- [문자열 <-> 숫자](#문자열---숫자)
- [특정 문자로 문자열 구분하기](#특정-문자로-문자열-구분하기)
- [push_back(인자)](#push_back인자)
- [삽입과 제거](#삽입과-제거)

# 개요
- 문자열 다루는 것은 어렵지만 현업에서 가장 중요하다.
- 코딩테스트를 넘어서 개발자라면 잘 다루어야 한다.
- 코딩테스트에서 문자열에 취약하다.
- [Reference](https://luv-n-interest.tistory.com/1166?category=973485)

# 문자열 슬라이싱
- 이전에는 문자열 문제를 자주 풀지 않아서 push_back()이나 str[idx]를 이용해서 복잡하게 슬라이싱을 했다.
- **substr**을 이용한다.
- 문자열의 일부를 리턴하고 이는 string type이 된다.
- 문자열의 일부를 이용하는 것은 문자열의 기초다.
~~~c++
int main()
{
    string str = "12345";
    string tmp = str.substr(3,2); //3번 인덱스 부터 2개의 문자를 슬라이싱하여 문자열로 반환한다.

    cout << tmp; //45를 출력하고 이는 문자다.    
}
~~~

# 문자열 <-> 숫자
- ![image](https://user-images.githubusercontent.com/55792986/191493039-909485d0-9142-447f-b5e1-aa8757d8ffee.png)
- 숫자를 문자열로 바꾸는 것은 to_string(숫자)

# 특정 문자로 문자열 구분하기
- 많은 라이브러리가 있지만 경험상 까먹을 확률이 매우 높다.
- 안전하게 벡터를 이용해서 구분하자.
~~~c++
int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    string str = "Hello My name is John";
    vector<string> v;
    string tmp = "";

    //공백을 찾아서 구분하고 vector에 저장한다.
    for(auto i : str)
    {
        if(i == ' ') //이걸 커스텀 하면 다양한 문자로 구분 할 수 있게 된다.
        {
            v.push_back(tmp);
            tmp = "";
        }
        else
        {
            tmp.push_back(i);
        }
    }

    //마지막에 저장하지 않은 문자열까지 챙겨준다.
    if(tmp.length() != 0)
    {
        v.push_back(tmp);
    }

    for(auto i : v)
    {
        cout << i << "\n";
    }
    /*
    Hello
    My
    name
    is  
    John
    */

   return 0;
}
~~~

# push_back(인자)
- push_back(인자)에서 인자는 char형만 가능하다.

# 삽입과 제거
~~~c++
string sstr = "abcde";
sstr.insert(3, "ffff"); // index 3에 문자열을 추가한다.
cout << sstr << "\n"; // 출력값 : abcffffde / 설명 : ffff가 추가된다.  

sstr.erase(4,1); // 두 번째 인자는 1개를 지운다를 의미한다.
cout << sstr << "\n"; // 출력값 : abcfffde / 설명 : index_4의 f가 사라진다. 
~~~

