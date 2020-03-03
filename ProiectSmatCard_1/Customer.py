import Crypto
from Crypto.PublicKey import RSA
from Crypto import Random
from Crypto.Random import random
import socket
import json
import base64
from Crypto.Cipher import AES
import hashlib
import sys
from Crypto.Cipher import PKCS1_OAEP
import ast
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



#generare chei client=======================
new_key = RSA.generate(1024)

public_key = new_key.publickey().exportKey("PEM")
private_key = new_key.exportKey("PEM")
#==========================================



#Preluarea cheii publice a vanzatorului
public_key_merchant=b""
with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
        
#Aes key==============
sha=hashlib.sha256()
sha.update(b"cheiameasecreta")
aes_key=sha.digest()
aes_cipher = AESCipher(aes_key)
# print(aes_key)
#=====================


#Criptare asimetrica: criptarea cheiei aes cu cheia vanzatorului si cheia publica a clientului cu cheia aes
public_key_merchant = RSA.importKey(public_key_merchant)

public_key_merchant = PKCS1_OAEP.new(public_key_merchant)

aes_key_encryped=public_key_merchant.encrypt(aes_key)

public_key_encrypted=aes_cipher.encrypt(str(public_key))
#=========================================================================================================

HOST = '127.0.0.1'  # The server's hostname or IP address
PORT = 1234         # The port used by the server

connection = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
connection.connect((HOST, PORT))

#Trimiterea cheii publice catre vanzator ca sa poate semna Sesiunea
connection.send(str(len(public_key_encrypted)).encode())
connection.send(public_key_encrypted)
connection.send(str(len(aes_key_encryped)).encode())
connection.send(aes_key_encryped)
#=================================================================


#Primirea semnaturii sesiunii===========================================
buf_size=connection.recv(3)
aes_key_merchant_encrypted=connection.recv(int(buf_size))
buf_size=connection.recv(2)
SessionID_encryped=connection.recv(int(buf_size))
buf_size=connection.recv(3)
SessionID_signed_merchant_encrypted=connection.recv(int(buf_size))
#======================================================================


private_key = RSA.importKey(private_key)

private_key1 = PKCS1_OAEP.new(private_key)

aes_key_merchant = private_key1.decrypt(aes_key_merchant_encrypted)

aes_cipher_merchant = AESCipher(aes_key_merchant)

SessionID = aes_cipher_merchant.decrypt(SessionID_encryped)
SessionID = str(SessionID)[4:-2]#Corectarea SessionsIS dupa primire


SessionID_signed_merchant = aes_cipher_merchant.decrypt(SessionID_signed_merchant_encrypted)


#Creatrea PI PM PO
PI=dict()
PI["CardN"]="1111222233334444"
PI["CardExp"]="10/20"
PI["CCode"]="123"
PI["Sid"]=int(SessionID)
PI["Amount"]=100
PI["PubKC"]=str(public_key)
#print("PUB K=",str(public_key))
PI["NC"]=Random.random.randint(100000000000,9999999999999)
PI["M"]="Enter merchant name here"
PI_json=json.dumps(PI)

hash = int.from_bytes(sha512(str(PI_json).encode()).digest(), byteorder='big')
PI_json_hash_signed = pow(hash, private_key.d, private_key.n)

hash = int.from_bytes(sha512(str(PI_json).encode()).digest(), byteorder='big')
hashFromSignature = pow(PI_json_hash_signed, private_key.e, private_key.n)
print("Semnatura PI realizata:", hash == hashFromSignature)
print("Semnatura PI esuata:", hash != hashFromSignature)


PM=dict()
PM["PI"]=PI_json
PM["SigC"]=PI_json_hash_signed
PM_json=json.dumps(PM)

PO=dict()
PO["OrderDesc"]="Enter order description here"
PO["Sid"]=int(SessionID)
PO["Amount"]=100
PO_json=json.dumps(PO)

hash = int.from_bytes(sha512(str(PO_json).encode()).digest(), byteorder='big')
PO_json_hash_signed = pow(hash, private_key.d, private_key.n)

hash = int.from_bytes(sha512(str(PO_json).encode()).digest(), byteorder='big')
hashFromSignature = pow(PO_json_hash_signed, private_key.e, private_key.n)
print("Semnatura PO realizata:", hash == hashFromSignature)
print("Semnatura PO esuata:", hash != hashFromSignature)



PO["SigC"]=PO_json_hash_signed


#============================================================================

#Realizarea cripatarii PM intai cu cheia publica a PG si dupa cripatarea PO si PM cu cheia publica a M
public_key_paymentgateway=b""
with open('PubKPG', 'rb') as f:
        public_key_paymentgateway=f.read()
public_key_paymentgateway=RSA.importKey(public_key_paymentgateway)


sha=hashlib.sha256()
sha.update((str)(Random.random.randint(100000000000,9999999999999)).encode())#adding salt
aes_key_for_paymentgateway=sha.digest()
aes_cipher_for_paymentgateway = AESCipher(aes_key_for_paymentgateway)


public_key_paymentgateway1 = PKCS1_OAEP.new(public_key_paymentgateway)

aes_key_for_paymentgateway_encrypted = public_key_paymentgateway1.encrypt(aes_key_for_paymentgateway)

PM_json_encrypted=aes_cipher_for_paymentgateway.encrypt(str(PM_json))

PM_json_encrypted=aes_cipher_merchant.encrypt(str(PM_json_encrypted))

PO_json_encrypted=aes_cipher_merchant.encrypt(str(PO_json))

#======================================================================================================

connection.send(str(len(aes_key_for_paymentgateway_encrypted)).encode())
connection.send(aes_key_for_paymentgateway_encrypted)

connection.send(str(len(PM_json_encrypted)).encode())
connection.send(PM_json_encrypted)

connection.send(str(len(PO_json_encrypted)).encode())
connection.send(PO_json_encrypted)



try:
    connection.settimeout(5)
    buf_size=connection.recv(3)
    aux_json_encryped=connection.recv(int(buf_size))
    aux_json=aes_cipher.decrypt(aux_json_encryped)
    aux_json=str(aux_json)[5:-3]
    aux=json.loads(aux_json)
    print(aux["Resp"])

except:
    print("Socket timeout exceeded!")



connection.close()