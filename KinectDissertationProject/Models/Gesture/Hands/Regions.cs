using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Hands
{
    /// <summary>
    /// XY Pre-Determined Regions. Not necessarily exhaustive but common regions included here. Z-Position not accounted for.
    /// </summary>
    public enum Region
    {
        RIGHT_TORSO_CLOSE,
        TORSO_MIDDLE,
        LEFT_TORSO_CLOSE,

        RIGHT_LEG_CLOSE, // Used as Idle spots usually
        LEG_MIDDLE,
        LEFT_LEG_CLOSE // Used as Idle spots usually
    }

    /// <summary>
    /// XY Pre-Determined Regions. Not necessarily exhaustive but common regions included here. 
    /// Left/Right regions determined from Shoudlers not Hips
    /// Z-Position not accounted for.
    /// </summary>
    static class BodyRegions
    {
        public static GestureResult IsIdle(this Body body, JointType IdleHand)
        {
            if (IdleHand == JointType.HandRight)
            {
                return body.InRegion(IdleHand, Region.RIGHT_LEG_CLOSE);
            }
            return body.InRegion(IdleHand, Region.LEFT_LEG_CLOSE);
        }

        public static GestureResult InRegion(this Body body, JointType hand, Region region)
        {
            switch (region)
            {
                case Region.LEFT_LEG_CLOSE:
                    return hand.InLLCRegion(body);
                default:
                    return GestureResult.FAILED;
            }
        }

        #region Head Regions

        private static GestureResult InLHCRegion(this JointType hand, Body body)
        {
            if (hand.InHeadSection(body))
            {
                if (hand.InLeftSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult InHMRegion(this JointType hand, Body body)
        {
            if (hand.InHeadSection(body))
            {
                if (hand.InMidSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult InRHCRegion(this JointType hand, Body body)
        {
            if (hand.InHeadSection(body))
            { 
                if (hand.InRightSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }

        #endregion

        #region Torso Regions

        private static GestureResult InLTCRegion(this JointType hand, Body body)
        {
            if (hand.InTorsoSection(body))
            {
                if (hand.InLeftSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult InTMRegion(this JointType hand, Body body)
        {
            if (hand.InTorsoSection(body))
            {
                if (hand.InMidSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult InRTCRegion(this JointType hand, Body body)
        {
            if (hand.InTorsoSection(body))
            {
                if (hand.InRightSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }

        #endregion

        #region Leg Regions
        private static GestureResult InLLCRegion(this JointType hand, Body body)
        {
            if (hand.InLegSection(body))
            {
                if (hand.InLeftSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult InLMRegion(this JointType hand, Body body )
        {
            if (hand.InLegSection(body))
            {
                if (hand.InMidSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult InRLCRegion(this JointType hand, Body body)
        {
            if (hand.InLegSection(body))
            {
                if (hand.InRightSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        #endregion

        #region Sections

        /// <summary>
        /// Left of Left Shoulder
        /// </summary>
        private static bool InLeftSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.X < body.Joints[JointType.ShoulderLeft].Position.X;
        }
        /// <summary>
        /// Between Right and Left Shoulders
        /// </summary>
        private static bool InMidSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.X > body.Joints[JointType.ShoulderLeft].Position.X && body.Joints[hand].Position.X < body.Joints[JointType.ShoulderRight].Position.X;
        }
        /// <summary>
        /// Right of Right Shoulder
        /// </summary>
        private static bool InRightSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.X > body.Joints[JointType.ShoulderRight].Position.X;
        }

        /// <summary>
        /// Above Shoulders
        /// </summary>
        private static bool InHeadSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.Y > body.Joints[JointType.SpineShoulder].Position.Y;
        }
        /// <summary>
        /// Between Hips and Shoulders
        /// </summary>
        private static bool InTorsoSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.Y < body.Joints[JointType.SpineShoulder].Position.Y && body.Joints[hand].Position.Y > body.Joints[JointType.SpineBase].Position.Y;
        }
        /// <summary>
        /// Beneath Hips
        /// </summary>
        private static bool InLegSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.Y < body.Joints[JointType.SpineBase].Position.Y;
        }
        #endregion

    }
}
