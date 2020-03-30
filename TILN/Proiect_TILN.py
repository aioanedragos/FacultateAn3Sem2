import re

# email gasit
f = open("Text.txt","r")
ceva = f.read()

email = re.findall('\S+@\S+', ceva)
print(email[0])