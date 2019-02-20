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

        private readonly int LinesToScroll = 4;
        private readonly IList<Control> SelectableOptions;

        public MockUp()
        {
            InitializeComponent();

            KinectViewModel = KinectViewModel.Instance;
            DataContext = KinectViewModel;

            SelectableOptions = new List<Control>
            {
                FirstName,
                Surname,
                Email,
                Address_1,
                Address_2,
                County,
                Postcode,
                ButtonOne,
                ButtonTwo,
                ButtonThree,
                ButtonFour,
                X_Ray
            };

            KinectViewModel.GestureOccurred += KinectViewModel_GestureOccurred;

            Loaded += MockUp_Loaded;
        }

        private void MockUp_Loaded(object sender, RoutedEventArgs e)
        {
            SelectableOptions[0].Focus();
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
                case GestureType.RIGHT_HAND_SWIPE_DOWN:
                    SelectDown();
                    break;
                case GestureType.RIGHT_HAND_SWIPE_UP:
                    SelectUp();
                    break;
            }
        }

        private int GetLocOfCurrentItem(IList<Control> list)
        {
            for (int i=0; i<list.Count; i++)
            {
                if (list[i].IsFocused)
                {
                    return i;
                }
            }
            return -1;
        }

        private IList<Control> FilterUnseen()
        {
            return SelectableOptions.Where(x => IsUserVisible(x, Container)).ToList();
        }

        private void SelectUp()
        {
            IList<Control> visible = FilterUnseen();
            int loc = GetLocOfCurrentItem(visible);
            if (loc == -1)
            {
                visible[visible.Count -1 ].Focus();
            }
            else
            {
                visible[loc - 1 < 0 ? 0 : loc -1 ].Focus();
            }
        }

        private void SelectDown()
        {
            IList<Control> visible = FilterUnseen();
            int loc = GetLocOfCurrentItem(visible);
            if (loc == -1)
            {
                visible[0].Focus();
            }
            else
            {
                visible[loc + 1].Focus();
            }
        }

        private bool IsUserVisible(FrameworkElement element, FrameworkElement container)
        {
            if (!element.IsVisible)
                return false;

            Rect bounds = element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            Rect rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
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
