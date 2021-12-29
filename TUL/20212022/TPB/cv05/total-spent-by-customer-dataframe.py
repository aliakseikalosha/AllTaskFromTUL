from pyspark.sql import SparkSession
from pyspark.sql.types import StructType, StructField, IntegerType, FloatType
import pyspark.sql.functions as func

# spark-submit total-spent-by-customer-dataframe.py
spark = SparkSession.builder.master("spark://05710b836584:7077").appName("SparkSQL").getOrCreate()
schema = StructType([
    StructField("customerID", IntegerType(), False),
    StructField("itemID", IntegerType(), False),
    StructField("cost", FloatType(), False)
])

customers = spark.read.schema(schema).csv("/files/customer-orders.csv")

customers.printSchema()
agg_data = customers.groupBy("customerID").agg({"cost": "sum"})
result = agg_data\
    .withColumn("total_spent", func.round(agg_data["sum(cost)"], 2))\
    .select("customerID","total_spent")\
    .sort("total_spent", ascending=False)
result.show()

spark.stop()
