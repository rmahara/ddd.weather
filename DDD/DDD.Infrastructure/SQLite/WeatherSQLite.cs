using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using System.Data.SQLite;

namespace DDD.Infrastructure.SQLite
{
    public class WeatherSQLite : IWeatherRepository
    {
        public IReadOnlyList<WeatherEntity> GetData()
        {
            throw new NotImplementedException();
        }

        public WeatherEntity GetLatest(int areaId) 
        {
            string sql = @"
select datadate, condition, temperature
from Weather
where areaid = @areaid
order by datadate desc
limit 1
";

            return SQLiteHelper.QuerySingle<WeatherEntity>(
                sql,
                new List<SQLiteParameter> 
                {
                    new SQLiteParameter("@areaid", areaId),

                }.ToArray(),
                reader =>
                {
                    return new WeatherEntity(
                            Convert.ToInt32(areaId),
                            Convert.ToDateTime(reader["DataDate"]),
                            Convert.ToInt32(reader["Condition"]),
                            Convert.ToSingle(reader["Temperature"]));
                }, null);
        }
    }
}
