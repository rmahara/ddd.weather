﻿using DDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.SQLite
{
    internal class SQLiteHelper
    {
        internal const string ConnectionString = @"Data Source=D:\@git\DDD\DDD\DDD.db;Version=3";

        internal static IReadOnlyList<T> Query<T>(
            string sql,
            Func<SQLiteDataReader, T> createEntity) 
        {
            var result = new List<T>();
            using (var connection = new SQLiteConnection(SQLiteHelper.ConnectionString))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(createEntity(reader));
                    }
                }
            }
        }
    }
}
