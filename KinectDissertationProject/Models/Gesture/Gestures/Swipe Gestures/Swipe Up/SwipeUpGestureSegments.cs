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
            return new SwipeGestureSegment(dominantHand, Region.ELBOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.LEFT_LEG_CLOSE, Region.RIGHT_LEG_CLOSE);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaised(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW, Region.ELBOW);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaisedLeft(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW, Region.ELBOW_LEFT);
        }
    }
    public static class SwipeUpGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW_ABOVE);
        }
        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.LEFT_TORSO_CLOSE, Region.RIGHT_TORSO_CLOSE);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaised(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW_ABOVE, Region.ELBOW);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaisedLeft(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW_ABOVE, Region.ELBOW_LEFT);
        }
    }

}
