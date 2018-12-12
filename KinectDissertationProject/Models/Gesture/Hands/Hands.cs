using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture.Hands
{

    public abstract class OneHand
    {
        public abstract JointType Hand { get; }
        public abstract JointType Shoulder { get; }
        public abstract JointType Elbow { get; }
        public abstract JointType Hip { get; }
    }

    public class RightHand : OneHand
    {
        public static RightHand Instance { get; private set; }

        static RightHand()
        {
            Instance = new RightHand();
        }
        public override JointType Hand
        {
            get
            {
                return JointType.HandRight;
            }
        }

        public override JointType Shoulder
        {
            get
            {
                return JointType.ShoulderRight;
            }
        }

        public override JointType Elbow
        {
            get
            {
                return JointType.ElbowRight;
            }
        }
        public override JointType Hip
        {
            get
            {
                return JointType.HipRight;
            }
        }
    }

    public class LeftHand : OneHand
    {
        public static LeftHand Instance { get; private set; }
        static LeftHand()
        {
            Instance = new LeftHand();
        }

        public override JointType Hand
        {
            get
            {
                return JointType.HandLeft;
            }
        }

        public override JointType Shoulder
        {
            get
            {
                return JointType.ShoulderLeft;
            }
        }

        public override JointType Elbow
        {
            get
            {
                return JointType.ElbowLeft;
            }
        }

        public override JointType Hip
        {
            get
            {
                return JointType.HipLeft;
            }
        }
    }

}
