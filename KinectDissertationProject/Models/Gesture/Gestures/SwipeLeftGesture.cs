using KinectDissertationProject.Models.Gestures;
using KinectDissertationProject.Models.Gestures.GestureSegments.Swipe_Gesture.Swipe_Left;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures
{
    class RightHandSwipeLeftGesture : GestureXYZ
    {
        private RelativeGestureSegment[] swipeLeftSegments;

        public RightHandSwipeLeftGesture()
        {
            swipeLeftSegments = new RelativeGestureSegment[3];
            swipeLeftSegments[0] = new SwipeLeftSegment1(JointType.HandRight, JointType.ElbowRight, JointType.ShoulderRight);
        }

        protected override RelativeGestureSegment[] gestureSegments {
            get
            {
                return swipeLeftSegments;
            }
        }

        protected override GestureType type
        {
            get
            {
                return GestureType.RIGHT_HAND_SWIPE_LEFT;
            }
        }
    }
}
