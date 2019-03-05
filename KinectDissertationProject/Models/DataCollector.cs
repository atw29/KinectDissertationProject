using Microsoft.Kinect;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models
{
    public static class DataCollectorFactory
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static readonly Data PoisonData = new Data(-0, 0,0,0,0,0,0,0, GestureType.NONE);
        public static DataCollector Start(string USER, int TASK_NUM)
        {
            string dir = Path.Combine("C:\\Users", "Alex", "Google Drive", "University Drive", "Bath Drive", "Third Year", "Diss", "Other", "Data", USER, "MI", TASK_NUM.ToString());

            Directory.CreateDirectory(dir);
            logger.Debug($"Data Writing To : {dir}");

            BlockingCollection<Data> datas = new BlockingCollection<Data>();

            DataCollector dataCollector = new DataCollector(datas, dir, USER, TASK_NUM);

            dataCollector.Start();
            return dataCollector;
        }

        public static string PrintTime(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss:fff");
        }
    }

    public class DataCollector
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private BlockingCollection<Data> Queue;

        private readonly string User;
        private readonly int Task_Num;

        private readonly string DataPath;
        private readonly string InfoPath;
        private string JointInfoPath;
        private readonly DateTime WhenOpened;

        public DataCollector(BlockingCollection<Data> Queue, string dir, string USER, int TASK_NUM)
        {
            WhenOpened = DateTime.Now;

            User = USER;
            Task_Num = TASK_NUM;

            string FileName = WhenOpened.ToString("yyyy.MM.dd HH.mm.ss");
            DataPath = Path.Combine(dir, $"{FileName}_Data.csv");
            InfoPath = Path.Combine(dir, $"{FileName}.info");
            JointInfoPath = Path.Combine(dir, $"{FileName}_JointInfo.csv");

            this.Queue = Queue;
        }

        public void Start()
        {
            CreateInfoFile();
            CreateJointInfoFile();
            CreateDataFile();

            Task.Factory.StartNew(Perform_Execution);
        }
        public void Stop()
        {
            logger.Debug("Stopping Data Collector Thread");
            Queue.Add(DataCollectorFactory.PoisonData);
        }

        private void CreateDataFile()
        {
            using (var writer = File.CreateText(DataPath))
            {
                writer.WriteLine($"CLICK_STATE,X_POS,Y_POS,TIME,PARASITE");
            }
        }

        private void CreateInfoFile()
        {
            using (var writer = File.CreateText(InfoPath))
            {
                writer.WriteLine($"start: {WhenOpened.PrintTime()}");
                writer.WriteLine($"user: {User}");
                writer.WriteLine($"type: EG");
                writer.WriteLine($"task_num: {Task_Num}");
                writer.WriteLine("----- Joint Info -----");
                writer.WriteLine("Of Form: JOINT, X-Val, Y-Val ");
                writer.WriteLine("** Elbow **");
                writer.WriteLine("Hand Above Elbow: hand.Y - elbow.Y > ElbowRightAboveTolerance.Y");
                writer.WriteLine("Hand Right Elbow: hand.X - elbow.X > ElbowRightAboveTolerance.X");
                writer.WriteLine("Hand Below Elbow: hand.Y - elbow.Y < ElbowLeftBelowTolerance.Y");
                writer.WriteLine("Hand Left  Elbow: hand.X - elbow.X < ElbowLeftBelowTolerance.X");
                writer.WriteLine("** Regions **");
                writer.WriteLine("TOP: Y > SpineShoulder. BOTTOM: Y < SpineBase. LEFT: X < ShoulderLeft. Right: X > ShoulderRight");
            }
        }

        private void CreateJointInfoFile()
        {
            using (var writer = File.CreateText(JointInfoPath))
            {
                writer.WriteLine($"JOINT_TYPE,X_POS,Y_POS");
                writer.WriteLine($"ElbowRightAboveTolerance,{Properties.Settings.Default.ElbowRightTolerance},{Properties.Settings.Default.ElbowAboveTolerance}");
                writer.WriteLine($"ElbowLeftBelowTolerance,{Properties.Settings.Default.ElbowLeftTolerance},{Properties.Settings.Default.ElbowBelowTolerance}");
            }
        }
        void Perform_Execution()
        {
            while (Queue.TryTake(out Data data, Timeout.Infinite))
            {
                if (data.Equals(DataCollectorFactory.PoisonData))
                {
                    break;
                }
                WriteString(DataPath, data.ToString());
            }
            if (Queue.Count > 0)
            {
                try
                {
                    while (true)
                    {
                        WriteString(DataPath, Queue.Take().ToString());
                    }
                }
                catch (InvalidOperationException)
                {
                    logger.Debug("Finished Writing Queue");

                }
            }

            logger.Debug("Finishing Writing Objects in Queue ");

            WriteString(InfoPath, $"end: {DateTime.Now.PrintTime()}");
        }

        private void WriteString(string Path, string toWrite)
        {
            using (var writer = new StreamWriter(Path, true))
            {
                writer.WriteLine(toWrite);
            }
        }

        private void AddRegionInfo(Body b)
        {
            using (var writer = new StreamWriter(InfoPath, true))
            {
                writer.WriteLine($"SpineShoulder,{b.Joints[JointType.SpineShoulder].Position.X},{b.Joints[JointType.SpineShoulder].Position.Y}");
                writer.WriteLine($"SpineBase,{b.Joints[JointType.SpineBase].Position.X},{b.Joints[JointType.SpineBase].Position.Y}");
                writer.WriteLine($"ShoulderLeft,{b.Joints[JointType.ShoulderLeft].Position.X},{b.Joints[JointType.ShoulderLeft].Position.Y}");
                writer.WriteLine($"ShoulderLeft,{b.Joints[JointType.ShoulderRight].Position.X},{b.Joints[JointType.ShoulderRight].Position.Y}");

            }
        }

        internal void CollectData(Data d)
        {
            Queue.Add(d);
        }
        bool first = true;
        internal void CollectData(Body b, GestureType g = GestureType.NONE)
        {
            if (first)
            {
                AddRegionInfo(b);
                first = false;
            }
            CollectData(b.ToDataObject(g));
        }

    }

    public static class DataHelper
    {
        public static Data ToDataObject(this Body b, GestureType g)
        {
            return new Data(
                b.Joints[JointType.HandLeft].Position.X,
                b.Joints[JointType.HandLeft].Position.Y,

                b.Joints[JointType.HandRight].Position.X,
                b.Joints[JointType.HandRight].Position.Y,

                b.Joints[JointType.ElbowLeft].Position.X,
                b.Joints[JointType.ElbowLeft].Position.Y,

                b.Joints[JointType.ElbowRight].Position.X,
                b.Joints[JointType.ElbowRight].Position.Y,
                
                g
                );
        }
    }


    public class Data
    {

        private readonly double LeftHandX;
        private readonly double LeftHandY;

        private readonly double RightHandX;
        private readonly double RightHandY;

        private readonly double LeftElbowX;
        private readonly double LeftElbowY;

        private readonly double RightElbowX;
        private readonly double RightElbowY;

        private readonly GestureType GestureType;

        public Data(double leftHandX, double leftHandY, double rightHandX, double rightHandY, double leftElbowX, double leftElbowY, double rightElbowX, double rightElbowY, GestureType gestureType)
        {
            LeftHandX = leftHandX;
            LeftHandY = leftHandY;
            RightHandX = rightHandX;
            RightHandY = rightHandY;
            LeftElbowX = leftElbowX;
            LeftElbowY = leftElbowY;
            RightElbowX = rightElbowX;
            RightElbowY = rightElbowY;
            GestureType = gestureType;
        }

        public override string ToString()
        {
            return $"{GestureType},{XPos},{YPos},{DateTime.Now.PrintTime()},";
        }
    }
}
