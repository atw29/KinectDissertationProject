using KinectDissertationProject.Models.Gesture.Gestures;
using KinectDissertationProject.Models.Gesture;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Right;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Up;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down;
using KinectDissertationProject.Models.Gesture.Gestures.Other_Gestures;

namespace KinectDissertationProject.Models.Gesture
{
    class GestureController
    {
        public event EventHandler<GestureEventArgs> GestureRecognised;

        private IList<Gesture> gestures = new List<Gesture>();
        public GestureController()
        {
            AddGesture(SwipeLeftGesture.UsingOneHand());
            //AddGesture(SwipeLeftGesture.UsingTwoHands());
            AddGesture(SwipeLeftGesture.WithOffHandRaised());
            
            AddGesture(SwipeRightGesture.UsingOneHand());
            //AddGesture(SwipeRightGesture.UsingTwoHands());
            AddGesture(SwipeRightGesture.WithOffHandRaised());
            
            AddGesture(SwipeUpGesture.UsingOneHand());
            //AddGesture(SwipeUpGesture.UsingTwoHands());
            AddGesture(SwipeUpGesture.WithOffHandRaised());
            
            AddGesture(SwipeDownGesture.UsingOneHand());
            //AddGesture(SwipeDownGesture.UsingTwoHands());
            AddGesture(SwipeDownGesture.WithOffHandRaised());


            //AddGesture(ExplosionGesture.In());
            //AddGesture(ExplosionGesture.Out());

            //AddGesture(CrossGesture.Use());
        }

        /// <summary>
        /// Checks Gesture.UpdateGesture() on each defined gesture.
        /// </summary>
        /// <param name="body">The body object </param>
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
