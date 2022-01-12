def bin_to_grey(b):
    g = b[0]
    for i in range(1, len(b)):
        g += '0' if b[i - 1] == b[i] else '1'
    return g


def task_bin_to_grey():
    for i in range(1, 256):
        b = format(i, "b")
        b = (8 - len(b)) * "0" + b
        print(i, "\t-> bin[", b, "] grey[", bin_to_grey(b), "]")


def task_mft_coder():
    print("MFT encoder demo")
    data = input("word to encode: ")
    abc = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
           "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"]
    result = []
    for char in data.upper():
        index = abc.index(char)
        abc.remove(char)
        abc.insert(0, char)
        result.append(index)
    print(data, "->", result)


def bwt_encode(data):
    variations = []
    enc = ""
    temp = data
    for i in range(0, len(data)):
        temp = temp[-1] + temp[0:-1]
        variations.append(temp)
    variations.sort()
    for word in variations:
        enc += word[-1]
    return enc, variations.index(data)


def btw_decode(encoded, index):
    variation = list(encoded)
    temp = variation.copy()
    for i in range(1, len(encoded)):
        temp.sort()
        for j in range(0, len(variation)):
            variation[j] += temp[j][-1]
        temp = variation.copy()
    variation.sort()
    return variation[index]


def task_bwt_coder_decoder():
    print("MFT encoder and decoder demo")
    data = input("word to encode: ")
    encoded, index = bwt_encode(data)
    decoded = btw_decode(encoded, index)
    print(data, "->", "encoded string :", encoded, "index:", index, "->", decoded)


def main():
    task_bin_to_grey()
    task_mft_coder()
    task_bwt_coder_decoder()


if __name__ == "__main__":
    main()
