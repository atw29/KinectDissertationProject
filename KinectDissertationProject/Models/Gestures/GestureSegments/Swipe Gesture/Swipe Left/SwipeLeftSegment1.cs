using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectDissertationProject.Models.Gestures.GestureSegments.Swipe_Gesture.Swipe_Left
{
    class SwipeLeftSegment1 : IRelativeGestureSegment
    {
        private JointType Hand;
        private JointType Elbow;

        public SwipeLeftSegment1(JointType Hand, JointType Elbow)
        {
            this.Hand = Hand;
            this.Elbow = Elbow;
        }

        public override GestureResult CheckGesture(Body body)
        {
            // Hand in front of respective shoulder
            if (body.Joints[Hand].Position.Z < body.Joints[Elbow].Position.Z )
            {
                logger.Debug("{0} is in front of {1}", Hand, Elbow);

            }
        }
    }
}
