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

        private IList<Gesture> gestures = new List<Gesture>();
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

        public void AddGesture(GestureType gestureType, IRelativeGestureSegment[] gestureSegments)
        {
            Gesture gesture = new Gesture(gestureType, gestureSegments);
            gesture.GestureRecognised += Gesture_GestureRecognised;
            gestures.Add(gesture);
        }

        private void Gesture_GestureRecognised(object sender, GestureEventArgs e)
        {
            GestureRecognised?.Invoke(this, e);
        }
    }
}
