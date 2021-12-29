from pyspark.ml.feature import VectorAssembler
from pyspark.ml.regression import DecisionTreeRegressor
from pyspark.sql import SparkSession

# spark-submit house-price-predict.py
spark = SparkSession.builder.master("spark://1cd02537f6f2:7077").appName("HousePricePredict").getOrCreate()

# | No|TransactionDate|HouseAge|DistanceToMRT|NumberConvenienceStores|Latitude|Longitude|PriceOfUnitArea|
data = spark.read.option("header", "true").option("inferSchema", "true").csv("/files/realestate.csv")
assembler = VectorAssembler(outputCol="features").setInputCols(
    [
        "HouseAge",
        "DistanceToMRT",
        "NumberConvenienceStores",
        "Latitude",
        "Longitude"
    ])

df = assembler.transform(data).select("features", "PriceOfUnitArea").withColumnRenamed("PriceOfUnitArea", "label")
data_split = df.randomSplit([0.9, 0.1])
train = data_split[0]
test = data_split[1]

tree = DecisionTreeRegressor(featuresCol='features', labelCol='label')
model = tree.fit(train)
predict = model.transform(test)
predict.select("label","prediction").show()


spark.stop()
