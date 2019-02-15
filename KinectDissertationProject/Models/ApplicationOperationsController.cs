using KinectDissertationProject.ViewModel;
using KinectDissertationProject.Views;
using KinectDissertationProject.Views.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.Models
{
    class ApplicationOperationsController
    {
        private KinectViewModel KinectViewModel;

        private IList<Window> Windows;

        public ApplicationOperationsController()
        {
            KinectViewModel = KinectViewModel.Instance;

            KinectViewModel.ApplicationOperationOccurred += KinectViewModel_ApplicationOperationOccurred;

            Windows = new List<Window>();
        }

        public int Add_Window(Window w)
        {
            int loc = Windows.Count();
            Windows.Add(w);
            return loc;
        }

        public void Remove_Window(Window w)
        {
            Windows.Remove(w);
        }

        private void Create_Window(Window w)
        {
            w.DataContext = this;
            Add_Window(w);
            w.Show();

            w.Closed += Window_ClosedEvent;
        }

        public void Switch_Window(int index)
        {
            Windows[index].Show();
        }

        #region Window Creation
        internal void Create_MockUp_Window()
        {
            Create_Window(new MockUp());
        }

        internal void Create_Camera_Window()
        {
            Create_Window(new Camera());
        }

        internal void Create_Menu_Task_Window()
        {
            Create_Window(new Menu_Task());
        }

        internal void Create_Lighting_Window()
        {
            Create_Window(new LightingControl());
        }

        internal void Create_X_Ray_Window()
        {
            Create_Window(new X_Rays());
        }
        #endregion

        private void Window_ClosedEvent(object sender, EventArgs e)
        {
            Remove_Window((Window)sender);
        }

        private void KinectViewModel_ApplicationOperationOccurred(object sender, ApplicationOperationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
