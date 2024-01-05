using System;
using UnityEngine;

namespace Platformer2D.Utils
{
    public class Timer
    {
        public float Duration { get; private set; }
        private float currentTime;
        public bool IsActive { get; private set; }
        public bool IsCompleted => IsActive && currentTime <= 0;

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
            if (IsActive)
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
            IsActive = true;
            currentTime = Duration;
        }

        public void Stop()
        {
            IsActive = false;
        }

        public void Reset()
        {
            currentTime = Duration;
            IsActive = false;
        }
    }

}
