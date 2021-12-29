from cv06.cv06 import read_all_data


def aritm_dic(data):
    dic = {}
    for char in data:
        if char not in dic:
            dic[char] = 0
        dic[char] += 1 / len(data)

    start = 0
    for key in dic:
        end = start + dic[key]
        dic[key] = (start, min(end, 1.0))
        start = end
    return dic


def aritm_code(data):
    dic = aritm_dic(data)
    low = 0
    high = 1
    for char in data:
        (zl, zh) = dic[char]
        (low, high) = (low + zl * (high - low), low + zh * (high - low))
    result = low + (high - low) / 2
    return result, dic


def get_key_for_interval(dic, number):
    for pair in dic.items():
        if pair[1][0] <= number < pair[1][1]:
            return pair[0]
    return '\0'


def aritm_decode(code, dic):
    c = code
    reuslt = ""
    low = 0
    high = 1
    k = c
    while True:
        char = get_key_for_interval(dic, k)
        if char == '\0':
            break
        reuslt += char
        (zl, zh) = dic[char]
        (low, high) = (low + zl * (high - low), low + zh * (high - low))
        k = (c - low) / (high - low)
    return reuslt


def main():
    data = read_all_data("Cv07_Aritm_data.bin") + "\0"
    (code, dic) = aritm_code(data)
    print("Data : ", data)
    print("Arithmetic : ", code, "\n", dic)
    print("Decode : ", aritm_decode(code, dic))
    pass


if __name__ == "__main__":
    main()
