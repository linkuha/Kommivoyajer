using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kommivoyajer.Helpers;
using Kommivoyajer.Methods;
using MessageBox = System.Windows.MessageBox;

namespace Kommivoyajer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Point> points = new List<Point>(); // список точек
        public double[,] matrix;

        private List<Point> toDrawLine = new List<Point>();
        public string serialized = "";

        public MainWindow()
        {
            InitializeComponent();
            //Cities cities = new Cities(4, 100);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myCanvas.Background = Brushes.Bisque;
        }

        private void point_MouseUp(object sender, MouseButtonEventArgs e)
        {

            var point = e.GetPosition(this.myCanvas); // получила позицию события, записала в переменную точка
            points.Add(point);  // записала в список точек эту точку, в которой произошло событие
            int count = Helpers.Drawing.CountElements("City", this.myCanvas); // посчитала количество точек

            Helpers.Drawing.DrawCity(point, myCanvas); // поинтс = список точек, соответственно, для всех точек нарисовать эллипсы
            Helpers.Drawing.DrawNumberOfCity(point, myCanvas, count.ToString()); //написала рядом с эллипсами их номера

        }

        private void myGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MesannealinggeBox.Show("Grid got Clicked");
        }

        private void clearCanvasButton_Click(object sender, RoutedEventArgs e)
        {
            Helpers.Drawing.ClearCanvas("City", myCanvas);
            Helpers.Drawing.ClearCanvas("nCity", myCanvas);
            Helpers.Drawing.ClearCanvas("Line", myCanvas);
            resultTextBlock.Text = "";
            matrix = null;
            points.Clear();
        }

        private void ExhaustiveSearch_Click(object sender, RoutedEventArgs e)
        {
            TimeTextBlock.Text = "";
            Stopwatch time = new Stopwatch();
            time.Start();

            double[,] adjMatrix = AdjacencyMatrix.PointToMatrix(points);
            matrix = adjMatrix;
            ExhaustiveSearch bn = new ExhaustiveSearch(points.Count, matrix);
            bn.generateCities();

            resultTextBlock.Text += bn.printShortTrip();

            time.Stop();
            var resultTime = time.ElapsedMilliseconds;
            TimeTextBlock.Text = resultTime.ToString();

            for (int i = 0; i < bn.CitiesOrder.Count - 1; i++)
            {
                var p1 = points[bn.CitiesOrder[i]];
                var p2 = points[bn.CitiesOrder[i + 1]];
                Helpers.Drawing.DrawLine(p1, p2, this.myCanvas);
            }


        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("trip.txt");

            StringBuilder sb = new StringBuilder();
            foreach (Point p in points)
            {
                sb.Append(Convert.ToString(p.X));
                sb.Append('.');
                sb.Append(Convert.ToString(p.Y));
                sb.Append(' ');
            }
            streamWriter.Write(sb);
            streamWriter.Close();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            //    matrix = null;
            //    const string path = @"trip.txt"; // из папки Debug
            //   //OpenFileDialog sd = new OpenFileDialog();
            //  string[] fileLines = File.ReadAllLines(path);
            //   //sd.Filter = "All Files (*.*)|*.*";
            //   //sd.FilterIndex = 1;
            //   //sd.Multiselect = true;

            //   //sd.ShowDialog();
            ////   string fileLines = System.IO.Path.GetDirectoryName(sd.FileName); 
            //   matrix = new double[fileLines.Length,fileLines[0].Split(' ').Length];
            //   for (int i = 0; i < fileLines.Length; ++i)
            //   {
            //       string line = fileLines[i];
            //       for (int j = 0; j < matrix.GetLength(1); ++j)
            //       {
            //           string[] split = line.Split(' ');
            //           matrix[i, j] = Convert.ToDouble(split[j]);
            //       }                              
            //   }    

            //  List<Point> p = new List<Point>();
            this.myCanvas.Children.Clear();
            serialized = File.ReadAllText(@"trip.txt");
            string[] groups = serialized.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string group in groups)
            {
                string[] coords = group.Split('.');
                points.Add(new Point(Convert.ToDouble(coords[0]), Convert.ToDouble(coords[1])));
            }

            int i = 0;
            foreach (Point point in points)
            {
                Helpers.Drawing.DrawCity(point, this.myCanvas);
                Helpers.Drawing.DrawNumberOfCity(point, myCanvas, i.ToString());
                i++;
            }
        }
    }
}
