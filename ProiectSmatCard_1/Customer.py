import socket,time
import Crypto
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES
import hashlib
from Crypto import Random
from Crypto.Random import random
import base64
from Crypto.Cipher import PKCS1_OAEP

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



# s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# s.connect(("127.0.0.1",1234))


public_key_merchant=b""

with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
        
PrivKC=hashlib.sha256()
PrivKC.update(b"PrivKC")
aes_key=PrivKC.digest()
aes_cipher = AESCipher(aes_key)


public_key_merchant=RSA.importKey(public_key_merchant)

public_key_merchant = AESCipher(public_key_merchant)


aes_key_encryped=public_key_merchant.encrypt(aes_key)

print()



# s.close()