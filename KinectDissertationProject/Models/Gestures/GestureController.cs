using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gestures
{
    class GestureController
    {

        private IList<IGesture> gestures = new List<IGesture>();
        public GestureController()
        {
        }

        public void CheckGestures(Body body)
        {
            foreach (IGesture gesture in gestures)
            {
                gesture.CheckResult(body);
            }
        }

        public void AddGesture(GestureType gestureType)
        {

        }

    }
}
