import datetime

import numpy as np
import pandas as pd
import pymongo

from init import collection

import matplotlib.pyplot as plt

import seaborn as sns


def log(*d):
    if True:
        print(*d)


def print_collection(data):
    for x in data:
        print(x)


def taskArticlesPerYear():
    result = collection.map_reduce("""
        function(){
            emit(this.date.substring(0,10) ,1);
        }
    """, """
        function(k,v){ return Array.sum(v);}
    """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "date": [],
        "count": [],
    }
    for doc in result:
        log(doc)
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


def taskAddArticles():
    result = collection.map_reduce("""
            function(){
                emit(this.date.substring(0,10) ,1);
            }
        """, """
            function(k,v){ return Array.sum(v);}
        """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "date": [],
        "count": [],
    }
    total_count = 0;
    for doc in result:
        date = datetime.datetime.strptime(doc["_id"], "%Y-%m-%d")
        total_count += int(doc["value"])
        log(doc, total_count)
        d["date"].append(pd.to_datetime(doc["_id"][0:7]))
        d["count"].append(total_count)

    df = pd.DataFrame(d)
    fig, ax = plt.subplots(1, 1, figsize=(12, 6))
    fig = sns.lineplot(x="date", y="count", data=df, estimator=None, ci=None, ax=ax)

    x_dates = df['date'].dt.strftime('%Y').sort_values().unique()
    ax.set_xticklabels(labels=x_dates, rotation=45, ha='right')
    plt.show()


def taskCountInCategory():
    result = collection.map_reduce("""
                function(){
                    emit(this.category ,1);
                }
            """, """
                function(k,v){ return Array.sum(v);}
            """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "category": [],
        "count": [],
        "percent": [],
    }
    for doc in result:
        category = doc["_id"]
        value = int(doc["value"])
        log(doc)
        d["category"].append(category)
        d["count"].append(value)

    d["category"] = np.array(d["category"])
    d["count"] = np.array(d["count"])
    d["percent"] = 100.0 * d["count"] / d["count"].sum()
    labels = ['{0} - {1:1.2f} %'.format(i, j) for i, j in zip(d["category"], d["percent"])]

    colors = sns.color_palette('pastel')[0:len(d["count"])]
    patches,_ = plt.pie(d["count"], colors=colors,)
    plt.legend(patches, labels, loc='center left', bbox_to_anchor=(-0.37, 0.51), fontsize=6)

    plt.show(bbox_inches="tight")


def main():
    # taskAddArticles()
    # taskArticlesPerYear()
    # taskCountInCategory()

    pass


if __name__ == "__main__":
    sns.set_theme(style="darkgrid")
    main()
