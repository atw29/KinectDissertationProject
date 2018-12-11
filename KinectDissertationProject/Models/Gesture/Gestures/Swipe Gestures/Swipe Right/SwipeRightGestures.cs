using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Right
{
    abstract class SwipeRightGestures : Gesture
    {
        abstract protected RelativeGestureSegment[] swipeLeftSegments { get; }

        protected override RelativeGestureSegment[] gestureSegments
        {
            get
            {
                return swipeLeftSegments;
            }
        }
    }
}
