using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kommivoyajer.Helpers
{
    class Drawing //класс-рисовальщик
    {
        public static void ClearCanvas(string name, Canvas canvas) // очистка канвы
        {
            List<UIElement> removeItems = canvas.Children.Cast<UIElement>().Where(ui => ui.Uid.StartsWith(name)).ToList(); // список графических
            //элементов, которые нужно удалить. По имени
            foreach (UIElement ui in removeItems)
            {
                canvas.Children.Remove(ui); // ремув = удалить дочерние элементы канвы
            }
        }

        public static int CountElements(string name, Canvas canvas)
        {
            List<UIElement> listUI = canvas.Children.Cast<UIElement>().Where(ui => ui.Uid.StartsWith(name)).ToList();
            return listUI.Count;
        }

        public static void DrawCity(Point p, Canvas canvas) //рисование точки
        {
            Ellipse ellipse = new Ellipse(); // новый эллипс
            ellipse.Uid = "City"; // его имя в памяти (чтобы потом было легко удалять его с канвы или считать их количество на канве)
            ellipse.Height = 10;
            ellipse.Width = 10;
            RadialGradientBrush brush = new RadialGradientBrush(); //тип кисти

            brush.GradientStops.Add(new GradientStop(Colors.DarkSlateGray, 0.250)); //цвет кисти
            //brush.GradientStops.Add(new GradientStop(Colors.DarkSlateGray, 0.100));
            //brush.GradientStops.Add(new GradientStop(Colors.DarkSlateGray, 5));
            ellipse.Fill = brush; //заполнить эллипс кистью
            Canvas.SetLeft(ellipse, p.X); //определить позицию эллипса по горизонтали
            Canvas.SetTop(ellipse, p.Y); // по вертикали

            canvas.Children.Add(ellipse); // добавить в дочерние элементы канвы эллипс

        }

        public static void DrawNumberOfCity(Point p, Canvas canvas, string text)
        {
            TextBlock textBlock = new TextBlock(); //то же самое, только создаётся текстбокс рядом с эллипсом, позиция чуть правее и чуть выше
            textBlock.Uid = "nCity";
            textBlock.Text = text;

            Canvas.SetLeft(textBlock, p.X + 8);
            Canvas.SetTop(textBlock, p.Y + 8);

            canvas.Children.Add(textBlock);

        }

        public static void DrawLine(Point p1, Point p2, Canvas canvas)
        {
            Line line = new Line();
            line.Uid = "Line";
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 2;
            line.Stroke = Brushes.DarkSlateBlue;
            line.X1 = p1.X;
            line.Y1 = p1.Y;
            line.X2 = p2.X;
            line.Y2 = p2.Y;

            canvas.Children.Add(line);
        }

        public static void DrawLayout(Canvas canvas)
        {
            Drawing.ClearCanvas("Line", canvas);
            Drawing.ClearCanvas("nCity", canvas);
            Drawing.ClearCanvas("City", canvas);

            double h = Math.Truncate(canvas.ActualHeight / 100);
            int heigth = (int)h;
            double w = Math.Truncate(canvas.ActualWidth / 100);
            int width = (int)w;

            for (int i = 0; i <= heigth; i++)
            {
                Point p1 = new Point(0, i * 100);
                Point p2 = new Point(canvas.ActualWidth, i * 100);
                Drawing.DrawLine(p1, p2, canvas);
            }

            for (int j = 0; j <= width; j++)
            {
                var p1 = new Point(j * 100, canvas.ActualHeight);
                var p2 = new Point(j * 100, 0);
                Drawing.DrawLine(p1, p2, canvas);
            }
            Drawing.DrawNumberOfCity(new Point(100, 100), canvas, "(100;100)");
        }
    }
}
