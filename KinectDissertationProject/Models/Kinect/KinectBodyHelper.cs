﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.Models.Kinect
{
    public static class KinectBodyHelper
    {
        const double HAND_LIFT_Z_DISTANCE = 0.15f;
        const double GESTURE_Y_OFFSET = -0.65f;
        const double GESTURE_X_OFFSET = 0.185f;

        #region Joints to Colour Space
        public static (Point point, float depth) ToCoordinatePoint(this Joint joint, CoordinateMapper coordinateMapper)
        {
            Point point = new Point();
            CameraSpacePoint jointPosition = joint.Position;
            ColorSpacePoint colorPoint = coordinateMapper.MapCameraPointToColorSpace(jointPosition);

            point.X = float.IsInfinity(colorPoint.X) ? 0 : colorPoint.X;
            point.Y = float.IsInfinity(colorPoint.Y) ? 0 : colorPoint.Y;
            return (point, jointPosition.Z);
        }

        /// <summary>
        ///     Gets the 2D point representative dictionay from list of joints as a Dictionary of JointType to a Tuple of 2D point and Tracked State
        /// </summary>
        /// <param name="joints"></param>
        /// <param name="_mode"></param>
        /// <param name="coordinateMapper"></param>
        /// <returns>Dictionary of JointType to Tuple. Tuple is of form (2D Point, !Not Tracked) </returns>
        public static Dictionary<JointType, (Point joint, bool tracked, float depth)> GetPointDictFromJoints(this Body body, CoordinateMapper coordinateMapper)
        {
            Dictionary<JointType, (Point joint, bool tracked, float depth)> dict = new Dictionary<JointType, (Point joint, bool tracked, float depth)>();

            foreach (KeyValuePair<JointType, Joint> pair in body.Joints)
            {
                dict.Add(pair.Key, pair.Value.GetPointTupleFromJoint(coordinateMapper));
            }

            return dict;
        }

        private static (Point point, bool tracked, float depth) GetPointTupleFromJoint(this Joint joint, CoordinateMapper coordinateMapper)
        {
            (Point point, float depth) point = joint.ToCoordinatePoint(coordinateMapper);
            (Point point, bool tracked, float depth) p = (point: point.point, tracked: joint.TrackingState != TrackingState.NotTracked, point.depth);
            return p;
            //return new Tuple<Point, bool>(joint.ToCoordinatePoint(coordinateMapper), joint.TrackingState != TrackingState.NotTracked);
        }

        #endregion

    }
}
