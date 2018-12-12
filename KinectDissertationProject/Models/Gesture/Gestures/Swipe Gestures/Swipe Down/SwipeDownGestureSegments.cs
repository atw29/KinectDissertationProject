using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{
    public static class SwipeDownGestureSegment1
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, GetRegion(dominantHand));
        }

        private static Region GetRegion(JointType dominantHand)
        {
            return dominantHand.IsRight() ? Region.RIGHT_TORSO_CLOSE : Region.LEFT_TORSO_CLOSE;
        }
    }
    public static class SwipeDownGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, GetRegion(dominantHand));
        }

        private static Region GetRegion(JointType dominantHand)
        {
            return dominantHand.IsRight() ? Region.RIGHT_LEG_CLOSE: Region.LEFT_LEG_CLOSE;
        }
    }
}
