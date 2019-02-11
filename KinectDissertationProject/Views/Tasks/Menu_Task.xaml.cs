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
    /// Interaction logic for Menu_Task.xaml
    /// </summary>
    public partial class Menu_Task : Window
    {
        internal KinectViewModel ViewModel { get; }

        private bool IsMainMenu;

        public Menu_Task()
        {
            ViewModel = KinectViewModel.Instance;

            InitializeComponent();

            Loaded += Menu_Task_Loaded;

            ViewModel.WindowOperationOccurred += Menu_Task_GestureEventOccurred;

        }

        private void FocusOnPreviousItem()
        {
            for (int i=items.Children.Capacity-1; i > -1; i--)
            {
                object obj = items.Children[i];
                if (obj is Button button)
                {
                    if (button.IsFocused)
                    {
                        if (i != 0) items.Children[i - 1].Focus();
                        break;
                    }
                }
            }
        }

        private void FocusOnNextItem()
        {
            for (int i=0; i<items.Children.Capacity; i++)
            {
                object obj = items.Children[i];
                if (obj is Button button)
                {
                    if (button.IsFocused)
                    {
                        if (i != items.Children.Capacity - 1) items.Children[i + 1].Focus();
                        break;
                    }
                }
            }            
        }

        private void Menu_Task_GestureEventOccurred(object sender, WindowOperationEventArgs e)
        {
            switch(e.Gesture)
            {
                case GestureType.RIGHT_HAND_SWIPE_DOWN:
                    FocusOnNextItem();
                    break;
                case GestureType.RIGHT_HAND_SWIPE_UP:
                    FocusOnPreviousItem();
                    break;
                default:
                    break;
            }
        }

        private void SetMainMenu()
        {
            IsMainMenu = true;
            BackButton.IsEnabled = false;
            label.Content = "Main Menu";

            Item1.Content = "Lighting Control";
            Item2.Content = "Interaction Parameters";
            Item3.Content = "Option 3";
            Item4.Content = "Data Search";
            Item5.Content = "Option 5";

            if (!ViewModel.IsMI) Item1.Focus();
        }

        private void SetDataMenu()
        {
            IsMainMenu = false;
            BackButton.IsEnabled = true;
            label.Content = "Patient Data";

            Item1.Content = "Patient Information";
            Item2.Content = "Operation List";
            Item3.Content = "Organ Information";
            Item4.Content = "X-Ray 1";
            Item5.Content = "X-Ray 2";

            if (!ViewModel.IsMI) Item1.Focus();
        }

        private void Menu_Task_Loaded(object sender, RoutedEventArgs e)
        {
            SetMainMenu();
        }
        private void Item1_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMainMenu)
            {
                ViewModel.Create_MockUp_Window();
            } else
            {
                ViewModel.Create_Lighting_Window();
            }
        }
        private void Item2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Item3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item4_Click(object sender, RoutedEventArgs e)
        {
            if (IsMainMenu)
            {
                SetDataMenu();
            } else
            {
                ViewModel.Create_X_Ray_Window();
            }
        }

        private void Item5_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMainMenu)
            {
                ViewModel.Create_X_Ray_Window();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsMainMenu)
            {
                SetMainMenu();
            }
        }
    }
}
