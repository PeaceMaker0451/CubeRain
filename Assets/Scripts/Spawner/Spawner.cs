using System;
using UnityEngine;

public abstract class Spawner<T> : BaseSpawner where T : UnityEngine.Object
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    public event Action<T> Despawned;

    public override int TotalObjects => _pool.TotalObjects;
    public override int TotalSpawned => _pool.TotalSpawned;
    public override int FreeObjects => _pool.FreeObjects;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(_prefab);
        _pool.ObjectCreated += InitializeParticle;
    }

    protected T Spawn()
    {
        var particle = _pool.Get();
        InvokeParticleSpawned();
        return particle;
    }

    protected abstract void InitializeParticle(T particle, Action despawnAction);

    protected abstract void DespawnParticle(T particle);

    private void InitializeParticle(T particle)
    {
        InitializeParticle(particle, () => Despawn(particle));
        InvokeParticleCreated();
    }

    private void Despawn(T particle)
    {
        DespawnParticle(particle);
        _pool.Release(particle);
        Despawned?.Invoke(particle);
    }
}
