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
        public static MockUp Instance { get; private set; }

        static MockUp()
        {
            Instance = new MockUp();
        }

        private MockUp()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
