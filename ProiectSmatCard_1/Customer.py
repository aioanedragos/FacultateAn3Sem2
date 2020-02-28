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


new_key = RSA.generate(1024)

public_key = new_key.publickey().exportKey("PEM")

private_key = new_key.exportKey("PEM")

public_key_merchant=b""
with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
        
#Aes key==============
sha=hashlib.sha256()
sha.update(b"cheiameasecreta")
aes_key=sha.digest()
aes_cipher = AESCipher(aes_key)

#=====================

public_key_merchant = RSA.importKey(public_key_merchant)

text = b'ceva nou nou'

encryptor = PKCS1_OAEP.new(public_key)
encrypted = encryptor.encrypt(text) #aici vine cheia AES


decryptor = PKCS1_OAEP.new(private_key)
decrypted = decryptor.decrypt(encrypted)





# aes_key_encryped=public_key_merchant.encrypt(aes_key,32)
# aes_key_encryped=aes_key_encryped[0]
# public_key_encrypted=aes_cipher.encrypt(str(public_key))
