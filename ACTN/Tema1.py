import numpy as np

def Coding(Message,base):
    k = len(str(Message)) + 1
    n = k + 2
    stop = 0
    rests = []
    y = []
    while stop != 1:
        rests.append(Message % base)
        Message = Message // base
        if Message == 0:
            stop = 1
            
    rests.reverse()
    rests.append(0)
    Polinom = np.poly1d(rests)
    for i in range(n):
        y.append(Polinom(i + 1) % base)

    return y

def mul(subsetMessage, i):
    result = 1
    for j in subsetMessage:
        if j != i:
            # print(j , i)
            # print(j/(j-i))
            result = result * (j/(j-i))
    return result

def fc(subsetMessage,y_copy):
    arr = [0,1,2]
    count = 0
    result = 0
    while count < 3:
        ceva = y_copy[subsetMessage[arr[count]]-1] * (mul(subsetMessage,subsetMessage[arr[count]]))
        result = result + (y_copy[subsetMessage[arr[count]]-1] * (mul(subsetMessage,subsetMessage[arr[count]])))
        print(ceva)
        count += 1
    # print(result)
    return result

def Decoding(Message, base):
    y_copy = Coding(Message, base)
    y = Coding(Message, base)
    y_copy[1] = 2
    mistake = 0
    subsetMessage = []
    for i in range(len(y_copy)):
        if y[i] != y_copy[i]:
            mistake = i + 1
    
    for i in range(len(y)):
        subsetMessage.append(i + 1)
    subsetMessage.remove(mistake)
    # print(subsetMessage)
    firstResultTry = fc(subsetMessage,y_copy)
    # if firstResultTry == 0:
    #     return True
    # else:
    #     return False
    return firstResultTry

print(Decoding(29,11))