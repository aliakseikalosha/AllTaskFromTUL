import struct


def rle_decode(data):
    result = ""
    for t in data:
        result += t[0] * str(t[1])
    return result


def rle_encode(data):
    count = 1
    prev_number = data[0]
    result = []
    for n in data[1:]:
        if prev_number == n:
            count += 1
        else:
            result.append((count, prev_number))
            count = 1
            prev_number = n
    result.append((count, prev_number))
    return result


def read_all_data(path):
    data = ""
    with open(path, 'rb') as f:
        while True:
            byte_s = f.read(1)
            if not byte_s:
                break
            data += str(struct.unpack('b', byte_s)[0])
    return data


def huff_encode(data):
    result = ""
    count = {}
    for n in data:
        if n in count:
            count[n] += 1
        else:
            count[n] = 1
    dic = {}
    steps = []

    def pop_smallest(data):
        m = min(data.values())
        for k in data.keys():
            if data[k] == m:
                return k, data.pop(k, None)
        return None, None

    def get_next_step(data):
        step = {}
        print(data)
        (key0, value0) = pop_smallest(data)
        (key1, value1) = pop_smallest(data)
        step[key1] = 1
        step[key0] = 0
        data[key1 + key0] = value1 + value0
        print((key0, value0, 0), (key1, value1, 1), sep="\n")
        return step, data

    while len(count) > 1:
        (step, count) = get_next_step(count)
        steps.append(step)

    for step in reversed(steps):
        for code in step:
            for char in code:
                if char not in dic:
                    dic[char] = ""
                dic[char] += str(step[code])

    for n in data:
        result += dic[n]

    return result, dic


def huff_decode(data, dic):
    result = ""
    inv_dic = {v: k for k, v in dic.items()}
    key = ""

    for char in data:
        key += char

        if key not in inv_dic:
            continue

        result += inv_dic[key]
        key = ""

    return result


def main():
    data = read_all_data("Cv06_RLE_data.bin")
    print(data)
    rle_enc = rle_encode(data)
    print("RLE : ", rle_enc)
    print("RLE decode : ", rle_decode(rle_enc))

    data = read_all_data("../cv05/Cv05_LZW_data.bin")
    (huff_enc, dec) = huff_encode(data)
    print("Huffman : ", huff_enc)
    print("Huffman dic: ", dec)
    print("Huffman decode : ", huff_decode(huff_enc, dec))


if __name__ == '__main__':
    main()
