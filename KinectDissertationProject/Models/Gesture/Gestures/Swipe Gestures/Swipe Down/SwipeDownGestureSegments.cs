using KinectDissertationProject.Models.Gesture.Hands;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Down
{
    public static class SwipeDownGestureSegment1
    {
        public static OneHandGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandSwipeGestureSegment(Region.LEFT_HEAD_CLOSE, Region.RIGHT_HEAD_CLOSE);
        }

    }
    public static class SwipeDownGestureSegment2
    {
        public static OneHandGestureSegment Using(JointType dominantHand)
        {
            return new SwipeGestureSegment(dominantHand, Region.ELBOW_BELOW);
        }

        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new TwoHandSwipeGestureSegment(Region.LEFT_TORSO_CLOSE, Region.RIGHT_TORSO_CLOSE);
        }
    }

    public static class SwipeDownGestureSegment3
    {
        public static TwoHandGestureSegment UsingTwoHands()
        {
            return new LenientTwoHandGestureSegment(Region.LEFT_HEAD_CLOSE, Region.RIGHT_HEAD_CLOSE, Region.LEFT_TORSO_CLOSE, Region.RIGHT_TORSO_CLOSE);
        }
    }

    public class LenientTwoHandGestureSegment : TwoHandGestureSegment
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Region LeftInitial;
        private readonly Region RightInitial;

        private readonly Region LeftTarget;
        private readonly Region RightTarget;

        public LenientTwoHandGestureSegment(Region leftInitial, Region rightInitial, Region leftTarget, Region rightTarget)
        {
            LeftInitial = leftInitial;
            RightInitial = rightInitial;
            LeftTarget = leftTarget;
            RightTarget = rightTarget;
        }

        private bool BothInitial(Body body)
        {
            if (LeftHand.InRegion(body, LeftInitial).IsSuceeding() && RightHand.InRegion(body, RightInitial).IsSuceeding())
            {
                return true;
            }
            return false;
        }

        public override GestureResult CheckGesture(Body body)
        {
            // If both initial FAIL
            // If one passes through then succeed and assume the other is coming
            // If both through then succeed

            //if (BothInitial(body))
            //{
            //    logger.Debug("Both Initial : FAIL");
            //    return GestureResult.FAILED;
            //}
            //logger.Debug("Not Both Initial : PASSTHROUGH");
            //return base.CheckGesture(body);

            GestureResult rightResult = RightHand.InRegion(body, RightTarget);
            if (rightResult.IsSuceeding())
            {
                logger.Debug("Right Hand Succeeding");
                return GestureResult.SUCEEDED;
            }
            GestureResult leftResult = LeftHand.InRegion(body, LeftTarget);
            if (leftResult.IsSuceeding())
            {
                logger.Debug("Left Hand Succeeding");
                return GestureResult.SUCEEDED;
            }
            return GestureResult.FAILED;
        }

        private GestureResult LenientCheck(Body body, JointType hand, Region initialRegion, Region targetRegion)
        {

            GestureResult initialResult = hand.InRegion(body, initialRegion);
            GestureResult targetResult = hand.InRegion(body, targetRegion);
            
            if (targetResult.IsSuceeding() || initialResult.IsSuceeding())
            {
                return GestureResult.SUCEEDED;
            }
            return GestureResult.FAILED;
        }

        protected override GestureResult LeftPosition(Body body)
        {
            return LenientCheck(body, LeftHand, LeftInitial, LeftTarget);
        }

        protected override GestureResult RightPosition(Body body)
        {
            return LenientCheck(body, RightHand, RightInitial, RightTarget);
        }
    }
}
