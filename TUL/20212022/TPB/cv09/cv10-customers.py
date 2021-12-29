from pyflink.table import EnvironmentSettings, TableEnvironment, StreamTableEnvironment, TableDescriptor, DataTypes, Schema
from pyflink.datastream import StreamExecutionEnvironment


def spend_count():
    print("Batch")
    env = EnvironmentSettings.in_stream_mode()
    t_env = TableEnvironment.create(env)

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
    spent = table.group_by(table.customerID).select(table.customerID, table.cost.sum.alias('total_spent'))
    out = TableDescriptor.for_connector("print").schema(Schema.new_builder()
                                                        .column("customerID", DataTypes.INT())
                                                        .column("total_spent", DataTypes.FLOAT())
                                                        .build()).build()
    t_env.create_temporary_table("sink", out)
    spent.execute_insert('sink').wait()


def spend_count_stream():
    print("Stream")
    env = StreamExecutionEnvironment.get_execution_environment()
    t_env = StreamTableEnvironment.create(env)

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
    spent = table.group_by(table.customerID).select(table.customerID, table.cost.sum.alias('total_spent'))
    sorted_spent = spent.order_by(spent.total_spent.desc)
    out = TableDescriptor.for_connector("print").schema(Schema.new_builder()
                                                        .column("customerID", DataTypes.INT())
                                                        .column("total_spent", DataTypes.FLOAT())
                                                        .build()).build()
    t_env.create_temporary_table("sink", out)
    sorted_spent.execute_insert('sink').wait()


if __name__ == "__main__":
    spend_count()
    # spend_count_stream()
