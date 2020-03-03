
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
PORT = 1235         # The port used by the server

soc = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
soc.bind((HOST, PORT))
soc.listen()
conn, addr = soc.accept()
print('Connected by', addr)


buf_size=conn.recv(3)
aes_key_customer_encrypted=conn.recv(int(buf_size))

buf_size=conn.recv(3)
aes_key_merchant_encrypted=conn.recv(int(buf_size))

buf_size=conn.recv(4)
PM_json_encrypted=conn.recv(int(buf_size))

buf_size=conn.recv(3)
aux_json_hash_signed_encryped=conn.recv(int(buf_size))


private_key=b""
with open('PrivKPG', 'rb') as f:
        private_key=f.read()
private_key=RSA.importKey(private_key)

private_key1 = PKCS1_OAEP.new(private_key)

aes_key_customer=private_key1.decrypt(aes_key_customer_encrypted)
aes_key_merchant=private_key1.decrypt(aes_key_merchant_encrypted)

aes_cipher_customer = AESCipher(aes_key_customer)
aes_cipher_merchant = AESCipher(aes_key_merchant)


PM_json_encrypted=aes_cipher_merchant.decrypt(PM_json_encrypted)

PM_json_encrypted=str(PM_json_encrypted)[7:-4]

PM_json_encrypted=str(PM_json_encrypted).encode()

PM_json=aes_cipher_customer.decrypt(PM_json_encrypted)


PM = json.loads(PM_json)


PI_json = PM["PI"]

PI = json.loads(PI_json)
public_key_customer=PI["PubKC"]
public_key_customer=str(public_key_customer)[2:-1]
public_key_customer=str(public_key_customer).replace("\\n",'\n')#fixing aes decryption result
public_key_customer=str(public_key_customer).encode()


aux=dict()
aux["Sid"]=int(PI["Sid"])
aux["PubKC"]=str(public_key_customer)
aux["amount"]=PI["Amount"]
aux_json=json.dumps(aux)
aux_json = str(aux_json).encode()


aux_json_hash_signed=aes_cipher_merchant.decrypt(aux_json_hash_signed_encryped)
aux_json_hash_signed=str(aux_json_hash_signed)
aux_json_hash_signed=aux_json_hash_signed[2:-1]
aux_json_hash_signed = str(aux_json_hash_signed).encode()
aux_json_hash_signed=int(aux_json_hash_signed)


public_key_merchant=b""
with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
public_key_merchant=RSA.importKey(public_key_merchant)


aux_json_hash=int.from_bytes(sha512(aux_json).digest(), byteorder='big')
hashFromSignature = pow(aux_json_hash_signed, public_key_merchant.e, public_key_merchant.n)






PI_json = str(PI_json).encode()

PI_json_hash = int.from_bytes(sha512(PI_json).digest(), byteorder='big')

public_key_customer = RSA.importKey(public_key_customer)
hashFromSignature1 = pow(PM["SigC"], public_key_customer.e, public_key_customer.n)


if (aux_json_hash == hashFromSignature) == False:
    Resp = "Semnatura vanzator invalida"
elif (PI_json_hash == hashFromSignature1) == False:
    Resp = "Semnatura client invalida"
else:
    Resp = "Totul este ok"



aux=dict()
aux["Resp"]=Resp
aux["Sid"]=PI["Sid"]
if Resp=="Totul este ok":
    aux["amount"]=PI["Amount"]
else:
    aux["amount"]=0
aux["NC"]=PI["NC"]
aux_json=json.dumps(aux)
aux_json = str(aux_json).encode()
hash = int.from_bytes(sha512(aux_json).digest(), byteorder='big')
aux_json_hash_signed = pow(hash, private_key.d, private_key.n)

aux=dict()
aux["Resp"]=Resp
aux["Sid"]=PI["Sid"]
aux["SigPG"]=aux_json_hash_signed
aux_json=json.dumps(aux)



sha=hashlib.sha256()
sha.update((str)(Random.random.randint(100000000000,9999999999999)).encode())#adding salt
aes_key=sha.digest()
aes_cipher = AESCipher(aes_key)

aux_json_encrypted = aes_cipher.encrypt(aux_json)

public_key_merchant1 = PKCS1_OAEP.new(public_key_merchant)

aes_key_encrypted = public_key_merchant1.encrypt(aes_key)


conn.send(str(len(aes_key_encrypted)).encode())
conn.send(aes_key_encrypted)
conn.send(str(len(aux_json_encrypted)).encode())
conn.send(aux_json_encrypted)

conn.close()