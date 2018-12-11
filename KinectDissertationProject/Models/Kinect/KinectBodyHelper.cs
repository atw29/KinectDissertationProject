﻿using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectDissertationProject.Models
{
    public static class KinectBodyHelper
    {
        const double HAND_LIFT_Z_DISTANCE = 0.15f;
        const double GESTURE_Y_OFFSET = -0.65f;
        const double GESTURE_X_OFFSET = 0.185f;

        public static GestureType CheckGesture(this Body body)
        {
            return GestureType.NONE;
        }

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

        #region His Stuff
        public static bool IsHandLiftForward(this Body body, bool isLeft)
        {
            return body.Joints[isLeft ? JointType.HandLeft : JointType.HandRight].Position.Z - body.Joints[JointType.SpineBase].Position.Z < -HAND_LIFT_Z_DISTANCE;
        }

        public static HandState GetHandState(this Body body, bool isLeft)
        {
            return isLeft ? body.HandLeftState : body.HandRightState;
        }

        public static void GetHandRelativePosition(this Body body, bool isLeft)
        {
            CameraSpacePoint handPos = body.Joints[isLeft ? JointType.HandLeft : JointType.HandRight].Position;
            CameraSpacePoint spineBase = body.Joints[JointType.SpineBase].Position;

            //return handPos.ToMVector2() - spineBase.ToMVector2() + gestureOffsets[isLeft ? 0 : 1];
        }

        public static void ToMVector2(this CameraSpacePoint jointPoint)
        {
            //return new MVector2(jointPoint.X, jointPoint.Y);
        }
        #endregion

    }
}