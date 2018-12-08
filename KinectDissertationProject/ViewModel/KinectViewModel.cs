using KinectDissertationProject.Models;
using KinectDissertationProject.Models.Gestures;
using KinectDissertationProject.Views;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.ViewModel
{
    class KinectViewModel
    {

        #if DEBUG
        bool debug = true;
        #endif

        private KinectReader kinectReader;
        private IList<Window> windows;
        private CoordinateMapper CoordinateMapper { get
            {
                return kinectReader.CoordinateMapper;
            }
        }
        public string TextBoxText{ get; set; }

        #region Hand Positions

        HandPos_TOP HandPos_TOP = new HandPos_TOP();
        HandPos_BOT HandPos_BOT = new HandPos_BOT();
        HandPos_LHS HandPos_LHS = new HandPos_LHS();
        HandPos_RHS HandPos_RHS = new HandPos_RHS();

        internal void Create_MockUp_Window()
        {
            MockUp MockUpWindow = MockUp.Instance;
            Add_Window(MockUpWindow);
            MockUpWindow.Show();
        }

        #endregion

        #region Event Handlers

        
        public event EventHandler<JointPositionEventArgs> JointPositionEventOccurred;
        public event EventHandler<WindowEventArgs> WindowEventOccurred;

        protected void RaiseJointPositionEventOccurred(IReadOnlyDictionary<JointType, Tuple<Point, bool>> jointPointMap)
        {
            JointPositionEventOccurred?.Invoke(this, new JointPositionEventArgs
            {
                JointPosDict = jointPointMap
            });
        }

        protected void RaiseWindowEvent(WindowEventArgs args)
        {
            WindowEventOccurred?.Invoke(this, args);
        }

        #endregion

        #region Initialisers
        public KinectViewModel()
        {
            windows = new List<Window>();
            TextBoxText = "Kinect Dissertation Project";    
        }

        internal void Load_Kinect()
        {
            kinectReader = new KinectReader();
            kinectReader.OnTrackedBody += Kinect_OnTrackedBody;
            kinectReader.OnLostTracking += Kinect_LostTracking;
        }

        internal void Open_Kinect()
        {
            kinectReader.Open();
        }

        internal void Close_Kinect()
        {
            kinectReader.Close();
        }

        #endregion

        private void Kinect_OnTrackedBody(object sender, BodyEventArgs e)
        {
            Body body = e.BodyData;

            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            // Check Window Gestures - i.e. 

            CheckApplicationGestures(body, activeWindow);

            CheckWindowGestures(body, activeWindow);

            RaiseJointPositionEventOccurred(body.GetPointDictFromJoints(kinectReader.CoordinateMapper));

            CheckHandPositions(body.Joints);

            // Raise Gesutre Occurred 
            // i.e. RaiseGesutreOccurred(activeWindow, Gesture.SwipeDown)

            // Raise Joint Positions

            //RaiseEventOccurred(
            //    activeWindow, 
            //    Operation.MINIMISE, 
            //    new Dictionary<string, object> {
            //        { "LeftHandLocation" , body.Joints[JointType.HandLeft].ToCoordinatePoint(kinectReader.CoordinateMapper).ToString()}
            //    }
            //);

        }

        /// <summary>
        /// Application Gestures are across all windows and involve acts like minimising, switching etc.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="activeWindow"></param>
        private void CheckApplicationGestures(Body body, Window activeWindow)
        {
            switch (body.GetApplicationGesture())
            {
                case ApplicationGesture.MINIMISE:
                    activeWindow.WindowState = WindowState.Minimized;
                    break;
                case ApplicationGesture.NONE:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Checks gestures applicable for the active window, such as scrolling, clicking etc.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="activeWindow"></param>
        private void CheckWindowGestures(Body body, Window activeWindow)
        {
            switch (body.GetWindowGesture())
            {
                case WindowGesture.CLICK:
                    break;
                case WindowGesture.NONE:
                    break;
                default:
                    break;
            }
        }

        private void CheckHandPositions(IReadOnlyDictionary<JointType, Joint> joints)
        {
            if (HandPos_TOP.In_Position(joints[JointType.HandLeft], CoordinateMapper) == HandPosition.IN_POSITION)
            {

            }
        }

        private void Kinect_LostTracking(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        public int Add_Window(Window w)
        {
            int loc = windows.Count();
            windows.Add(w);
            return loc;
        }

        public void Switch_Window(int index)
        {
            windows[index].Show();
        }

    }


}
