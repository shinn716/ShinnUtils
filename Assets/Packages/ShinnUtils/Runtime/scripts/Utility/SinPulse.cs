// Author: Shinn
// Date: 20200402
//
// Sample:
// Pulse pulse = new Pulse(mainCallback);
// void mainCallback(float value)
// {
//    print("mainCallback " + value);
// }

using UnityEngine;
using System;
using System.Threading;
namespace Shinn
{
    public class SinPulse
    {

        Thread thread;
        bool isRun = true;
        float m_count;
        float value;

        /// <summary>
        /// 0 -> 1 -> 0 make Sin Pulse.
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="microSeconds"></param>
        /// <param name="functionName"></param>
        public SinPulse(float magnitude, int microSeconds, Action<float> functionName)
        {
            StartPluse(magnitude, microSeconds, functionName);
        }

        private void StartPluse(float magnitude, int microSeconds, Action<float> functionName)
        {
            thread = new Thread(() => Counting(magnitude, microSeconds, functionName));
            thread.Start();
            //Debug.Log("StartPluse");
        }

        private void Counting(float magnitude, int microSeconds, Action<float> functionName)
        {

            while (isRun)
            {
                Thread.Sleep(microSeconds);
                m_count += .1f;
                value = magnitude * Mathf.Sin(m_count);
                Callback(functionName);
                //Debug.Log(value);
                if (value < 0)
                {
                    value = 0;
                    Callback(functionName);
                    //Debug.Log("End");
                    isRun = false; // disable while loop
                    thread.Abort();
                }
            }
        }

        private void Callback(Action<float> functionName)
        {
            functionName(GetValue());
        }

        private float GetValue()
        {
            return value;
        }
    }
}
