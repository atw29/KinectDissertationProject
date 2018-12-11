using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Hands
{
    class RightHand : IOneHand
    {
        public JointType Hand
        {
            get
            {
                return JointType.HandRight;
            }
        }

        public JointType Shoulder
        {
            get
            {
                return JointType.ShoulderRight;
            }
        }

        public JointType Elbow
        {
            get
            {
                return JointType.ElbowRight;
            }
        }
    }

    class LeftHand : IOneHand
    {
        public JointType Hand
        {
            get
            {
                return JointType.HandLeft;
            }
        }

        public JointType Shoulder
        {
            get
            {
                return JointType.ShoulderLeft;
            }
        }

        public JointType Elbow
        {
            get
            {
                return JointType.ElbowLeft;
            }
        }
    }

    interface IOneHand
    {
        JointType Hand { get; }
        JointType Shoulder { get; }
        JointType Elbow { get; }
    }
}
