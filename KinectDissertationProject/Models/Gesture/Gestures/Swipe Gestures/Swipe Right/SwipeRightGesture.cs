using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Right
{

    public static class SwipeRightGesture
    {
        public static OneHandGesture UsingOneHand(JointType DominantHand = JointType.HandRight)
        {
            return new OneHandGesture(DominantHand, GetGestureType(DominantHand), GetSegments(DominantHand));
        }

        public static TwoHandGesture UsingTwoHands()
        {
            return new TwoHandGesture(GestureType.LARGE_SWIPE_RIGHT, GetTwoHandSegments());
        }

        public static OneHandGesture WithOffHandRaised(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetOffHandRaisedType(dominantHand), GetOffHandRaisedSegments(dominantHand));
        }

        private static OneHandGestureSegment[] GetOffHandRaisedSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] gestureSegments = new OffHandNonIdleGestureSegment[2];
            gestureSegments[0] = SwipeRightGestureSegment1.WithOffHandRaised(dominantHand);
            gestureSegments[1] = SwipeRightGestureSegment2.WithOffHandRaised(dominantHand);
            return gestureSegments;
        }

        private static GestureType GetOffHandRaisedType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_SWIPE_RIGHT_LEFT_HAND_RAISED : GestureType.LEFT_SWIPE_RIGHT_RIGHT_HAND_RAISED;
        }

        private static TwoHandGestureSegment[] GetTwoHandSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = SwipeRightGestureSegment1.UsingTwoHands();
            gestureSegments[1] = SwipeRightGestureSegment2.UsingTwoHands();
            return gestureSegments;
        }

        private static OneHandGestureSegment[] GetSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] relativeGestureSegments = new OneHandGestureSegment[2];
            relativeGestureSegments[0] = SwipeRightGestureSegment1.Using(dominantHand);
            relativeGestureSegments[1] = SwipeRightGestureSegment2.Using(dominantHand);
            return relativeGestureSegments;
        }

        private static GestureType GetGestureType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_HAND_SWIPE_RIGHT : GestureType.LEFT_HAND_SWIPE_RIGHT;
        }
    }
}
