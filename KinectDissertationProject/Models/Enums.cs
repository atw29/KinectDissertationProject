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
        LARGE_SWIPE_LEFT,
        LARGE_SWIPE_RIGHT,

        SMALL_SWIPE_DOWN,
        SMALL_SWIPE_UP,

        EXPLOSION_IN,
        EXPLOSION_OUT,

        CROSS_ARMS,

        RIGHT_SWIPE_DOWN_LEFT_HAND_RAISED,
        LEFT_SWIPE_DOWN_RIGHT_HAND_RAISED,

        RIGHT_SWIPE_UP_LEFT_HAND_RAISED,
        LEFT_SWIPE_UP_RIGHT_HAND_RAISED,

        RIGHT_SWIPE_LEFT_LEFT_HAND_RAISED,
        LEFT_SWIPE_LEFT_RIGHT_HAND_RAISED,
        
        RIGHT_SWIPE_RIGHT_LEFT_HAND_RAISED,
        LEFT_SWIPE_RIGHT_RIGHT_HAND_RAISED,

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
