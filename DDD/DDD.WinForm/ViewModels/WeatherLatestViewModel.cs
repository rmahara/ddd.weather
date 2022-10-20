using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastructure.SQLite;
using System.ComponentModel;

namespace DDD.WinForm.ViewModels
{
    public class WeatherLatestViewModel : ViewModelBase
    {
        private IWeatherRepository _weather;
        private IAreasRepository _areas;

        public WeatherLatestViewModel() 
            : this(new WeatherSQLite(), null)
        {
        }

        public WeatherLatestViewModel(IWeatherRepository weather,
            IAreasRepository areasRepository)
        {
            _weather = weather;
            _areas = areasRepository;

            foreach (var area in _areas.GetData()) 
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }
        }

        private string _areaIdText = string.Empty;
        public string AreaIdText {
            get => _areaIdText;
            set 
            {
                SetProperty(ref _areaIdText, value);
            } 
        }
        private string _dataDateText = string.Empty;
        public string DataDateText
        {
            get => _dataDateText;
            set
            {
                SetProperty(ref _dataDateText, value);
            }
        }
        private string _conditionText = string.Empty;
        public string ConditionText
        {
            get => _conditionText;
            set
            {
                SetProperty(ref _conditionText, value);
            }
        }
        private string _temperatureText = string.Empty;
        public string TemperatureText
        {
            get => _temperatureText;
            set
            {
                SetProperty(ref _temperatureText, value);
            }
        }

        public BindingList<AreaEntity> Areas { get; set; }
            = new BindingList<AreaEntity>();

        public void Search()
        {
            var entity = _weather.GetLatest(Convert.ToInt32(AreaIdText));
            if (entity != null)
            {
                DataDateText = entity.DataDate.ToString();
                ConditionText = entity.Condition.DisplayValue;
                TemperatureText = entity.Temperature.DisplayValueWithUnitSpace;
            }
        }
    }
}
