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
            return new SwipeGestureSegment(dominantHand, Region.ELBOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.RIGHT_TORSO_CLOSE, Region.RIGHT_TORSO_CLOSE);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaised(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW, Region.ELBOW);
        }
    }
    public static class SwipeLeftGestureSegment2
    {
        public static SwipeGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW_LEFT);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.TORSO_MIDDLE, Region.TORSO_MIDDLE);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaised(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW_LEFT, Region.ELBOW);
        }
    }
}
