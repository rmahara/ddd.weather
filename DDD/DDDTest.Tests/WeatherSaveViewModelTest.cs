﻿using ChainingAssertion;
using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.WinForm.ViewModels;
using Moq;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherSaveViewModelTest
    {
        [TestMethod]
        public void 天気登録シナリオ() 
        {
            var areasMock = new Mock<IAreasRepository>();
            var areas = new List<AreaEntity>();
            areas.Add(new AreaEntity(1, "東京"));
            areas.Add(new AreaEntity(2, "神戸"));
            areasMock.Setup(x => x.GetData()).Returns(areas);

            var viewModelMock = new Mock<WeatherSaveViewModel>(areasMock.Object);
            viewModelMock.Setup(x => x.GetDateTime()).Returns(
                Convert.ToDateTime("2018/1/1 12:34:56"));
            
            var viewModel = viewModelMock.Object;
            viewModel.SelectedAreaId.IsNull();
            viewModel.DataDateValue.Is(Convert.ToDateTime("2018/1/1 12:34:56"));
            viewModel.SelectedCondition.Is(1);
            viewModel.TemperatureTest.Is("");

            viewModel.Areas.Count.Is(2);
            viewModel.Conditions.Count.Is(4);
        }
    }
}
