from pyspark import SparkConf, SparkContext

# spark-submit total-spent-by-customer.py
conf = SparkConf().setMaster("spark://05710b836584:7077").setAppName("TotalSpentByCustomer")
# conf = SparkConf().setMaster("local").setAppName("MinTemperatures")
sc = SparkContext(conf=conf)


def parseLine(line):
    fields = line.split(',')
    custome_id = fields[0]
    price = fields[2]
    return (custome_id, price)


lines = sc.textFile("/files/customer-orders.csv")
spent_by_customer = lines.map(parseLine)\
    .reduceByKey(lambda x, y: float(x) + float(y))\
    .map(lambda x: (x[1], x[0]))\
    .sortByKey(ascending=False)\
    .map(lambda x: (x[1], x[0]))
results = spent_by_customer.collect()

for customer_id, spending in results:
    print(customer_id, spending)
