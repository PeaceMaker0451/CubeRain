using System;
using UnityEngine;

public abstract class Particle : MonoBehaviour
{
    private Action<Particle> _despawnAction;
    
    public Action Initialized;
    public Action StateReset;
    public Action Triggered;

    public bool IsTriggered { get; private set;  }
    public bool IsInitialized { get; private set;  }

    public void Initialize(Action<Particle> onDespawn)
    {
        if(IsInitialized) 
            return;

        _despawnAction = onDespawn;
        IsInitialized = true;
        Initialized?.Invoke();
    }

    public void ResetState()
    {
        IsTriggered = false;
        StateReset?.Invoke();
    }
    
    public void Trigger()
    {
        if (IsTriggered)
            return;

        IsTriggered = true;
        Triggered?.Invoke();
    }

    protected void Despawn()
    {
        _despawnAction?.Invoke(this);
    }
}
