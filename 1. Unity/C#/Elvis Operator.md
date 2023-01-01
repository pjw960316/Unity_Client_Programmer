# 맨날 기억 잘 못해서 따로 뺐다.

# Elvis Operator (= '?' = 널 조건부 연산자)
~~~C#
string s1 = "hello"; //stack에 메모리를 저장하는 s1, heap에 실제 데이터인 hello를 저장.
string s2 = null; //stack에 메모리를 저장하는 s2, 하지만 heap에는 실제 데이터가 없다. 즉 참조하고 있지 않다.
int n1 = 10; // stack에 n1 변수를 만들고 그에 10이라는 값을 저장
Nullable<int> n2 = null; //nullable은 int와 bool을 모두 저장해서 stack에 n2 변수를 만들고, 이 것이 값이 없음을 표현한다.
int? n3 = null; //Nullable<int> n3 = null과 완벽히 동일한 코드다.
~~~
  - 코드에서 '?' 키워드를 많이 보았다.