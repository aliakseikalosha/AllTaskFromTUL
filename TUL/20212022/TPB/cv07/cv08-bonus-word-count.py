from pyspark.sql import SparkSession
import pyspark.sql.functions as func
import re

spark = SparkSession.builder.master("spark://1cd02537f6f2:7077").appName("cv8WordCountBonus").getOrCreate()


def parse_word(w):
    return re.sub(r'\W+', '', w.lower())


parse_word_udf = func.udf(parse_word)
lines = spark.readStream.option("maxFilePerTrigger", 1).text("/files/demoCv8/")

words = lines.select(func.explode(func.split(lines.value, " ")).alias("split_word")) \
    .withColumn("word", parse_word_udf("split_word")) \
    .select("word")
word_count = words.groupBy("word") \
    .count() \
    .orderBy(func.col("count").desc())
query = word_count.writeStream.outputMode("complete") \
    .format("console").option("truncate", "false") \
    .queryName("word_count_bonus").start()
query.awaitTermination()
spark.stop()
