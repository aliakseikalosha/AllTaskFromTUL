
=begin
https://www.onlinegdb.com/online_ruby_interpreter
                            Online Ruby Interpreter.
                Code, Compile, Run and Debug Ruby script online.
Write your code in this editor and press "Run" button to execute it.


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
        print(f"{char} <{low},{high})")
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
    data = read_all_data("Cv07_Aritm_data.bin")+"\0"
    (code, dic) = aritm_code(data)
    print("Data : ", data)
    print("Arithmetic : ", code, "\n", dic)
    print("Decode : ", aritm_decode(code, dic))
    pass


if __name__ == "__main__":
    main()



=end


def aritm_dic(input)
    dic = {}
    data = input.split('')
    for char in data
        if dic[char] == nil
            print("Add [", char, "] to the dic\n")
            dic[char] = 0.0
        end
        print("key [",char,"]  value for key ",dic[char]," in hash :\n",dic,"\n")
        dic[char] = (dic[char] + 1.0 / data.length)
    end
    start = 0.0
    dic2 = {}
    dic.each do |key, value|
        end_c = start + value
        dic2[key] = [start, [end_c, 1.0].min]
        start = end_c
    end
    return dic2
end


def aritm_code(input)
    dic = aritm_dic(input)
    low = 0
    high = 1
    data = input.split('')
    for char in data
        zl = dic[char][0]
        zh = dic[char][1]
        old_h = high
        high = low + zh * (old_h - low)
        low = low + zl * (old_h - low)
        print char," <", low ," ", high, ")\n"
    end
    result = low + (high - low) / 2
    return [result, dic]
end

def aritm_decode(code, dic)
    print "\naritm_decode\n"
    c = code
    reuslt = ""
    low = 0
    high = 1
    k = c
    while true
        char = get_key_for_interval(dic, k)
        if char == "\0"
            break
        end
        reuslt += char
        zl = dic[char][0]
        zh = dic[char][1]
        old_h = high
        
        high = low + zh * (old_h - low)
        low = low + zl * (old_h - low)
        k = (c - low) / (high - low)
    end
    return reuslt
end

def get_key_for_interval(dic, number)
    dic.each do |key, value|
        if ((value[0] <= number) and (number < value[1]))
            print value[0],"\t<=\t",number,"\t<\t",value[1]," => [",key,"]\n"
            return key
        end
    end
    return "\0"
end

def main()
    data = "3211231413"+"\0"
    d = aritm_code(data)
    code =  d[0]
    dic = d[1]
    print("Data : ", data,"\n")
    print("Arithmetic : ", code, "\n", dic,"\n")
    print("Decode : ", aritm_decode(code, dic),"\n")
end

main()
