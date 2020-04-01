import re

# 3.email gasit
f = open("Text.txt","r",encoding='utf-8')
ceva = f.read()

email = re.findall('\S+@\S+', ceva)
print(email[0])

# 2.adresa https gasita
urls = re.findall('http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\(\), ]|(?:%[0-9a-fA-F][0-9a-fA-F]))+', ceva)
print(urls[0])

# 6. echipe sau nu
# echipe = re.findall('^[0-9].[0-9]. [(Participants and procedure)|(Participants)]*[\n]*[\w =\n()!@#$%^&*()-_=]+\\n$',ceva)
# regex = r"^[0-9].[0-9].[ ]*[(Participants and procedure)|(Participants)]*[\n]*[\w =\n()!@#$%^&*()-_=]+\n$"
regex = r"[0-9]\.[0-9]\.[ ]*(Participants and procedure|Participants)(.*?)[0-9]\.[0-9]\."

matches = re.finditer(regex, ceva, re.DOTALL)
sinonime = ['cluster', 'grouping', 'gang', 'category', 'band', 'pack', 'bunch', 'clump', 'class', 'set', 'aggregation', 'panel', 'collection', 'formation', 'party', 'pair', 'husband and wife', 'twosome', 'two', 'match', 'squad', 'crew', 'gang', 'group', 'side']


ceva1 = []
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())
nr = 0   
# print(ceva1)
for i in sinonime:
    if ceva1[0].find(i) > 0:
        nr+= ceva1[0].find(i)

if nr > 2:
    print("pe echipe")
else:
    print("individua")

# 7numar participanti
 
regex = r"[0-9]\.[0-9]\.[ ]*(Participants and procedure|Participants)(.*?)[0-9]\.[0-9]\."
matches = re.finditer(regex, ceva, re.DOTALL)

ceva1 = []  
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())

result = [int(s) for s in ceva1[0].split() if s.isdigit()]
print(result[0])

# 8Media varstei

# re = '/((median)|(mean)) [a-zA-Z ]*=?[ 0-9]+[.0-9]*/m';
regex = r"((median)|(mean))[ ]*[a-zA-Z ]*=?[ 0-9]+[.0-9]*"

matches = re.finditer(regex, ceva, re.MULTILINE)


ceva1 = []  
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())

result = ceva1[0].split()

print(result[len(result) - 1])

# 9ceva SD chestie

regex = r"SD[ ]*=[ ]*[1-9]+.?[0-9]*"

matches = re.finditer(regex, ceva, re.MULTILINE)


ceva1 = []  
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())

result = ceva1[0].split()

print(result[len(result) - 1])
# 10 studii
# regex = r"^[0-9].[0-9].[ ]*[(Participants and procedure)|(Participants)]*[\n]*[\w =\n()!@#$%^&*()-_=]+\n$"
regex = r"[0-9]\.[0-9]\.[ ]*(Participants and procedure|Participants)(.*?)[0-9]\.[0-9]\."

matches = re.finditer(regex, ceva, re.DOTALL)
sinonime = ['high school', 'University']


ceva1 = []
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())
nr = 0
education = "" 
for i in sinonime:
    if ceva1[0].find(i) > 0:
        nr += 1
        education = i

if nr == 0:
    print("Not specified")
elif nr == 1:
    print(education)
else:
    print("Mixted")

# 11male sau female
# regex = r"^[0-9].[0-9].[ ]*[(Participants and procedure)|(Participants)]*[\n]*[\w =\n()!@#$%^&*()-_=]+\n$"
regex = r"[0-9]\.[0-9]\.[ ]*(Participants and procedure|Participants)(.*?)[0-9]\.[0-9]\."

matches = re.finditer(regex, ceva, re.DOTALL)
gender = ['male','female']


ceva1 = []
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())
male = 0
female = 0   
male = ceva1[0].find('male')
female = ceva1[0].find('female')

if male > 0 and (female == 0 or female < 0):
    print("male")
elif female > 0 and (male == 0 or male < 0):
    print("female")
else: 
    print("Mixted")

# 12 nationalitate
# regex = r"^[0-9].[0-9].[ ]*[(Participants and procedure)|(Participants)]*[\n]*[\w =\n()!@#$%^&*()-_=]+\n$"
regex = r"[0-9]\.[0-9]\.[ ]*(Participants and procedure|Participants)(.*?)[0-9]\.[0-9]\."


matches = re.finditer(regex, ceva, re.DOTALL)
sinonime = ['Asian', 'Afghan', 'Albanian', 'Algerian', 'American', 'Andorran', 'Angolan', 'Anguillan', 'Citizen of Antigua and Barbuda', 'Argentine', 'Armenian', 'Australian', 'Austrian', 'Azerbaijani']


ceva1 = []
for matchNum, match in enumerate(matches, start=1):
    ceva1.append(match.group())
nr = 0
nationality = "" 
for i in sinonime:
    if ceva1[0].find(i) > 0:
        nr += 1
        nationality = i

if nr == 0:
    print("Not specified")
elif nr == 1:
    print(nationality)
else:
    print("Mixted")
