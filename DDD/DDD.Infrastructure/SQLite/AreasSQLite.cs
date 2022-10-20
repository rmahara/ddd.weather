using DDD.Domain.Entities;
using DDD.Domain.Repositories;

namespace DDD.Infrastructure.SQLite
{
    public sealed class AreasSQLite : IAreasRepository
    {
        public IReadOnlyList<AreaEntity> GetData()
        {
            string sql = @"
select areaid, areaname
from areas
";

            return SQLiteHelper.Query(sql,
                reader => 
                {
                    return new AreaEntity(
                        Convert.ToInt32(reader["AreaId"]),
                        Convert.ToString(reader["AreaName"]));
                });
        }
    }
}
