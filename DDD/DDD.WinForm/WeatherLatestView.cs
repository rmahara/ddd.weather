using System.Data;
using System.Data.SQLite;

namespace DDD.WinForm
{
    public partial class WeatherLatestView : Form
    {
        private readonly string ConnectionString = @"Data Source=C:\Users\09689\Desktop\DDD.db;Version=3";

        public WeatherLatestView()
        {
            InitializeComponent();
        }

        private void LatestButton_Click(object sender, EventArgs e)
        {
            string sql = @"
select datadate, condition, temperature
from Weather
where areaid = @areaid
order by datadate desc
limit 1
";
            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection(ConnectionString)) 
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@areaid", this.AreaIdTextBox.Text);
                using (var adapter = new SQLiteDataAdapter(command)) 
                {
                    adapter.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0) 
            {
                DataDateLabel.Text = dt.Rows[0]["datadate"].ToString();
                ConditionLabel.Text = dt.Rows[0]["condition"].ToString();
                TempetureLabel.Text =
                    Math.Round(Convert.ToSingle(dt.Rows[0]["temperature"].ToString()), 2) + "Åé";
            }
        }
    }
}