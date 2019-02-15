using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models
{
    
    public enum ApplicationOperation
    {
        MINIMISE,
        MAXIMISE,

        SWITCH_WINDOW,

        NONE
    }

    public enum GestureType
    {
        NONE,

        RIGHT_HAND_SWIPE_LEFT,
        LEFT_HAND_SWIPE_LEFT,

        RIGHT_HAND_SWIPE_RIGHT,
        LEFT_HAND_SWIPE_RIGHT,

        RIGHT_HAND_SWIPE_UP,
        LEFT_HAND_SWIPE_UP,

        RIGHT_HAND_SWIPE_DOWN,
        LEFT_HAND_SWIPE_DOWN,

        LARGE_SWIPE_DOWN,
        LARGE_SWIPE_UP,

        SMALL_SWIPE_DOWN,
        SMALL_SWIPE_UP
    }

    public enum GestureResult
    {
        SUCEEDED,
        FAILED,
        LOST_TRACK,
        PAUSED
    }

    public static class EnumExtensions
    {
        /// <summary>
        /// Returns true if the result is either SUCEEDED or PAUSED
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsSuceeding(this GestureResult result)
        {
            return result == GestureResult.SUCEEDED || result == GestureResult.PAUSED ? true : false;
        }
    }
}
