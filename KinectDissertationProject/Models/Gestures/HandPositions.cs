using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Kinect;

/// <summary>
/// Encapsulates all Recognised Hand Positions.
/// Can be combined to form gestures or used as localisation.
/// </summary>
/// 
namespace KinectDissertationProject.Models.Gestures
{
    static class HandPositions
    {
        public const float TOP_MIN = 0;
        public const float TOP_MAX = 216; // 1080 / 10 * 2

        public const float BOT_MIN = 864;
        public const float BOT_MAX = 1080;

        public const float LHS_MIN = 0;
        public const float LHS_MAX = 384; // 1920 / 10 * 2

        public const float RHS_MIN = 1536;
        public const float RHS_MAX = 1920;

    }

    enum Region
    {
        TOP,
        BOT,
        LHS,
        RHS
    }

    interface IHandPosition
    {
        HandPosition In_Position(Joint hand, CoordinateMapper coordinateMapper);
    }

    #region Region Positions

    class HandPos_TOP : IHandPosition
    {
        public HandPosition In_Position(Joint hand, CoordinateMapper coordinateMapper)
        {
            Point p = hand.ToCoordinatePoint(coordinateMapper);
            return p.Y.In_Range(HandPositions.TOP_MIN, HandPositions.TOP_MAX) ? HandPosition.IN_POSITION : HandPosition.NOT_IN_POSITION;
        }
    }

    class HandPos_BOT : IHandPosition
    {
        public HandPosition In_Position(Joint hand, CoordinateMapper coordinateMapper)
        {
            Point p = hand.ToCoordinatePoint(coordinateMapper);
            return p.Y.In_Range(HandPositions.BOT_MIN, HandPositions.BOT_MAX) ? HandPosition.IN_POSITION : HandPosition.NOT_IN_POSITION;
        }
    }
    class HandPos_LHS : IHandPosition
    {
        public HandPosition In_Position(Joint hand, CoordinateMapper coordinateMapper)
        {
            Point p = hand.ToCoordinatePoint(coordinateMapper);
            return p.X.In_Range(HandPositions.LHS_MIN, HandPositions.LHS_MAX) ? HandPosition.IN_POSITION : HandPosition.NOT_IN_POSITION;
        }
    }
    class HandPos_RHS : IHandPosition
    {
        public HandPosition In_Position(Joint hand, CoordinateMapper coordinateMapper)
        {
            Point p = hand.ToCoordinatePoint(coordinateMapper);
            return p.X.In_Range(HandPositions.RHS_MIN, HandPositions.RHS_MAX) ? HandPosition.IN_POSITION : HandPosition.NOT_IN_POSITION;
        }
    }

    #endregion

    static class MicroExtension
    {
        public static bool In_Range(this double val, float min, float max)
        {
            return val >= min && val <= max;
        }
    }
    public enum HandPosition
    {
        IN_POSITION,
        NOT_IN_POSITION
    }

}
