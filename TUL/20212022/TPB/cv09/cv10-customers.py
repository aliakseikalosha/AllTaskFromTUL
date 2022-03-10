from pyflink.table import EnvironmentSettings, TableEnvironment, StreamTableEnvironment, TableDescriptor, DataTypes, Schema
from pyflink.datastream import StreamExecutionEnvironment
from pyflink.table.udf import udf
from pyflink.common import Row


def map_function(a: Row) -> Row:
    return Row(a[0], round(a[1], 2))

def spend_count():
    print("Batch")
    env = EnvironmentSettings.in_batch_mode()
    t_env = TableEnvironment.create(env)

    round_total = udf(map_function, result_type=DataTypes.ROW(
        [DataTypes.FIELD("customerID", DataTypes.INT()),
         DataTypes.FIELD("total_spent", DataTypes.FLOAT())]))

    td = TableDescriptor.for_connector("filesystem") \
        .schema(Schema.new_builder()
                .column("customerID", DataTypes.INT())
                .column("itemID", DataTypes.INT())
                .column("cost", DataTypes.FLOAT())
                .build()
                ) \
        .option("path", "customer-orders.csv") \
        .format("csv") \
        .build()
    t_env.create_temporary_table("source", td)
    table = t_env.from_path("source")
    spent = table.group_by(table.customerID)\
        .select(table.customerID, table.cost.sum.alias('total_spent'))\
        .map(round_total).alias('customerID','total_spent')
    sorted = spent.order_by(spent.total_spent.desc)
    out = TableDescriptor.for_connector("print").schema(Schema.new_builder()
                                                        .column("customerID", DataTypes.INT())
                                                        .column("total_spent", DataTypes.FLOAT())
                                                        .build()).build()
    t_env.create_temporary_table("sink", out)
    sorted.execute_insert('sink').wait()


if __name__ == "__main__":
    spend_count()
