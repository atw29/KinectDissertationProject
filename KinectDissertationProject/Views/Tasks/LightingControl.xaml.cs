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
        }

        internal KinectViewModel ViewModel { get; }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
