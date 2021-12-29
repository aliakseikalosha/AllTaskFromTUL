from pyspark.sql import SparkSession
import pyspark.sql.functions as func
import re

spark = SparkSession.builder.master("spark://1cd02537f6f2:7077").appName("cv8WordCount").getOrCreate()


def parse_word(w):
    return re.sub(r'\W+', '', w.lower())


parse_word_udf = func.udf(parse_word)
lines = spark.readStream.format("socket").option("host", "localhost").option("port", 9999).load()

words = lines.select(func.explode(func.split(lines.value, " ")).alias("split_word")) \
    .withColumn("word", parse_word_udf("split_word")) \
    .select("word") \
    .withColumn("time", func.current_timestamp())
windowed_word_count = words.groupBy(func.window(func.col("time"), "30 seconds", "15 seconds"), func.col("word")) \
    .count() \
    .orderBy(func.col("window").asc(), func.col("count").desc())
query = windowed_word_count.writeStream.outputMode("complete")\
    .format("console").option("truncate", "false")\
    .queryName("word_count").start()
query.awaitTermination()
spark.stop()
