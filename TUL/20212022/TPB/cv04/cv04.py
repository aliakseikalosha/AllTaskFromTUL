import datetime

import pandas as pd
import pymongo

from init import collection

import matplotlib.pyplot as plt

import seaborn as sns


def print_collection(data):
    for x in data:
        print(x)


def taskAddArticles():
    print(collection.find_one()["date"])
    result = collection.map_reduce("""
        function(){
            emit(this.date.substring(0,10) ,1);
        }
    """, """
        function(k,v){ return Array.sum(v);}
    """, "WordsInTitle").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "date": [],
        "count": [],
    }
    for doc in result:
        print(doc)
        date = datetime.datetime.strptime(doc["_id"], "%Y-%m-%d")
        value = int(doc["value"])
        d["date"].append(pd.to_datetime(doc["_id"][0:4]))
        d["count"].append(value)

    df = pd.DataFrame(d)
    fig, ax = plt.subplots(figsize=(12, 6))
    fig = sns.barplot(x="date", y="count", data=df, estimator=sum, ci=None, ax=ax)

    x_dates = df['date'].dt.strftime('%Y').sort_values().unique()
    ax.set_xticklabels(labels=x_dates, rotation=45, ha='right')
    plt.show()


def main():
    taskAddArticles()


if __name__ == "__main__":
    sns.set_theme(style="darkgrid")
    main()
