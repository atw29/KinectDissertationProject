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

    abstract class OneHandGestureSegmentX : RelativeGestureSegment
    {
        abstract protected JointType DominantHand { get; }
        abstract protected JointType IdleHand { get; }

        public override GestureResult CheckGesture(Body body)
        {
            GestureResult idleResult = body.IsIdle(IdleHand);
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
