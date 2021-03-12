def save_person(path, persons):
    f = open(path, "w")
    csv = ""
    for person in persons:
        csv += "{};{}\n".format(person["name"], person["age"])

    f.write(csv)
    f.close()


def load_persons(path):
    f = open(path, "r")
    lines = f.readlines()
    persons = []
    for line in lines:
        data = line.strip().split(";")
        persons.append({"name": data[0], "age": data[1]})
    f.close()
    return persons


def text_analysis(path):
    f = open(path, "r")
    words = f.read().replace("\n", " ").split()
    f.close()
    symbols_count = {}
    words_count = {}

    def sort_dic(dic, reverse):
        return dict(sorted(dic.items(), key=lambda item: item[1], reverse=reverse))

    for word in words:
        clean_word = "".join([c for c in list(word.lower()) if c.isalpha()])
        for char in clean_word:
            if char not in symbols_count:
                symbols_count[char] = 0

            symbols_count[char] += 1
        if clean_word not in words_count:
            words_count[clean_word] = 0

        words_count[clean_word] += 1

    return sort_dic(symbols_count, False), sort_dic(words_count, True)


def get_words(n, m, corpus):
    words = []
    for data in corpus.items():
        if n > 0 and len(data[0]) >= m:
            n -= 1
            words.append(data)
        elif n == 0:
            break
    return words


if __name__ == "__main__":
    save_person("person.csv", [{"name": "AA", "age": 10}, {"name": "BB", "age": 11}, {"name": "CC", "age": 22}])
    print(load_persons("person.csv"))
    symbols, words = text_analysis("sh.txt")
    print("===========SYMBOLS============")
    print(symbols)
    print("============WORDS=============")
    print(words)
    print(get_words(10, 4, words))
