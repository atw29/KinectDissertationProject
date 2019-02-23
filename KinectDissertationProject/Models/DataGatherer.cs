using KinectDissertationProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectDissertationProject.Models
{
    class DataGatherer
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly DateTime opened;
        private StreamWriter writer;

        private const string folder = "C:\\Users\\Alex\\Google Drive\\University Drive\\Bath Drive\\Third Year\\Diss\\Other\\data\\";
        private readonly string path;

        public DataGatherer(KinectViewModel viewModel)
        {
            viewModel.GestureOccurred += ViewModel_GestureOccurred;

            SetUpShutdownHook();

            opened = DateTime.Now;

            path = $"{folder}{opened.ToString("yyyy.MM.dd HH.mm.ss")}.csv";

            CreateFile(opened);

        }

        private void CreateFile(DateTime opened)
        {
            using (writer = File.CreateText(path))
            {
                writer.WriteLine($"GESTURE, TIME, PARASITE");
                writer.WriteLine($"start, {opened.ToString("HH:mm:ss")},");
            }
        }

        private void SetUpShutdownHook()
        {
            logger.Debug("Adding Shutdown Hook");
            var domain = AppDomain.CurrentDomain;
            domain.ProcessExit += Process_Exited;
            domain.DomainUnload += Process_Exited;
        }

        private void ViewModel_GestureOccurred(object sender, WindowOperationEventArgs e)
        {
            WriteString($"{e.Gesture.ToString()},{DateTime.Now.PrintTime()},");
        }

        private void WriteString(string toWrite)
        {
            using (var writer = new StreamWriter(path, true))
            {
                writer.WriteLine(toWrite);
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            WriteString($"end,{DateTime.Now.PrintTime()},");
        }
    }

    public static class DateTimeHelper
    {
        public static string PrintTime(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }
    }

}
