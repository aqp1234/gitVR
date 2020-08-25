from PyDictionary import PyDictionary
import sys

dictionary=PyDictionary()

text = sys.argv[1]

data = dictionary.meaning(text)
print(text + ": ")
if 'Noun' in data:
    print('Noun')
    print(str(data['Noun']))
if 'Verb' in data:
    print('Verb')
    print(str(data['Verb']))
if 'Adjective' in data:
    print('Adjective')
    print(str(data['Adjective']))
if 'Adverb' in data:
    print('Adverb')
    print(str(data['Adverb']))

# if 'Noun' in data:
#     noun = data['Noun'].sp
#     for i in range(0, len(data['Noun'])):
#         data['Noun'][i]
