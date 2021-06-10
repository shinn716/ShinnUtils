using System.Text;
using UnityEngine;

namespace Shinn
{
    public static class Math
    {
        /// <summary>
        /// 取偶數, 小數點第N位 (小數點最大第二位)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float GetEven(float input, int n = 1)
        {
            input = (float)((int)(input * Mathf.Pow(10, n)) / Mathf.Pow(10, n));
            input *= Mathf.Pow(10, n);
            return input % 2 == 0 ? input / Mathf.Pow(10, n) : (input + 1) / Mathf.Pow(10, n);
        }

        /// <summary>
        /// 取偶數
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetEven(int input)
        {
            return input % 2 == 0 ? input : (input + 1);
        }

        /// <summary>
        /// 取 除數 數值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static int GetCustomValue(int input, int divisor)
        {
            return input % divisor == 0 ? input : input - (input % divisor);
        }

        /// <summary>
        /// 兩四元數 求夾角 
        /// </summary>
        /// <param name="qBase"></param>
        /// <param name="qTarget"></param>
        /// <returns></returns>
        public static float GetQ2Euler(Quaternion qBase, Quaternion qtarget)
        {
            Quaternion qDiff = Quaternion.Inverse(qBase) * qtarget;
            Vector3 vEuler = qDiff.eulerAngles;
            float yaw = vEuler.y;
            if (yaw > 180)
                yaw -= 360;
            return yaw;
        }

        // qbase = Quaternion.identity
        public static float GetQ2Ground(Quaternion qcurr, Quaternion qbase)
        {
            Quaternion qv1 = new Quaternion(1, 0, 0, 0);
            Quaternion qbaseinv = qbase;
            qbaseinv = Quaternion.Inverse(qbaseinv);
            Quaternion qrot = qcurr * qbaseinv;
            Quaternion qrotinv = qrot;
            qrotinv = Quaternion.Inverse(qrotinv);
            Quaternion qv2 = qrot * qv1 * qrotinv;
            double Angle = Mathf.Acos(-qv2.z) * 180 / Mathf.PI;
            if (Angle.Equals(float.NaN))
                Angle = 0;
            return (float)Angle;
        }
    }
}
