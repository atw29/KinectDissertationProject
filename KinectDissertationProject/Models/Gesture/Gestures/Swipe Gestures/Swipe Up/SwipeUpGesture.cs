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
        public static SwipeGesture Using(JointType DominantHand)
        {
            return new SwipeGesture(GetType(DominantHand), GetSegments(DominantHand));
        }

        private static RelativeGestureSegment[] GetSegments(JointType dominantHand)
        {
            RelativeGestureSegment[] relativeGestureSegments = new RelativeGestureSegment[2];
            relativeGestureSegments[0] = SwipeUpGestureSegment1.Using(dominantHand);
            relativeGestureSegments[1] = SwipeUpGestureSegment2.Using(dominantHand);
            return relativeGestureSegments;
        }

        private static GestureType GetType(JointType dominantHand)
        {
            return dominantHand == JointType.HandRight ? GestureType.RIGHT_HAND_SWIPE_UP : GestureType.LEFT_HAND_SWIPE_UP;
        }
    }

    //class SwipeUpGesture : SwipeGesture
    //{
    //    protected override GestureType Type => throw new NotImplementedException();

    //    protected override void AddGestureSegments(JointType dominantHand)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
