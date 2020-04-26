import time
import numbers as number
from random import randrange
import random

n1_length = 12
n_length = 12
from functools import reduce


def CMMDC(a,b):
	print(a)
	while (a!=b):
		if a>b:
			a-=b             #a=a-b
		else:
			b-=a;            #b=b-a
	return a;



def chinese_remainder(n, a):
    sum = 0
    prod = reduce(lambda a, b: a*b, n)
    for n_i, a_i in zip(n, a):
        p = prod // n_i
        sum += a_i * mul_inv(p, n_i) * p
    return sum % prod
 
 
 
def mul_inv(a, b):
    b0 = b
    x0, x1 = 0, 1
    if b == 1: return 1
    while a > 1:
        q = a // b
        a, b = b, a%b
        x0, x1 = x1 - q * x0, x0
    if x1 < 0: x1 += b0
    return x1



def extended_euclidean(a, b): 
	if a == 0: 
		return (b, 0, 1) 
	else: 
		g, y, x = extended_euclidean(b % a, a) 
		return (g, x - (b // a) * y, y) 

# modular inverse driver function 
def modinv(a, m): 
	g, x, y = extended_euclidean(a, m) 
	return x % m 




p = 9606386123153647926437742548933834377929149420413686918158038681437670302090540073919586518905723118116980828458094443382291005132381230905605808294351467
q = 8071308789846342920274707015169510583872950277691392718850127160021566973859686760318020205339748103078430771656318717139469328739625256587763860685874641
print("p = ",p)
print("q = ",q)
p2 = p*p
print("P la patrat = ", p2)
v1 = []
v1.append(q)
v1.append(p2)
n = p * p * q
print("n = ",n)
phi = p * (p-1) * (q-1)
print("phi = ",phi)
# e = number.getPrime(n_length, os.urandom)
ok = 0
while ok != 1:
    e = randrange(phi + 1)	
    if CMMDC(e,phi) == 1:
        ok = 1
# e = number.getPrime(n_length, os.urandom)
d = modinv(e, phi)
print("e = ",e)
print("d = ",d)
x = random.randrange(n)
print("*******************************Numarul dat = ",x)
# y = (x ** e) % n
y = pow(x, e, n)
print("Codificare numar(x) = ", y)
######################################### Cu librarie
start_time3 = time.time()
rez_lib = pow(y,d,n)
print("Rezultatul cu libraria = ", rez_lib)
print("--- %s seconds ---" % (time.time() - start_time3))
time3 = time.time() - start_time3
#######################################################
# y_q = ((y % q) ** (d % (q-1))) % q
y_q = pow(y % q,d % (q - 1), q)
# y_0 = ((y % p) ** (d % (p-1))) % p
y_0 = pow(y % p,d % (p - 1), p)
yp2 = y % p2
# E = (yp2 - ((y_0 ** e) % p2)) % p2
E = (yp2 - pow(y_0,e,p2)) % p2
# toInverse = (((y_0 **(e-1)) % p) * e) % p
toInverse = (pow(y_0, e - 1, p) * e) % p
y_1 = (E // p * (modinv(toInverse,p))) % p
y_1_2 = y_0 + (y_1 * p)
print("y_q = ", y_q)
print("y_0 = ", y_0)
print("y_1 = ", y_1)
print("y_1_2 = ", y_1_2)
v2 = []
v2.append(y_q)
v2.append(y_1_2)
start_time4 = time.time()
dataf = chinese_remainder(v1,v2)
print("*******************************Numarul decodificat = ", dataf)
print("--- %s seconds ---" % (time.time() - start_time4))
time4 = time.time() - start_time4
#return dataf
# return time3, time4