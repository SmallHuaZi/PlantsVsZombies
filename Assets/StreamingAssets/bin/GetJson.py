import json as js
import os

print(os.getcwd())

path = os.getcwd() + '/Assets/StreamingAssets/config/scene.json'

file = open(path, 'r')

strContent = file.read()

print(strContent)

deser = js.loads(strContent)
