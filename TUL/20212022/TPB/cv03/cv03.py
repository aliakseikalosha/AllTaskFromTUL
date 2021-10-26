import json

import pymongo

from init import collection


def print_collection(data):
    for x in data:
        print(x)


if __name__ == "__main__":
    print(collection.find_one())
    mydoc = collection.count_documents({})
    print("The number of documents in collection : ", collection.count_documents({}))

    print("Seznam dublicitnych clanku:")
    print_collection(collection.aggregate([
        {"$group":
            {
                "_id": '$title',
                "count": {
                    "$sum": 1
                }
            }
        },
        {
            "$match": {
                "count": {
                    "$gt": 1
                }
            }
        },
        {
            "$sort": {
                "count": pymongo.DESCENDING
            }
        }
    ]))

    print("Nejvic comentu ma :", collection.find().sort("comment_count", -1).limit(1)[0]['title'])
    print("Nevic fotek u clanku je", collection.find().sort("photo_count", -1).limit(1)[0]['photo_count'])
    print("Pocet clanku podle roku:")
    print_collection(collection.map_reduce("""
        function(){emit(this.date.substring(0,4),1);}
    """, """
        function(k,v){ return Array.sum(v);}
    """, "Years").find({}).sort("value", pymongo.DESCENDING))
    print("Pocet unikalmich kategorii:")
    print_collection(collection.aggregate([
        {"$group":
            {
                "_id": '$category',
                "count": {
                    "$sum": 1
                }
            }
        },
        {
            "$group": {"_id": "null", "count": {"$sum": 1}}
        }
    ]))
    print("Vse kategorii:")
    print_collection(collection.aggregate([
        {"$group":
            {
                "_id": '$category',
                "count": {
                    "$sum": 1
                }
            }
        }
    ]))
    print("5 nejcastejsich slov v nazvu clanku")
    print_collection(collection.map_reduce("""
        function(){
            if(this.date.substring(0,4) != "2021")
            {
                return;
            }
            this.title.split(' ').forEach(function(c){
                emit(c.toLowerCase(),1);
            });
        }
    """, """
        function(k,v){ return Array.sum(v);}
    """, "WordsInTitle").find({}).sort("value", pymongo.DESCENDING).limit(5))
    print("8 nejcastejsich slov v textu")

    print_collection(collection.map_reduce("""
            function(){
                this.body_text.split(' ').forEach(function(c){
                    if(c.length >= 5)
                    {
                        emit(c.toLowerCase(),1);
                    }
                });
            }
        """, """
            function(k,v){ return Array.sum(v);}
        """, "WordsInText").find({}).sort("value", pymongo.DESCENDING).limit(8))

    print("Nejstarsi clanek:", collection.find({}).sort("date", pymongo.ASCENDING).limit(1)[0]["date"])
    print("Pocet komentaru:")
    print_collection(collection.aggregate([{
        "$group": {
            "_id": "null",
            "total": {
                "$sum": "$comment_count"
            }
        }
    }]))
    print("Pocet slov:")
    print_collection(collection.map_reduce("""
                function(){
                    emit("TotalWords",this.body_text.split(' ').length);
                }
            """, """
                function(k,v){ return Array.sum(v);}
            """, "Words").find({}))
    print("Nejvetsi pocet slova 'Covid-19' v clankach:")
    print_collection(collection.map_reduce("""
                function(){
                    emit(this.title ,((this.body_text|| '').match(/(Covid-19)\W/g) || []).length);
                }
            """, """
                function(k,v){ return Array.sum(v);}
            """, "WordsInText").find({}).sort("value", pymongo.DESCENDING).limit(3))
