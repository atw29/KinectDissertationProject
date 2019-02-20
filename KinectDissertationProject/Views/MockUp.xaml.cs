using KinectDissertationProject.ViewModel;
using KinectDissertationProject.Models;
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
    /// Interaction logic for MockUp.xaml
    /// </summary>
    public partial class MockUp : Window
    {
        internal KinectViewModel KinectViewModel { get; }

        private readonly int LinesToScroll = 5;

        public MockUp()
        {
            InitializeComponent();

            KinectViewModel = KinectViewModel.Instance;
            DataContext = KinectViewModel;

            KinectViewModel.GestureOccurred += KinectViewModel_GestureOccurred;
        }

        private void KinectViewModel_GestureOccurred(object sender, WindowOperationEventArgs e)
        {
            switch (e.Gesture)
            {
                case GestureType.RIGHT_SWIPE_DOWN_LEFT_HAND_RAISED:
                    ScrollDown();
                    break;
                case GestureType.RIGHT_SWIPE_UP_LEFT_HAND_RAISED:
                    ScrollUp();
                    break;
            }
        }

        private void ScrollDown()
        {
            RepeatAction(ScrollBar.LineDown);
        }

        private void ScrollUp()
        {
            RepeatAction(ScrollBar.LineUp);
        }

        private void RepeatAction(Action action)
        {
            for (int i=0; i<LinesToScroll; i++)
            {
                action();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KinectViewModel.Create_X_Ray_Window();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VerifiedCheckBox.IsChecked = true;
        }
    }
}
