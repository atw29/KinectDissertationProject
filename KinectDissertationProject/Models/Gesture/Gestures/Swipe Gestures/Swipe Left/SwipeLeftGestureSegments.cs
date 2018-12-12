using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left
{
    public static class SwipeLeftGestureSegment1
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, GetRegion(dominantHand));
        }

        private static Region GetRegion(JointType dominantHand)
        {
            return dominantHand.IsRight() ? Region.RIGHT_TORSO_CLOSE : Region.TORSO_MIDDLE;
        }
    }
    public static class SwipeLeftGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, GetRegion(dominantHand));
        }

        private static Region GetRegion(JointType dominantHand)
        {
            return dominantHand.IsRight() ? Region.TORSO_MIDDLE : Region.LEFT_TORSO_CLOSE;
        }
    }
}
