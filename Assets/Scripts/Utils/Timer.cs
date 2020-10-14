using System;
using System.Collections;
using UnityEngine;

namespace Game.Utils
{
    class Timer
    {
        public event Action OnTimerTriggered;
        private float _delay;
        private Coroutine _coroutine;

        /// <summary>
        /// Initializes timer delay
        /// </summary>
        /// <param name="timeDelay"></param>
        public Timer(float timeDelay)
        {
            _delay = timeDelay;
        }

        /// <summary>
        /// Initializes timer delay and action which timer perform
        /// </summary>
        /// <param name="timeDelay"></param>
        /// <param name="action"></param>
        public Timer(float timeDelay, Action action) : this(timeDelay)
        {
            OnTimerTriggered += action;
        }

        /// <summary>
        /// Start timer.
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
        /// Stop timer.
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
            yield return new WaitForSeconds(_delay);
            OnTimerTriggered?.Invoke();
        }
    }

    class Timer<T>
    {
        public event Action<T> OnTimerTriggered;
        private float _delay;

        /// <summary>
        /// Initializes timer delay
        /// </summary>
        /// <param name="timeDelay"></param>
        public Timer(float timeDelay)
        {
            _delay = timeDelay;
        }

        /// <summary>
        /// Initializes timer delay and action which timer perform
        /// </summary>
        /// <param name="timeDelay"></param>
        /// <param name="action"></param>
        public Timer(float timeDelay, Action<T> action) : this(timeDelay)
        {
            OnTimerTriggered += action;
        }

        /// <summary>
        /// Start timer.
        /// initializer - the object to which it is attached timer. 
        /// value - value with which the timer will work.
        /// </summary>
        /// <param name="initializer"></param>
        /// <param name="value"></param>
        public void StartTimer(MonoBehaviour initializer, T value)
        {
            initializer.StartCoroutine(Start(value));            
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
            yield return new WaitForSeconds(_delay);
            OnTimerTriggered?.Invoke(value);
        }
    }
}
