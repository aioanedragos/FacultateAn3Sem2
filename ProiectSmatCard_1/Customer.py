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
        More_public_key=b""
        with open('MorePubK', 'rb') as f:
            More_public_key=f.read()
        More_public_key = RSA.importKey(More_public_key)

        More_public_key = PKCS1_OAEP.new(More_public_key)
        to_return = More_public_key.encrypt(raw.encode("utf8"))
        return to_return
        

    def decrypt( self, enc ):
        enc = base64.b64decode(enc)
        iv = enc[:16]
        cipher = AES.new(self.key, AES.MODE_CBC, iv )
        return unpad(cipher.decrypt( enc[16:] ))


new_key = RSA.generate(1024)

public_key = new_key.publickey()

private_key = new_key.exportKey("PEM")

public_key_merchant=b""
with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
        
#Aes key==============
sha=hashlib.sha256()
sha.update(b"cheiameasecreta")
aes_key=sha.digest()
aes_cipher = AESCipher(aes_key)
print(aes_key)
#=====================

public_key_merchant = RSA.importKey(public_key_merchant)

public_key_merchant = PKCS1_OAEP.new(public_key_merchant)

#Cheia publica a utilizatorului este criptata cu o cheie AES


aes_key_encryped=public_key_merchant.encrypt(aes_key)



public_key_encrypted=aes_cipher.encrypt(str(public_key))
