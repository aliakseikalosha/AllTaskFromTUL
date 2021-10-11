import numpy as np
import matplotlib.pyplot as plt
import struct
with open('cv01_dobryden.wav', 'rb') as f:
    #head
    print("RIFF or RIFX: ", f.read(4))
    A1 = struct.unpack('i', f.read(4))[0]
    print("A1", A1)
    f.seek(25, 0)
    VF = struct.unpack('h', f.read(2))[0]
    print("VF", VF)
    f.seek(36, 0)
    print("Data:",f.read(4))
    f.seek(40, 0)
    A2 = struct.unpack('<i', f.read(4))[0]
    print("A2", A2)
    #data
    SIG = np.zeros(A2)
    for i in range(0, A2):
        SIG[i] = struct.unpack('B', f.read(1))[0]
t = np.arange(A2).astype(float)/VF
plt.plot(t, SIG)
plt.xlabel('t[s]')
plt.ylabel('A[-]')
plt.show()