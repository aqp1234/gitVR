from gtts import gTTS
import pygame
from io import BytesIO
import sys

text = sys.argv[1]

def say(text):
    tts = gTTS(text=text, lang='en')
    fp = BytesIO()      #음성객체를 넣기 위한 바이트스트림 생성
    tts.write_to_fp(fp)     #바이트 스트림에 음성객체 주입
    fp.seek(0)    # 음성의 처음부분부터 시작을 위해 파일 포인터 위치 지정
    pygame.mixer.init()     # mixer 초기화
    pygame.mixer.music.load(fp)     # 원하는 음성데이터가 들어있는 바이트스트림을 메모리에 로드
    pygame.mixer.music.play()       # 메모리에 로드되어 있는 소리를 재생
    while pygame.mixer.music.get_busy():        #음악이 실행되고 있는지 확인
        pygame.time.Clock().tick(10)        #초당 프레임을 맞추기 위한 딜레이 설정
#say("Hello ProWriter! Thanks for being part of our community. Your account details are as follows:")
say(text)

# import os

# fh = open("test.txt", "r")
# myText = fh.read().replace("\n", " ")
# #myText = "안녕하세요 파이썬 음성합성입니다"

# language = 'ko'

# output = gTTS(text=myText, lang=language, slow=False)

# output.save("output.mp3")
# fh.close()
# os.system("output.mp3")
