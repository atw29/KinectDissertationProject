using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{
 
    public static class SwipeDownGesture
    {
        public static SwipeGesture Using(JointType dominantHand)
        {
            return new SwipeGesture(GetType(dominantHand), GetSegments(dominantHand));
        }

        private static RelativeGestureSegment[] GetSegments(JointType dominantHand)
        {
            RelativeGestureSegment[] relativeGestureSegments = new RelativeGestureSegment[2];
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
