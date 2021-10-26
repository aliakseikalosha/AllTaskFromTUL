import numpy as np
import matplotlib.pyplot as plt
import struct


def display_graph(t, sig):
    plt.plot(t, sig)
    plt.xlabel('t[s]')
    plt.ylabel('A[-]')
    plt.show()

def display_wave(file_path):
    print(file_path)
    with open(file_path, 'rb') as f:
        # head
        type = f.read(4)
        if type != b'RIFF' and type != b'RIFX':
            raise ValueError("broken description of file no RIFF or RIFX")
        print("RIFF or RIFX: ", type)
        A1 = struct.unpack('i', f.read(4))[0]
        print("A1", A1)
        print(f.read(4))
        print(f.read(4))
        AF = struct.unpack("i", f.read(4))[0]
        K = struct.unpack('h', f.read(2))[0]
        C = struct.unpack('h', f.read(2))[0]
        VF = struct.unpack('i', f.read(4))[0]
        print("VF", VF)
        PB = struct.unpack('i', f.read(4))[0]
        VB = struct.unpack('h', f.read(2))[0]
        VV = struct.unpack('h', f.read(2))[0]
        print("Data:", f.read(4))
        A2 = struct.unpack('i', f.read(4))[0]
        print("A2", A2)
        # data
        length = int(A2 / (VV/8)/C)
        SIG = np.zeros([C,length])
        for i in range(0, length):
            for ch in range(0,C):
                SIG[ch, i] = struct.unpack('h', f.read(int(VV/8)))[0]

    t = np.arange(length).astype(float) / VF
    for ch in range(0, C):
        display_graph(t,SIG[ch])


if __name__ == '__main__':
    for i in range(1,7):
        try:
            display_wave("../cv02/cv02_wav_0"+str(i)+".wav")
        except ValueError as error:
            print(error)