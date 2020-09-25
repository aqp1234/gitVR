import os
import sys
import urllib.request
import json
import pprint

client_id = "_QgHbIDCNwOBPDUaqpZL" # 개발자센터에서 발급받은 Client ID 값
client_secret = "1RNaIu6EHg" # 개발자센터에서 발급받은 Client Secret 값

url = "https://openapi.naver.com/v1/papago/n2mt"

#번역할 언어와 내용에 대해
text = sys.argv[1]
encText = urllib.parse.quote(text)
data = "source=ko&target=en&text=" + encText

# 웹 요청
request = urllib.request.Request(url)
request.add_header("X-Naver-Client-Id",client_id)
request.add_header("X-Naver-Client-Secret",client_secret)

#결과 받아오는 부분
response = urllib.request.urlopen(request, data=data.encode("utf-8"))
rescode = response.getcode()
if(rescode==200):
    response_body = response.read()
    data = response_body.decode("utf-8")
    data = json.loads(data) #딕셔너리화
    #pprint.pprint(data)

    trans_text = data['message']['result']['translatedText']
    print(trans_text)
else:
    print("Error Code:" + rescode)