using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left.Gesture_Segments
{
    abstract class SwipeLeftSegment : RelativeGestureSegment
    {
        abstract protected JointType Hand { get; }
        abstract protected JointType Elbow { get; }
        abstract protected JointType Shoulder { get; }

    }
}
