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
        public static Gesture UsingOneHand(JointType DominantHand)
        {
            return new OneHandGesture(DominantHand, GetGestureType(DominantHand), GetSegments(DominantHand));
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
