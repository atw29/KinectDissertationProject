using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gesture
{
    public abstract class Gesture
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        #region Params
        /// <summary>
        /// The Type of Gesture we are
        /// </summary>
        protected GestureType Type
        {
            get;
        }

        /// <summary>
        /// The parts that make up this gesture
        /// </summary>
        protected RelativeGestureSegment[] gestureSegments
        {
            get;
        }

        /// <summary>
        /// The current gesture part that we are matching against
        /// </summary>
        private int currentGestureSegment = 0;

        /// <summary>
        /// the number of frames to pause for when a pause is initiated
        /// </summary>
        private int pausedFrameCount;

        /// <summary>
        /// The current frame that we are on
        /// </summary>
        private int frameCount = 0;

        /// <summary>
        /// Are we paused?
        /// </summary>
        private bool paused = false;

        /// <summary>
        /// Maximum frames to read before resetting
        /// </summary>
        const int MAX_FRAME_COUNT = 50;


        #endregion
        protected Gesture(GestureType type, RelativeGestureSegment[] gestureSegments)
        {
            Type = type;
            this.gestureSegments = gestureSegments;
            pausedFrameCount = SuccessfulPausedFrameCount();
        }

        public event EventHandler<GestureEventArgs> GestureRecognised;

        private void PerformCheck(Body body)
        {
            RelativeGestureSegment relativeGestureSegment = gestureSegments[currentGestureSegment];
            if (relativeGestureSegment is TwoHandGestureSegment && currentGestureSegment != 0) logger.Trace("Resuming");
            GestureResult result = relativeGestureSegment.CheckGesture(body);
            if (result == GestureResult.SUCEEDED)
            {
                if (relativeGestureSegment is TwoHandGestureSegment) logger.Debug($"{Type} Succeeded at Step {currentGestureSegment + 1}");
                if (currentGestureSegment + 1 < gestureSegments.Length)
                {
                    if (relativeGestureSegment is TwoHandGestureSegment) logger.Trace("PAUSING");
                    currentGestureSegment++;
                    frameCount = 0;
                    pausedFrameCount = SuccessfulPausedFrameCount();
                    paused = true;
                }
                else
                {
                    RaiseGestureRecognised();
                    Reset();
                }
            }
            else if (result == GestureResult.FAILED || frameCount == MAX_FRAME_COUNT)
            {
                if (currentGestureSegment > 0)
                {
                    if (result == GestureResult.FAILED)
                    {
                        logger.Debug($"{Type} Failed at Step {currentGestureSegment + 1} : FAILED");
                    }
                    else
                    {
                        logger.Debug($"{Type} Failed at Step {currentGestureSegment + 1} : MAX_FRAME_COUNT hit");

                    }
                }
                Reset();
            }
            else
            {
                frameCount++;
                pausedFrameCount = FailedPausedFrameCount();
                paused = true;
            }
        }

        /// <summary>
        /// Do we want to pause between segments?
        /// </summary>
        /// <returns></returns>
        protected abstract bool CheckPause();

        public void UpdateGesture(Body body)
        {
            if (CheckPause() && paused)
            {
                if (frameCount == pausedFrameCount)
                {
                    paused = false;
                }

                frameCount++;
            } else
            {
                PerformCheck(body);
            }
        }

        protected abstract int SuccessfulPausedFrameCount();

        protected abstract int FailedPausedFrameCount();

        private void RaiseGestureRecognised()
        {
            logger.Debug("{0} Recognised", Type);
            GestureRecognised?.Invoke(this, new GestureEventArgs { GestureType = Type });
        }

        public void Reset()
        {
            currentGestureSegment = 0;
            frameCount = 0;
            pausedFrameCount = 5;
            paused = true;
        }

    }
}
