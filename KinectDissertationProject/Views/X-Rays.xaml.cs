using KinectDissertationProject.Models;
using KinectDissertationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KinectDissertationProject.Views
{
    /// <summary>
    /// Interaction logic for X_Rays.xaml
    /// </summary>
    public partial class X_Rays : Window
    {
        private const int Big_Increment = 40;
        private KinectViewModel kinectViewModel;
        public X_Rays()
        {
            InitializeComponent();
            kinectViewModel = KinectViewModel.Instance;

            kinectViewModel.GestureOccurred += KinectViewModel_GestureOccurred;
        }

        private void KinectViewModel_GestureOccurred(object sender, WindowOperationEventArgs e)
        {
            if (e.Window == this)
            {
                switch (e.Gesture)
                {
                    case GestureType.RIGHT_HAND_SWIPE_UP:
                        Pan(Direction.UP);
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_DOWN:
                        Pan(Direction.DOWN);
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_LEFT:
                        Pan(Direction.LEFT);
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_RIGHT:
                        Pan(Direction.RIGHT);
                        break;

                    case GestureType.RIGHT_SWIPE_UP_LEFT_HAND_RAISED_LEFT:
                        LargePan(Direction.UP);
                        break;
                    case GestureType.RIGHT_SWIPE_DOWN_LEFT_HAND_RAISED_LEFT:
                        LargePan(Direction.DOWN);
                        break;
                    case GestureType.RIGHT_SWIPE_LEFT_LEFT_HAND_RAISED_LEFT:
                        LargePan(Direction.LEFT);
                        break;
                    case GestureType.RIGHT_SWIPE_RIGHT_LEFT_HAND_RAISED_LEFT:
                        LargePan(Direction.RIGHT);
                        break;

                    case GestureType.RIGHT_SWIPE_UP_LEFT_HAND_RAISED:
                        Picture.ZoomCentre(true);
                        break;
                    case GestureType.RIGHT_SWIPE_DOWN_LEFT_HAND_RAISED:
                        Picture.ZoomCentre(false);
                        break;

                }
            }
        }

        private void LargePan(Direction direction)
        {
            Pan(direction, Big_Increment);
        }

        private void Pan(Direction direction, int increment = 10)
        {
            Picture.Pan(direction, increment);
        }

        private void Zoom_In(object sender, RoutedEventArgs e)
        {
            Picture.ZoomCentre(true);
        }
        private void Zoom_Out(object sender, RoutedEventArgs e)
        {
            Picture.ZoomCentre(false);
        }
    }
}
