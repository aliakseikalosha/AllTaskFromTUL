import re

from pyspark.sql import SparkSession
from pyspark.sql import functions as func
import codecs

spark = SparkSession.builder.master("spark://05710b836584:7077").appName("MostPopularSuperhero").getOrCreate()


def load_marvel_names():
    n = {}
    p = re.compile(r"([0-9]+)\s(.+)")
    with codecs.open("/files/marvel-names.txt", "r", encoding="ISO-8859-1", errors="ignore") as f:
        for line in f:
            match = p.match(line)
            data = [match[1], match[2].strip()]
            n[data[0]] = data[1]
    return n


def get_marvel_name(id):
    return names.value[id]


get_marvel_name_udf = func.udf(get_marvel_name)

names = spark.sparkContext.broadcast(load_marvel_names())

lines = spark.read.text("/files/marvel-graph.txt")

# Small tweak vs. what's shown in the video: we trim each line of whitespace as that could
# throw off the counts.
connections = lines.withColumn("id", func.split(func.trim(func.col("value")), " ")[0]) \
    .withColumn("connections", func.size(func.split(func.trim(func.col("value")), " ")) - 1) \
    .groupBy("id").agg(func.sum("connections").alias("connections"))
min_connection = connections.agg(func.min("connections").alias("min_c")).collect()[0][0]
result = connections.filter(connections.connections == min_connection).withColumn("Name",
                                                                                  get_marvel_name_udf(func.col("id")))
# .filter(connections.connections == connections.min)

result.show(100)

spark.stop()
