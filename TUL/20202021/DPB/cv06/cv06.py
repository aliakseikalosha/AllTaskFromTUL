from init import collection
from bson.son import SON
from bson.code import Code

'''
DPB - 6. cvičení - Agregační roura a Map-Reduce

V tomto cvičení si můžete vybrat, zda ho budete řešit v Mongo shellu nebo pomocí PyMongo knihovny.

Před testováním Vašich řešení si nezapomeňte zapnout Mongo v Dockeru - používáme stejná data jako v minulých cvičeních.

Pro pomoc je možné např. použít https://api.mongodb.com/python/current/examples/aggregation.html a přednášku.

Všechny výsledky limitujte na 10 záznamů. Nepoužívejte české názvy proměnných!

Struktura záznamu v db:
{
  "address": {
     "building": "1007",
     "coord": [ -73.856077, 40.848447 ],
     "street": "Morris Park Ave",
     "zipcode": "10462"
  },
  "borough": "Bronx",
  "cuisine": "Bakery",
  "grades": [
     { "date": { "$date": 1393804800000 }, "grade": "A", "score": 2 },
     { "date": { "$date": 1378857600000 }, "grade": "A", "score": 6 },
     { "date": { "$date": 1358985600000 }, "grade": "A", "score": 10 },
     { "date": { "$date": 1322006400000 }, "grade": "A", "score": 9 },
     { "date": { "$date": 1299715200000 }, "grade": "B", "score": 14 }
  ],
  "name": "Morris Park Bake Shop",
  "restaurant_id": "30075445"
}
'''


def print_delimiter(n):
    print('\n', '#' * 10, 'Úloha', n, '#' * 10, '\n')


def print_collection(data):
    for x in data:
        print(x)


'''
Agregační roura
Zjistěte počet restaurací pro každé PSČ (zipcode)
 a) seřaďte podle zipcode vzestupně
 b) seřaďte podle počtu restaurací sestupně
Výpis limitujte na 10 záznamů a k provedení použijte collection.aggregate(...)
'''
print_delimiter('1 a)')
pipe = [
    {"$group": {"_id": "$address.zipcode", "count": {"$sum": 1}}},
    {"$sort": SON([("_id", 1)])},
    {"$limit": 10}]
print_collection(collection.aggregate(pipe))

print_delimiter('1 b)')
pipe = [
    {"$group": {"_id": "$address.zipcode", "count": {"$sum": 1}}},
    {"$sort": SON([("count", -1)])},
    {"$limit": 10}]
print_collection(collection.aggregate(pipe))

'''
Agregační roura

Restaurace obsahují pole grades, kde jsou jednotlivá hodnocení. Vypište průměrné score pro každou hodnotu grade.
V agregaci vynechte grade pro hodnotu "Not Yet Graded" (místo A, B atd. se může vyskytovat tento řetězec).

'''
print_delimiter(2)
pipe = [
    {"$unwind": "$grades"},
    {"$match": {"grades.grade": {"$ne": "Not Yet Graded"}}},
    {"$group": {"_id": "$grades.grade", "score": {"$avg": "$grades.score"}}},
    {"$sort": SON([("_id", 1)])},
]
print_collection(collection.aggregate(pipe))

'''
Map-Reduce

Zadání je stejné jako u 1. příkladu pro agregační rouru - pouze realizovat přes Map-Reduce. 

Při řešení může pomoct:
https://pymongo.readthedocs.io/en/stable/examples/aggregation.html
https://pymongo.readthedocs.io/en/stable/api/pymongo/collection.html#pymongo.collection.Collection.map_reduce 
'''
print_delimiter(3)
mapper = Code("""function() { emit(this.address.zipcode, 1);}""")
reducer = Code("""function(key, values){return Array.sum(values);}""")
result = collection.map_reduce(mapper, reducer, "myresults")
print_collection(result.find().sort("_id", 1).limit(10))

'''
Map-Reduce

Zadání je stejné jako u 2. příkladu pro agregační rouru - pouze realizovat přes Map-Reduce.
'''
print_delimiter(4)
mapper = Code("""function() { this.grades.forEach(e=> {if(e.grade != "Not Yet Graded") {emit(e.grade, e.score);}});}""")
reducer = Code("""function(key, values){return Array.avg(values);}""")
result = collection.map_reduce(mapper, reducer, "myresults")
print_collection(result.find().sort("_id", 1).limit(10))