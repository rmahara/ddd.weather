using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using System.Data.SQLite;

namespace DDD.Infrastructure.SQLite
{
    public class WeatherSQLite : IWeatherRepository
    {
        public WeatherEntity GetLatest(int areaId) 
        {
            string sql = @"
select datadate, condition, temperature
from Weather
where areaid = @areaid
order by datadate desc
limit 1
";
            using (var connection = new SQLiteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@areaid", areaId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        return new WeatherEntity(
                            Convert.ToInt32(areaId),
                            Convert.ToDateTime(reader["DataDate"]),
                            Convert.ToInt32(reader["Condition"]),
                            Convert.ToSingle(reader["Temperature"]));
                    }
                }
            }
            return null;
        }
    }
}
