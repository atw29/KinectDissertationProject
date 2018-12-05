using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.Models
{
    class WindowEventArgs : EventArgs
    {
        public Window Window { get; set; }
        public Operation Operation { get; set; }
        public IReadOnlyDictionary<string, object> Data { get; set; }

    }

    public class BodyEventArgs : EventArgs
    {
        public Body BodyData { get; private set; }

        public BodyEventArgs(Body bodyData)
        {
            BodyData = bodyData;
        }
    }

    public class JointPositionEventArgs : EventArgs
    {
        public IReadOnlyDictionary<JointType, Tuple<Point, bool>> JointPosDict { get; set; }

    }

    public enum Operation
    {
        MINIMISE,
        MAXIMISE,
        GAIN_FOCUS,
        LOSE_FOCUS
    }
}
