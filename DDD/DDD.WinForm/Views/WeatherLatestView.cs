using DDD.WinForm.Common;
using DDD.WinForm.Data;
using System.Data;
using System.Data.SQLite;

namespace DDD.WinForm
{
    public partial class WeatherLatestView : Form
    {
        
        public WeatherLatestView()
        {
            InitializeComponent();
        }

        private void LatestButton_Click(object sender, EventArgs e)
        {
            var dt = WeathearSQLite.GetLatest(Convert.ToInt32(AreaIdTextBox.Text));

            if (dt.Rows.Count > 0) 
            {
                DataDateLabel.Text = dt.Rows[0]["datadate"].ToString();
                ConditionLabel.Text = dt.Rows[0]["condition"].ToString();
                TemperatureLabel.Text =
                    CommonFunc.RoundString(
                        Convert.ToSingle(dt.Rows[0]["temperature"].ToString()),
                        CommonConst.TemperatureDecimalPoint) + CommonConst.TemperatureUnitName;
            }
        }
    }
}