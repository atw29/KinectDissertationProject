using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{
    abstract class SwipeDownGesture : Gesture
    {
        abstract protected RelativeGestureSegment[] swipeDownSegments { get; }

        protected override RelativeGestureSegment[] gestureSegments
        {
            get
            {
                return swipeDownSegments;
            }
        }
    }

    class RightHandSwipeDownGesture : SwipeDownGesture
    {
        protected override RelativeGestureSegment[] swipeDownSegments => throw new NotImplementedException();

        protected override GestureType type => throw new NotImplementedException();
    }
}
