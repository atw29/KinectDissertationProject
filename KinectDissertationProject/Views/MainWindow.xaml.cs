using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectDissertationProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Win_Num { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Win_Num = KinectViewModel.Add_Window(this);
            KinectViewModel.Load_Kinect();
            KinectViewModel.Open_Kinect(); // Want to Open Initially?
            KinectViewModel.PropertyChanged += KinectViewModel_PropertyChanged;
            KinectViewModel.EventOccurred += KinectViewModel_EventOccurred;
        }

        private void KinectViewModel_EventOccurred(object sender, ViewModel.KinectViewModel.CustomEventArgs e)
        {
            switch (e.args.Item1)
            {
                case "handmoved":
                    textblock.Text = e.args.Item2;
                    break;
            }
        }

        private void KinectViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "open":
                    textblock.Text = "Open";
                    break;
                case "test":
                    textblock.Text = "Changeed";
                    break;
                default:
                    break;

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            KinectViewModel.Close_Kinect();
        }
    }
}
