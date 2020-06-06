# import random
# import math

# def modulo(base, exponent, mod):  
#     x = 1;  
#     y = base;  
#     while (exponent > 0):  
#         if (exponent % 2 == 1):  
#             x = (x * y) % mod;  
  
#         y = (y * y) % mod;  
#         exponent = exponent // 2;  
  
#     return x % mod;  



# def JACOBI_SYMBOL(a,n):
#     if a == 0:
#         return 0
#     if a == 1:
#         return 1
#     e = 0
#     a1 = a
#     while a1 % 2 == 0:
#         a1 = a1 // 2
#         e += 1
#     if e % 2 == 0:
#         s = 1
#     else:
#         if n % 8 == 1 or n % 8 == 7:
#             s = 1
#         if n % 8 == 3 or n % 8 == 5:
#             s = -1

#     if n % 4 == 3 and a1 % 4 == 3:
#         s = -s
#     n1 = n % a1

#     if a1 == 1:
#         return s
#     else:
#         return s * JACOBI_SYMBOL(n1,a1)
    

# # print(JACOBI_SYMBOL(1236,20003))

# def solovoyStrassen(p, iterations):  
  
#     if (p < 2):  
#         return False;  
#     if (p != 2 and p % 2 == 0):  
#         return False;  
  
#     for i in range(iterations): 
          
#         # Generate a random number a  
#         a = random.randrange(p - 1) + 1;  
#         jacobian = (p + JACOBI_SYMBOL(a, p)) % p;  
#         mod = modulo(a, (p - 1) / 2, p);  
  
#         if (jacobian == 0 or mod != jacobian):  
#             return False;  
  
#     return True; 




# def checkIfMErsenneNumber(number):
#     number1 = number + 1
#     power = 0
#     ans = 0
#     i = 0
#     while 1:
#         power = int(math.pow(2,i))
#         if power > number1:
#             break
#         elif power == number1:
#             ans = 1
#         i += 1
#     if ans == 0:
#         return False
#     else:
#         return i - 1



# def isPrime(p): 

#     checkNumber = 2 ** p - 1
 
#     nextval = 4 % checkNumber 
 
#     for i in range(1, p - 1): 
#         nextval = (nextval * nextval - 2) % checkNumber 

#     if (nextval == 0): return True
#     else: return False

# # p = 7
# p = checkIfMErsenneNumber(127)
# checkNumber = 2 ** p - 1
  
# if isPrime(p): 
#     print(checkNumber, 'is Prime.') 
# else: 
#     print(checkNumber, 'is not Prime') 


# # print(solovoyStrassen(113,50))

import macpath

print(pow(13,15,17))
# print(pow(16,15,17))