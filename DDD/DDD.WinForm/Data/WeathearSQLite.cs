using DDD.WinForm.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.Data
{
    public static class WeathearSQLite
    {
        public static DataTable GetLatest(int areaId) 
        {
            string sql = @"
select datadate, condition, temperature
from Weather
where areaid = @areaid
order by datadate desc
limit 1
";
            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection(CommonConst.ConnectionString))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@areaid", areaId);
                using (var adapter = new SQLiteDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
    }
}
