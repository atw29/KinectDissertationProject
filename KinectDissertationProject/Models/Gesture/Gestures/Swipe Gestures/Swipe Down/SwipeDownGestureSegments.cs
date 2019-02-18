using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{
    public static class SwipeDownGestureSegment1
    {
        public static OneHandGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.LEFT_HEAD_CLOSE, Region.RIGHT_HEAD_CLOSE);
        }

    }
    public static class SwipeDownGestureSegment2
    {
        public static OneHandGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW_BELOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandGestureSegment(Region.LEFT_TORSO_CLOSE, Region.RIGHT_TORSO_CLOSE);
        }
    }
}
