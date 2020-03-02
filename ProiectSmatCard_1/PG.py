
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


conn.close()