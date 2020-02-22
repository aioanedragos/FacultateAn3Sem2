import socket,time
s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect(("127.0.0.1",1234))
s.send(b"Mesaj 1")
time.sleep(1)
s.send(b"Mesaj 2")
time.sleep(1)
s.send(b"exit")
s.close()