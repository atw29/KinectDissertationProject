using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down.Gesture_Segments
{
    class SwipeDownSegment1 : OneHandGestureSegment
    {
        protected override JointType Hand => throw new NotImplementedException();

        protected override JointType Elbow => throw new NotImplementedException();

        protected override JointType Shoulder => throw new NotImplementedException();

        public override GestureResult CheckGesture(Body body)
        {
            return GestureResult.FAILED;
        }
    }
}
