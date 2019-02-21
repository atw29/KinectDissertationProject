using KinectDissertationProject.ViewModel;
using KinectDissertationProject.Views;
using KinectDissertationProject.Views.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private IDictionary<Window, WindowInfo> SnappedLeft;
        private IDictionary<Window, WindowInfo> SnappedRight;

        public ApplicationOperationsController()
        {

            KinectViewModel.Instance.GestureOccurred += WindowOperationOccurred;
            Windows = new List<Window>();
            SnappedLeft = new Dictionary<Window, WindowInfo>();
            SnappedRight = new Dictionary<Window, WindowInfo>();
        }

        private void WindowOperationOccurred(object sender, WindowOperationEventArgs e)
        {
            if (!(e.Window is MainWindow) || e.Gesture == GestureType.LARGE_SWIPE_UP) // Don't want any operations that might close the background window
            {
                switch (e.Gesture)
                {
                    case GestureType.LARGE_SWIPE_DOWN:
                        Minimise(e);
                        break;
                    case GestureType.LARGE_SWIPE_UP:
                        Restore();
                        break;

                    case GestureType.LARGE_SWIPE_RIGHT:
                        SnapRight(e);
                        break;
                    case GestureType.LARGE_SWIPE_LEFT:
                        SnapLeft(e);
                        break;

                    case GestureType.EXPLOSION_IN:
                        Dock(e);
                        break;
                    case GestureType.EXPLOSION_OUT:
                        Maximise(e);
                        break;

                    case GestureType.LEFT_SWIPE_RIGHT_RIGHT_HAND_RAISED:
                        Tab(e);
                        break;

                    case GestureType.CROSS_ARMS:
                        Close(e);
                        break;
                }
            }
        }

        private void Tab(WindowOperationEventArgs e)
        {
            int currentLoc = Windows.IndexOf(e.Window);
            Window ToMoveToFront = Windows[currentLoc + 1 >= Windows.Count ? 0 : currentLoc + 1];

            //ToMoveToFront.WindowState = WindowState.Minimized;
            //ToMoveToFront.Show();
            //ToMoveToFront.WindowState = WindowState.Normal;

            ToMoveToFront.Activate();
            ToMoveToFront.Focus();
            
        }

        private void Close(WindowOperationEventArgs e)
        {
            e.Window.Close();
            RemoveInfo(e.Window);
        }

        private void RemoveInfo(Window window)
        {
            SnappedRight.Remove(window);
            SnappedLeft.Remove(window);
            if (lastMinimised != null && lastMinimised.Equals(window))
            {
                lastMinimised = null;
            }
        }

        private static void Maximise(WindowOperationEventArgs e)
        {
            if (e.Window.WindowState == WindowState.Normal)
            {
                e.Window.WindowState = WindowState.Maximized;
            }
        }

        private static void Dock(WindowOperationEventArgs e)
        {
            if (e.Window.WindowState == WindowState.Maximized)
            {
                e.Window.WindowState = WindowState.Normal;
            }
        }

        private void SnapLeft(WindowOperationEventArgs e)
        {
            PerformSnap(e.Window, false);
        }

        private void SnapRight(WindowOperationEventArgs e)
        {
            PerformSnap(e.Window, true);
        }

        private void Restore()
        {
            if (lastMinimised != null)
            {
                lastMinimised.WindowState = minimisedWindowState;
            }
        }

        private void Minimise(WindowOperationEventArgs e)
        {
            minimisedWindowState = e.Window.WindowState;
            e.Window.WindowState = WindowState.Minimized;
            lastMinimised = e.Window;
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
            if (SnappedRight.Keys.Contains(window))
            {
                if (!Right)
                {
                    RestoreWindowInfo(window, SnappedRight[window]);
                    SnappedRight.Remove(window);
                }
                return true;
            }
            if (SnappedLeft.Keys.Contains(window))
            {
                if (Right)
                {
                    RestoreWindowInfo(window, SnappedLeft[window]);
                    SnappedLeft.Remove(window);
                }
                return true;
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
            WindowInfo CurrentInfo = new WindowInfo(window.Top, window.Left, window.Height, window.Width, window.WindowState);

            window.WindowState = WindowState.Normal;
            window.Height = Properties.Settings.Default.ScreenHeight;
            window.Width = Properties.Settings.Default.ScreenWidth / 2;
            window.Top = 0;
            window.Left = Right ? Properties.Settings.Default.ScreenWidth / 2 : 0;

            if (Right)
            {
                SnappedRight.Add(window, CurrentInfo);
            } else
            {
                SnappedLeft.Add(window, CurrentInfo);
            }
        }

        private class WindowInfo
        {
            public readonly double Top;
            public readonly double Left;
            public readonly double Height;
            public readonly double Width;

            public WindowState State;

            public WindowInfo(double top, double left, double height, double width, WindowState state)
            {
                Top = top;
                Left = left;
                Height = height;
                Width = width;
                State = state;
            }
        }

        public void Add_Window(Window w)
        {
            Windows.Add(w);
        }

        public void Remove_Window(Window w)
        {
            Windows.Remove(w);
        }

        private void Create_Window(Window w)
        {
            w.DataContext = KinectViewModel.Instance;
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

    }


}
