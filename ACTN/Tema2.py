from random import randrange





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

# function implementing Chinese remainder theorem 
# list m contains all the modulii 
# list x contains the remainders of the equations 
def crt(m, x): 

	# We run this loop while the list of 
	# remainders has length greater than 1 
	while True: 
		
		# temp1 will contain the new value 
		# of A. which is calculated according 
		# to the equation m1' * m1 * x0 + m0' 
		# * m0 * x1 
		temp1 = modinv(m[1],m[0]) * x[0] * m[1] + modinv(m[0],m[1]) * x[1] * m[0] 

		# temp2 contains the value of the modulus 
		# in the new equation, which will be the 
		# product of the modulii of the two 
		# equations that we are combining 
		temp2 = m[0] * m[1] 

		# we then remove the first two elements 
		# from the list of remainders, and replace 
		# it with the remainder value, which will 
		# be temp1 % temp2 
		x.remove(x[0]) 
		x.remove(x[0]) 
		x = [temp1 % temp2] + x 

		# we then remove the first two values from 
		# the list of modulii as we no longer require 
		# them and simply replace them with the new 
		# modulii that we calculated 
		m.remove(m[0]) 
		m.remove(m[0]) 
		m = [temp2] + m 

		# once the list has only one element left, 
		# we can break as it will only contain 
		# the value of our final remainder 
		if len(x) == 1: 
			break

	# returns the remainder of the final equation 
	return x[0] 

def CMMDC(a,b):
	print(a)
	while (a!=b):
		if a>b:
			a-=b             #a=a-b
		else:
			b-=a;            #b=b-a
	return a;



def tema3_punctul_a():
	p = 3
	q = 5
	r = 7
	n = p * q * r

	phi = (p-1)*(q-1)*(r-1)
	ok = 0
	while ok != 1:
		e = randrange(phi + 1)	
		if CMMDC(e,phi) == 1:
			ok = 1

			
	ok = 0
	d = 2
	while ok != 1:
		if ((d * e) % phi) == 1:
			ok = 1
		else:
			d += 1




	# d = 29
	x = randrange(n)

	print("Textul initial este ",x)
	m = []
	m.append(p)
	m.append(q)
	m.append(r)

	y = (x**e) % n
	print("Y initial este : ", y)
	# y = (y**d) % n
	# print("dec(y) este : ", y)

	x_p = (y % p) ** (d % (p - 1)) % p
	print("x_p este : ", x_p)

	x_q = (y % q) ** (d % (q - 1)) % q
	print("x_q este : ", x_q)

	x_r = (y % r) ** (d % (r - 1)) % r
	print("x_r este : ", x_r)

	x1 = []
	x1.append(x_p)
	x1.append(x_q)
	x1.append(x_r)

	return crt(m,x1)

print(tema3_punctul_a())
