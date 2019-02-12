using KinectDissertationProject.Models;
using KinectDissertationProject.Models.Gesture;
using KinectDissertationProject.Models.Gesture.Hands;
using KinectDissertationProject.Models.Kinect;
using KinectDissertationProject.Views;
using KinectDissertationProject.Views.Tasks;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.ViewModel
{
    class KinectViewModel : INotifyPropertyChanged
    {

        #region Params

        public bool IsMI = false;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        #if DEBUG
        bool debug = true;
        #endif

        public static KinectViewModel Instance { get; private set; }

        static KinectViewModel()
        {
            Instance = new KinectViewModel();
        }

        private KinectReader kinectReader;
        private GestureController GestureController;

        private IList<Window> windows;
        private CoordinateMapper CoordinateMapper { get
            {
                return kinectReader.CoordinateMapper;
            }
        }

        #region UI Components

        private string textBoxText;
        public string TextBoxText
        {
            get
            {
                return textBoxText;
            }
            set
            {
                textBoxText = value;
                RaisePropertyChanged("TextBoxText");
            }
        }
        private string rightHandPositionText;
        public string RightHandPositionText
        {
            get
            {
                return rightHandPositionText;
            }
            set
            {
                rightHandPositionText = value;
                RaisePropertyChanged("RightHandPositionText");
            }
        }
        private void SetRightHandPosition(double x, double y)
        {
            RightHandPositionText = $"Right Hand : {x:0.00} , {y:0.00}";
        }
        private string rightElbowPositionText;
        public string RightElbowPositionText
        {
            get
            {
                return rightElbowPositionText;
            }
            set
            {
                rightElbowPositionText = value;
                RaisePropertyChanged("RightElbowPositionText");
            }
        }
        private void SetRightElbowPosition(double x, double y)
        {
            RightElbowPositionText = $"Right Elbow : {x:0.00} , {y:0.00}";
        }
        private string rightJointsPositionText;
        public string RightJointsPositionText
        {
            get
            {
                return rightJointsPositionText;
            }
            set
            {
                rightJointsPositionText = value;
                RaisePropertyChanged("RightJointsPositionText");
            }
        }

        private void SetRightJointsDebugText(Body body, Dictionary<JointType, (Point joint, bool tracked, float depth)> pointDict)
        {
            Joint elbowJoint = body.Joints[JointType.ElbowLeft];
            SetRightElbowPosition(elbowJoint.Position.X, elbowJoint.Position.Y);
            //SetRightElbowPosition(pointDict[JointType.ElbowRight].joint.X, 0.0f);

            Joint handJoint = body.Joints[JointType.HandLeft];
            SetRightHandPosition(handJoint.Position.X, handJoint.Position.Y);

            float jointsXDiff = handJoint.Position.X - elbowJoint.Position.X;
            float jointsYDiff = handJoint.Position.Y - elbowJoint.Position.Y ;

            RightJointsPositionText = $"Diff : {jointsXDiff:0.00} , {jointsYDiff:0.00}\nIn Region ? {JointType.HandLeft.InRegion(body, Region.ELBOW)}";

        }

        #endregion

        #endregion

        #region Create Windows

        public int Add_Window(Window w)
        {
            int loc = windows.Count();
            windows.Add(w);
            return loc;
        }

        public void Remove_Window(Window w)
        {
            windows.Remove(w);
        }

        private void Create_Window(Window w)
        {
            w.DataContext = this;
            Add_Window(w);
            w.Show();

            w.Closed += Window_ClosedEvent;
        }

        internal void Create_MockUp_Window()
        {
            Create_Window(new MockUp());
        }

        internal void Create_Camera_Window()
        {
            Create_Window(new Camera());
        }

        internal void Create_Menu_Task_Window()
        {
            Create_Window(new Menu_Task());
        }

        internal void Create_Lighting_Window()
        {
            Create_Window(new LightingControl());
        }

        internal void Create_X_Ray_Window()
        {
            Create_Window(new X_Rays());
        }

        private void Window_ClosedEvent(object sender, EventArgs e)
        {
            Remove_Window( (Window) sender);
        }

        #endregion

        #region Event Handlers

        public event EventHandler<JointPositionEventArgs> JointPositionEventOccurred;
        public event EventHandler<ColourEventArgs> ColourEventOccurred;

        public event EventHandler<WindowOperationEventArgs> WindowOperationOccurred;
        public event EventHandler<ApplicationOperationEventArgs> ApplicationOperationOccurred;
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected void RaiseColourEvent(ColourEventArgs args)
        {
            ColourEventOccurred?.Invoke(this, args);
        }

        protected void RaiseJointPositionEventOccurred(IReadOnlyDictionary<JointType, (Point point, bool tracked, float depth)> dict)
        {
            JointPositionEventOccurred?.Invoke(this, new JointPositionEventArgs
            {
                JointPosDict = dict
            });
        }

        protected void RaiseWindowOperation(Window window, GestureType gesture, IReadOnlyDictionary<string, object> data)
        {
            WindowOperationOccurred?.Invoke(this, new WindowOperationEventArgs(window, gesture, data));
        }

        protected void RaiseApplicationOperation(Window window, ApplicationOperation operation)
        {
            ApplicationOperationOccurred?.Invoke(this, new ApplicationOperationEventArgs(window, operation));
        }

        #endregion

        #region Initialisers
        public KinectViewModel()
        {
            logger.Debug("Kinect View Model Loaded");
            windows = new List<Window>();
            GestureController = new GestureController();

            GestureController.GestureRecognised += GestureController_GestureRecognised;
            
            TextBoxText = "Kinect Dissertation Project";
            
        }
        
        internal void Load_Kinect()
        {
            kinectReader = new KinectReader();

            kinectReader.OnTrackedBody += Kinect_OnTrackedBody;
            kinectReader.OnLostTracking += Kinect_LostTracking;

            kinectReader.ColourTracked += Kinect_ColourTracked;
        }

        internal void Open_Kinect()
        {
            kinectReader.Open();
        }

        internal void Close_Kinect()
        {
            kinectReader?.Close();
        }

        #endregion

        #region Tasks

        public void Create_Task_One()
        {
            Create_Menu_Task_Window();
        }

        public void Create_Task_Two()
        {
            Create_Menu_Task_Window();
        }

        public void Create_Task_Three()
        {

        }

        #endregion

        /// <summary>
        /// Triggers whenever the kinect gets ANY body data back.
        /// Used to draw skeleton etc. No gesture processing should occur here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kinect_OnTrackedBody(object sender, BodyEventArgs e)
        {
            Body body = e.BodyData;

            GestureController.CheckGestures(body);

            if (debug)
            {
                Dictionary<JointType, (Point joint, bool tracked, float depth)> pointDict = body.GetPointDictFromJoints(kinectReader.CoordinateMapper);
                //SetRightJointsDebugText(body, pointDict);

                RaiseJointPositionEventOccurred(pointDict);

            }

        }
        
        /// <summary>
        /// Triggers whenever a gesture is recognsied. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GestureController_GestureRecognised(object sender, GestureEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (debug) TextBoxText = e.GestureType.ToString();
            switch (e.GestureType)
            {
                case GestureType.LARGE_SWIPE_DOWN:
                    RaiseApplicationOperation(activeWindow, ApplicationOperation.MINIMISE);
                    break;
                default:
                    RaiseWindowOperation(activeWindow, e.GestureType, new Dictionary<string, object>());
                    break;
            }
        }

        private void Kinect_ColourTracked(object sender, ColourEventArgs e)
        {
            RaiseColourEvent(e);
        }

        private void RecordData(Body body)
        {
            Dictionary<JointType, (Point point, bool tracked, float depth)> data = body.GetPointDictFromJoints(kinectReader.CoordinateMapper);

            string path = Path.GetTempFileName().Replace(".tmp", ".csv");
            
            Console.Write(string.Format("********** {0} *************", path));

            foreach (KeyValuePair<JointType, (Point point, bool tracked, float depth)> entry in data)
            {
                using (var w = new StreamWriter(path, true, Encoding.UTF8))
                {
                    w.WriteLine(string.Format("{0},{1},{2},{3}",entry.Key.ToString(), entry.Value.point.X, entry.Value.point.Y, entry.Value.depth ));

                }
            }
            
        }

        private void Kinect_LostTracking(object sender, EventArgs e)
        {
            logger.Info("Lost Tracking");
        }

        public void Switch_Window(int index)
        {
            windows[index].Show();
        }

    }


}
