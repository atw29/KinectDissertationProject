using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Up
{
    public static class SwipeUpGesture
    {
        public static OneHandGesture UsingOneHand(JointType DominantHand)
        {
            return new OneHandGesture(DominantHand, GetType(DominantHand), GetSegments(DominantHand));
        }

        public static TwoHandGesture UsingTwoHands()
        {
            return new TwoHandGesture(GestureType.LARGE_SWIPE_UP, GetTwoHandGestureSegments());
        }

        private static TwoHandGestureSegment[] GetTwoHandGestureSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = SwipeUpGestureSegment1.UsingTwoHands();
            gestureSegments[1] = SwipeUpGestureSegment2.UsingTwoHands();
            return gestureSegments;
        }

        private static OneHandGestureSegment[] GetSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] relativeGestureSegments = new OneHandGestureSegment[2];
            relativeGestureSegments[0] = SwipeUpGestureSegment1.Using(dominantHand);
            relativeGestureSegments[1] = SwipeUpGestureSegment2.Using(dominantHand);
            return relativeGestureSegments;
        }

        private static GestureType GetType(JointType dominantHand)
        {
            return dominantHand == JointType.HandRight ? GestureType.RIGHT_HAND_SWIPE_UP : GestureType.LEFT_HAND_SWIPE_UP;
        }
    }
}
