import glob
import json
from init import collection


def print_collection(data):
    for x in data:
        print(x)


def insert_article(data):
    x = collection.insert_one(data)
    print(x)


def insert_data():
    x = collection.delete_many({})
    print(x.deleted_count, " documents deleted.")
    for filepath in glob.iglob(r'./data/*.txt'):
        print(filepath)
        with open(filepath) as file:
            data = json.load(file)
            insert_article(data)


insert_data()
