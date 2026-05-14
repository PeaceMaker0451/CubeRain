using System;
using UnityEngine;

public abstract class Particle : MonoBehaviour
{
    private Action<Particle> _despawnAction;
    
    public event Action Initialized;
    public event Action StateReset;
    public event Action Triggered;

    public bool IsTriggered { get; private set;  }
    public bool IsInitialized { get; private set;  }

    public void Initialize(Action<Particle> onDespawn)
    {
        if(IsInitialized) 
            return;

        _despawnAction = onDespawn;
        IsInitialized = true;
        
        OnInitialized();
        Initialized?.Invoke();
    }

    public void ResetState()
    {
        IsTriggered = false;
        
        OnStateReset();
        StateReset?.Invoke();
    }
    
    public void Trigger()
    {
        if (IsTriggered)
            return;

        IsTriggered = true;
        
        OnTriggered();
        Triggered?.Invoke();
    }

    protected void Despawn()
    {
        _despawnAction?.Invoke(this);
    }

    protected abstract void OnInitialized();
    protected abstract void OnStateReset();
    protected abstract void OnTriggered();
}
