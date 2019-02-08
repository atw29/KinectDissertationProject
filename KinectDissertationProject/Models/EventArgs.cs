using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.Models
{
    class WindowOperationEventArgs : EventArgs
    {
        /// <summary>
        /// Contains the arguments for an Operation performed on a specific Window (e.g. scroll down)
        /// </summary>
        /// <param name="window">The Window to Act On</param>
        /// <param name="operation">The Operation to Perform (e.g. scroll down)</param>
        /// <param name="data">Any Extra Data required </param>
        public WindowOperationEventArgs(Window window, GestureType gesture, IReadOnlyDictionary<string, object> data)
        {

            Window = window;
            Gesture = gesture;
            Data = data;
        }

        public Window Window { get; private set; }
        public GestureType Gesture { get; private set; }
        public IReadOnlyDictionary<string, object> Data { get; private set; }

    }

    class ApplicationOperationEventArgs : EventArgs
    {
        /// <summary>
        /// Contains the information for performing a Application-level operation (e.g. switch window)
        /// </summary>
        /// <param name="window">The Window to base the action on (most likely to be the currently active window) </param>
        /// <param name="operation">The operation to perfrom (e.g. switch window) </param>
        public ApplicationOperationEventArgs(Window window, ApplicationOperation operation)
        {
            Window = window;
            Operation = operation;
        }

        public Window Window { get; private set; }
        public ApplicationOperation Operation { get; private set; }
    }

    public class GestureEventArgs : EventArgs
    {
        public GestureType GestureType { get; set; }
    }


    #region Old Stuff

    public class BodyEventArgs : EventArgs
    {
        public Body BodyData { get; private set; }

        public BodyEventArgs(Body bodyData)
        {
            BodyData = bodyData;
        }
    }

    public class ColourEventArgs : EventArgs
    {
        public ColorFrame ColourFrame { get; private set; }

        public ColourEventArgs(ColorFrame colourFrame)
        {
            ColourFrame = colourFrame;
        }
    }

    public class JointPositionEventArgs : EventArgs
    {
        public IReadOnlyDictionary<JointType, (Point point, bool tracked, float depth)> JointPosDict { get; set; }

    }
    #endregion


}
