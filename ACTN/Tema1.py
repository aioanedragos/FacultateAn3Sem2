import numpy as np
from random import randrange
import time

def Coding(Message,base):
    k = len(str(Message)) + 1
    n = k + 2
    stop = 0
    rests = []
    y = []
    while stop != 1:
        rests.append(Message % base)
        Message = Message // base
        if Message == 0:
            stop = 1
            
    rests.reverse()
    rests.append(0)
    Polinom = np.poly1d(rests)
    for i in range(n):
        y.append(Polinom(i + 1) % base)

    return y

def inverse(a,p):
    return a**(p-2) % p

def mul(subsetA,i,base):
    prod = 1
    for j in subsetA:
        if j != subsetA[i]:
            prod *= j * inverse(j-subsetA[i],base)
    return prod
    

def mul2(x, subsetA, i, base):
    prod = 1
    for q in subsetA:
        if q != subsetA[i]:
            prod *= ((x - q + base) % base) * inverse(((subsetA[i] - q + base) % base), base)
    return prod

def Decoding(Message, base):
    firstSum = 0
    A = []
    subsetA = []
    y_copy = Coding(Message, base)
    y = Coding(Message, base)
    random_err = randrange(5)
    y_copy[random_err] = 2
    # print(y_copy)

    mistake = 0
    while mistake < len(y_copy):
        if y[mistake] != y_copy[mistake]:
            break
        mistake += 1
    for i in range(len(y_copy)):
        if i != mistake:
            A.append(i + 1)

    # print(A)
    subsetA = [A[0],A[1],A[2]]

    # print(subsetA)
    firstSum = 0
    for i in range(len(subsetA)):
        firstSum += y_copy[subsetA[i] - 1] * mul(subsetA,i,base)
        # print(y_copy[subsetA[i] - 1])
    res = firstSum % 11

    # print(res)
    secondParameters = []

    for x in range(len(subsetA)):
        ceva =( y_copy[subsetA[x] - 1] * mul2(x + 1, subsetA, x, base))%base
        print(ceva)
        # secondParameters.append()


print(Decoding(29,11))

