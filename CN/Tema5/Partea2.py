import numpy as np
import random
import math

from scipy.sparse import random
from scipy import stats
class CustomRandomState(np.random.RandomState):
    def randint(self, k):
        i = np.random.randint(k)
        return i - i % 2
np.random.seed(12345)
rs = CustomRandomState()
rvs = stats.poisson(25, loc=10).rvs
S = random(5, 5, density=0.25, random_state=rs, data_rvs=rvs)
A = S.A










m = 5
p = 5
def numar_absolut_maxim(p):
    maxim = 0
    for i in range(m):
        if abs(p[i]) > maxim:
            maxim = abs(p[i])
    return maxim

import random
def metoda_puterii(A):
    x = []
    maxim = A.max()
    copie = 5
    while copie != 0:
        x.append(random.randrange(0,maxim))
        copie = copie - 1

    p = A.dot(x)

    n = numar_absolut_maxim(p)

    n = 1/n

    x1 = n*p

    print(x1)

    for i in range(m):
        x[i] = x1[i]

    p = A.dot(x)

    n = numar_absolut_maxim(p)

    n = 1/n

    x1 = n*p

    print(x1)
    iteratii = 10000
    while iteratii != 0:
        norma = 0
        x_test = []
        x_test = np.subtract(x1,x)
        for i in range(m):
            norma += x_test[i] ** 2
        
        print(math.sqrt(norma))
        if math.sqrt(norma) < 10 ** (-10):
            print("n-ul este :", n)
            print("Vectorul este :", x1)
            exit()
        
        for i in range(m):
            x[i] = x1[i]

        p = A.dot(x)

        n = numar_absolut_maxim(p)
        if n == 0:
            print("n-ul este :", n)
            print("Vectorul este :", x1)
            exit()
        n = 1/n

        x1 = n*p
        iteratii -= 1
    
    print("nu exista solutie")
        

        
# A = np.array([[2,1],[4,2]])
# metoda_puterii(A)


u, s, vh = np.linalg.svd(A, full_matrices=True)
print("u este :", u)
print("s este :", s)
print("v este :", vh)
nr = 0
for i in s:
    if i > 0:
        nr += 1
print("Rangul matricei A este :",nr)

maxim = 0
minim = 999999999999
for i in s:
    if i > maxim:
        maxim = i
    if i < minim and i > 0:
        minim = i

print("Numarul de conditionare al matricei A este :", maxim/minim)

result = vh.dot(s)
result = result.dot(np.transpose(u))
nr = 0
for i in s:
    nr += 1


count = 0
s_copy = []
while count != nr:
    vector = [0] * nr
    vector[count] = s[count]
    s_copy.append(vector)
    count += 1
s = np.array(s_copy)
print(s)
# print("pseudoinversa Moore-Penrose a matricei A este :", result)


