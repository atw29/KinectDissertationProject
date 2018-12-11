﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models.Gestures
{
    abstract class Gesture
    {
        #region Params
        /// <summary>
        /// The Type of Gesture we are
        /// </summary>
        protected abstract GestureType type
        {
            get;
        }

        /// <summary>
        /// The parts that make up this gesture
        /// </summary>
        protected abstract RelativeGestureSegment[] gestureSegments
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
        private int pausedFrameCount = 10;

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

        public event EventHandler<GestureEventArgs> GestureRecognised;

        public void UpdateGesture(Body body)
        {
            if (paused)
            {
                if (frameCount == pausedFrameCount)
                {
                    paused = false;
                }

                frameCount++;
            }

            GestureResult result = gestureSegments[currentGestureSegment].CheckGesture(body);
            if (result == GestureResult.SUCEEDED)
            {
                if (currentGestureSegment + 1 < gestureSegments.Length)
                {
                    currentGestureSegment++;
                    frameCount = 0;
                    pausedFrameCount = 10;
                    paused = true;
                }
                else
                {
                    if (GestureRecognised != null)
                    {
                        GestureRecognised.Invoke(this, new GestureEventArgs { GestureType = type });
                        Reset();
                    }
                } 
            }
            else if (result == GestureResult.FAILED || frameCount == MAX_FRAME_COUNT)
            {
                Reset();
            }
            else
            {
                frameCount++;
                pausedFrameCount = 5;
                paused = true;
            }
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
