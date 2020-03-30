import re

# email gasit
f = open("Text.txt","r")
ceva = f.read()

email = re.findall('\S+@\S+', ceva)
print(email[0])

# adresa https gasita
urls = re.findall('http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\(\), ]|(?:%[0-9a-fA-F][0-9a-fA-F]))+', ceva)
print(urls[0])
