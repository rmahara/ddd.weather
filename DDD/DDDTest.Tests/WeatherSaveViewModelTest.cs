﻿using ChainingAssertion;
using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Repositories;
using DDD.WinForm.ViewModels;
using Moq;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherSaveViewModelTest
    {
        [TestMethod]
        public async Task 天気登録シナリオ() 
        {
            var weatherMock = new Mock<IWeatherRepository>();
            var areasMock = new Mock<IAreasRepository>();
            var areas = new List<AreaEntity>();
            areas.Add(new AreaEntity(1, "東京"));
            areas.Add(new AreaEntity(2, "神戸"));
            areasMock.Setup(x => x.GetData()).Returns(areas);

            var viewModelMock = new Mock<WeatherSaveViewModel>(weatherMock.Object, areasMock.Object);
            viewModelMock.Setup(x => x.GetDateTime()).Returns(
                Convert.ToDateTime("2018/1/1 12:34:56"));
            
            var viewModel = viewModelMock.Object;
            viewModel.SelectedAreaId.IsNull();
            viewModel.DataDateValue.Is(Convert.ToDateTime("2018/1/1 12:34:56"));
            viewModel.SelectedCondition.Is(1);
            viewModel.TemperatureTest.Is("");
            viewModel.TemperatureUnitName.Is("℃");

            viewModel.Areas.Count.Is(2);
            viewModel.Conditions.Count.Is(4);

            var ex = await ExceptionAssert.ThrowsAsync<InputException>(() => viewModel.SaveAsync());
            ex.Message.Is("エリアを選択してください");

            viewModel.SelectedAreaId = 2;
            ex = await ExceptionAssert.ThrowsAsync<InputException>(() => viewModel.SaveAsync());
            ex.Message.Is("温度の入力に誤りがあります");

            viewModel.TemperatureTest = "12.345";

            weatherMock.Setup(x => x.Save(It.IsAny<WeatherEntity>())).
                Callback<WeatherEntity>(saveValue => {
                    saveValue.AreaId.Value.Is(2);
                    saveValue.DataDate.Is(Convert.ToDateTime("2018/1/1 12:34:56"));
                    saveValue.Condition.Value.Is(1);
                    saveValue.Temperature.Value.Is(12.345f);
                });

            await viewModel.SaveAsync();
            //Save系のテストをする際は必ずVerifyAllを入れる
            weatherMock.VerifyAll();
        }
    }
}
