﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left.Gesture_Segments;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gestures.GestureSegments.Swipe_Gesture.Swipe_Left
{
    abstract class SwipeLeftSegment1 : SwipeLeftSegment
    {
 
        public override GestureResult CheckGesture(Body body)
        {
            GestureResult gestureResult = GestureResult.FAILED;
            // Hand in front of respective shoulder
            if (body.Joints[Hand].Position.Z < body.Joints[Elbow].Position.Z )
            {
                logger.Debug("{0} is in front of {1} - Pass", Hand, Elbow);
                // Hand vertically between neck and hip
                if (body.Joints[Hand].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[Hand].Position.Y > body.Joints[JointType.SpineBase].Position.Y)
                {
                    logger.Debug("{0} is bewteen Neck and Hip - Pass", Hand);
                    // Hand is to the right of the respective shoulder
                    if (body.Joints[Hand].Position.X > body.Joints[Shoulder].Position.X)
                    {
                        logger.Debug("{0} is to the right of {1} - Pass", Hand, Shoulder);
                        gestureResult = GestureResult.SUCEEDED;
                    }

                    logger.Debug("{0} undetermied to right of {1}", Hand, Shoulder);
                    gestureResult = GestureResult.PAUSED;
                }

                logger.Debug("{0} is not between Neck and Hip - FAIL", Hand);

                gestureResult = GestureResult.FAILED;

            }
            logger.Debug("{0} is not in front of {1}", Hand, Elbow);
            return gestureResult;
        }
    }

    class RightHandSwipeLeftSegment1 : SwipeLeftSegment1
    {
        protected override JointType Hand
        {
            get
            {
                return JointType.HandRight;
            }
        }

        protected override JointType Elbow
        {
            get
            {
                return JointType.ElbowRight;
            }
        }

        protected override JointType Shoulder
        {
            get
            {
                return JointType.ShoulderRight;
            }
        }
    }
    class LeftHandSwipeLeftSegment1 : SwipeLeftSegment1
    {
        protected override JointType Hand
        {
            get
            {
                return JointType.HandLeft;
            }
        }

        protected override JointType Elbow
        {
            get
            {
                return JointType.ElbowLeft;
            }
        }

        protected override JointType Shoulder
        {
            get
            {
                return JointType.ShoulderLeft;
            }
        }
    }

}