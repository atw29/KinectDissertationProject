using KinectDissertationProject.Models;
using KinectDissertationProject.Models.Gestures;
using KinectDissertationProject.Views;
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
    class KinectViewModel
    {

        #if DEBUG
        bool debug = true;
        #endif

        #region Params

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
        public string TextBoxText{ get; set; }


        #endregion

        #region Hand Positions

        HandPos_TOP HandPos_TOP = new HandPos_TOP();
        HandPos_BOT HandPos_BOT = new HandPos_BOT();
        HandPos_LHS HandPos_LHS = new HandPos_LHS();
        HandPos_RHS HandPos_RHS = new HandPos_RHS();

        #endregion

        #region Create Windows

        public int Add_Window(Window w)
        {
            int loc = windows.Count();
            windows.Add(w);
            return loc;
        }

        internal void Create_MockUp_Window()
        {
            MockUp MockUpWindow = MockUp.Instance;
            Add_Window(MockUpWindow);
            MockUpWindow.Show();
        }

        internal void Create_Camera_Window()
        {
            Camera CameraWindow = new Camera();
            CameraWindow.DataContext = this;

            Add_Window(CameraWindow);
            CameraWindow.Show();
        }

        internal void Create_X_Ray_Window()
        {
            X_Rays X_Rays_Window = X_Rays.Instance;
            X_Rays_Window.DataContext = this;
            Add_Window(X_Rays_Window);
            X_Rays_Window.Show();
        }
        #endregion

        #region Event Handlers

        public event EventHandler<JointPositionEventArgs> JointPositionEventOccurred;
        public event EventHandler<ColourEventArgs> ColourEventOccurred;

        public event EventHandler<WindowOperationEventArgs> WindowOperationOccurred;
        public event EventHandler<ApplicationOperationEventArgs> ApplicationOperationOccurred;

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

        protected void RaiseWindowOperation(Window window, WindowOperation operation, IReadOnlyDictionary<string, object> data)
        {
            WindowOperationOccurred?.Invoke(this, new WindowOperationEventArgs(window, operation, data));
        }

        protected void RaiseApplicationOperation(Window window, ApplicationOperation operation)
        {
            ApplicationOperationOccurred?.Invoke(this, new ApplicationOperationEventArgs(window, operation));
        }

        #endregion

        #region Initialisers
        public KinectViewModel()
        {
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

        private void Kinect_OnTrackedBody(object sender, BodyEventArgs e)
        {
            Body body = e.BodyData;

            GestureController.CheckGestures(body);

            #region Test stuff
            //RaiseJointPositionEventOccurred(body.GetPointDictFromJoints(kinectReader.CoordinateMapper));
            //RecordData(body);


            //CheckHandPositions(body.Joints);

            // Raise Gesutre Occurred 
            // i.e. RaiseGesutreOccurred(activeWindow, Gesture.SwipeDown)

            // Raise Joint Positions

            //RaiseEventOccurred(
            //    activeWindow, 
            //    Operation.MINIMISE, 
            //    new Dictionary<string, object> {
            //        { "LeftHandLocation" , body.Joints[JointType.HandLeft].ToCoordinatePoint(kinectReader.CoordinateMapper).ToString()}
            //    }
            //);
            #endregion

        }
        
        private void GestureController_GestureRecognised(object sender, GestureEventArgs e)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

            switch (e.GestureType)
            {
                case GestureType.LARGE_SWIPE_DOWN:
                    RaiseApplicationOperation(activeWindow, ApplicationOperation.MINIMISE);
                    break;
                case GestureType.RIGHT_HAND_SWIPE_LEFT:
                    RaiseApplicationOperation(activeWindow, ApplicationOperation.SWITCH_WINDOW);
                    break;
                case GestureType.NONE:
                    break;
                default:
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

       private void CheckHandPositions(IReadOnlyDictionary<JointType, Joint> joints)
        {
            if (HandPos_TOP.In_Position(joints[JointType.HandLeft], CoordinateMapper) == HandPosition.IN_POSITION)
            {

            }
        }

        private void Kinect_LostTracking(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Switch_Window(int index)
        {
            windows[index].Show();
        }

    }


}
