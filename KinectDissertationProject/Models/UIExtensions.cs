using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace KinectDissertationProject.Models
{
    static class UIExtensions
    {
  
        public static void DrawPoint(this Canvas canvas, Tuple<Point, bool> point)
        {
            // 1) check whether the joint is tracked (or, more accurately, isn't NOT tracked)
            if (point.Item2)
            {

                // Create a WPF Ellipse
                Ellipse ellipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = new SolidColorBrush(Colors.LightBlue)
                };

                // 4) Position the ellipse according to the joint's coordinates
                Canvas.SetLeft(ellipse, point.Item1.X - ellipse.Width / 2);
                Canvas.SetTop(ellipse, point.Item1.Y - ellipse.Height / 2);

                // 5) Add the ellipse to canvas
                canvas.Children.Add(ellipse);
            }

        }

        public static void DrawLine(this Canvas canvas, Tuple<Point, bool> first, Tuple<Point, bool> second)
        {
            if (!first.Item2 || !second.Item2) return;

            Line line = new Line
            {
                X1 = first.Item1.X,
                Y1 = first.Item1.Y,
                X2 = second.Item1.X,
                Y2 = second.Item1.Y,
                StrokeThickness = 8,
                Stroke = new SolidColorBrush(Colors.LightBlue)
            };

            canvas.Children.Add(line);
        }

    }

    
}
