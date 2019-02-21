using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Other_Gestures
{
    public static class ExplosionGesture
    {
        public static TwoHandGesture Out()
        {
            return new ExplosionGestureClass(GestureType.EXPLOSION_OUT, OutGestureSegments());
        }
        public static TwoHandGesture In()
        {
            return new ExplosionGestureClass(GestureType.EXPLOSION_IN, InGestureSegments());
        }

        private class ExplosionGestureClass : TwoHandGesture
        {
            public ExplosionGestureClass(GestureType type, TwoHandGestureSegment[] gestureSegments) : base(type, gestureSegments)
            {
            }

            protected override bool CheckPause()
            {
                return base.CheckPause();
            }

            protected override int FailedPausedFrameCount()
            {
                return base.FailedPausedFrameCount();
            }

            protected override int SuccessfulPausedFrameCount()
            {
                return 20;
            }
        }


        private static TwoHandGestureSegment[] OutGestureSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = Closed();
            gestureSegments[1] = Expanded();
            return gestureSegments;
        }

        private static TwoHandGestureSegment[] InGestureSegments()
        {
            TwoHandGestureSegment[] gestureSegments = new TwoHandGestureSegment[2];
            gestureSegments[0] = Expanded();
            gestureSegments[1] = Closed();
            return gestureSegments;
        }

        private static TwoHandGestureSegment Expanded()
        {
            return new ExplosionGestureSegment(Region.LEFT_LEG_CLOSE, Region.LEFT_TORSO_CLOSE, Region.RIGHT_HEAD_CLOSE);
        }

        private static TwoHandGestureSegment Closed()
        {
            return new TwoHandGestureSegment(Region.TORSO_MIDDLE, Region.TORSO_MIDDLE);
        }

        private class ExplosionGestureSegment : TwoHandGestureSegment
        {
            private readonly Region LeftRegionAlt;
            public ExplosionGestureSegment(Region leftRegion, Region leftRegionAlt, Region rightRegion) : base(leftRegion, rightRegion)
            {
                LeftRegionAlt = leftRegionAlt;
            }

            public override GestureResult CheckGesture(Body body)
            {
                return ANDGestures(LeftPositionAlt(body), RightPosition(body));
            }

            protected GestureResult LeftPositionAlt(Body body)
            {
                return ORGestures(LeftHand.InRegion(body, LeftRegion, false), LeftHand.InRegion(body, LeftRegionAlt, false));
            }
        }

    }

}
