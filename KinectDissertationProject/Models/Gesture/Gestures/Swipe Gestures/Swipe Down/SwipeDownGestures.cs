using System;
using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{

    public static class SwipeDownGesture
    {
        public static OneHandGesture UsingOneHand(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetType(dominantHand), GetOneHandSegments(dominantHand));
        }

        public static TwoHandGesture UsingTwoHands()
        {
            return new TwoHandGesture(GestureType.LARGE_SWIPE_DOWN, GetTwoHandGestureSegments());
        }

        public static OneHandGesture WithOffHandRaised(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetOffHandRaisedType(dominantHand), GetOffHandRaisedSegments(dominantHand));
        }

        public static OneHandGesture WithOffHandRaisedLeft(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetOffHandRaisedLeftType(dominantHand), GetOffHandRaisedLeftSegments(dominantHand));
        }

        private static OneHandGestureSegment[] GetOffHandRaisedLeftSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] gestureSegments = new OffHandNonIdleGestureSegment[2];
            gestureSegments[0] = SwipeDownGestureSegment1.WithOffHandRaisedLeft(dominantHand);
            gestureSegments[1] = SwipeDownGestureSegment2.WithOffHandRaisedLeft(dominantHand);
            return gestureSegments;
        }

        private static GestureType GetOffHandRaisedLeftType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_SWIPE_DOWN_LEFT_HAND_RAISED_LEFT : throw new NotImplementedException();
        }

        private static OneHandGestureSegment[] GetOffHandRaisedSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] gestureSegments = new OffHandNonIdleGestureSegment[2];
            gestureSegments[0] = SwipeDownGestureSegment1.WithOffHandRaised(dominantHand);
            gestureSegments[1] = SwipeDownGestureSegment2.WithOffHandRaised(dominantHand);
            return gestureSegments;
        }

        private static OneHandGestureSegment[] GetOneHandSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] relativeGestureSegments = new OneHandGestureSegment[2];
            relativeGestureSegments[0] = SwipeDownGestureSegment1.Using(dominantHand);
            relativeGestureSegments[1] = SwipeDownGestureSegment2.Using(dominantHand);
            return relativeGestureSegments;
        }

        private static TwoHandGestureSegment[] GetTwoHandGestureSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = SwipeDownGestureSegment1.UsingTwoHands();
            gestureSegments[1] = SwipeDownGestureSegment2.UsingTwoHands();

            return gestureSegments;
        }

        private static GestureType GetType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_HAND_SWIPE_DOWN : GestureType.LEFT_HAND_SWIPE_DOWN;
        }

        private static GestureType GetOffHandRaisedType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_SWIPE_DOWN_LEFT_HAND_RAISED : GestureType.LEFT_SWIPE_DOWN_RIGHT_HAND_RAISED;
        }
    }

}
