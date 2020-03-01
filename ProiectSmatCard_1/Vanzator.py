import socket
HOST = '127.0.0.1'  # The server's hostname or IP address
PORT = 1234         # The port used by the server

soc = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
soc.bind((HOST, PORT))
soc.listen()
conn, addr = soc.accept()
print('Connected by', addr)



buff_size = conn.recv(2)
public_key_encrypted_customer = conn.recv(int(buff_size))
buff_size = conn.recv(2)
aes_key_encrypted_customer = conn.recv(int(buff_size))







conn.close()
