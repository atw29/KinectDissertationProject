using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures
{
    public abstract class SwipeGesture : Gesture
    {
        private RelativeGestureSegment[] relativeGestureSegments;

        public SwipeGesture(JointType dominantHand)
        {
            relativeGestureSegments = new RelativeGestureSegment[2];
            AddGestureSegments(dominantHand);
        }

        protected abstract void AddGestureSegments(JointType dominantHand);

        protected override RelativeGestureSegment[] gestureSegments
        {
            get
            {
                return relativeGestureSegments;
            }
        }
    }
}
