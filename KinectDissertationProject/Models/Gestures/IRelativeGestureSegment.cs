using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gestures
{
    interface IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture 
        /// </summary>
        /// <param name="body">Body data</param>
        /// <returns>GestureResult based on whether the gesture succeded </returns>
        GestureResult CheckGesture(Body body);
    }
}
