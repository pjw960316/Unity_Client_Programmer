# 목차
- [목차](#목차)
- [개요](#개요)
- [예외처리 방식\_1 : If문](#예외처리-방식_1--if문)
- [예외처리 방식\_2 : Out Parameter 활용](#예외처리-방식_2--out-parameter-활용)
- [예외처리 방식\_3 : try-catch-finally](#예외처리-방식_3--try-catch-finally)
    - [1. 키워드](#1-키워드)
    - [2. 장점](#2-장점)
    - [3. 정리](#3-정리)
- [try-catch 연습](#try-catch-연습)
- [try-catch 추가 내용](#try-catch-추가-내용)
- [try-catch 예외처리의 비용과 블록 스코프](#try-catch-예외처리의-비용과-블록-스코프)

<br/><br/><br/>

# 개요
- 입사 초기에 인강 듣고 정리한 걸로 기억한다.

<br/><br/><br/>

# 예외처리 방식_1 : If문 
- 결론부터 말하면 좋지 않은 방식이다.
- ![image](https://user-images.githubusercontent.com/55792986/207993589-d98a8e4f-3f23-464e-8b67-bd53ef1f4416.png)
  - 코드 -> 오류 처리 -> 코드 -> 오류 처리 
    - 코드가 너무 더러워진다.
    - 코드의 가독성이 매우 떨어진다.
  - return 값으로 -1을 적어주면 이 것이 오류인지 실제 반환 값을 의미하는 것인지 구분이 애매하다.
    - 다시 말해, 오류에 대한 자세한 정보를 알 수 없다.

<br/><br/><br/>

# 예외처리 방식_2 : Out Parameter 활용 
- 리턴 값과 오류를 분리한다.
- 리턴 값은 메서드의 성공과 실패만 나타내고 out parameter로 연산의 결과를 이용합니다.  
  - 예시 ![20221216_093212](https://user-images.githubusercontent.com/55792986/207995059-a9d0aeff-2a8c-4f13-a47d-10620605db61.png)
- ![image](https://user-images.githubusercontent.com/55792986/207995132-466c5e4d-99c0-42c4-981b-d3f8daa80114.png)
  - :star: 내 생각 : 협업에서 예외를 반드시 처리하라는 강제성은 매우 중요할 것 이다.

<br/><br/><br/>

# 예외처리 방식_3 : try-catch-finally
### 1. 키워드
- try 
  - 예외가 존재하는 지 검사하는 블록.
- catch
  - 예외가 발생하면 해당 예외를 처리해주는 키워드 (=예외 처리 핸들러)
    - try 블록 안에서 예외가 발생하면 catch 블록이 실행된다.
  - 매개변수가 있다.
- finally
  - Exception의 발생 여부와 상관 없이 마지막에 반드시 실행되는 블럭이다.
    - try 블록을 벗어나면 항상 실행된다.
    - try에서 return 문을 사용해도 finally는 호출 된다.
  - 파일과 네트워크 관련 객체는 gc가 처리해주지 않으므로 dispose를 해주어야 한다. 이 때 finally를 이용해서 dispose를 하면 항상 실행되므로 이용하기 좋다.
- 순서
  - catch에서 잡히면 catch 부터 실행 되고 finally가 실행 된다.

### 2. 장점
- 오류에 대한 정보를 자세하게 나타낼 수 있다.
  - 클래스 타입이므로 예외에 대한 많은 정보를 나타낼 수 있다.
- :star:**개발자는 에러를 발생시키는 위치와 에러를 처리하는 부분을 여러 수준에서 분리하여 개발할 수 있다.**
  - try-catch 방식은 콜 스택을 통해서 적절한 catch 문이 구성된 위치까지 콜스택을 통해 전파된다. 

### 3. 정리
- ![20221216_094000](https://user-images.githubusercontent.com/55792986/207995915-7776cb17-ad3f-43e5-a27e-bff969769ad5.png)

<br/><br/><br/>

# try-catch 연습 
~~~c#
            try
            {
                int a = 3;
                int b = 0;
                int c = a / b;
            }
            catch (Exception e)
            {
                Console.WriteLine("에러다!" + e);
                throw;
            }
~~~
  - 콘솔에서 divide zero 예외 처리를 진행한다.

<br/><br/><br/>

# try-catch 추가 내용
- System.Exception 클래스를 열심히 봐라.
- Exception 클래스는 최상위 부모 클래스이므로 모든 예외를 잡는다.
  - 그 결과 아래와 같은 문제가 발생한다.
  - ![image](https://user-images.githubusercontent.com/55792986/207998040-9e0f7578-c42c-4973-9921-21339b627e90.png)
    - 개발자는 WebException에서 예외를 처리하고 싶음에도 불구하고 Exception에서 처리할 것 이다.
    - 이를 해결하기 위해 C#에서는 Exception은 항상 마지막 catch 구문에 적도록 강제했다.
- 예시
~~~c#
        public string DownloadString(Uri address)
        {
            ThrowIfNull(address, nameof(address));

            StartOperation();
            try
            {
                WebRequest request;
                byte[] data = DownloadDataInternal(address, out request);
                return GetStringUsingEncoding(request, data);
            }
            finally
            {
                EndOperation();
            }
        }
~~~
  - Webclient 클래스의 메서드 DownloadString에 대한 예외처리다. 
    - 나는 String으로 매개변수를 받았지만 Uri로 또 변환하는 메서드 처리가 있으므로 위의 코드를 보는 게 맞다.
      - 이 말이 이해가 안 되면 그냥 당시에 검증 했으니 넘어가면 된다.
  - 라이브러리의 메서드에서 내부적으로 예외처리를 해주고 있다.
    - 그로 인해 address에 'http://www.naver.com'대신 'http://www.never.com'을 적어주면 예외를 받고 처리하게 되는 것 이다.

<br/><br/><br/>

# try-catch 예외처리의 비용과 블록 스코프
- 예외를 처리하는 작업은 일반적인 메서드 호출보다 훨씬 더 시간이 많이 걸린다.
- [Real Test](https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=hermet&logNo=104819461)
  - Try-Catch가 좋은 구문이지만 if로 예외처리 하는 것 보다 비용이 많이 든다.