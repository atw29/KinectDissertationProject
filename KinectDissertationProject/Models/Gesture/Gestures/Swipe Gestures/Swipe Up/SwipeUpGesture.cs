using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Up
{
    public static class SwipeUpGesture
    {
        public static OneHandGesture UsingOneHand(JointType DominantHand = JointType.HandRight)
        {
            return new OneHandGesture(DominantHand, GetType(DominantHand), GetSegments(DominantHand));
        }

        public static TwoHandGesture UsingTwoHands()
        {
            return new TwoHandGesture(GestureType.LARGE_SWIPE_UP, GetTwoHandGestureSegments());
        }

        public static OneHandGesture WithOffHandRaised(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetOffHandRaisedType(dominantHand), GetOffHandSegments(dominantHand));
        }
        public static OneHandGesture WithOffHandRaisedLeft(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetOffHandRaisedLeftType(dominantHand), GetOffHandRaisedLeftSegments(dominantHand));
        }

        private static OneHandGestureSegment[] GetOffHandRaisedLeftSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] gestureSegments = new OffHandNonIdleGestureSegment[2];
            gestureSegments[0] = SwipeUpGestureSegment1.WithOffHandRaisedLeft(dominantHand);
            gestureSegments[1] = SwipeUpGestureSegment2.WithOffHandRaisedLeft(dominantHand);
            return gestureSegments;
        }

        private static GestureType GetOffHandRaisedLeftType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_SWIPE_UP_LEFT_HAND_RAISED_LEFT : throw new NotImplementedException();
        }

        private static OneHandGestureSegment[] GetOffHandSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] gestureSegments = new OffHandNonIdleGestureSegment[2];
            gestureSegments[0] = SwipeUpGestureSegment1.WithOffHandRaised(dominantHand);
            gestureSegments[1] = SwipeUpGestureSegment2.WithOffHandRaised(dominantHand);
            return gestureSegments;
        }

        private static GestureType GetOffHandRaisedType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_SWIPE_UP_LEFT_HAND_RAISED : GestureType.LEFT_SWIPE_UP_RIGHT_HAND_RAISED;
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
