using ChainingAssertion;
using DDD.WinForm.ViewModels;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherSaveViewModelTest
    {
        [TestMethod]
        public void 天気登録シナリオ() 
        {
            var viewModel = new WeatherSaveViewModel();
            viewModel.SelectedAreaId.IsNull();
            viewModel.DataDateValue.Is(Convert.ToDateTime("2018/1/1 12:34:56"));
            viewModel.SelectedCondition.Is(1);
            viewModel.TemperatureTest.Is("");

        }
    }
}
