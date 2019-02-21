using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture
{
    public class TwoHandGesture : Gesture
    {
        public TwoHandGesture(GestureType type, TwoHandGestureSegment[] gestureSegments) : base(type, gestureSegments)
        {
        }

        protected override bool CheckPause()
        {
            return true;
        }

        protected override int FailedPausedFrameCount()
        {
            return 5;
        }

        protected override int SuccessfulPausedFrameCount()
        {
            return 10;
        }
    }
    public class TwoHandGestureSegment : RelativeGestureSegment
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        protected readonly JointType RightHand = JointType.HandRight;
        protected readonly JointType LeftHand = JointType.HandLeft;

        protected Region LeftRegion;
        protected Region RightRegion;

        public TwoHandGestureSegment(Region leftRegion, Region rightRegion)
        {
            LeftRegion = leftRegion;
            RightRegion = rightRegion;
        }

        /// <summary>
        /// Composes two Gesture Results. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        protected GestureResult ANDGestures(GestureResult one, GestureResult two)
        {
            if (one == two)
                return one;

            if (one == GestureResult.FAILED || two == GestureResult.FAILED)
                return GestureResult.FAILED;
            
            if (one == GestureResult.PAUSED || two == GestureResult.PAUSED)
                return GestureResult.PAUSED;

            if (one == GestureResult.LOST_TRACK || two == GestureResult.LOST_TRACK)
                return GestureResult.LOST_TRACK;

            throw new ArgumentException($"Unknown Composition of GestureResults {one} and {two}");
        }

        protected GestureResult ORGestures(GestureResult one, GestureResult two)
        {
            
            if (one == GestureResult.SUCEEDED || two == GestureResult.SUCEEDED) 
                return GestureResult.SUCEEDED;
            if (one == GestureResult.PAUSED || two == GestureResult.PAUSED)
                return GestureResult.PAUSED;
            return GestureResult.FAILED;

        }

        public override GestureResult CheckGesture(Body body)
        {
            GestureResult gestureResult = ANDGestures(LeftPosition(body), RightPosition(body));
            //logger.Debug($"Compared Result : {gestureResult}");
            return gestureResult;
        }

        protected GestureResult LeftPosition(Body body)
        {
            return LeftHand.InRegion(body, LeftRegion, false);
        }
        protected GestureResult RightPosition(Body body)
        {
            return RightHand.InRegion(body, RightRegion, false);
        }
    }
}
