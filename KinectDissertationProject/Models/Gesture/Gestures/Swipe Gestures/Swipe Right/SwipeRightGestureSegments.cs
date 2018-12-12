using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Right
{
    class SwipeRightGestureSegment1 : SwipeGestureSegment
    {
        private Region region;
        protected override Region Region
        {
            get
            {
                return region;
            }
        }

        public SwipeRightGestureSegment1(JointType dominantHand) : base(dominantHand)
        {
            region = dominantHand == JointType.HandRight ? Region.TORSO_MIDDLE : Region.LEFT_TORSO_CLOSE;
        }
    }

    class SwipeRightGestureSegment2 : SwipeGestureSegment
    {
        private Region region;
        protected override Region Region
        {
            get
            {
                return region;
            }
        }

        public SwipeRightGestureSegment2(JointType dominantHand) : base(dominantHand)
        {
            region = dominantHand == JointType.HandRight ? Region.RIGHT_TORSO_CLOSE : Region.TORSO_MIDDLE;
        }
    }
}
