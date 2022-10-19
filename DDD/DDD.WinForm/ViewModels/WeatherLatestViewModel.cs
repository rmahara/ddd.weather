using DDD.Domain.Repositories;
using DDD.WinForm.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.ViewModels
{
    public class WeatherLatestViewModel
    {
        private IWeatherRepository _weather;

        public WeatherLatestViewModel(IWeatherRepository weather)
        {
            _weather = weather;
        }

        public string AreaIdText { get; set; } = string.Empty;
        public string DataDateText { get; set; } = string.Empty;
        public string ConditionText { get; set; } = string.Empty;
        public string TemperatureText { get; set; } = string.Empty;

        public void Search()
        {
            var dt = _weather.GetLatest(Convert.ToInt32(AreaIdText));
            if (dt.Rows.Count > 0)
            {
                DataDateText = dt.Rows[0]["datadate"].ToString();
                ConditionText = dt.Rows[0]["condition"].ToString();
                TemperatureText =
                    CommonFunc.RoundString(
                        Convert.ToSingle(dt.Rows[0]["temperature"].ToString()),
                        CommonConst.TemperatureDecimalPoint) + " " + CommonConst.TemperatureUnitName;
            }
        }
    }
}
