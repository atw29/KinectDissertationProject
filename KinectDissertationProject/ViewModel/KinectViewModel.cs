using KinectDissertationProject.Models;
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
        private KinectReader kinectReader;
        private IList<Window> windows;
        public string TextBoxText{ get; set; }

        #region Event Handlers

        public event EventHandler<WindowEventArgs> EventOccurred;
        public event EventHandler<JointPositionEventArgs> JointPositionEventOccurred;

        protected void RaiseEventOccurred(Window w, Operation o, IReadOnlyDictionary<string, object> _data)
        {
            EventOccurred?.Invoke(this, new WindowEventArgs
            {
                Window = w,
                Operation = o,
                Data = _data
            });
        }

        protected void RaiseJointPositionEventOccurred(IReadOnlyDictionary<JointType, Tuple<Point, bool>> jointPointMap)
        {
            JointPositionEventOccurred?.Invoke(this, new JointPositionEventArgs
            {
                JointPosDict = jointPointMap
            });
        }

        #endregion

        #region Initialisers
        public KinectViewModel()
        {
            windows = new List<Window>();
            TextBoxText = "Hello World";    
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

            RaiseJointPositionEventOccurred(body.GetPointDictFromJoints(kinectReader.CoordinateMapper));


            //Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

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

        public class CustomEventArgs : EventArgs
        {
            public Tuple<string, string> args { get; private set; }

            public CustomEventArgs(string _event, string _data)
            {
                args = new Tuple<string, string>(_event, _data);
            }
        }
    }


}
