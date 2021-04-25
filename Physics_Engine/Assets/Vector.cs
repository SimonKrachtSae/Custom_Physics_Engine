using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Vector 
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Length { get; set; }
        public float SqrLength { get; set; }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;

            SqrLength = X * X + Y * Y + Z * Z;
            Length = Mathf.Sqrt(SqrLength);
        }
        public static float Dot(Vector3 a, Vector3 b)
        {
            float f = a.x * b.x + a.y * b.y + a.z * b.z;
            return f;
        }
        public static Vector3 Project(Vector3 _v, Vector3 _onto)
        {
            float t = Dot(_v, _onto) / _onto.sqrMagnitude;
            return _onto * t;
        }
        public static Vector3 ProjectOnPlane(Vector3 _v, Vector3 _normal)
        {
            Vector3 c = Project(_v, _normal);
            return _v - c;
        }
        public static Vector Cross(Vector a, Vector b)
        {
            Vector v = new Vector();

            v.X = a.Y * b.Z - a.Z * b.Y;
            v.Y = a.Z * b.X - a.X * b.Z;
            v.Z = a.X * b.Y - a.Y * b.X;

            return v;
        }
        public static Vector3 GetDirectionVector(Vector3 a, Vector3 b)
        {
            return b - a;
        }
        public static Vector operator / (Vector v, float f)
        {
            return new Vector(v.X / f, v.Y / f, v.Z / f);
        }
        public static Vector operator * (Vector v, float f)
        {
            return new Vector(v.X * f, v.Y * f, v.Z * f);
        }
        public static Vector operator + (Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector operator - (Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
    }
}
