using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Hands
{
    /// <summary>
    /// XY Pre-Determined Regions. Not necessarily exhaustive but common regions included here. 
    /// Left/Right regions determined from Shoudlers not Hips
    /// Z-Position not accounted for.
    /// </summary>
    public enum Region
    {
        // Head
        RIGHT_HEAD_CLOSE,
        HEAD_MIDDLE,
        LEFT_HEAD_CLOSE,

        // Torso
        RIGHT_TORSO_CLOSE,
        TORSO_MIDDLE,
        LEFT_TORSO_CLOSE,

        // Leg
        RIGHT_LEG_CLOSE, // Used as Idle spots usually
        LEG_MIDDLE,
        LEFT_LEG_CLOSE, // Used as Idle spots usually

        //Eblow
        ELBOW,
        LEFT_OF_ELBOW,
        ABOVE_ELBOW,
        RIGHT_OF_ELBOW,
        BELOW_ELBOW,
    }

    /// <summary>
    /// XY Pre-Determined Regions. Not necessarily exhaustive but common regions included here. 
    /// Left/Right regions determined from Shoudlers not Hips
    /// Z-Position not accounted for.
    /// </summary>
    static class BodyRegions
    {
        /// <summary>
        /// Gets the respective Elbow from the Hand
        /// </summary>
        public static JointType Elbow(this JointType hand)
        {
            return hand == JointType.HandRight ? JointType.ElbowRight : JointType.ElbowLeft;
        }

        public static bool IsRight(this JointType hand)
        {
            return hand == JointType.HandRight;
        }

        public static GestureResult IsIdle(this JointType IdleHand, Body body)
        {
            if (IdleHand == JointType.HandRight)
            {
                return IdleHand.InRegion(body, Region.RIGHT_LEG_CLOSE);
            }
            return IdleHand.InRegion(body, Region.LEFT_LEG_CLOSE);
        }

        /// <summary>
        /// Checks if the hand is in the given region. 
        /// </summary>
        /// <param name="hand">Hand to check. Will throw an error if it's not HandRight or HandLeft</param>
        /// <param name="body">Body data</param>
        /// <param name="region">Region to check</param>
        /// <returns>SUCCEEDED if true, PAUSED if waiting or FAILED if false</returns>
        public static GestureResult InRegion(this JointType hand, Body body, Region region)
        {
            if (hand != JointType.HandRight && hand != JointType.HandLeft)
            {
                throw new ArgumentException(string.Format("Joint passed to Region check must be either HandRight or HandLeft. Given: {0}", hand.ToString()));
            }
            switch (region)
            {
                // Head
                case Region.RIGHT_HEAD_CLOSE:
                    return hand.InRHCRegion(body);
                case Region.HEAD_MIDDLE:
                    return hand.InHMRegion(body);
                case Region.LEFT_HEAD_CLOSE:
                    return hand.InLHCRegion(body);

                // Torso
                case Region.RIGHT_TORSO_CLOSE:
                    return hand.InRTCRegion(body);
                case Region.TORSO_MIDDLE:
                    return hand.InTMRegion(body);
                case Region.LEFT_TORSO_CLOSE:
                    return hand.InLTCRegion(body);

                // Leg
                case Region.RIGHT_LEG_CLOSE:
                    return hand.InRLCRegion(body);
                case Region.LEFT_LEG_CLOSE:
                    return hand.InLLCRegion(body);
                case Region.LEG_MIDDLE:
                    return hand.InLMRegion(body);

                //Elbow
                case Region.ELBOW:
                    return hand.InElbowRegion(body);
                case Region.LEFT_OF_ELBOW:
                    return hand.LeftOfElbowRegion(body);
                case Region.ABOVE_ELBOW:
                    throw new NotImplementedException("Not implemented above elbow region");
                case Region.RIGHT_OF_ELBOW:
                    return hand.RightOfElbowRegion(body);
                case Region.BELOW_ELBOW:
                    throw new NotImplementedException("Not implemnted below elbow region");


                // Should Never Reach
                default:
                    throw new ArgumentException(string.Format("Region {0} is not valid. Check the Region has its respective implementation", region.ToString()));
            }
        }


        #region Regions

        #region Z-Regions
        public static bool InFrontOf(this JointType hand, JointType jointBehind, Body body)
        {
            return body.Joints[hand].Position.Z < body.Joints[jointBehind].Position.Z;
        }
        #endregion

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

        #region Elbow Regions

        private static GestureResult InElbowRegion(this JointType hand, Body body)
        {
            if (hand.InVerticalElbowSection(body))
            {
                if (hand.InHorizontalElbowSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            } else if (hand.InHorizontalElbowSection(body))
            {
                if (hand.InVerticalElbowSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult LeftOfElbowRegion(this JointType hand, Body body)
        {
            if (hand.InVerticalElbowSection(body))
            {
                if (hand.LeftOfElbowSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }
        private static GestureResult RightOfElbowRegion(this JointType hand, Body body)
        {
            if (hand.InVerticalElbowSection(body))
            {
                if (hand.RightOfElbowSection(body))
                {
                    return GestureResult.SUCEEDED;
                }
                return GestureResult.PAUSED;
            }
            return GestureResult.FAILED;
        }

        #endregion

        #endregion

        #region Sections

        #region Elbows

        private static bool InVerticalElbowSection(this JointType hand, Body body)
        {
            return !hand.AboveElbowSection(body) && !hand.BelowElbowSection(body);
        }
        private static bool InHorizontalElbowSection(this JointType hand, Body body)
        {
            return !hand.LeftOfElbowSection(body) && !hand.RightOfElbowSection(body);
        }
        private static bool AboveElbowSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.Y - body.Joints[hand.Elbow()].Position.Y > 0.06;
        }
        private static bool BelowElbowSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.Y - body.Joints[hand.Elbow()].Position.Y < -0.06;
        }

        private static bool LeftOfElbowSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.X - body.Joints[hand.Elbow()].Position.X < -0.15;
        }
        private static bool RightOfElbowSection(this JointType hand, Body body)
        {
            return body.Joints[hand].Position.X - body.Joints[hand.Elbow()].Position.X > 0.06;
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

        #endregion

    }
}
