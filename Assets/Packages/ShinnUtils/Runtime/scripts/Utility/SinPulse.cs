// Author: Shinn
// Date: 20220805
//
// Sample:
// SinPulse pulse = new SinPulse(Callback);
// void Callback(float value)
// {
//    print("Callback " + value);
// }

using UnityEngine;
using System;
using System.Threading;

namespace Shinn
{
    public class SinPulse
    {
        private Thread thread = null;
        private bool isRun = true;
        private float m_count = 0;
        private float value = 0;

        /// <summary>
        /// 0 -> 1 -> 0 make Sin Pulse.
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="time"></param>
        /// <param name="callback"></param>
        public SinPulse(float _magnitude, float _time, Action<float> _callback, bool _loop = false)
        {
            thread = new Thread(() => StartPulse(_magnitude, _time, _callback, _loop));
            thread.Start();
        }

        public void Dispose()
        {
            if (thread != null)
            {
                thread.Abort();
                thread = null;
            }
        }

        private void StartPulse(float _magnitude, float _time, Action<float> _callback, bool _loop = false)
        {
            int microTime = Convert.ToInt32(_time *= 1000 / 33);
            while (isRun)
            {
                Thread.Sleep(microTime);
                m_count += .1f;
                value = _magnitude * Mathf.Sin(m_count);
                Callback(_callback);

                if (value <= 0.05f && !_loop)
                {
                    value = 0;
                    isRun = false;
                    Callback(_callback);

                    if (thread != null)
                    {
                        thread.Abort();
                        thread = null;
                    }
                }
            }
        }

        private void Callback(Action<float> _callback)
        {
            if (_callback != null)
                _callback.Invoke(value);
        }
    }
}
