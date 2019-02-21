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

namespace KinectDissertationProject.Views.Tasks
{
    /// <summary>
    /// Interaction logic for LightingControl.xaml
    /// </summary>
    public partial class LightingControl : Window
    {
        public LightingControl()
        {
            ViewModel = KinectViewModel.Instance;

            InitializeComponent();

            ViewModel.GestureOccurred += ViewModel_GestureOccurred;
        }

        private void ViewModel_GestureOccurred(object sender, WindowOperationEventArgs e)
        {
            if (e.Window == this)
            {
                switch (e.Gesture)
                {
                    case GestureType.RIGHT_SWIPE_LEFT_LEFT_HAND_RAISED:
                        DecreaseSlider(5);
                        break;
                    case GestureType.RIGHT_SWIPE_LEFT_LEFT_HAND_RAISED_LEFT:
                        DecreaseSlider(1);
                        break;

                    case GestureType.RIGHT_SWIPE_RIGHT_LEFT_HAND_RAISED:
                        IncreaseSlider(5);
                        break;
                    case GestureType.RIGHT_SWIPE_RIGHT_LEFT_HAND_RAISED_LEFT:
                        IncreaseSlider(1);
                        break;
                        
                    case GestureType.RIGHT_HAND_SWIPE_DOWN:
                        ChangeSelection();
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_UP:
                        ChangeSelection();
                        break;
                    
                }
            }
        }

        private void DecreaseSlider(int change)
        {
            slider.Value = slider.Value - change % 100;
        }

        private void IncreaseSlider(int change)
        {
            slider.Value = slider.Value + change % 100;
        }

        private void ChangeSelection()
        {
            if (Exit.IsFocused)
            {
                Apply.Focus();
            } else
            {
                Exit.Focus();
            }
        }

        internal KinectViewModel ViewModel { get; }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
