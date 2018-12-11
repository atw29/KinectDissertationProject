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

    public enum WindowOperation
    {
        SCROLL_UP,
        SCROLL_DOWN,
        CLICK,
        NONE
    }

    public enum GestureType
    {
        NONE,

        RIGHT_HAND_SWIPE_LEFT,
        LEFT_HAND_SWIPE_LEFT,

        LARGE_SWIPE_DOWN,
        LARGE_SWIPE_UP,

        SMALL_SWIPE_DOWN,
        SMALL_SWIPE_UP
    }

    enum GestureResult
    {
        SUCEEDED,
        FAILED,
        LOST_TRACK,
        PAUSED
    }
}
