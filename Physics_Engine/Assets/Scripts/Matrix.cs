using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Matrix
    {
        public float[,] Values { get; }

        public Matrix(float[,] _values)
        {
            if (_values.GetLength(0) != 4 || _values.GetLength(1) != 4)
            {
                throw new System.ArgumentException();
            }

            Values = _values;
        }

        public static Matrix Translate(Vector3 _v)
        {
            return new Matrix(new float[4, 4]
            {
                { 1, 0, 0, _v.x },
                { 0, 1, 0, _v.y },
                { 0, 0, 1, _v.z },
                { 0, 0, 0, 1 }
            });
        }

        public static Matrix Scale(Vector3 _v)
        {
            return new Matrix(new float[4, 4]
            {
                { _v.x, 0, 0, 0 },
                { 0, _v.y, 0, 0 },
                { 0, 0, _v.z, 0 },
                { 0, 0, 0, 1 }
            });
        }

        public static Matrix Rotation(Vector3 _v)
        {
            return RotationZ(_v.z) * RotationX(_v.x) * RotationY(_v.y);
        }

        public static Matrix TRS(Vector3 _translation, Vector3 _rotation, Vector3 _scale)
        {
            return Scale(_scale) * Rotation(_rotation) * Translate(_translation);
        }

        private static Matrix RotationX(float _v)
        {
            float sinX = Mathf.Sin(_v * Mathf.Deg2Rad);
            float cosX = Mathf.Cos(_v * Mathf.Deg2Rad);

            return new Matrix(new float[4, 4]
           {
                { 1, 0, 0, 0 },
                { 0, cosX, -sinX, 0 },
                { 0, sinX, cosX, 0 },
                { 0, 0, 0, 1 }
           });
        }
        private static Matrix RotationY(float _v)
        {
            float sinY = Mathf.Sin(_v * Mathf.Deg2Rad);
            float cosY = Mathf.Cos(_v * Mathf.Deg2Rad);

            return new Matrix(new float[4, 4]
           {
                { cosY, 0, sinY, 0 },
                { 0, 1, 0, 0 },
                { -sinY, 0, cosY, 0 },
                { 0, 0, 0, 1 }
           });
        }

        private static Matrix RotationZ(float _v)
        {
            float sinZ = Mathf.Sin(_v * Mathf.Deg2Rad);
            float cosZ = Mathf.Cos(_v * Mathf.Deg2Rad);

            return new Matrix(new float[4, 4]
           {
                { cosZ, -sinZ, 0, 0 },
                { sinZ, cosZ, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
           });
        }

        public static Vector3 operator *(Matrix _m, Vector3 _v)
        {
            return new Vector3(_m.Values[0, 0] * _v.x + _m.Values[0, 1] * _v.y + _m.Values[0, 2] * _v.z + _m.Values[0, 3],
                                _m.Values[1, 0] * _v.x + _m.Values[1, 1] * _v.y + _m.Values[1, 2] * _v.z + _m.Values[1, 3],
                                _m.Values[2, 0] * _v.x + _m.Values[2, 1] * _v.y + _m.Values[2, 2] * _v.z + _m.Values[2, 3]);
        }

        public static Matrix operator* (Matrix _m1, Matrix _m2)
        {
            if (_m1.Values.GetLength(0) != _m2.Values.GetLength(0))
            {
                throw new System.ArgumentException();
            }

            Matrix result = new Matrix(new float[4, 4]);

            for (int i = 0; i < _m1.Values.GetLength(1); i++)
            {
                for (int j = 0; j < _m1.Values.GetLength(0); j++)
                {
                    for (int k = 0; k < _m1.Values.GetLength(1); k++)
                    {
                        result.Values[j, i] += _m1.Values[k, i] * _m2.Values[j, k];
                    }
                }
            }

            return result;
        }
    }
}
