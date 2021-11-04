import struct


def get_start_dictionary():
    # d = {'a': 1, 'b': 2, 'c': 3}
    d = {}
    for i in range(1, 6):
        d[str(i)] = i
    return d


def reverse_dictionary(d):
    r = {}
    for k in d.keys():
        r[d[k]] = k
    return r


def decompress(data):
    start_dic = reverse_dictionary(get_start_dictionary())
    decompressed = ""
    i = 0
    length = len(data)
    previously_decompressed = ""
    while i < length:
        current_data = data[i]

        if current_data not in start_dic:
            start_dic[current_data] = previously_decompressed + previously_decompressed[0]

        decompressed += start_dic[current_data]

        if i > 0:
            start_dic[max(start_dic) + 1] = previously_decompressed + start_dic[current_data][0]

        previously_decompressed = start_dic[current_data]
        i += 1

    return decompressed, start_dic


def compress(data):
    start_dic = get_start_dictionary()
    compressed = []
    i = 0
    length = len(data)
    while i < length:
        j = 1
        while data[i:i + j] in start_dic and i + j < length:
            j += 1
        if i == i + j - 1:
            compressed.append(start_dic[data[i:i + j]])
        else:
            compressed.append(start_dic[data[i:i + j - 1]])

        key = data[i:min(i + j, length)]
        
        if key not in start_dic:
            val = max(start_dic.values()) + 1
            start_dic[key] = val

        i += max(1, j - 1)

    return compressed, start_dic


def read_all_data():
    data = ""
    with open("Cv05_LZW_data.bin", 'rb') as f:
        while True:
            byte_s = f.read(1)
            if not byte_s:
                break
            data += str(struct.unpack('b', byte_s)[0])
    return data


def main():
    data = read_all_data()  # "abcabcabcbcba"
    (compress_data, compress_dic) = compress(data)
    (decompress_data, decompress_dic) = decompress(compress_data)
    print(data, compress_data, decompress_data, compress_dic, decompress_dic, sep='\n')


if __name__ == '__main__':
    main()
