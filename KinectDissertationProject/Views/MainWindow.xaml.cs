﻿using KinectDissertationProject.Models;
using KinectDissertationProject.ViewModel;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KinectDissertationProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectViewModel KinectViewModel;

        public MainWindow()
        {
            KinectViewModel = KinectViewModel.Instance;

            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer(
                new TimeSpan(0, 0, 1),
                DispatcherPriority.Normal,
                delegate
                {
                    time.Text = DateTime.Now.PrintTime();
                },
                Dispatcher
            );

            DataContext = KinectViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KinectViewModel.Start();

            KinectViewModel.Add_Window(this);

            KinectViewModel.JointPositionEventOccurred += DrawSkeleton;

        }

        private void DrawSkeleton(object sender, JointPositionEventArgs e)
        {
            canvas.Children.Clear();
            DrawSkeleton(e.JointPosDict);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            KinectViewModel.Close();
        }

        private void DrawSkeleton(IReadOnlyDictionary<JointType, (Point point, bool tracked, float depth)> pointDict)
        {
            
            foreach ((Point point, bool tracked, float depth) point in pointDict.Values)
            {
                canvas.DrawPoint(point);
            }

            #region Draw Lines

            canvas.DrawLine(pointDict[JointType.Head], pointDict[JointType.Neck]);
            canvas.DrawLine(pointDict[JointType.Neck], pointDict[JointType.SpineShoulder]);
            canvas.DrawLine(pointDict[JointType.SpineShoulder], pointDict[JointType.ShoulderLeft]);
            canvas.DrawLine(pointDict[JointType.SpineShoulder], pointDict[JointType.ShoulderRight]);
            canvas.DrawLine(pointDict[JointType.SpineShoulder], pointDict[JointType.SpineMid]);
            canvas.DrawLine(pointDict[JointType.ShoulderLeft], pointDict[JointType.ElbowLeft]);
            canvas.DrawLine(pointDict[JointType.ShoulderRight], pointDict[JointType.ElbowRight]);
            canvas.DrawLine(pointDict[JointType.ElbowLeft], pointDict[JointType.WristLeft]);
            canvas.DrawLine(pointDict[JointType.ElbowRight], pointDict[JointType.WristRight]);
            canvas.DrawLine(pointDict[JointType.WristLeft], pointDict[JointType.HandLeft]);
            canvas.DrawLine(pointDict[JointType.WristRight], pointDict[JointType.HandRight]);
            canvas.DrawLine(pointDict[JointType.HandLeft], pointDict[JointType.HandTipLeft]);
            canvas.DrawLine(pointDict[JointType.HandRight], pointDict[JointType.HandTipRight]);
            canvas.DrawLine(pointDict[JointType.HandTipLeft], pointDict[JointType.ThumbLeft]);
            canvas.DrawLine(pointDict[JointType.HandTipRight], pointDict[JointType.ThumbRight]);
            canvas.DrawLine(pointDict[JointType.SpineMid], pointDict[JointType.SpineBase]);
            canvas.DrawLine(pointDict[JointType.SpineBase], pointDict[JointType.HipLeft]);
            canvas.DrawLine(pointDict[JointType.SpineBase], pointDict[JointType.HipRight]);
            canvas.DrawLine(pointDict[JointType.HipLeft], pointDict[JointType.KneeLeft]);
            canvas.DrawLine(pointDict[JointType.HipRight], pointDict[JointType.KneeRight]);
            canvas.DrawLine(pointDict[JointType.KneeLeft], pointDict[JointType.AnkleLeft]);
            canvas.DrawLine(pointDict[JointType.KneeRight], pointDict[JointType.AnkleRight]);
            canvas.DrawLine(pointDict[JointType.AnkleLeft], pointDict[JointType.FootLeft]);
            canvas.DrawLine(pointDict[JointType.AnkleRight], pointDict[JointType.FootRight]);

            #endregion

        }
    }
}
