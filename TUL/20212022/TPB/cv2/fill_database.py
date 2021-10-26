import glob
import json
from init import collection
import datetime


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
            try:
                data["date"] = datetime.datetime.strptime(data["date"][0:-3], "%Y-%m-%dT%H:%M").isoformat()
                data["photo_count"] = int(data["photo_count"])
                data["comment_count"] = int(data["comment_count"])
                if None not in data.values():
                    insert_article(data)
            except Exception as e:
                print(filepath, "\nWRONG DATE :", data["date"], "\n", e)


if __name__ == "__main__":
    insert_data()
