﻿using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Domain.ValueObjects;
using System.Data.SQLite;

namespace DDD.Infrastructure.SQLite
{
    public class WeatherSQLite : IWeatherRepository
    {
        public IReadOnlyList<WeatherEntity> GetData()
        {
            string sql = @"
select A.areaId, ifnull(B.areaName, '') as areaName, A.dataDate, A.condition, A.temperature
from Weather A
left outer join Areas B
on A.areaId = B.areaId
";
            return SQLiteHelper.Query(sql,
                reader => {
                    return new WeatherEntity(
                            Convert.ToInt32(reader["areaid"]),
                            Convert.ToString(reader["areaName"]),
                            Convert.ToDateTime(reader["DataDate"]),
                            Convert.ToInt32(reader["Condition"]),
                            Convert.ToSingle(reader["Temperature"]));
                });
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

        public void Save(WeatherEntity weather)
        {
            string insert = @"
insert into weather
(areaid, datadate, condition, temperature)
values
(@areaid, @datadate, @condition, @temperature);
";
            string update = @"
update weather set
  condition = @condition
, temperature = @temperature
where areaid = @areaid
and datadate = @datadate;
";
            var args = new List<SQLiteParameter>()
            {
                new SQLiteParameter("@areaid", weather.AreaId.Value),
                new SQLiteParameter("@datadate", weather.DataDate),
                new SQLiteParameter("@condition", weather.Condition.Value),
                new SQLiteParameter("@temperature", weather.Temperature.Value),
            };

            SQLiteHelper.Execute(insert, update, args.ToArray());
        }
    }
}
