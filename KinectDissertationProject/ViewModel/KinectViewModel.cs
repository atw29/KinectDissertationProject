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
    class KinectViewModel : INotifyPropertyChanged
    {
        private KinectReader kinectReader;
        private IList<Window> windows;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<CustomEventArgs> EventOccurred;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseEventOccurred(string _eventIdentifier, string _data)
        {
            EventOccurred?.Invoke(this, new CustomEventArgs(_eventIdentifier, _data));
        }

        public string TextBoxText{ get; set; }

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
            RaisePropertyChangedEvent("open");
        }

        internal void Close_Kinect()
        {
            kinectReader.Close();
        }

        #endregion

        private void Kinect_OnTrackedBody(object sender, BodyEventArgs e)
        {
            Body body = e.BodyData;

            RaiseEventOccurred("handmoved", body.Joints[JointType.HandLeft].GetPointFromJoint(kinectReader.coordinateMapper).ToString());

        }

        private void Kinect_LostTracking(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void _bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            
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
