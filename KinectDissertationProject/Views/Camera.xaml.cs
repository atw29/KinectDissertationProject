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
using KinectDissertationProject.Models;
using KinectDissertationProject.ViewModel;
using Microsoft.Kinect;

namespace KinectDissertationProject.Views
{
    /// <summary>
    /// Interaction logic for Camera.xaml
    /// </summary>
    public partial class Camera : Window
    {
        public Camera()
        {
            InitializeComponent();

            KinectViewModel = KinectViewModel.Instance;
            DataContext = KinectViewModel;
        }

        internal KinectViewModel KinectViewModel { get; }

        private void DrawImage(object sender, ColourEventArgs e)
        {
            image.Source = e.ColourFrame.ToBitmap();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KinectViewModel.ColourEventOccurred += DrawImage;
        }
    }
}
