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
        public static SwipeGesture Using(JointType dominantHand)
        {
            return new SwipeGesture(GetType(dominantHand), GetSegments(dominantHand));
        }

        private static RelativeGestureSegment[] GetSegments(JointType dominantHand)
        {
            RelativeGestureSegment[] relativeGestureSegments = new RelativeGestureSegment[2];
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
