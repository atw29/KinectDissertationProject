using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Other_Gestures
{
    public static class CrossGesture 
    {
        public static TwoHandGesture Use()
        {
            return new CrossGestureClass(GestureType.CROSS_ARMS, GetSegments());
        }

        private class CrossGestureClass : TwoHandGesture
        {
            public CrossGestureClass(GestureType type, TwoHandGestureSegment[] gestureSegments) : base(type, gestureSegments)
            {
            }

            protected override int SuccessfulPausedFrameCount()
            {
                return 15;
            }
        }

        private static TwoHandGestureSegment[] GetSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = new TwoHandGestureSegment(Region.LEFT_LEG_CLOSE, Region.RIGHT_LEG_CLOSE);
            gestureSegments[1] = new CrossGestureSegment();
            return gestureSegments;
        }

        private class CrossGestureSegment : TwoHandGestureSegment
        {
            public CrossGestureSegment() : base(Region.TORSO_MIDDLE, Region.TORSO_MIDDLE)
            {
            }

            protected new GestureResult LeftPosition(Body body)
            {
                return ANDGestures(LeftHand.InRegion(body, LeftRegion, false), HandsCrossed(body));
            }

            private GestureResult HandsCrossed(Body body)
            {
                if (body.Joints[LeftHand].Position.X > body.Joints[RightHand].Position.X)
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.FAILED;
            }

            protected new GestureResult RightPosition(Body body)
            {
                return ANDGestures(RightHand.InRegion(body, RightRegion, false), HandsCrossed(body));
            }

        }
    }
}
