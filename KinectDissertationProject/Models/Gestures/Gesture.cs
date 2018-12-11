using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gestures
{
    class Gesture
    {
        #region Params
        /// <summary>
        /// The Type of Gesture we are
        /// </summary>
        private GestureType type;

        /// <summary>
        /// The parts that make up this gesture
        /// </summary>
        private IRelativeGestureSegment[] gestureParts;

        /// <summary>
        /// The current gesture part that we are matching against
        /// </summary>
        private int currentGesturePart = 0;

        /// <summary>
        /// the number of frames to pause for when a pause is initiated
        /// </summary>
        private int pausedFrameCount = 10;

        /// <summary>
        /// The current frame that we are on
        /// </summary>
        private int frameCount = 0;

        /// <summary>
        /// Are we paused?
        /// </summary>
        private bool paused = false;

        #endregion
        public Gesture(GestureType type, IRelativeGestureSegment[] gestureParts)
        {
            this.type = type;
            this.gestureParts = gestureParts;
        }

        public EventHandler<GestureEventArgs> GestureRecognised;


    }
}
