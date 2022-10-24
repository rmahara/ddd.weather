using DDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.WinForm.ViewModels
{
    public sealed class WeatherListViewModelWeather : ViewModelBase
    {
        private WeatherEntity entity;
        public WeatherListViewModelWeather(WeatherEntity entity)
        {
            this.entity = entity;
        }
    }
}
