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
            switch (e.Gesture)
            {
                case GestureType.RIGHT_SWIPE_LEFT_LEFT_HAND_RAISED:
                    DecreaseSlider();
                    break;
                case GestureType.RIGHT_SWIPE_RIGHT_LEFT_HAND_RAISED:
                    IncreaseSlider();
                    break;
                case GestureType.RIGHT_HAND_SWIPE_DOWN:
                    ChangeSelection();
                    break;
                case GestureType.RIGHT_HAND_SWIPE_UP:
                    ChangeSelection();
                    break;
                    
            }
        }

        private void DecreaseSlider()
        {
            slider.Value = slider.Value - 5 % 100;
        }

        private void IncreaseSlider()
        {
            slider.Value = slider.Value + 5 % 100;
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
