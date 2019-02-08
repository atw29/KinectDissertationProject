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
        private List<Button> buttonList;

        internal KinectViewModel ViewModel { get; }

        public Menu_Task()
        {
            ViewModel = KinectViewModel.Instance;

            InitializeComponent();

            Loaded += Menu_Task_Loaded;

            ViewModel.WindowOperationOccurred += Menu_Task_GestureEventOccurred;

        }

        private void IterateItemList(Action<Button> buttonMethod)
        {

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

        private void Menu_Task_Loaded(object sender, RoutedEventArgs e)
        {
            //buttonList = new List<Button>()
            //{
            //    Lighting_Control,
            //    Interaction_Params,
            //    Dummy_One,
            //    Data_Search,
            //    Dummy_Two
            //};
            Lighting_Control.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
