using KinectDissertationProject.Models.Gesture.Gestures;
using KinectDissertationProject.Models.Gesture;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Right;

namespace KinectDissertationProject.Models.Gesture
{
    class GestureController
    {

        public event EventHandler<GestureEventArgs> GestureRecognised;

        private IList<Gesture> gestures = new List<Gesture>();
        public GestureController()
        {
            // AddGesture(SwipeLeftGesture.With(RightHand));
            AddGesture(new RightHandSwipeLeftGesture());
            AddGesture(new LeftHandSwipeLeftGesture());

            AddGesture(SwipeRightGesture.Using(JointType.HandRight));
            AddGesture(SwipeRightGesture.Using(JointType.HandLeft));
        }

        public void CheckGestures(Body body)
        {
            foreach (Gesture gesture in gestures)
            {
                gesture.UpdateGesture(body);
            }
        }

        private void AddGesture(Gesture gesture)
        {
            if (!gestures.Contains(gesture))
            {
                gesture.GestureRecognised += Gesture_GestureRecognised;
                gestures.Add(gesture);
            }
        }

        private void Gesture_GestureRecognised(object sender, GestureEventArgs e)
        {
            GestureRecognised?.Invoke(this, e);

            foreach (Gesture g in gestures)
            {
                g.Reset();
            }
        }
    }
}
