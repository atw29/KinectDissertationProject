﻿using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left.Gesture_Segments;
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
    abstract class SwipeLeftGesture : Models.Gestures.Gesture
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

    class RightHandSwipeLeftGesture : SwipeLeftGesture
    {
        private RelativeGestureSegment[] relativeGestureSegments;
        protected override RelativeGestureSegment[] swipeLeftSegments
        {
            get
            {
                return relativeGestureSegments;
            }
        }

        public RightHandSwipeLeftGesture()
        {
            relativeGestureSegments = new RelativeGestureSegment[2];
            relativeGestureSegments[0] = new RightHandSwipeLeftSegment1();
            relativeGestureSegments[1] = new RightHandSwipeLeftSegment2();
        }

        protected override GestureType type
        {
            get
            {
                return GestureType.RIGHT_HAND_SWIPE_LEFT;
            }
        }

    }

    class LeftHandSwipeLeftGesture : SwipeLeftGesture
    {
        private RelativeGestureSegment[] relativeGestureSegments;
        protected override RelativeGestureSegment[] swipeLeftSegments
        {
            get
            {
                return relativeGestureSegments;
            }
        }

        public LeftHandSwipeLeftGesture()
        {
            relativeGestureSegments = new RelativeGestureSegment[2];
            relativeGestureSegments[0] = new LeftHandSwipeLeftSegment1();
            relativeGestureSegments[1] = new LeftHandSwipeLeftSegment2();
        }

        protected override GestureType type
        {
            get
            {
                return GestureType.LEFT_HAND_SWIPE_LEFT;
            }
        }
    }
}