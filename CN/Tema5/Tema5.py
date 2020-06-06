import numpy as np
import random
import math

from scipy.sparse import random
from scipy import stats

from numpy.linalg import inv
import time
class CustomRandomState(np.random.RandomState):
    def randint(self, k):
        i = np.random.randint(k)
        return i - i % 2
np.random.seed(12345)
rs = CustomRandomState()
rvs = stats.poisson(25, loc=10).rvs
S = random(5, 5, density=0.55, random_state=rs, data_rvs=rvs)
A = S.A

for i in range(len(A)):
    for j in range(len(A[0])):
        if i > j:
            A[i][j] = 0

for i in range(len(A)):
    for j in range(len(A[0])):
        if i > j:
            A[i][j] = A[j][i]

# print(A)



# with open('a_500.txt', 'r') as f:
#         A = [[float(num) for num in line.split(',')] for line in f]
# A = np.array(A)





# m = 5
# p = 5
def numar_absolut_maxim(p):
    maxim = 0
    for i in range(len(A[0])):
        if abs(p[i]) > maxim:
            maxim = abs(p[i])
    return maxim

import random
def metoda_puterii(A):
    x = []
    maxim = A.max()
    copie = len(A[0])
    while copie != 0:
        x.append(random.randrange(0,int(maxim)))
        copie = copie - 1

    p = A.dot(x)

    n = numar_absolut_maxim(p)

    n = 1/n

    x1 = n*p

    # print(x1)

    for i in range(len(A[0])):
        x[i] = x1[i]

    p = A.dot(x)

    n = numar_absolut_maxim(p)

    n = 1/n

    x1 = n*p

    # print(x1)
    iteratii = 10000
    while iteratii != 0:
        norma = 0
        x_test = []
        x_test = np.subtract(x1,x)
        for i in range(len(A[0])):
            norma += x_test[i] ** 2
        
        # print(math.sqrt(norma))
        if math.sqrt(norma) < 10 ** (-10):
            print("n-ul este :", n)
            print("Vectorul este :", x1)
            exit()
        
        for i in range(len(A[0])):
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
        

        
# metoda_puterii(A)



def parte3():

    with open('test.txt', 'r') as f:
        A_secundar = [[int(num) for num in line.split(',')] for line in f]

    
    # print(np.array(l))
    A_secundar = np.array(A_secundar)
    # print(len(l[0]))
    # print(l)
    print(A_secundar)
    p = len(A_secundar)
    m = len(A_secundar[0])






    u, s, vh = np.linalg.svd(A_secundar, full_matrices=True)
    print("u este :\n", u)
    print("s este :", s)
    print("v este :\n", vh)
    # ==========================================================
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

    # result = vh.dot(s)
    # result = result.dot(np.transpose(u))
    # nr = 0
    # for i in s:
    #     nr += 1

    if p > m:
        count = 0
        s_copy = []
        while count != m:
            vector = [0] * m
            vector[count] = s[count]
            s_copy.append(vector)
            count += 1


        while count != p:
            for i in s_copy:
                i.append(0)
            count += 1
        s = np.array(s_copy)
        # print(s)
    else:
        count = 0
        s_copy = []
        while count != p:
            vector = [0] * p
            vector[count] = s[count]
            s_copy.append(vector)
            count += 1


        while count != m:
            vector = [0] * p
            s_copy.append(vector)
            count += 1
        s = np.array(s_copy)
        # print(s)


    A_i = (vh.T).dot(np.linalg.inv(s))
    A_i = A_i.dot(u.T)
    print("prima pseudoinversa Moore-Penrose a matricei A este :\n", A_i)

    s_i = np.array(s)

    # print(s_i)

    if p > m:
        for i in range(m):
            s_i[i][i] = 1 / s[i][i]
    else:
        for i in range(p):
            s_i[i][i] = 1 / s[i][i]

    A_i_2 = vh.dot(s_i)
    A_i_2 = A_i_2.dot(np.transpose(u))
    print("a doua pseudoinversa Moore-Penrose a matricei A este :\n", A_i)


    b = [7, 19, 11]
    # x_i = A_i.dot(b)
    x_i = np.matmul(A_i,b)

    print("x_i este : ", x_i)


    # b = [3]*p
    x_i_2 = A_i_2.dot(b)

    print("x_i_2 este : ", x_i)
    # print(A_secundar)
    # print(np.transpose(A_secundar))

    # A_inmultit_cu_x = A_secundar.dot(x_i)
    A_inmultit_cu_x = np.matmul(A_secundar,x_i)
    result = np.subtract(b,A_inmultit_cu_x)


    norma1 = 0
    for i in range(p):
        norma1 += result[i] ** 2

    print("Prima norma este : ", math.sqrt(norma1))




    A_j = inv(np.transpose(A_secundar).dot(A_secundar)).dot(np.transpose(A_secundar))

    print(" pseudo-invers˘a ın sensul celor mai mici patrate: \n", A_j)


    A_final = np.subtract(A_i , A_j)
    sum = 0
    for i in range(len(A_final)):
        for j in range(len(A_final[0])):
            sum += A_final[i][j]
    print("Norma este : ", sum)

# parte3()


def partea1():
    # with open('a_500.txt', 'r') as f:
    #     A = [[float(num) for num in line.split(',')] for line in f]
    
    # A = np.array(A)
    print(A)
    matrice = []
    linie = []
    tupla = ()
    for i in range(len(A)):
        for j in range(len(A[0])):
            if A[i][j] != 0:
                tupla = tupla + (A[i][j],)
                tupla = tupla + (j,)
                # print(tupla)
                linie.append(tupla)
                tupla = tuple()
        if linie:
            matrice.append(linie)
        linie = []
    print(matrice)

# partea1()















import PySimpleGUI as sg

def func(message):
    print(message)

layout = [[sg.Button('1'), sg.Button('2'),sg.Button('3'), sg.Exit()] ]

window = sg.Window('ORIGINAL').Layout(layout)

while True:             # Event Loop
    event, values = window.Read()
    if event in (None, 'Exit'):
        break
    if event == '1':
        metoda_puterii(A)
    elif event == '2':
        parte3()
    elif event == '3':
        partea1()
window.Close()