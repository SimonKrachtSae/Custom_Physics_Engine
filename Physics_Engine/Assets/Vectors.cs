using UnityEngine;

namespace CustomMafs
{
    public struct Vectors
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vectors(float _x, float _y, float _z)
        {
            X = _x;
            Y = _y;
            Z = _z;
        }

        public float GetLength()
        {
            return Mathf.Sqrt(X * X + Y * Y + Z * Z);
        }

        public float GetSqrLength()
        {
            return X * X + Y * Y + Z * Z;
        }

        public Vectors Normalize()
        {
            float length = GetLength();
            if (Mathf.Approximately(length, 0))
            {
                return new Vectors(0, 0, 0);
            }
            return (this / GetLength());
        }

        public Vectors Scale(Vectors _v)
        {
            Vectors v = new Vectors(X, Y, Z);

            v.X *= _v.X;
            v.Y *= _v.Y;
            v.Z *= _v.Z; 

            return v;
        }

        public static Vectors Project(Vectors _v, Vectors _onto)
        {
            float t = Dot(_v, _onto) / _onto.GetSqrLength();
            return _onto * t;
        }

        public static Vectors ProjectOnPlane(Vectors _v, Vectors _normal)
        {
            Vectors c = Project(_v, _normal);
            return _v - c;
        }

        public override string ToString()
        {
            return $"{X}|{Y}|{Z}";
        }

        public static Vectors GetDirectionVector(Vectors _a, Vectors _b)
        {
            // A -> B => B - A
            return _b - _a;
        }

        public static float Dot(Vectors _left, Vectors _right)
        {
            return _left.X * _right.X + _left.Y * _right.Y + _left.Z * _right.Z;
        }

        public static Vectors Cross(Vectors _left, Vectors _right)
        {
            Vectors v = new Vectors();

            v.X = _left.Y * _right.Z - _left.Z * _right.Y;
            v.Y = _left.Z * _right.X - _left.X * _right.Z;
            v.Z = _left.X * _right.Y - _left.Y * _right.X;

            return v;
        }

        public static implicit operator Vector3(Vectors _v)
        {
            return new Vector3(_v.X, _v.Y, _v.Z);
        }

        public static implicit operator Vectors(Vector3 _v)
        {
            return new Vectors(_v.x, _v.y, _v.z);
        }


        public static Vectors operator-(Vectors _left)
        {
            return _left * -1;
        }
        public static Vectors operator-(Vectors _left, Vectors _right)
        {
            Vectors v = new Vectors();
            v.X = _left.X - _right.X;
            v.Y = _left.Y - _right.Y;
            v.Z = _left.Z - _right.Z;

            return v;
        }

        public static Vectors operator+(Vectors _left, Vectors _right)
        {
            Vectors v = new Vectors();
            v.X = _left.X + _right.X;
            v.Y = _left.Y + _right.Y;
            v.Z = _left.Z + _right.Z;

            return v;
        }

        public static Vectors operator*(Vectors _left, float _right)
        {
            Vectors v = new Vectors();
            v.X = _left.X * _right;
            v.Y = _left.Y * _right;
            v.Z = _left.Z * _right;
            return v;
        }

        public static Vectors operator/(Vectors _left, float _right)
        {
            Vectors v = new Vectors();
            v.X = _left.X / _right;
            v.Y = _left.Y / _right;
            v.Z = _left.Z / _right;
            return v;
        }

    }
}
