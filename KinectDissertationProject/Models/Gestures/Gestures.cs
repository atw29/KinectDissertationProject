using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gestures
{
    class Gestures
    {

    }

    class Gesture_SwipeDown
    {

        IHandPosition[] handPositions;

        public Gesture_SwipeDown()
        {
            HandPos_TOP handPos_TOP = new HandPos_TOP();
            HandPos_BOT handPos_BOT = new HandPos_BOT();

            handPositions = new IHandPosition[]
            {
                handPos_TOP,
                handPos_BOT
            };
        }

        public GestureResult CheckResult(Body body)
        {

        }

    }

    enum GestureResult
    {
        SUCEEDED,
        FAILED,
        LOST_TRACK
    }

}
