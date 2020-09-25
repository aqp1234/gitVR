import urllib3
import json
import base64
import pyaudio
import wave
from ast import literal_eval


openApiURL = "http://aiopen.etri.re.kr:8000/WiseASR/Recognition"
accessKey = "9b1c2929-a23e-4c7c-832e-50602959b318"
audioFilePath = "output.wav"
languageCode = "english"

CHUNK = 1024
FORMAT = pyaudio.paInt16
CHANNELS = 1
RATE = 16000
RECORD_SECONDS = 5

p = pyaudio.PyAudio()

stream = p.open(format=FORMAT,
                channels=CHANNELS,
                rate=RATE,
                input=True,
                frames_per_buffer=CHUNK)

#print("* recording")

frames = []

for i in range(0, int(RATE / CHUNK * RECORD_SECONDS)):
    data = stream.read(CHUNK)
    frames.append(data)

#print("* done recording")

stream.stop_stream()
stream.close()
p.terminate()

wf = wave.open(audioFilePath, 'wb')
wf.setnchannels(CHANNELS)
wf.setsampwidth(p.get_sample_size(FORMAT))
wf.setframerate(RATE)
wf.writeframes(b''.join(frames))
wf.close()


 
file = open(audioFilePath, "rb")
audioContents = base64.b64encode(file.read()).decode("utf8")
file.close()
 
requestJson = {
    "access_key": accessKey,
    "argument": {
        "language_code": languageCode,
        "audio": audioContents
    }
}
 
http = urllib3.PoolManager()
response = http.request(
    "POST",
    openApiURL,
    headers={"Content-Type": "application/json; charset=UTF-8"},
    body=json.dumps(requestJson)
)
 
#print("[responseCode] " + str(response.status))

str_response = str(response.data,"utf-8")
dictionary = literal_eval(str_response)

print(dictionary["return_object"]["recognized"])

#print(str(response.data,"utf-8"))
