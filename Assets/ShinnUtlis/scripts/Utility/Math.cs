using UnityEngine;

namespace Shinn
{
    public class Math
    {
        /// <summary>
        /// 取偶數, 小數點第N位 (小數點最大第二位)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static float Even(float input, int n = 1)
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
        public static int Even(int input)
        {
            return input % 2 == 0 ? input : (input + 1);
        }
    }
}
