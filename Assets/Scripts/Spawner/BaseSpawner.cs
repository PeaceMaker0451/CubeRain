using System;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    public event Action ParticleCreated;
    public event Action ParticleSpawned;

    public abstract int TotalObjects { get; }
    public abstract int TotalSpawned { get; }
    public abstract int FreeObjects { get; }

    protected void InvokeParticleCreated()
    {
        ParticleCreated?.Invoke();
    }

    protected void InvokeParticleSpawned()
    {
        ParticleSpawned?.Invoke();
    }
}
