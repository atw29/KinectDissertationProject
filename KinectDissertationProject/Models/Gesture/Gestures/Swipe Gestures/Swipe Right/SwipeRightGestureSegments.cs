using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Right
{
    public static class SwipeRightGestureSegment1
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, dominantHand == JointType.HandRight ? Region.TORSO_MIDDLE : Region.LEFT_TORSO_CLOSE);
        }
    }

    public static class SwipeRightGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, dominantHand == JointType.HandRight ? Region.RIGHT_TORSO_CLOSE : Region.TORSO_MIDDLE);
        }
    }
}
