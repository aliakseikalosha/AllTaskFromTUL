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
    patches, _ = plt.pie(d["count"], colors=colors, )
    plt.legend(patches, labels, loc='center left', bbox_to_anchor=(-0.37, 0.51), fontsize=6)

    plt.show(bbox_inches="tight")


def taskWordCount():
    result = collection.map_reduce("""
                function(){
                    emit(this.title ,this.body_text.split(" ").length);
                }
            """, """
                function(k,v){ return v[0];}
            """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "number": [],
        "count": [],
    }
    for doc in result:
        d["number"].append(int(doc["value"]))

    d["count"] = [d["number"].count(i) for i in d["number"]]

    plt.hist(d["count"])
    plt.show()


def taskWordLength():
    result = collection.map_reduce("""
                    function (){
                        var words = this.body_text.split(" ");
                        words.forEach(e => {
                            if(e.length >= 1){
                                emit(e.length, 1);
                            }
                        });
                    }
                """, """
                    function(k,v){ return Array.sum(v);}
                """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "number": [],
        "count": [],
    }
    for doc in result:
        d["number"].append(int(doc["_id"]))
        d["count"].append(int(doc["value"]))

    df = pd.DataFrame(d)
    fig, ax = plt.subplots(figsize=(18, 6))
    fig = sns.barplot(x="number", y="count", data=df, ax=ax)

    numbers = df['number']
    ax.set_xticklabels(labels=numbers, rotation=45, ha='right')
    plt.show()


def taskCovidTimeMap():
    result = collection.map_reduce("""
                function(){
                    var title = this.title.toLowerCase();
                    var covidNames = ["koronavir", "covid", "korona"];
                    var vacineName = ["vakcína", "očkovaní", "vakcin", "očkova"];
                    var message = [0,0];
                    if(covidNames.some(c => title.includes(c))){
                        message[0] = 1;
                    }
                    if(vacineName.some(c => title.includes(c))){
                        message[1] = 1;
                    }
                    if(message.some(a=>a>0)){
                        emit(this.date.substring(0,10) ,message);
                    }
                }
            """, """
                function(k,v){
                    var message = [0,0];
                    v.forEach(e=>{
                            message[0]+=e[0];
                            message[1]+=e[1];
                        }); 
                    return message;
                }
            """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "date": [],
        "count_cov": [],
        "count_vac": [],
    }
    for doc in result:
        d["date"].append(pd.to_datetime(doc["_id"][0:7]))
        d["count_cov"].append(int(doc["value"][0]))
        d["count_vac"].append(int(doc["value"][1]))
        print(doc)

    df = pd.DataFrame(d)
    fig, (ax1, ax2) = plt.subplots(2, 1, figsize=(18, 6))
    sns.barplot(x="date", y="count_cov", data=df, ci=None, ax=ax1)
    sns.barplot(x="date", y="count_vac", data=df, ci=None, ax=ax2)

    fig.tight_layout(pad=3.0)

    x_dates = df['date'].dt.strftime('%Y-%m').unique()
    ax1.set_xticklabels(labels=x_dates, rotation=45, ha='right')
    ax2.set_xticklabels(labels=x_dates, rotation=45, ha='right')
    plt.show()
    pass


def taskAddArticlesPerDayOfWeek():
    result = collection.map_reduce("""
                function(){
                    emit(this.date.substring(0,10) ,1);
                }
            """, """
                function(k,v){ return Array.sum(v);}
            """, "perYear").find({}).sort("_id", pymongo.ASCENDING)
    d = {
        "dayOfWeek": ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"],
        "date": [0, 1, 2, 3, 4, 5, 6],
        "count": [0, 0, 0, 0, 0, 0, 0],
    }
    for doc in result:
        date = datetime.datetime.strptime(doc["_id"], "%Y-%m-%d").weekday()
        d["count"][date] += int(doc["value"])

    df = pd.DataFrame(d)
    fig, ax = plt.subplots(1, 1, figsize=(12, 6))
    fig = sns.barplot(x="date", y="count", data=df, ci=None, ax=ax)

    ax.set_xticklabels(labels=df['dayOfWeek'], rotation=30, ha='right')
    plt.show()


def main():
    # taskAddArticles()
    # taskArticlesPerYear()
    # taskCountInCategory()
    # taskWordCount()
    # taskWordLength()
    taskCovidTimeMap()
    # taskAddArticlesPerDayOfWeek()


if __name__ == "__main__":
    sns.set_theme(style="darkgrid")
    main()
