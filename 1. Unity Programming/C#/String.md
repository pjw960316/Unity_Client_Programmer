# String.Format
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/93353d92-9167-4ebd-adda-f5c9aa4d4519)
- ![20230921_175704](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/c0952a3e-cf67-4f10-872f-f12486715adf)
  - 첫 째 자리 일 때가 의미하는 것
  - > 네, 맞습니다. “0.0” 형식을 사용하면 소수점 첫째 자리까지만 표시됩니다. 따라서, 값이 0.11111이라면 0.1로 표시됩니다. 이는 반올림 없이 소수점 이하를 잘라내는 방식입니다. 만약 반올림을 원한다면, Math.Round() 함수를 사용할 수 있습니다. 예를 들어, Math.Round(_myStatValue, 1)은 _myStatValue를 소수점 첫째 자리까지 반올림합니다. 이렇게 하면 0.11111은 0.1로, 0.11611은 0.1로 표시됩니다.