using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{

    public static class SwipeDownGesture
    {
        public static OneHandGesture UsingOneHand(JointType dominantHand)
        {
            return new OneHandGesture(dominantHand, GetType(dominantHand), GetSegments(dominantHand));
        }

        private static OneHandGestureSegment[] GetSegments(JointType dominantHand)
        {
            OneHandGestureSegment[] relativeGestureSegments = new OneHandGestureSegment[2];
            relativeGestureSegments[0] = SwipeDownGestureSegment1.Using(dominantHand);
            relativeGestureSegments[1] = SwipeDownGestureSegment2.Using(dominantHand);
            return relativeGestureSegments;
        }

        private static GestureType GetType(JointType dominantHand)
        {
            return dominantHand.IsRight() ? GestureType.RIGHT_HAND_SWIPE_DOWN : GestureType.LEFT_HAND_SWIPE_DOWN;
        }
    }

}
