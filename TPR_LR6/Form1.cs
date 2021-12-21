using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Data.OleDb;
using System.Windows.Forms.DataVisualization.Charting;


namespace TPR_LR6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }

        private void brnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //this.stocksBindingSource = new System.Windows.Forms.BindingSource(this.components);
                //this.stocksTableAdapter = new TPR_LR6.DatabaseTableAdapters.StocksTableAdapter();
                MessageBox.Show("Your data has been saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "database.Stocks". При необходимости она может быть перемещена или удалена.
            //this.stocksTableAdapter.Fill(this.database.Stocks);
            dt = new DataTable();
            comboBox1.SelectedItem = comboBox1.Items[1];
            comboBox1.Visible = false;
            lblType.Visible = false;
            domainUpDown1.Visible = false;
            //chart1.Legends[0]
            groupBox2.Visible = false;
            btnEase.Visible = false;
            groupBox3.Visible = false;
            btnClear.Visible = false;

            btnLoad.Visible = false;
            brnSave.Visible = false;



            //dlya data grid view
            dataGridView1.Visible = false;

        }

        private DataTable dt;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            



            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;

            chart1.Series["Daily"].XValueMember = "<DATE>";
            chart1.Series["Daily"].YValueMembers = "<HIGH>,<LOW>,<OPEN>,<CLOSE>";
            chart1.Series["Daily"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            chart1.Series["Daily"].CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";
            //chart1.Series["Daily"]["OpenCloseStyle"] = "Triangle";
            chart1.Series["Daily"]["ShowOpenClose"] = "Both";
            chart1.DataManipulator.IsStartFromFirst = true;

            chart1.DataSource = dt;
            chart1.DataBind();

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            //List<DataStocks> dataForDB = Read(filename);
            List<string[]> rows = File.ReadAllLines(filename).Select(x => x.Split(',')).ToList();
            //dataGridView1.RowCount = rows.Count();
            // добавить столбцы в  dataGridView
            for (int i = 0; i < rows[0].Length; i++)
            {
                string columnName = rows[0][i].ToString();
                dt.Columns.Add(columnName);
                //dataGridView1.Columns.Add(columnName, columnName);
            }
            for (int i = 1; i < rows.Count; i++)
            {
                //dataGridView1.Rows.Add(rows[i]);
                dt.Rows.Add(rows[i]);
            }
            int k = -1;
            for (k = 0; k<dt.Columns.Count; ++k)
            {
                if (dt.Columns[k].Caption == "<DATE>")
                    break;
            }
            this.dataGridView1.DataSource = dt;
            for (int j = 0; j<dt.Rows.Count; ++j)
            {
                dataGridView1[k, j].Value = convertString(Convert.ToString(dataGridView1[k, j].Value));
            }
            //MessageBox.Show(rows[0][0]);
            //test();

            try
            {
                lblType.Visible = true;
                comboBox1.Visible = true;
                groupBox2.Visible = true;
                groupBox3.Visible = true;
                btnEase.Visible = true;
                btnClear.Visible = true;
                
                //this.stocksBindingSource = new System.Windows.Forms.BindingSource(this.components);
                //this.stocksTableAdapter = new TPR_LR6.DatabaseTableAdapters.StocksTableAdapter();
                //MessageBox.Show("Your data has been saved", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 1;
                chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 1;
                chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;

                chart1.ChartAreas["ChartArea2"].AxisX.MajorGrid.LineWidth = 1;
                chart1.ChartAreas["ChartArea2"].AxisY.MajorGrid.LineWidth = 1;
                chart1.ChartAreas["ChartArea2"].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart1.ChartAreas["ChartArea2"].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart1.ChartAreas["ChartArea2"].AxisX.MajorGrid.LineColor = Color.LightGray;
                chart1.ChartAreas["ChartArea2"].AxisY.MajorGrid.LineColor = Color.LightGray;

                chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;

                chart1.ChartAreas["ChartArea2"].AxisX.Interval = 1;
                chart1.ChartAreas["ChartArea2"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
                //chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffset = 1;

                chart1.Series["Daily"].XValueMember = "<DATE>";
                chart1.Series["Daily"].YValueMembers = "<HIGH>,<LOW>,<OPEN>,<CLOSE>";
                chart1.Series["Daily"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Daily"].CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";
                //chart1.Series[1].CustomProperties = "PriceDownColor=Red,PriceUpColor=Green";

                chart1.Series["Volume"].XValueMember = "<DATE>";
                chart1.Series["Volume"].YValueMembers = "<VOL>";
                chart1.Series["Volume"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
                chart1.Series["Volume"].CustomProperties = "Color=Blue";

                //chart1.Series["Daily"]["OpenCloseStyle"] = "Triangle";
                chart1.Series["Daily"]["ShowOpenClose"] = "Both";
                chart1.DataManipulator.IsStartFromFirst = true;

                chart1.DataSource = dt;
                chart1.DataBind();

                chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
                // chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.BackColor = Color.Black;
                chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonColor = Color.LightGray;
                chart1.ChartAreas["ChartArea1"].AxisY.ScrollBar.ButtonColor = Color.LightGray;
                chart1.ChartAreas["ChartArea1"].AxisY.ScaleView.Zoomable = true;
                //chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(0, 100);
                chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.SmallScrollSize = 10;
                chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
                chart1.ChartAreas["ChartArea1"].CursorX.AutoScroll = false;
                chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
                chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;

                chart1.MouseWheel += chart1_MouseWheel;

                chart1.ChartAreas["ChartArea2"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.None;
                chart1.ChartAreas["ChartArea2"].AxisX.ScrollBar.Enabled = false;
                chart1.ChartAreas["ChartArea2"].CursorX.AutoScroll = false;
                chart1.ChartAreas["ChartArea2"].CursorX.IsUserEnabled = false;
                chart1.ChartAreas["ChartArea2"].CursorX.IsUserSelectionEnabled = false;

                //chart1.ChartAreas["ChartArea2"].AxisX.ScrollBar.IsPositionedInside = true;


                string fileName = Path.GetFileNameWithoutExtension(filename);
                string[] nn = fileName.Split('_');
                char[] tmp = nn[1].ToCharArray();
                string date_start = "";
                date_start = string.Join("",tmp[4],tmp[5]);
                date_start += ".";
                date_start += string.Join("", tmp[2], tmp[3]);
                date_start += ".";
                date_start += string.Join("", tmp[0], tmp[1]);

                tmp = nn[2].ToCharArray();
                string date_end = "";
                date_end = string.Join("", tmp[4], tmp[5]);
                date_end += ".";
                date_end += string.Join("", tmp[2], tmp[3]);
                date_end += ".";
                date_end += string.Join("", tmp[0], tmp[1]);

                chart1.Legends[0].Title = "Name : " + nn[0] + "\nDate start: " + date_start + "\nDate end: " + date_end;

                chart1.ChartAreas["ChartArea1"].Position.Y = 0;
                chart1.ChartAreas["ChartArea1"].Position.X = 0;
                chart1.ChartAreas["ChartArea1"].Position.Height = (float)(70);
                chart1.ChartAreas["ChartArea2"].Position.Y = 70;
                chart1.ChartAreas["ChartArea2"].Position.X = 0;
                chart1.ChartAreas["ChartArea2"].Position.Height = (float)(30);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                   
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    //var xMin = xAxis.ScaleView.ViewMinimum;
                    //var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    //var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    //var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    //xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        public string convertString(string s)
        {
            char[] tmp = s.ToCharArray();
            string date_end = "";
            date_end = string.Join("", tmp[6], tmp[7]);
            date_end += ".";
            date_end += string.Join("", tmp[4], tmp[5]);
            date_end += ".";
            date_end += string.Join("", tmp[0], tmp[1],tmp[2], tmp[3]);
            return date_end;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            domainUpDown1.Visible = false;
            //chart1.Series.Remove(Series )
            while (chart1.Series.Count >2)
            {
                chart1.Series.RemoveAt(2);
            }
            if (comboBox1.SelectedIndex == 0)
            {
                
                chart1.Series["Daily"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Stock;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                chart1.Series["Daily"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                chart1.Series["Daily"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //chart1.Series["Daily"].YValuesPerPoint = 10;

                domainUpDown1.Visible = true;
                List<Color> col = new List<Color>();
                col.Add(Color.Cyan);
                col.Add(Color.Blue);
                col.Add(Color.DarkCyan);
                col.Add(Color.DarkBlue);

                for (int i = 2; i < 6; ++i)
                {
                    Series lin = new Series();
                    chart1.Series.Add(lin);
                    int cnt = chart1.Series.Count;
                    if (i==2)
                        chart1.Series[cnt - 1].Color = Color.Red;
                    else 
                        chart1.Series[cnt - 1].Color = col[i - 2];
                    chart1.Series[cnt-1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series[cnt - 1].ChartArea = "ChartArea1";
                    chart1.Series[cnt-1].Legend = "Legend1";
                    chart1.Series[cnt - 1].LegendText = dataGridView1.Columns[i].HeaderText;
                    for (int j =0; j < dataGridView1.RowCount; ++j)
                    {
                        chart1.Series[cnt - 1].Points.AddXY(0, dataGridView1[i, j].Value);
                        //chart1.Series[cnt - 1].Points.AddY(dataGridView1[i, j].Value);
                        //chart1.Series[cnt-1].XValueMember = dt.Columns[0].ColumnName;
                    }

                }

                //foreach (Temperature t in temperatureBindingSource.DataSource as List<Temperature>)
                //{
                //    chart1.Series.Add(t.Location);
                //    chart1.Series[t.Location].Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                //    chart1.Series[t.Location].Legend = "Legend1";
                //    chart1.Series[t.Location].ChartArea = "ChartArea1";
                //    chart1.Series[t.Location].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    for (int i = 1; i <= 12; i++)
                //        chart1.Series[t.Location].Points.AddXY(i, Convert.ToInt32(t[$"M{i}"]));

                //}
               
            }
            chart1.Update();
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            //MessageBox.Show("AVG:" + GetAverage(chart1, chart1.Series[0], e).ToString());

            if (e.Axis != chart1.ChartAreas["ChartArea1"].AxisY)
                if (e.ChartArea == chart1.ChartAreas["ChartArea1"])
                {
                    chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.Position = e.NewPosition;
                    chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.Size = e.NewSize;
                    chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.SizeType = e.NewSizeType;
                }


            //chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            //richTextBox1.Text = "";
            //{
            //    ChartArea CA = chart1.ChartAreas[0];
            //    Series S = chart1.Series[0];
            //    double min = CA.AxisX.ScaleView.ViewMinimum;
            //    double max = CA.AxisX.ScaleView.ViewMaximum;

            //    // these are the respective DataPoints:
            //    DataPoint pt0 = S.Points.Select(x => x)
            //                     .Where(x => x.XValue >= min)
            //                     .DefaultIfEmpty(S.Points.First()).First();
            //    DataPoint pt1 = S.Points.Select(x => x)
            //                     .Where(x => x.XValue <= max)
            //                     .DefaultIfEmpty(S.Points.Last()).Last();

            //    // test output:
            //    for (int i = S.Points.IndexOf(pt0); i < S.Points.IndexOf(pt1); i++)
            //        richTextBox1.Text += (i + " :  " + S.Points[i] + "\n");
            //}


        }

        double GetAverage(Chart chart, Series series, ViewEventArgs e)
        {
            ChartArea CA = e.ChartArea;  // short..
            Series S = series;           // references  

            DataPoint pt0 = S.Points.Select(x => x)
                                    .Where(x => x.XValue >= CA.AxisX.ScaleView.ViewMinimum)
                                    .DefaultIfEmpty(S.Points.First()).First();
            DataPoint pt1 = S.Points.Select(x => x)
                                    .Where(x => x.XValue <= CA.AxisX.ScaleView.ViewMaximum)
                                    .DefaultIfEmpty(S.Points.Last()).Last();
            double sum = 0;
            for (int i = S.Points.IndexOf(pt0); i < S.Points.IndexOf(pt1); i++)
                sum += S.Points[i].YValues[0];

            return sum / (S.Points.IndexOf(pt1) - S.Points.IndexOf(pt0) + 1);
        }

        double X1;
        double X2;
        private void chart1_SelectionRangeChanged(object sender, CursorEventArgs e)
        {
            //richTextBox1.Text = "";
            {
                //ChartArea CA = chart1.ChartAreas["ChartArea1"];
                //Series S = chart1.Series["Daily"];
                //double min = CA.AxisX.ScaleView.ViewMinimum;
                //double max = CA.AxisX.ScaleView.ViewMaximum;

                

                //DataPoint pt0 = S.Points.Select(x => x)
                //                 .Where(x => x.XValue >= min)
                //                 .DefaultIfEmpty(S.Points.First()).First();
                //DataPoint pt1 = S.Points.Select(x => x)
                //                 .Where(x => x.XValue <= max)
                //                 .DefaultIfEmpty(S.Points.Last()).Last();

                //MessageBox.Show(S.Points.IndexOf(pt0).ToString() + " " + S.Points.IndexOf(pt1).ToString());

                
                //for (int i = S.Points.IndexOf(pt0); i < S.Points.IndexOf(pt1); i++)
                //    richTextBox1.Text += (i + " :  " + S.Points[i] + "\n");

            }

            //richTextBox1.Text += chart1.Series[0].Points[Convert.ToInt32(X1)].ToString() + "\n";

            if (Math.Abs(X2 - X1) >= 35)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
                chart1.ChartAreas["ChartArea2"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
            }
            else
            {
                chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
                chart1.ChartAreas["ChartArea2"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            }
            chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.Zoom(X1, X2);


        }

        private void chart1_SelectionRangeChanging(object sender, CursorEventArgs e)
        {
            X1 = e.NewSelectionStart; // or: chart1.ChartAreas[0].CursorX.SelectionStart;
            X2 = e.NewSelectionEnd;        // or: x2 = chart1.ChartAreas[0].CursorX.SelectionEnd;

            //richTextBox1.Text += "x1 = " + X1.ToString() + " x2 = " + X2.ToString() + "\n";
        }

        private void chart1_AxisScrollBarClicked(object sender, ScrollBarEventArgs e)
        {
            if (e.ButtonType == ScrollBarButtonType.ZoomReset)
            {
                chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
                chart1.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();

                chart1.ChartAreas["ChartArea2"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;
                chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.ZoomReset();
            }




        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            List<Color> col = new List<Color>();
                col.Add(Color.Cyan);
                col.Add(Color.Blue);
                col.Add(Color.DarkCyan);
                col.Add(Color.DarkBlue);

                for (int i = 2; i < 6; ++i)
                {
                    chart1.Series[i].Color = col[i - 2];
                }

            chart1.Series[domainUpDown1.SelectedIndex+2].Color = Color.Red;
            chart1.Update();
        }

        private void brnSliding_Click(object sender, EventArgs e)
        {
            clear();

            double mm = 1.0;
            double br = 1.0;
            double emv = 1.0;

            try
            {
                Series lin = new Series("ease");
                chart1.Series.Add(lin);
                chart1.Series["ease"].Color = Color.Coral;
                chart1.Series["ease"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["ease"].ChartArea = "ChartArea1";
                chart1.Series["ease"].Legend = "Legend1";
                chart1.Series["ease"].LegendText = "Ease of Movement";

                for (int i = 1; i < dataGridView1.RowCount; ++i)
                {
                    mm = (double.Parse(dataGridView1[3, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) - double.Parse(dataGridView1[4, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)) / 2;
                    mm -= (double.Parse(dataGridView1[3, i - 1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) - double.Parse(dataGridView1[4, i - 1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)) / 2;

                    br = (double.Parse(dataGridView1[6, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)/10000.0);
                    br /= (double.Parse(dataGridView1[3, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) - double.Parse(dataGridView1[4, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture));

                    emv = mm/br;
                    chart1.Series["ease"].Points.AddXY(0, emv);

                }
            }
            catch { }

        }

        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {
            clear();


            Series lin = new Series("slid");
            chart1.Series.Add(lin);
            chart1.Series["slid"].Color = Color.Purple;
            chart1.Series["slid"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["slid"].ChartArea = "ChartArea1";
            chart1.Series["slid"].Legend = "Legend1";
            chart1.Series["slid"].LegendText = "Sliding Average " + domainUpDown2.SelectedItem.ToString();
            if (rbSimple.Checked)
                chart1.Series["slid"].LegendText += " SMA";
            if (rbExp.Checked)
                chart1.Series["slid"].LegendText += " EMA";
            for (int j = 0; j < dataGridView1.RowCount; ++j)
            {
                if (rbSimple.Checked)
                {
                    chart1.Series["slid"].Points.AddXY(0, slidingAverage(j));
                }
                if (rbExp.Checked)
                {
                    try
                    {
                        //double cv = 2 / (j + 1);
                        //double ema = 0.0;
                        //if (domainUpDown2.SelectedIndex != 4)
                        //    ema = double.Parse(dataGridView1[domainUpDown2.SelectedIndex + 2, j].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) * cv + slidingAverage(j) * (1.0 - cv);
                        //else
                        //{
                        //    double asd = (double.Parse(dataGridView1[2, j].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) + double.Parse(dataGridView1[5, j].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)) / 2;
                        //    ema = asd * cv + slidingAverage(j) * (1.0 - cv);

                        //}
                        chart1.Series["slid"].Points.AddXY(0, emaAvg(j));
                    }
                    catch
                    {
                        //MessageBox.Show(domainUpDown2.SelectedItem.ToString());
                    }
                }
                //chart1.Series[cnt - 1].Points.AddY(dataGridView1[i, j].Value);
                //chart1.Series[cnt-1].XValueMember = dt.Columns[0].ColumnName;
            }
        }

        private double emaAvg(int k)
        {
            double cv;

            if (String.IsNullOrEmpty(textBox1.Text))
                cv = 2 / (k + 1);
            else {
                int per = Convert.ToInt32(textBox1.Text);
                cv = 2 / (per + 1);
            }
            double ema = 0.0;
            if (domainUpDown2.SelectedIndex != 4)
                ema = double.Parse(dataGridView1[domainUpDown2.SelectedIndex + 2, k].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) * cv + slidingAverage(k) * (1.0 - cv);
            else
            {
                double asd = (double.Parse(dataGridView1[2, k].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) + double.Parse(dataGridView1[5, k].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)) / 2;
                ema = asd * cv + slidingAverage(k) * (1.0 - cv);

            }

            return ema;
        }

        private double slidingAverage(int k, int smh = 0)
        {
            double ans = 0.0;

            if (smh == 0)
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {

                    {
                        for (int i = 0; i < k; ++i)
                        {
                            //ans += Convert.ToDouble(dataGridView1[5, k].Value.ToString());
                            if (domainUpDown2.SelectedIndex != 4)
                                ans += double.Parse(dataGridView1[domainUpDown2.SelectedIndex + 2, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                            else
                            {
                                double asd = (double.Parse(dataGridView1[2, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) + double.Parse(dataGridView1[5, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)) / 2;
                                ans += asd;
                            }
                        }
                        if (k != 0)
                            ans /= k;
                    }


                }
                else
                {
                    int per = Convert.ToInt32(textBox1.Text);
                    int i = 0;
                    if ((k - per) > 0)
                        i = k - per;
                    else
                        i = 0;
                    for (; i < k; ++i)
                    {
                        //ans += Convert.ToDouble(dataGridView1[5, k].Value.ToString());
                        if (domainUpDown2.SelectedIndex != 4)
                            ans += double.Parse(dataGridView1[domainUpDown2.SelectedIndex + 2, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        else
                        {
                            double asd = (double.Parse(dataGridView1[2, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) + double.Parse(dataGridView1[5, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)) / 2;
                            ans += asd;
                        }
                    }
                    if (k != 0)
                        ans /= per;
                }
            }
            else
            {
                int per = smh;
                int i = 0;
                if ((k - per) > 0)
                    i = k - per;
                else
                    i = 0;
                for (; i < k; ++i)
                {
                    ans += double.Parse(dataGridView1[5, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                }
                if (k != 0)
                    ans /= per;
            }


            //for (int i =2; i<6; ++i)
            //{
            //    ans += Convert.ToDouble(dataGridView1[i, k].Value);
            //}
            //ans /= 4;

            //for (int i = 0; i < k; ++i)
            //{
            //    double fg = 0.0;
            //    for (int j = 0; j < i; j++)
            //    {
            //        double d = Convert.ToDouble(dataGridView1[5, j].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);




            //        fg += d;
            //    }

            //    if (i != 0)
            //        ans += (fg / i);
            //    else
            //        ans += fg;
            //}
            //if (k != 0)
            //    ans /= k;

            return ans;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            chart1.ChartAreas["ChartArea2"].AxisX.ScaleView.ZoomReset();
            chart1.ChartAreas["ChartArea2"].AxisY.ScaleView.ZoomReset();
            chart1.ChartAreas["ChartArea2"].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Months;

            if (chart1.Series.Contains(chart1.Series.FindByName("slid")))
                chart1.Series.Remove(chart1.Series.FindByName("slid"));
            if (chart1.Series.Contains(chart1.Series.FindByName("ease")))
                chart1.Series.Remove(chart1.Series.FindByName("ease"));
            if (chart1.Series.Contains(chart1.Series.FindByName("OBV")))
                chart1.Series.Remove(chart1.Series.FindByName("OBV"));
            if (chart1.Series.Contains(chart1.Series.FindByName("PVT")))
                chart1.Series.Remove(chart1.Series.FindByName("PVT"));
            if (chart1.Series.Contains(chart1.Series.FindByName("Bol0")))
            {
                chart1.Series.Remove(chart1.Series.FindByName("Bol0"));
                chart1.Series.Remove(chart1.Series.FindByName("Bol1"));
                chart1.Series.Remove(chart1.Series.FindByName("Bol2"));
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)
                )
            {
                e.Handled = true;
            }
        }

        private void btnOBV_Click(object sender, EventArgs e)
        {
            clear();
            try
            {
                Series lin = new Series("OBV");
                chart1.Series.Add(lin);
                chart1.Series["OBV"].Color = Color.DarkRed;
                chart1.Series["OBV"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["OBV"].ChartArea = "ChartArea1";
                chart1.Series["OBV"].Legend = "Legend1";
                chart1.Series["OBV"].LegendText = "On Balance Volume";

                double obv = double.Parse(dataGridView1[6, 0].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                chart1.Series["OBV"].Points.AddXY(0, obv);
                for (int i = 1; i < dataGridView1.RowCount; ++i)
                {
                    double pt0 = double.Parse(dataGridView1[5, i - 1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    double pt1 = double.Parse(dataGridView1[5, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    if (pt1 > pt0)
                        obv += double.Parse(dataGridView1[6, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    else if (pt1 < pt0)
                        obv -= double.Parse(dataGridView1[6, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    chart1.Series["OBV"].Points.AddXY(0, obv);
                }
            }
            catch { }


            
        }

        private void btnPVT_Click(object sender, EventArgs e)
        {
            clear();
            try
            {
                Series lin = new Series("PVT");
                chart1.Series.Add(lin);
                chart1.Series["PVT"].Color = Color.Indigo;
                chart1.Series["PVT"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["PVT"].ChartArea = "ChartArea1";
                chart1.Series["PVT"].Legend = "Legend1";
                chart1.Series["PVT"].LegendText = "Price and Volume Trend";
                //chart1.Series["PVT"].BorderDashStyle = ChartDashStyle.Dash;

                double pvt = double.Parse(dataGridView1[6, 0].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                chart1.Series["PVT"].Points.AddXY(0, pvt);
                for (int i = 1; i < dataGridView1.RowCount; ++i)
                {
                    double pt0 = double.Parse(dataGridView1[5, i - 1].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    double pt1 = double.Parse(dataGridView1[5, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    pvt += (pt1-pt0)* double.Parse(dataGridView1[6, i].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)/pt0;
                    chart1.Series["PVT"].Points.AddXY(0, pvt);
                }
            }
            catch { }
        }

        private void btnBol_Click(object sender, EventArgs e)
        {
            clear();
            try
            {
                int n = 20;
                int D = 2;
                for (int cnt = 0; cnt < 3; ++cnt)
                {
                    string name = "Bol" + cnt.ToString();
                    Series lin = new Series(name);
                    chart1.Series.Add(lin);
                    chart1.Series[name].Color = Color.Lime;
                    chart1.Series[name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series[name].ChartArea = "ChartArea1";
                    chart1.Series[name].Legend = "Legend1";
                    chart1.Series[name].LegendText = "Bollinger Bands" + cnt.ToString();
                    if (cnt!=1)
                        chart1.Series[name].BorderDashStyle = ChartDashStyle.Dash;

                    for (int i = 0; i < dataGridView1.RowCount; ++i)
                    {
                        double sma = slidingAverage(i, n);
                        double aa = sma;

                        if (cnt != 1)
                        {
                            double tmp = 0.0;
                            int j;

                            if ((i - n) > 0)
                                j = i - n;
                            else
                                j = 0;
                            for (; j < i; ++j)
                            {
                                tmp += (double.Parse(dataGridView1[5, j].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)-sma)* (double.Parse(dataGridView1[5, j].Value.ToString(), System.Globalization.CultureInfo.InvariantCulture) - sma);
                            }
                            tmp /= n;
                            if (cnt==0)
                                aa += Math.Abs(D*Math.Sqrt(tmp));
                            if (cnt == 2)
                                aa -= Math.Abs(D * Math.Sqrt(tmp));
                        }
                        


                        chart1.Series[name].Points.AddXY(0, aa);
                    }


                }
            }
            catch { }
        }

        //public List<DataStocks> Read(string filePath)
        //{
        //    if (File.Exists(filePath))
        //    {
        //        Stream stream = File.OpenRead(filePath);
        //        BinaryFormatter deserializer = new BinaryFormatter();
        //        var details = (List<DataStocks>)deserializer.Deserialize(stream);
        //        stream.Close();
        //        return details;
        //    }
        //    return null; // file not exists
        //}

        //public void test()
        //{
        //    openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        //    if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    string filename = openFileDialog1.FileName;
        //    string directoryPath = Path.GetDirectoryName(filename);
        //    string fileName = Path.GetFileNameWithoutExtension(filename);

        //    MessageBox.Show(directoryPath);
        //    MessageBox.Show(fileName);



        //    //using (var stream = File.Open(filename, FileMode.Open))
        //    {
        //        // Use stream

        //        string strConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + directoryPath + "\"; Extended Properties = text";
        //        OleDbConnection StrCon = new OleDbConnection(strConnection);


        //        //OleDbConnection StrCon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=directoryPath;Extended Properties=text");
        //        //Строка для выборки данных
        //        //string Select1 = "SELECT * FROM [" + fileName + "]";
        //        string Select1 = "SELECT * FROM [" + fileName + "]";
        //        //Создание объекта Command
        //        OleDbCommand comand1 = new OleDbCommand(Select1, StrCon);
        //        //Определяем объект Adapter для взаимодействия с источником данных
        //        OleDbDataAdapter adapter1 = new OleDbDataAdapter(comand1);
        //        //Определяем объект DataSet
        //        DataSet AllTables = new DataSet();
        //        //Открываем подключение
        //        StrCon.Open();
        //        //Заполняем DataSet таблицей из источника данных
        //        adapter1.Fill(AllTables);
        //        //Заполняем обект datagridview для отображения данных на форме
        //        dataGridView1.DataSource = AllTables.Tables[0];
        //        StrCon.Close();
        //    }
        //}


    }
}
