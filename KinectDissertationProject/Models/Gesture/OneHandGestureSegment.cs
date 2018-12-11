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
        abstract protected Hands.IOneHand Joint { get; }
        protected JointType Hand
        {
            get
            {
                return Joint.Hand;
            }
        }
        protected JointType Elbow
        {
            get
            {
                return Joint.Elbow;
            }
        }
        protected JointType Shoulder
        {
            get
            {
                return Joint.Shoulder;
            }
        }

    }

    interface IOneHandGestureSegment : IRelativeGestureSegment
    {
        IOneHand Joint { get; }
    }
}
