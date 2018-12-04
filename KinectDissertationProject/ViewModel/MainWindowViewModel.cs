using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.ViewModel
{
    class MainWindowViewModel
    {
        private KinectSensor _sensor;
        private BodyFrameSource _bodyFrameSource;
        private BodyFrameReader _bodyFrameReader;

        private IList<Window> windows;

        public MainWindowViewModel()
        {

        }

        public void Add_Window(Window w)
        {
            windows.Add(w);
        }

        internal void Load_Kinect()
        {
            _sensor = KinectSensor.GetDefault();
            
            if (_sensor != null)
            {
                _bodyFrameSource = _sensor.BodyFrameSource;
                _bodyFrameReader = _bodyFrameSource.OpenReader();
            }
        }

        internal void Close_Kinect()
        {
            
            if (_bodyFrameReader != null )
            {
                _bodyFrameReader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }
    }
}
