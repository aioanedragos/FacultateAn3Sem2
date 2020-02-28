from Crypto.Cipher import PKCS1_OAEP
from Crypto.PublicKey import RSA

message = b"To be encrypted"

public_key_merchant=b""
with open('PubKM', 'rb') as f:
        public_key_merchant=f.read()
        
public_key_merchant = RSA.importKey(public_key_merchant)

# key = RSA.importKey(open('PubKM').read())
cipher = PKCS1_OAEP.new(public_key_merchant)
ciphertext = cipher.encrypt(message)

private_key_merchant=b""
with open('PrivKM', 'rb') as f:
        private_key_merchant=f.read()
        
private_key_merchant = RSA.importKey(private_key_merchant)


from Crypto.Cipher import PKCS1_OAEP

cipher = PKCS1_OAEP.new(private_key_merchant)
message = cipher.decrypt(ciphertext)