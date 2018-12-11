using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture
{
    abstract class RelativeGestureSegment
    {
        
        /// <summary>
        /// Checks the gesture 
        /// </summary>
        /// <param name="body">Body data</param>
        /// <returns>GestureResult based on whether the gesture succeded </returns>
        public abstract GestureResult CheckGesture(Body body);
    }
}
