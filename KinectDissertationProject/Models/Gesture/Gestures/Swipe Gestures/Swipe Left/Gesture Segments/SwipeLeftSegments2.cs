using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Gestures.Swipe_Gestures.Swipe_Left.Gesture_Segments
{
    abstract class SwipeLeftSegment2 : OneHandGestureSegment
    {
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public override GestureResult CheckGesture(Body body)
        {
               
            // Hand in front of respective shoulder
            if (body.Joints[Hand].Position.Z < body.Joints[Elbow].Position.Z)
            {
                log.Trace("{0} is in front of {1} - Pass", Hand, Elbow);
                // Hand vertically between neck and hip
                if (body.Joints[Hand].Position.Y < body.Joints[JointType.Head].Position.Y && body.Joints[Hand].Position.Y > body.Joints[JointType.SpineBase].Position.Y)
                {
                    log.Trace("{0} is bewteen Neck and Hip - Pass", Hand);
                    // Hand is to the left of the respective shoulder
                    if (body.Joints[Hand].Position.X < body.Joints[Shoulder].Position.X)
                    {
                        log.Trace("{0} is to the left of {1} - Pass", Hand, Shoulder);
                        return GestureResult.SUCEEDED;
                    }

                    log.Trace("{0} undetermied to left of {1}", Hand, Shoulder);
                    return GestureResult.PAUSED;
                }

                log.Trace("{0} is not between Neck and Hip - FAIL", Hand);

                return GestureResult.FAILED;

            }
            log.Trace("{0} is not in front of {1}", Hand, Elbow);
            return GestureResult.FAILED;
        }
    }

    class RightHandSwipeLeftSegment2 : SwipeLeftSegment2
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

    class LeftHandSwipeLeftSegment2 : SwipeLeftSegment2
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
