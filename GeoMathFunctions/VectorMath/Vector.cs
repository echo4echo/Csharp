/////////////////////////////////////////////////////////////////////////////////////
///        Name space: NibotUI.Business.VectorMath
///        File name : Vector
///        Description:
///        Version Nr:
///        Created data : 10/15/2019 10:42:40 AM
///        
///        Author: Han Liu
///        Copyright (c) 2018 All Rights Reserved  
///        
/// Version History:
///*********************************************************************************
///        Date       Version        Author          Description
///*********************************************************************************
///   | xx.xx.xxxx |    1.0    |     Han Liu   |  Initial Version:
///  
/// //////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibotUI.Business.VectorMath
{
    public class Vector
    {
        public double X;
        public double Y;

        // Constructors.
        public Vector(double x, double y) { X = x; Y = y; }
        public Vector() : this(double.NaN, double.NaN) { }

        public static Vector operator -(Vector v, Vector w)
        {
            return new Vector(v.X - w.X, v.Y - w.Y);
        }

        public static Vector operator +(Vector v, Vector w)
        {
            return new Vector(v.X + w.X, v.Y + w.Y);
        }

        public static double operator *(Vector v, Vector w)
        {
            return v.X * w.X + v.Y * w.Y;
        }

        public static Vector operator *(Vector v, double mult)
        {
            return new Vector(v.X * mult, v.Y * mult);
        }

        public static Vector operator *(double mult, Vector v)
        {
            return new Vector(v.X * mult, v.Y * mult);
        }

        public double Cross(Vector v)
        {
            return X * v.Y - Y * v.X;
        }

        public override bool Equals(object obj)
        {
            var v = (Vector)obj;
            return (X - v.X).IsZero() && (Y - v.Y).IsZero();
        }

        public override string ToString()
        {
            return string.Format("({0};{1})", X, Y);
        }
    }

    public static class Extensions
    {
        private const double Epsilon = 1e-10;

        public static bool IsZero(this double d)
        {
            return Math.Abs(d) < Epsilon;
        }
    }
}
