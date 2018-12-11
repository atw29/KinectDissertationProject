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

        public EventHandler<GestureEventArgs> GestureRecognised;

        private IList<GestureXYZ> gestures = new List<GestureXYZ>();
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

        public void AddGesture(GestureType gestureType, RelativeGestureSegment[] gestureSegments)
        {
            GestureXYZ gesture = new GestureXYZ(gestureType, gestureSegments);
            gesture.GestureRecognised += Gesture_GestureRecognised;
            gestures.Add(gesture);
        }

        private void Gesture_GestureRecognised(object sender, GestureEventArgs e)
        {
            GestureRecognised?.Invoke(this, e);
        }
    }
}
