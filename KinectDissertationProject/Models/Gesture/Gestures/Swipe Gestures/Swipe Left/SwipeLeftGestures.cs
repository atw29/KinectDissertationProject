using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left
{
    public static class SwipeLeftGesture
    {
        public static OneHandGesture UsingOneHand(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetType(dominantHand), GetSegments(dominantHand));
        }

        public static TwoHandGesture UsingTwoHands()
        {
            return new TwoHandGesture(GestureType.LARGE_SWIPE_LEFT, GetTwoHandsSegments());
        }

        public static OneHandGesture WithOffHandRaised(JointType dominantHand = JointType.HandRight)
        {
            return new OneHandGesture(dominantHand, GetOffHandRaisedType(dominantHand), GetOffHandRaisedSegments(dominantHand));
        }

        private static OneHandGestureSegment[] GetOffHandRaisedSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] gestureSegments = new OffHandNonIdleGestureSegment[2];
            gestureSegments[0] = SwipeLeftGestureSegment1.WithOffHandRaised(dominantHand);
            gestureSegments[1] = SwipeLeftGestureSegment2.WithOffHandRaised(dominantHand);
            return gestureSegments;
        }

        private static GestureType GetOffHandRaisedType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_SWIPE_LEFT_LEFT_HAND_RAISED : GestureType.LEFT_SWIPE_LEFT_RIGHT_HAND_RAISED;
        }

        private static TwoHandGestureSegment[] GetTwoHandsSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = SwipeLeftGestureSegment1.UsingTwoHands();
            gestureSegments[1] = SwipeLeftGestureSegment2.UsingTwoHands();
            return gestureSegments;
        }

        private static OneHandGestureSegment[] GetSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] relativeGestureSegments = new OneHandGestureSegment[2];
            relativeGestureSegments[0] = SwipeLeftGestureSegment1.Using(dominantHand);
            relativeGestureSegments[1] = SwipeLeftGestureSegment2.Using(dominantHand);
            return relativeGestureSegments;
        }

        private static GestureType GetType(JointType dominantHand)
        {
            return dominantHand == JointType.HandRight ? GestureType.RIGHT_HAND_SWIPE_LEFT : GestureType.LEFT_HAND_SWIPE_LEFT;
        }
    }
}
