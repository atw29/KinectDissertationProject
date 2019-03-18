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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.ViewModel
{
    class KinectViewModel : INotifyPropertyChanged
    {

        // PUT USER'S NAME HERE
        const string USER = "ALEX_TEST";
        // CHANGE THE TASK NUM
        const int TASK_NUM = 3;

        #region Params

        public bool IsMI = false;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        #if DEBUG
            bool debug = true;
#else
            bool debug = false;
#endif

        public static KinectViewModel Instance { get; private set; }

        static KinectViewModel()
        {
            Instance = new KinectViewModel();

        }

        private KinectReader kinectReader;
        private GestureController GestureController;

        private CoordinateMapper CoordinateMapper {
            get
            {
                return kinectReader.CoordinateMapper;
            }
        }

        #region UI Components

        private string textBoxText;
        private Process procress;

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

        private ApplicationOperationsController ApplicationOperationsController;
        private DataCollector DataCollector;
        
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
        private string rightHandRegionText;
        private string leftHandRegionText;
        private GestureType CurrentGesture;

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

        public string RightHandRegionText
        {
            get
            {
                return rightHandRegionText;
            }
            set
            {
                rightHandRegionText = value;
                RaisePropertyChanged("RightHandRegionText");
            }
        }
        public string LeftHandRegionText
        {
            get
            {
                return leftHandRegionText;
            }
            set
            {
                leftHandRegionText = value;
                RaisePropertyChanged("LeftHandRegionText");
            }
        }

        private void SetRegionText(Body body)
        {
            List<Region> left = JointType.HandLeft.GetRegions(body);
            List<Region> right = JointType.HandRight.GetRegions(body);
            LeftHandRegionText = $"LEFT :\n {string.Join(", ", left)}";
            RightHandRegionText = $"RIGHT :\n {string.Join(", ", right)}";
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

            RightJointsPositionText = $"Diff : {jointsXDiff:0.00} , {jointsYDiff:0.00}\nIn Region ? {JointType.HandLeft.InRegion(body, Region.ELBOW, true)}";

        }

        #endregion

        #endregion

        #region Event Handlers

        public event EventHandler<JointPositionEventArgs> JointPositionEventOccurred;
        public event EventHandler<ColourEventArgs> ColourEventOccurred;

        public event EventHandler<WindowOperationEventArgs> GestureOccurred;
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

        protected void RaiseGesture(Window window, GestureType gesture, IReadOnlyDictionary<string, object> data)
        {
            GestureOccurred?.Invoke(this, new WindowOperationEventArgs(window, gesture, data));
        }

        #endregion

        #region Initialisers
        public KinectViewModel()
        {
            try
            {
                logger.Debug("Kinect View Model Loaded");

                GestureController = new GestureController();

            } catch(Exception e)
            {
                throw e;
            }

        }

        public void Start()
        {
            ApplicationOperationsController = new ApplicationOperationsController();

            ApplicationOperationsController.Create_Task(TASK_NUM);

            DataCollector = DataCollectorFactory.Start(USER, TASK_NUM);

            GestureController.GestureRecognised += GestureController_GestureRecognised;
            
            TextBoxText = "Kinect Dissertation Project";

            SetUpKinect();

        }

        private void SetUpKinect()
        {
            logger.Info("Setting Up Kinect");
            Load_Kinect();
            Open_Kinect();
            logger.Info("Kinect Loaded");

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

        internal void Close()
        {
            Close_Kinect();
            DataCollector.Stop();
        }

        internal void Close_Kinect()
        {
            kinectReader?.Close();
        }

        #region Windows
        internal void Add_Window(MainWindow window)
        {
            //ApplicationOperationsController.Add_Window(window);
        }
        internal void Create_MockUp_Window()
        {
            ApplicationOperationsController.Create_MockUp_Window();
        }

        internal void Create_Camera_Window()
        {
            ApplicationOperationsController.Create_Camera_Window();
        }

        internal void Create_Menu_Task_Window()
        {
            ApplicationOperationsController.Create_Menu_Task_Window();
        }

        internal void Create_Lighting_Window()
        {
            ApplicationOperationsController.Create_Lighting_Window();
        }

        internal void Create_X_Ray_Window()
        {
            ApplicationOperationsController.Create_X_Ray_Window();
        }

        #endregion

        #endregion
        /// <summary>
        /// Triggers whenever the kinect gets ANY body data back.
        /// Used to draw skeleton and give the Gesture Controller the body data. No actual gesture processing should occur here
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Kinect_OnTrackedBody(object sender, BodyEventArgs e)
        {
            Body body = e.BodyData;

            CurrentGesture = GestureType.NONE;
            GestureController.CheckGestures(body);

            if (TASK_NUM != 0) DataCollector.CollectData(body, CurrentGesture);

            if (debug)
            {
                Dictionary<JointType, (Point joint, bool tracked, float depth)> pointDict = body.GetPointDictFromJoints(kinectReader.CoordinateMapper);
                //SetRightJointsDebugText(body, pointDict);
                SetRegionText(body);

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
            CurrentGesture = e.GestureType;
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (debug)
            {
                if (TextBoxText.Contains(e.GestureType.ToString()))
                {
                    TextBoxText += "I";
                }
                else
                {
                    TextBoxText = e.GestureType.ToString();
                }

            }

            RaiseGesture(activeWindow, e.GestureType, new Dictionary<string, object>());
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

    }


}
