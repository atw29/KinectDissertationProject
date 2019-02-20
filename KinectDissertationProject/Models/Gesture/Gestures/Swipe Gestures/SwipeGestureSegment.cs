using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures
{
    public class SwipeGestureSegment : OneHandGestureSegment
    {
        public SwipeGestureSegment(JointType hand, Region region) : base(hand)
        {
            Region = region;
        }

        protected readonly Region Region;

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

        protected override GestureResult OffHandCheck(Body body)
        {
            return OffHand.IsIdle(body);
        }
    }

    public class OffHandNonIdleGestureSegment : SwipeGestureSegment
    {
        private readonly Region OffHandRegion;
        public OffHandNonIdleGestureSegment(JointType dominantHand, Region dominantHandRegion, Region offHandRegion) : base(dominantHand, dominantHandRegion)
        {
            OffHandRegion = offHandRegion;
        }

        protected override GestureResult OffHandCheck(Body body)
        {
            return OffHand.InRegion(body, OffHandRegion);
        }

    }

}
