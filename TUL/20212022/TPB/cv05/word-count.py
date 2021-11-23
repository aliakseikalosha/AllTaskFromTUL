from pyspark import SparkConf, SparkContext
import re

# spark-submit word-count.py
conf = SparkConf().setMaster("spark://05710b836584:7077").setAppName("WordCount")
# conf = SparkConf().setMaster("local").setAppName("WordCount")
sc = SparkContext(conf=conf)

input = sc.textFile("/files/book.txt")
words = input.flatMap(lambda x: x.split()) \
    .map(lambda x: x.encode('ascii', 'ignore')) \
    .filter(lambda x: x) \
    .map(lambda x: re.sub(r'\W+', '', x.decode()).lower())
wordCounts = words.countByValue()
sorted = list(wordCounts.items())
sorted.sort(key=lambda x: x[1], reverse=True)
for word, count in sorted[0:20]:
    print(word + " " + str(count))
