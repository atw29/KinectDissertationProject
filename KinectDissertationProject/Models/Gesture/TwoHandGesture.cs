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
        public TwoHandGesture(GestureType type, RelativeGestureSegment[] gestureSegments) : base(type, gestureSegments)
        {
        }


    }
    public abstract class TwoHandGestureSegment : RelativeGestureSegment
    {
        protected readonly JointType RightHand = JointType.HandRight;
        protected readonly JointType LeftHand = JointType.HandLeft;
        
        /// <summary>
        /// Composes two Gesture Results. 
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        private GestureResult CompareGestures(GestureResult one, GestureResult two)
        {
            if (one == two) return one;

            if (one == GestureResult.FAILED || two == GestureResult.FAILED) return GestureResult.FAILED;
            
            if (one == GestureResult.PAUSED || two == GestureResult.PAUSED) return GestureResult.PAUSED;

            if (one == GestureResult.LOST_TRACK || two == GestureResult.LOST_TRACK) return GestureResult.LOST_TRACK;

            throw new ArgumentException($"Unknown Composition of GestureResults {one} and {two}");
        }

        public override GestureResult CheckGesture(Body body)
        {
            return CompareGestures(LeftPosition(body), RightPosition(body));
        }

        protected abstract GestureResult LeftPosition(Body body);
        protected abstract GestureResult RightPosition(Body body);
    }
}
