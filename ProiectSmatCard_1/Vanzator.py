
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
        return base64.b64encode( iv + cipher.encrypt( raw ) )

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


SessionID=Random.random.randint(100000000000,9999999999999)
SessionID_signed = private_key.sing(SessionID,32)

conn.close()
