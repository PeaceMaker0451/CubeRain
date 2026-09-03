using System;
using UnityEngine;

public abstract class Spawner<T> : BaseSpawner where T : UnityEngine.Object
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    private Action<T, Action> _onInitialized;
    private Action<T> _onDespawned;

    public event Action<T> Despawned;

    public override int TotalObjects => _pool.TotalObjects;
    public override int TotalSpawned => _pool.TotalSpawned;
    public override int FreeObjects => _pool.FreeObjects;

    protected T Spawn()
    {
        var particle = _pool.Get();
        InvokeParticleSpawned();
        return particle;
    }

    protected void Initialize(Action<T, Action> onInitialize, Action<T> onDespawned)
    {
        _onInitialized = onInitialize;
        _onDespawned = onDespawned;

        _pool = new ObjectPool<T>(_prefab);
        _pool.ObjectCreated += InitializeParticle;
    }

    private void InitializeParticle(T particle)
    {
        _onInitialized?.Invoke(particle, () => Despawn(particle));
        InvokeParticleCreated();
    }

    private void Despawn(T particle)
    {
        _onDespawned?.Invoke(particle);
        _pool.Release(particle);
        Despawned?.Invoke(particle);
    }
}
