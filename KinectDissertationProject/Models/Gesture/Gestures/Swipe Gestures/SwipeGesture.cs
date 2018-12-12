using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures
{
    public class SwipeGesture : Gesture
    {
        private RelativeGestureSegment[] RelativeGestureSegments;
        protected override RelativeGestureSegment[] gestureSegments
        {
            get
            {
                return RelativeGestureSegments;
            }
        }

        private GestureType type;
        protected override GestureType Type
        {
            get
            {
                return type;
            }
        }

        public SwipeGesture(GestureType _type, RelativeGestureSegment[] relativeGestureSegments)
        {
            type = _type;
            RelativeGestureSegments = relativeGestureSegments;
        }
        

    }
}
