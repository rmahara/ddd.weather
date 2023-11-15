using DDD.Domain.Entities;
using DDD.Domain.Helpers;
using DDD.Domain.Repositories;
using DDD.Domain.ValueObjects;
using DDD.Infrastructure.SQLite;
using DDD.WPF.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DDD.WPF.ViewModels
{
    public class WeatherSaveViewModel : ViewModelBase, IDialogAware
    {

        private IWeatherRepository _wather;
        private IAreasRepository _areasRepository;
        private IMessageService _messageServices;

        public WeatherSaveViewModel()
            : this(new WeatherSQLite(),
                  new AreasSQLite(),
                  new MessageService())
        {
        }

        public WeatherSaveViewModel(
            IWeatherRepository wather,
            IAreasRepository areas,
            IMessageService messageServices)
        {
            _wather = wather;
            _areasRepository = areas;
            _messageServices = messageServices;

            DataDateValue = GetDateTime();
            SelectedCondition = Condition.Sunny;
            TemperatureText = string.Empty;

            foreach (var area in _areasRepository.GetData())
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }

            SaveButton = new DelegateCommand(SaveButtonExecute);
        }


        public string Title => "登録画面";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        private AreaEntity _selectedArea;
        public AreaEntity SelectedArea
        {
            get => _selectedArea;
            set => SetProperty(ref _selectedArea, value);
        }
        public DateTime? DataDateValue { get; set; }
        private Condition _selectedCondition;
        public Condition SelectedCondition
        {
            get => _selectedCondition;
            set => SetProperty(ref _selectedCondition, value);
        }
        private string _temperatureText;
        public string TemperatureText
        {
            get => _temperatureText;
            set => SetProperty(ref _temperatureText, value);
        }

        private ObservableCollection<AreaEntity> _areas = new ObservableCollection<AreaEntity>();
        public ObservableCollection<AreaEntity> Areas
        {
            get => _areas;
            set => SetProperty(ref _areas, value);
        }
        private ObservableCollection<Condition> _conditions =
            new ObservableCollection<Condition>(Condition.ToList());
        public ObservableCollection<Condition> Conditions
        {
            get => _conditions;
            set => SetProperty(ref _conditions, value);
        }

        public string TemperatureUnitName => Temperature.UnitName;

        public void SaveButtonExecute()
        {
            Guard.IsNull(SelectedArea, "エリアを選択してください");
            Guard.IsNull(DataDateValue, "日時を入力してください");

            var temperature =
                Guard.IsFloat(TemperatureText, "温度の入力に誤りがあります");

            if (_messageServices.Question(
                "保存しますか？") != System.Windows.MessageBoxResult.OK)
            {
                return;
            }

            var entity = new WeatherEntity(
                SelectedArea.AreaId,
                DataDateValue.Value,
                SelectedCondition.Value,
                temperature);

            _wather.Save(entity);

            _messageServices.ShowDialog("保存しました");
        }

        public DelegateCommand SaveButton { get; }
    }
}
