import socket
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind(("127.0.0.1",1234))
s.listen(1)
(connection, address) = s.accept()
print ("Connectd address:",address)
while True:
    data = connection.recv(100).decode("UTF-8")
    if not data: break
    print("Received: ",data)
    if "exit" in data: break
connection.close()
print ("Server closed")