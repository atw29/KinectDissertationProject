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
        private IList<WindowInfo> SnappedLeft;
        private IList<WindowInfo> SnappedRight;

        public ApplicationOperationsController()
        {

            KinectViewModel.Instance.WindowOperationOccurred += WindowOperationOccurred;
            Windows = new List<Window>();
            SnappedLeft = new List<WindowInfo>();
            SnappedRight = new List<WindowInfo>();
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

                case GestureType.LARGE_SWIPE_RIGHT:
                    PerformSnap(e.Window, true);
                    break;
                case GestureType.LARGE_SWIPE_LEFT:
                    PerformSnap(e.Window, false);
                    break;

                case GestureType.EXPLOSION_IN:
                    if (e.Window.WindowState == WindowState.Maximized)
                    {
                        e.Window.WindowState = WindowState.Normal;
                    }
                    break;
                case GestureType.EXPLOSION_OUT:
                    if (e.Window.WindowState == WindowState.Normal)
                    {
                        e.Window.WindowState = WindowState.Maximized;
                    }
                    break;
            }
        }

        private void PerformSnap(Window window, bool Right)
        {
            if (!AlreadySnapped(window, Right))
            {
                Snap(window, Right);
            }
        }

        /// <summary>
        /// If the window is already snapped then snapping again in the same direction
        /// does nothing and snapping in the previous direction returns to whatever
        /// window state it had before (e.g. maximised or docked)
        /// </summary>
        /// <param name="window"></param>
        /// <param name="Right"></param>
        /// <returns></returns>
        private bool AlreadySnapped(Window window, bool Right)
        {
            foreach (WindowInfo winInfo in SnappedRight)
            {
                if (winInfo.Window.Equals(window))
                {
                    if (!Right)
                    {
                        RestoreWindowInfo(window, winInfo);
                        SnappedRight.Remove(winInfo);
                    }
                    return true;
                }
            }
            foreach (WindowInfo winInfo in SnappedLeft)
            {
                if (winInfo.Window.Equals(window))
                {
                    if (Right)
                    {
                        RestoreWindowInfo(window, winInfo);
                        SnappedLeft.Remove(winInfo);
                    }
                    return true;
                }
            }
            return false;
        }

        private void RestoreWindowInfo(Window window, WindowInfo winInfo)
        {
            window.WindowState = winInfo.State;
            window.Top = winInfo.Top;
            window.Left = winInfo.Left;
            window.Height = winInfo.Height;
            window.Width = winInfo.Width;
        }

        private void Snap(Window window, bool Right)
        {
            WindowInfo CurrentInfo = new WindowInfo(window.Top, window.Left, window.Height, window.Width, window.WindowState, window);

            window.Height = Properties.Settings.Default.ScreenHeight;
            window.Width = Properties.Settings.Default.ScreenWidth / 2;
            window.Top = 0;
            window.Left = Right ? Properties.Settings.Default.ScreenWidth / 2 : 0;

            if (Right)
            {
                SnappedRight.Add(CurrentInfo);
            } else
            {
                SnappedLeft.Add(CurrentInfo);
            }
        }

        private class WindowInfo
        {
            public readonly double Top;
            public readonly double Left;
            public readonly double Height;
            public readonly double Width;

            public readonly WindowState State;
            public readonly Window Window;

            public WindowInfo(double top, double left, double height, double width, WindowState state, Window window)
            {
                Top = top;
                Left = left;
                Height = height;
                Width = width;
                State = state;
                Window = window;
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
