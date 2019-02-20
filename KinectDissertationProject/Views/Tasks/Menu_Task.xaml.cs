using KinectDissertationProject.Models;
using KinectDissertationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
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

            ViewModel.GestureOccurred += Menu_Task_GestureEventOccurred;

        }

        private int GetFocussedItem()
        {
            for (int i = 0; i < items.Children.Capacity; i++)
            {
                object obj = items.Children[i];
                if (obj is Button button)
                {
                    if (button.IsFocused)
                    {
                        return i;
                    }
                }
            }
            throw new IndexOutOfRangeException("No Focussed Item Found");
        }

        private void FocusOnPreviousItem()
        {
            int i = GetFocussedItem();
            if (i != 0) items.Children[i - 1].Focus();
        }

        private void FocusOnNextItem()
        {
            int focussedItem = GetFocussedItem();
            if(focussedItem != items.Children.Capacity - 1) items.Children[focussedItem + 1].Focus();
        }

        private void ClickButton(Button button)
        {
            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();

        }

        private void SelectCurrentItem()
        {
            foreach(object obj in items.Children)
            {
                if (obj is Button button)
                {
                    if (button.IsFocused)
                    {
                        ClickButton(button);
                    }
                }
            }
        }

        private void SelectBackButton()
        {
            if (BackButton.IsEnabled)
            {
                ClickButton(BackButton);
            }
        }

        private void Menu_Task_GestureEventOccurred(object sender, WindowOperationEventArgs e)
        {
            if (e.Window == this)
            {
                switch(e.Gesture)
                {
                    case GestureType.RIGHT_HAND_SWIPE_DOWN:
                        FocusOnNextItem();
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_UP:
                        FocusOnPreviousItem();
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_RIGHT:
                        SelectCurrentItem();
                        break;
                    case GestureType.RIGHT_HAND_SWIPE_LEFT:
                        SelectBackButton();
                        break;
                    default:
                        break;
                }
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
