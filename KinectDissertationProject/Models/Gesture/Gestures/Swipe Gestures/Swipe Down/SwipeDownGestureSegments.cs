﻿using KinectDissertationProject.Models.Gesture.Hands;
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

        public static OffHandNonIdleGestureSegment WithOffHandRaised(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW, Region.ELBOW);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaisedLeft(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW, Region.ELBOW_LEFT);
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

        public static OffHandNonIdleGestureSegment WithOffHandRaised(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW_BELOW, Region.ELBOW);
        }

        public static OffHandNonIdleGestureSegment WithOffHandRaisedLeft(JointType dominantHand)
        {
            return new OffHandNonIdleGestureSegment(dominantHand, Region.ELBOW_BELOW, Region.ELBOW_LEFT);
        }
    }
}
