using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.SmallSwipes
{
    public static class SwipeRightSmallGesture
    {
        public static SwipeGesture Using(JointType DominantHand)
        {
            return new SwipeGesture(GestureType.RIGHT_HAND_SWIPE_RIGHT_SMALL, GetSegments(DominantHand));
        }

        private static RelativeGestureSegment[] GetSegments(JointType dominantHand)
        {
            RelativeGestureSegment[] relativeGestureSegments = new RelativeGestureSegment[3];
            relativeGestureSegments[0] = new SmallGestureSegment(dominantHand);


            return relativeGestureSegments;
        }
    }

    class SmallGestureSegment : OneHandGestureSegment
    {
        public SmallGestureSegment(JointType dominantHand) : base(dominantHand)
        {
        }

        protected override GestureResult PerformDominantCheck(Body body)
        {
            if (DominantHand.InFrontOf(DominantHand.Elbow(), body))
            {

            }
            return GestureResult.FAILED;
        }
    }
}
