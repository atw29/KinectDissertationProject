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
        private Window lastMinimised;
        private WindowState minimisedWindowState;

        private IList<Window> Windows;

        public ApplicationOperationsController()
        {

            KinectViewModel.Instance.WindowOperationOccurred += WindowOperationOccurred;
            Windows = new List<Window>();
        }

        private void WindowOperationOccurred(object sender, WindowOperationEventArgs e)
        {
            switch (e.Gesture)
            {
                case GestureType.LARGE_SWIPE_DOWN:
                    if (!(e.Window is MainWindow))
                    {
                        minimisedWindowState = e.Window.WindowState;
                        e.Window.WindowState = WindowState.Minimized;
                        lastMinimised = e.Window;
                    }
                    break;
                case GestureType.LARGE_SWIPE_UP:
                    if (lastMinimised != null)
                    {
                        lastMinimised.WindowState = minimisedWindowState;
                    }
                    break;
            }
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
            switch (e.Operation)
            {
                
            }
        }
    }
}
