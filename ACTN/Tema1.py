import numpy as np
import random
from random import randrange
import time
import math

def Coding(Message,base):
    k = len(str(Message)) + 1
    n = k + 2
    stop = 0
    rests = []
    y = []
    y1 = []
    while stop != 1:
        rests.append(Message % base)
        Message = Message // base
        if Message == 0:
            stop = 1
            
    rests.reverse()
    rests.append(0)
    Polinom = np.poly1d(rests)
    # print(rests)
    copie = 1
    index = 0
    
    while copie <= n:
        ceva = 0
        putere = len(rests) - 1
        for i in rests:
            ceva += i * copie ** putere 
            putere -= 1
        y1.append(ceva % base)
        copie += 1
    # print(y1)
    return y1

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

def suma(A,B):
    size=max(len(A),len(B)) 
    sum=[0 for i in range(size)] 
    for i in range(0, len(A)): 
        sum[i]=A[i]
    for i in range(len(B)):
        sum[i]+=B[i]
    return sum


def inmultire(A,B):
    prod=[0]*(len(A)+len(B)-1)
    for i in range(len(A)):
        for j in range(len(B)): 
            prod[i+j]+=A[i]*B[j]
    return prod


def produs(subsetA, i, base,k,y_copy):
    unProdusDeAdunat=[1]
    polinomIntermediar=[]
    for j in subsetA:
        polinomIntermediar=[1,-1*j]
        if j!=i:
            for index in range(0,len(polinomIntermediar)):
                polinomIntermediar[index]*=inverse(i-j,base)
                polinomIntermediar[index]%=base
            unProdusDeAdunat=inmultire(unProdusDeAdunat,polinomIntermediar)
        for index in range(0,len(unProdusDeAdunat)):
            unProdusDeAdunat[index]%=base
    for index in range(0,len(unProdusDeAdunat)):
        unProdusDeAdunat[index]*= y_copy[i - 1]    
    return unProdusDeAdunat


def Decoding(Message, base):
    firstSum = 0
    A = []
    subsetA = []
    y_copy = Coding(Message, base)
    y = Coding(Message, base)
    random_err = randrange(len(str(Message)) + 3)
    y_copy[random_err] = -1
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
    subsetA = random.sample(A,len(str(Message)) + 1)

    # print(subsetA)
    firstSum = 0
    for i in range(len(subsetA)):
        firstSum += y_copy[subsetA[i] - 1] * mul(subsetA,i,base)
        # print(y_copy[subsetA[i] - 1])
    FC = firstSum % base

    # print(FC)
    CoeficientiPolinomFinal = []

    if FC == 0:
        CoeficientiPolinomFinal=[]
        for i in subsetA:
            prod = produs(subsetA, i, base, len(str(Message)),y_copy)
            CoeficientiPolinomFinal=suma(CoeficientiPolinomFinal,prod)
            for id in range(0,len(CoeficientiPolinomFinal)):
                CoeficientiPolinomFinal[id]%=base
        # print("polynom final: ",np.poly1d(CoeficientiPolinomFinal))
        # print(CoeficientiPolinomFinal)
        print()
        rezFinal = 0
        putere = len(CoeficientiPolinomFinal) - 1
        for i in CoeficientiPolinomFinal:
            rezFinal += i * base ** (putere - 1)
            putere -= 1
        print(rezFinal)
        rezFinal = int(rezFinal)
        recoveredbytes = rezFinal.to_bytes((rezFinal.bit_length() + 7) // 8, 'little')
        recoveredstring = recoveredbytes.decode('utf-8')
        print(recoveredstring)
    else:
        print("FC nu este 0")



    # for x in range(len(subsetA)):
    #     ceva =( y_copy[subsetA[x] - 1] * mul2(x + 1, subsetA, x, base))%base
    #     print(ceva)
        # secondParameters.append()

def codare(text):
    mystring = text
    mybytes = mystring.encode('utf-8')
    myint = int.from_bytes(mybytes, 'little')
    return myint

def start_program():
    message = open("test.txt", "r")
    message = message.read()
    print(message)
    int_message = codare(message)
    Decoding(int_message,13)

start_program()

# print(Decoding(2842415256790796333592662320610113588895060394619331222997620021381555017594467875230387625445938312933261213861150457773561368308496308121383865611792568619036557195001589705870711954828462736208475491341659268485512362067329474351168909132163514921,13))





# def main():
#     count = 1635149155 // 10
    
#     while True:
#         isprime = True
        
#         for x in range(2, int(math.sqrt(count) + 1)):
            
#             if count % x == 0: 
#                 isprime = False
#                 break
        
#         if isprime:
#             print (count)
#             break
#         # print(count)
#         count += 1

# main()