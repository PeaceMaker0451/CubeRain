using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action TimerEnded;
    
    public float Time {  get; private set; }
    public bool IsCounting { get; private set; }

    void Update()
    {
        Count();
    }

    public void StartTimer(float time)
    {
        if (time <= 0)
            throw new InvalidOperationException("Время на таймере должно быть больше нуля.");
        
        IsCounting = true;
        Time = time;
    }

    public void StopTimer()
    {
        IsCounting = false;
    }

    private void Count()
    {
        if (IsCounting == false)
            return;

        Time -= UnityEngine.Time.deltaTime;

        if( Time <= 0 )
        {
            IsCounting = false;
            TimerEnded?.Invoke();
        }
    }
}
