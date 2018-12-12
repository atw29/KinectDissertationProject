using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture
{
    abstract class OneHandGestureSegment : RelativeGestureSegment
    {
        abstract protected JointType Hand { get; }
        abstract protected JointType Elbow { get; }
        abstract protected JointType Shoulder { get; }
        
    }

    abstract class OneHandGesture : Gesture
    {
        public OneHandGesture(JointType DominantHand)
        {
            CheckHandValid(DominantHand);
        }

        private void CheckHandValid(JointType hand)
        {
            if (hand != JointType.HandLeft && hand != JointType.HandRight)
            {
                throw new ArgumentException(string.Format("Dominant Hand must be either HandRight or HandLeft. Currently it is {0}", hand));
            }
        }

    }

    public abstract class OneHandGestureSegmentX : RelativeGestureSegment
    {
        private JointType _dominantHand;
        protected JointType DominantHand
        {
            get
            {
                return _dominantHand;
            }
            set
            {
                if (value == JointType.HandRight || value == JointType.HandLeft)
                {
                    _dominantHand = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Dominant Hand must be either HandRight or HandLeft. Currently it is {0}", DominantHand));
                }
            }
        }
        protected JointType IdleHand
        {
            get
            {
                if (DominantHand == JointType.HandRight)
                {
                    return JointType.HandLeft;
                } else if (DominantHand == JointType.HandLeft)
                {
                    return JointType.HandRight;
                } else
                {
                    throw new ArgumentException(string.Format("Dominant Hand must be either HandRight or HandLeft. Currently it is {0}", DominantHand));
                }
            }
        }

        public OneHandGestureSegmentX(JointType dominantHand)
        {
            DominantHand = dominantHand;
        }

        /// <summary>
        /// Checks that non-dominant hand is in the IDLE state then calls PerformDominantCheck
        /// </summary>
        public override GestureResult CheckGesture(Body body)
        {
            GestureResult idleResult = IdleHand.IsIdle(body);
            if (idleResult == GestureResult.SUCEEDED)
            {
                return PerformDominantCheck(body);
            }
            return idleResult; // Can be PAUSED
        }

        protected abstract GestureResult PerformDominantCheck(Body body);

    }

    //interface IOneHandGestureSegment : IRelativeGestureSegment
    //{
    //    IOneHand Joint { get; }
    //}
}
