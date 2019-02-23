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

        private Process process;
        private IList<(string Gesture, string Time, string parasite)> gestureList;
        private readonly DateTime opened;
        private StreamWriter writer;

        public DataGatherer(KinectViewModel viewModel)
        {
            viewModel.GestureOccurred += ViewModel_GestureOccurred;

            SetUpShutdownHook();

            gestureList = new List<(string, string, string)>();

            opened = DateTime.Now;

            AddInitialData(opened);

        }

        private void AddInitialData(DateTime opened)
        {
            gestureList.Add(("GESTURE", "TIME","PARASITE"));
            gestureList.Add(("start", opened.ToString("HH:mm:ss"), ""));
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
            Add_Row(e.Gesture);
        }

        private void Add_Row(GestureType gesture, string parasite = "")
        {
            gestureList.Add((gesture.ToString(), DateTime.Now.ToString("HH:mm:ss"), parasite));
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            logger.Debug("Writing Output Data");
            using (writer = File.CreateText($"C:\\Users\\Alex\\prog\\data\\{opened.ToString("yyyy.MM.dd HH.mm.ss")}.csv"))
            {
                foreach ((string gesture, string time, string parasite) line in gestureList)
                {
                    writer.WriteLine($"{line.gesture},{line.time},{line.parasite}");
                    writer.Flush();
                }
            }
        }
    }
}
