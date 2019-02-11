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
    /// Interaction logic for MockUp.xaml
    /// </summary>
    public partial class MockUp : Window
    {
        internal KinectViewModel KinectViewModel { get; }

        public MockUp()
        {
            InitializeComponent();

            KinectViewModel = KinectViewModel.Instance;
            DataContext = KinectViewModel;

            KinectViewModel.JointPositionEventOccurred += KinectViewModel_JointPositionEventOccurred;
        }

        private void KinectViewModel_JointPositionEventOccurred(object sender, Models.JointPositionEventArgs e)
        {
            Console.Write(e.JointPosDict.ToString());
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
