
import Crypto
from Crypto.PublicKey import RSA
from Crypto import Random
from Crypto.Random import random
import socket
import base64
import sys
import json
import hashlib
from Crypto.Cipher import AES
from Crypto.Cipher import PKCS1_OAEP
import ast
import socket
from hashlib import sha512

BS = 16
def pad(s):
    return s + (BS - len(s) % BS) * chr(BS - len(s) % BS)
def unpad(s):
    return s[0:-s[-1]] 
    

class AESCipher:
    def __init__( self, key ):
        self.key = key

    def encrypt( self, raw ):
        raw = pad(raw)
        iv = Random.new().read( AES.block_size )
        cipher = AES.new( self.key, AES.MODE_CBC, iv )
        return base64.b64encode( iv + cipher.encrypt( raw.encode("utf8") ) )

        

    def decrypt( self, enc ):
        enc = base64.b64decode(enc)
        iv = enc[:16]
        cipher = AES.new(self.key, AES.MODE_CBC, iv )
        return unpad(cipher.decrypt( enc[16:] ))




HOST = '127.0.0.1'  # The server's hostname or IP address
PORT = 1234         # The port used by the server

soc = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
soc.bind((HOST, PORT))
soc.listen()
conn, addr = soc.accept()
print('Connected by', addr)


#Primire si decriptare cheie publica client si cheie aes decriptare
buff_size = conn.recv(3)
public_key_encrypted_customer = conn.recv(int(buff_size.decode()))
buff_size = conn.recv(3)
aes_key_encrypted_customer = conn.recv(int(buff_size.decode()))


with open('PrivKM', 'rb') as f:
    private_key=f.read()

private_key = RSA.importKey(private_key)

private_key1 = PKCS1_OAEP.new(private_key)

aes_key_customer = private_key1.decrypt(aes_key_encrypted_customer)

aes_chiper_customer = AESCipher(aes_key_customer)

public_key_customer = aes_chiper_customer.decrypt(public_key_encrypted_customer)

#==================================================================

#Corectarea cheii publice de la customer dupa primirea aceseia si pregatirea acesteia pentru utilizare

public_key_customer=str(public_key_customer)[4:-2]
public_key_customer=str(public_key_customer).replace("\\\\n",'\n')
public_key_customer=str(public_key_customer).encode()


public_key_customer = RSA.importKey(public_key_customer)
#=====================================================================================================

#Semnarea sesiunii======================================================
SessionID=Random.random.randint(100000000000,9999999999999)
SessionID = str(SessionID).encode()
hash = int.from_bytes(sha512(SessionID).digest(), byteorder='big')
SessionID_signed = pow(hash, private_key.d, private_key.n)



hash = int.from_bytes(sha512(SessionID).digest(), byteorder='big')
hashFromSignature = pow(SessionID_signed, private_key.e, private_key.n)
print("Semnatura Sesiunii realizata:", hash == hashFromSignature)
print("Semnatura Sesiunii esuata:", hash != hashFromSignature)
# print("Signature:", hex(SessionID_signed))
#=======================================================================

#Criptarea asimetrica pentru SessionID si semnatura acesteia

sha=hashlib.sha256()
sha.update(b"cheiameasimaisecreta")
aes_key=sha.digest()
aes_cipher = AESCipher(aes_key)

#Criptare cheie aes
public_key_customer1 = PKCS1_OAEP.new(public_key_customer)

aes_key_encryped = public_key_customer1.encrypt(aes_key)
#Criptarea semnaturii
SessionID_signed_encrypted = aes_cipher.encrypt(str(SessionID_signed))

#Criptarea Sessiosn ID

SessionID_encrypted = aes_cipher.encrypt(str(SessionID))


conn.send(str(len(aes_key_encryped)).encode())
conn.send(aes_key_encryped)
conn.send(str(len(SessionID_encrypted)).encode())
conn.send(SessionID_encrypted)
conn.send(str(len(SessionID_signed_encrypted)).encode())
conn.send(SessionID_signed_encrypted)

#===============================================================


buf_size=conn.recv(3)
#print(aes_keyaes_ke"BUFF=",buf_size)
aes_key_customer_for_paymentgateway_encrypted=conn.recv(int(buf_size))
buf_size=conn.recv(4)
PM_json_encrypted=conn.recv(int(buf_size))
buf_size=conn.recv(3)
PO_json_encrypted=conn.recv(int(buf_size))


public_key_paymentgateway=b""
with open('PubKPG', 'rb') as f:
        public_key_paymentgateway=f.read()
public_key_paymentgateway=RSA.importKey(public_key_paymentgateway)

sha=hashlib.sha256()
sha.update((str)(Random.random.randint(100000000000,9999999999999)).encode())
aes_key_for_paymentgateway=sha.digest()
aes_cipher_for_paymentgateway = AESCipher(aes_key_for_paymentgateway)

public_key_paymentgateway1 = PKCS1_OAEP.new(public_key_paymentgateway)

aes_key_for_paymentgateway_encrypted = public_key_paymentgateway1.encrypt(aes_key_for_paymentgateway)

PM_json_deencrypted=aes_cipher.decrypt(PM_json_encrypted)

# print("PM JSON ENC=",PM_json_deencrypted)

PM_json_encrypted=aes_cipher_for_paymentgateway.encrypt(str(PM_json_deencrypted))

PO_json=aes_cipher.decrypt(PO_json_encrypted)
PO=json.loads(PO_json)

aux=dict()
aux["Sid"]=int(SessionID)
aux["PubKC"]=str(public_key_customer.exportKey())
aux["amount"]=PO["Amount"]
# print(PO["Amount"])
aux_json=json.dumps(aux)



aux_json = str(aux_json).encode()
hash = int.from_bytes(sha512(aux_json).digest(), byteorder='big')
aux_json_hash_signed = pow(hash, private_key.d, private_key.n)



hash = int.from_bytes(sha512(aux_json).digest(), byteorder='big')
hashFromSignature = pow(aux_json_hash_signed, private_key.e, private_key.n)
print("Semnatura pasului 4 realizata:", hash == hashFromSignature)
print("Semnatura pasului 4 esuata:", hash != hashFromSignature)

aux_json_hash_signed_encryped=aes_cipher_for_paymentgateway.encrypt(str(aux_json_hash_signed))





# aux_json_hash=hashlib.sha256(aux_json.encode()).digest()



HOST = '127.0.0.1'  # The server's hostname or IP address
PORT = 1235         # The port used by the server

soc = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
soc.connect((HOST, PORT))
print('Connected to', HOST,':', PORT)

print("Connecting to Payment Gateway...")
soc.send(str(len(aes_key_customer_for_paymentgateway_encrypted)).encode())
soc.send(aes_key_customer_for_paymentgateway_encrypted)

soc.send(str(len(aes_key_for_paymentgateway_encrypted)).encode())
soc.send(aes_key_for_paymentgateway_encrypted)

soc.send(str(len(PM_json_encrypted)).encode())
soc.send(PM_json_encrypted)

soc.send(str(len(aux_json_hash_signed_encryped)).encode())
soc.send(aux_json_hash_signed_encryped)





conn.close()
soc.close()
