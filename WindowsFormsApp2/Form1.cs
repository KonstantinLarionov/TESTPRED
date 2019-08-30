using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        List<double> MainData = new List<double>();
        public Form1()
        {
            InitializeComponent();
        }
        public List<double> Load_TXT(string p)
        {
            List<string> fill = new List<string>();
            using (StreamReader sc = new StreamReader(p))
            {
                sc.ReadLine();
                while (!sc.EndOfStream)
                {
                    fill.Add(sc.ReadLine());
                }
            }
            List<double> info2 = new List<double>();
            for (int j = 0; j < fill.Count; j++)
            {
                info2.Add(Convert.ToDouble(fill[j].Split('-')[1].Replace(".", ",")));
            }
            return info2;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            MainData = Load_TXT(@"C:\Users\kosty\OneDrive\Рабочий стол\Prediction-master\Данные\Сетевые-Атаки.txt");
            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            foreach (var item in MainData)
            {
                series.Points.Add(item);
            }
            series.Color = Color.Blue;
            chart1.Series.Add(series);

            Series series2 = new Series();
            series2.ChartType = SeriesChartType.Point;
            foreach (var item in MainData)
            {
                series2.Points.Add(item);
            }
            series2.Color = Color.Blue;
            chart1.Series.Add(series2);
        }
      
        private void Button2_Click(object sender, EventArgs e)
        {
            List<double> prediction = Prediction(MainData);

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            foreach (var item in prediction)
            {
                series.Points.Add(item);
            }
            series.Color = Color.Red;
            chart1.Series.Add(series);


            Series series2 = new Series();
            series2.ChartType = SeriesChartType.Point;
            foreach (var item in prediction)
            {
                series2.Points.Add(item);
            }
            series2.Color = Color.Red;
            chart1.Series.Add(series2);
        }


        public List<double> Prediction(List<double> data)
        {
            List<double> prediction = new List<double>();

            double y_mean = data.Average(x=>x); // Среднее значение массива

            List <double> listA = new List<double>(); // Набор коэффициентов А по каждой гармоники
            List<double> listB = new List<double>(); // Набор коэффициентов B по каждой гармоники

            List<List<double>> harmonics = new List<List<double>>(); //Набор гармоник

            for (int i = 0; i < data.Count / 2; i++) // Цикл гармоник
            {
                double a = 0; double b = 0; List<double> harmonic = new List<double>();
                //---> Расчитать коэффициенты гармоники 
                for (int j = 0; j < data.Count; j++) // Цикл перебора данных
                {
                    a = 2 * y_mean * (Math.Cos(2 * Math.PI * i * (j / data.Count)));
                    b = 2 * y_mean * (Math.Sin(2 * Math.PI * i * (j / data.Count)));
                    harmonic.Add((a * Math.Cos(2 * Math.PI * i * (j / data.Count))) + (b * Math.Sin(2 * Math.PI * i * (j / data.Count))));
                }
                listA.Add(a); listA.Add(b);
                harmonics.Add(harmonic);
                //---> Расчитать гармонику с коэффициентами 
            }

            for (int i = 0; i < data.Count / 2; i++)
            {

            }

            //---> Суммирование гармоник 
            return prediction;
        }
    }
}
