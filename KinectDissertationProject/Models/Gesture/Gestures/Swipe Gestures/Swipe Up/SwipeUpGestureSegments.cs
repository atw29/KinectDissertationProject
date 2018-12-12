using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Up
{
    public static class SwipeUpGestureSegment1
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, dominantHand == JointType.HandRight ? Region.RIGHT_TORSO_CLOSE : Region.LEFT_TORSO_CLOSE);
        }
    }
    public static class SwipeUpGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, dominantHand == JointType.HandRight ? Region.RIGHT_HEAD_CLOSE : Region.LEFT_HEAD_CLOSE);
        }
    }

}
