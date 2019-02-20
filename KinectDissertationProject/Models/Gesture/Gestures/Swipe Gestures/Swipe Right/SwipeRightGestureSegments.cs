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
            return new SwipeGestureSegment(dominantHand, Region.ELBOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.LEFT_TORSO_CLOSE, Region.LEFT_TORSO_CLOSE);
        }
    }

    public static class SwipeRightGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW_RIGHT);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.TORSO_MIDDLE, Region.TORSO_MIDDLE);
        }
    }
}
