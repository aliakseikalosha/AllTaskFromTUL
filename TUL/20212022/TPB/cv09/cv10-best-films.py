from pyflink.table import *
from pyflink.datastream import StreamExecutionEnvironment
from pyflink.common import Row
from pyflink.table.udf import udf


def count_percent(a: Row) -> Row:
    return Row(a[0], (a[1] * 100.0) / a[2])


def find_best_movies():
    print("Batch")
    env = EnvironmentSettings.in_batch_mode()  # in_streaming_mode()
    t_env = TableEnvironment.create(env)

    calculate_ratio = udf(count_percent, result_type=DataTypes.ROW(
        [DataTypes.FIELD("MovieID", DataTypes.INT()),
         DataTypes.FIELD("Ratio", DataTypes.FLOAT())]))

    tdm = TableDescriptor.for_connector("filesystem") \
        .schema(Schema.new_builder()
                .column("UserID", DataTypes.INT())
                .column("MovieID", DataTypes.INT())
                .column("Rating", DataTypes.INT())
                .column("Timestamp", DataTypes.BIGINT())
                .build()
                ) \
        .option("path", "u.data") \
        .format("csv") \
        .format(FormatDescriptor.for_format("csv").option("field-delimiter", "\t").build()) \
        .build()
    tdn = TableDescriptor.for_connector("filesystem") \
        .schema(Schema.new_builder()
                .column("MovieID", DataTypes.INT())
                .column("Name", DataTypes.STRING())
                .build()) \
        .option("path", "u-mod.item") \
        .format(FormatDescriptor.for_format("csv").option("field-delimiter", "|").build()) \
        .build()
    t_env.create_temporary_table("source", tdm)
    movies = t_env.from_path("source")
    t_env.create_temporary_table("source_name", tdn)
    names = t_env.from_path("source_name")
    rating = movies.group_by(movies.MovieID)\
        .select(movies.MovieID.alias('MID'), movies.Rating.count.alias('NumberOfRating'))

    five_star_rating = movies.where(movies.Rating == 5)\
        .group_by(movies.MovieID).select(movies.MovieID, movies.Rating.count.alias("NumberOfFiveStars"))

    top10 = five_star_rating.order_by(five_star_rating.NumberOfFiveStars.desc).fetch(10)
    top10 = top10.join(rating).where(rating.MID == top10.MovieID)\
        .select(top10.MovieID, top10.NumberOfFiveStars, rating.NumberOfRating)

    top10 = top10.map(calculate_ratio).alias('MID', 'Ratio')

    top10_named = top10.join(names)\
        .where(top10.MID == names.MovieID)\
        .select(names.MovieID, names.Name, top10.Ratio)\
        .order_by(top10.Ratio.desc)

    out = TableDescriptor.for_connector("print").schema(Schema.new_builder()
                                                        .column("MovieID", DataTypes.INT())
                                                        .column("Name", DataTypes.STRING())
                                                        .column("Ratio", DataTypes.FLOAT())
                                                        .build()).build()
    print('create sink')
    t_env.create_temporary_table("sink", out)
    print('wait')
    top10_named.execute_insert('sink').wait()


if __name__ == "__main__":
    find_best_movies()
