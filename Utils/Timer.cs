using System;
using UnityEngine;

namespace Platformer2D.Utils
{
    public class Timer
    {
        public float Duration { get; private set; }
        private float currentTime;
        public bool IsRunning { get; private set; }
        public bool IsCompleted => IsRunning && currentTime <= 0;

        public event Action OnComplete;

        public Timer(float duration)
        {
            Duration = duration;
            Reset();
        }

        /// <summary>
        /// It will only run if it's active
        /// </summary>
        public void UpdateTimer()
        {
            if (IsRunning)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    Stop();
                    OnComplete?.Invoke();
                }
            }
        }

        public void Start()
        {
            IsRunning = true;
            currentTime = Duration;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            currentTime = Duration;
            IsRunning = false;
        }
    }

}
