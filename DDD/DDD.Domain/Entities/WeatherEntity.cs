using DDD.Domain.ValueObjects;
using System.Data;

namespace DDD.Domain.Entities
{
    public sealed class WeatherEntity
    {
        //完全コンストラクタパターン
        public WeatherEntity(int areaId,
            DateTime dataDate,
            int condition,
            float temperature)
        {
            AreaId = areaId;
            DataDate = dataDate;
            Condition = condition;
            Temperature = new Temperature(temperature);
        }
        public int AreaId { get; }
        public DateTime DataDate { get; }
        public int Condition { get; }
        public Temperature Temperature { get; }

        public bool IsOK() 
        {
            if (DataDate < DateTime.Now.AddMonths(-1)) 
            {
                if (Temperature.Value < 10) 
                {
                    return false;
                }
            }

            return true;
        }
    }
}
