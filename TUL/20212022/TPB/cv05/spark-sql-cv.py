from pyspark.sql import SparkSession
# spark-submit spark-sql-cv.py
spark = SparkSession.builder.master("spark://05710b836584:7077").appName("SparkSQL").getOrCreate()

people = spark.read.option("header", "true").option("inferSchema", "true").csv("/files/fakefriends-header.csv")

print("Here is our inferred schema:")
people.printSchema()
people.select("age", "friends").groupBy("age").mean("friends").withColumnRenamed("avg(friends)", "avg friends count").sort("age").show()
spark.stop()

