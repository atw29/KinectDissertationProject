using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KinectDissertationProject.Views
{
    /// <summary>
    /// Taken from https://stackoverflow.com/questions/741956/pan-zoom-image
    /// </summary>
    public class ZoomBorder : Border
    {
        private UIElement child = null;
        private Point origin;
        private Point start;

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != Child)
                    Initialize(value);
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                MouseWheel += Child_MouseWheel;
                MouseLeftButtonDown += Child_MouseLeftButtonDown;
                MouseLeftButtonUp += Child_MouseLeftButtonUp;
                MouseMove += Child_MouseMove;
                PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  Child_PreviewMouseRightButtonDown);
            }
        }

        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        public void ZoomCentre(bool In)
        {
            if (child != null && child is Image image)
            {
                Zoom(In ? 1 : 0, new Point(image.ActualWidth/2, image.ActualHeight/2));
            }
        }

        public void Zoom(int MouseWheelDelta, Point point)
        {
            var st = GetScaleTransform(child);
            var tt = GetTranslateTransform(child);

            double zoom = MouseWheelDelta > 0 ? .2 : -.2;
            if (!(MouseWheelDelta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                return;

            double abosuluteX;
            double abosuluteY;

            abosuluteX = point.X * st.ScaleX + tt.X;
            abosuluteY = point.Y * st.ScaleY + tt.Y;

            st.ScaleX += zoom;
            st.ScaleY += zoom;

            tt.X = abosuluteX - point.X * st.ScaleX;
            tt.Y = abosuluteY - point.Y * st.ScaleY;
            
        }

        #region Child Events

        private void Child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                Zoom(e.Delta, e.GetPosition(child));
            }
        }

        private void Child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void Child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                Cursor = Cursors.Arrow;
            }
        }

        void Child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Reset();
        }

        public void Pan(Direction direction, int increment = 10)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                origin = new Point(tt.X, tt.Y);
                switch (direction)
                {
                    case Direction.UP:
                        tt.Y = origin.Y - increment;   
                        break;
                    case Direction.DOWN:
                        tt.Y = origin.Y + increment;   
                        break;
                    case Direction.LEFT:
                        tt.X = origin.X - increment;
                        break;
                    case Direction.RIGHT:
                        tt.X = origin.X + increment;
                        break;
                }
            }
        }

        private void Child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured)
                {
                    Vector v = start - e.GetPosition(this);
                    var tt = GetTranslateTransform(child);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                }
            }
        }

        #endregion
    }

    public enum Direction
    {
        UP, DOWN, LEFT, RIGHT
    }
}