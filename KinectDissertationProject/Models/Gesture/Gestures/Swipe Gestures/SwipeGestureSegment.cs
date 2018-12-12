﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures
{
    abstract class SwipeGestureSegment : OneHandGestureSegmentX
    {
        public SwipeGestureSegment(JointType hand) : base(hand) { }

        abstract protected Region Region { get; }

        /// <summary>
        /// Checks that the hand is in front of the respective elbow then checks it's in the correct Region
        /// </summary>
        protected override GestureResult PerformDominantCheck(Body body)
        {
            if (DominantHand.InFrontOf(DominantHand.Elbow(), body))
            {
                return DominantHand.InRegion(body, Region);
            }
            return GestureResult.FAILED;
        }
    }
}
