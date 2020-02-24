import socket,time
import Crypto
from Crypto.PublicKey import RSA
from Crypto.Cipher import AES

# s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
# s.connect(("127.0.0.1",1234))

new_key = RSA.generate(1024)

public_key = new_key.publickey().exportKey("PEM")

private_key = new_key.exportKey("PEM")

# print(private_key)

public_key_merchant=b""
with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
print(public_key_merchant)



# s.close()