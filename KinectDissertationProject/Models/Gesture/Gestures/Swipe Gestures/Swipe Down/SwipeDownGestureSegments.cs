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
            return new SwipeGestureSegment(dominantHand, Region.ELBOW);
        }
    }
    public static class SwipeDownGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW_BELOW);
        }
    }
}
