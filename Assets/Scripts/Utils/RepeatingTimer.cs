using System;
using System.Collections;
using UnityEngine;
namespace Game.Utils
{
    class RepeatingTimer
    {
        public event Action OnTimerTriggered;
        private Coroutine _coroutine;
        private float _timerRate;
        private float _countdown;

        /// <summary>
        /// Initializes timer rate
        /// </summary>
        /// <param name="timerRate"></param>
        public RepeatingTimer(float timerRate)
        {
            _timerRate = timerRate;
            _countdown = timerRate;
            _coroutine = null;
        }

        /// <summary>
        /// Initializes timer rate and action which timer perform
        /// </summary>
        /// <param name="timerRate"></param>
        /// <param name="action"></param>
        public RepeatingTimer(float timerRate, Action action) : this(timerRate)
        {
            OnTimerTriggered += action;
        }

        /// <summary>
        /// Start repeating timer action.
        /// initializer - the object to which it is attached timer. 
        /// </summary>
        /// <param name="initializer"></param>
        public void StartTimer(MonoBehaviour initializer)
        {
            if (_coroutine == null)
            {
                _coroutine = initializer.StartCoroutine(Start());
            }
        }

        /// <summary>
        /// Stop repeating timer action.
        /// initializer - the object to which it is attached timer.
        /// </summary>
        /// <param name="initializer"></param>
        public void StopTimer(MonoBehaviour initializer)
        {
            if (_coroutine != null)
            {
                initializer.StopCoroutine(_coroutine);
                _coroutine = null;
            }           
        }

        /// <summary>
        /// Unsubscribe timer from all events.
        /// </summary>
        public void Clear()
        {
            OnTimerTriggered = null;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                if (Mathf.Approximately(_countdown, 0))
                {
                    OnTimerTriggered?.Invoke();
                    _countdown = _timerRate;
                }
                else
                {
                    _countdown -= Time.deltaTime;
                }
            }
        }
    }

    class RepeatingTimer<T>
    {
        public event Action<T> OnTimerTriggered;
        private Coroutine _coroutine;
        private float _timerRate;
        private float _countdown;

        /// <summary>
        /// Initializes timer rate
        /// </summary>
        /// <param name="timerRate"></param>
        public RepeatingTimer(float timerRate)
        {
            _timerRate = timerRate;
            _countdown = timerRate;
            _coroutine = null;
        }

        /// <summary>
        /// Initializes timer rate and action which timer perform
        /// </summary>
        /// <param name="timerRate"></param>
        /// <param name="action"></param>
        public RepeatingTimer(float timerRate, Action<T> action) : this(timerRate)
        {
            OnTimerTriggered += action;
        }

        /// <summary>
        /// Start repeating timer action.
        /// initializer - the object to which it is attached timer.
        /// value - value with which the timer will work.
        /// </summary>
        /// <param name="initializer"></param>
        /// <param name="value"></param>
        public void StartTimer(MonoBehaviour initializer, T value)
        {
            if (_coroutine == null)
            {
                _coroutine = initializer.StartCoroutine(Start(value));
            }
        }

        /// <summary>
        /// Stop repeating timer action.
        /// initializer - the object to which it is attached timer.
        /// </summary>
        /// <param name="initializer"></param>
        public void StopTimer(MonoBehaviour initializer)
        {
            if (_coroutine != null)
            {
                initializer.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        /// <summary>
        /// Unsubscribe timer from all events.
        /// </summary>
        public void Clear()
        {
            OnTimerTriggered = null;
        }

        private IEnumerator Start(T value)
        {
            while (true)
            {
                if (Mathf.Approximately(_countdown, 0))
                {
                    OnTimerTriggered?.Invoke(value);
                    _countdown = _timerRate;
                }
                else
                {
                    _countdown -= Time.deltaTime;
                }
            }
        }
    }
}
