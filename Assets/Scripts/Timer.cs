using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action TimerEnded;

    public bool IsCounting { get; private set; }

    private float _time;

    private void Update()
    {
        Count();
    }

    public void StartTimer(float time)
    {
        if (time <= 0)
            throw new InvalidOperationException("Время на таймере должно быть больше нуля.");
        
        IsCounting = true;
        _time = time + Time.time;
    }

    public void StopTimer()
    {
        IsCounting = false;
    }

    private void Count()
    {
        if (IsCounting == false)
            return;

        if( _time <= Time.time )
        {
            IsCounting = false;
            TimerEnded?.Invoke();
        }
    }
}
