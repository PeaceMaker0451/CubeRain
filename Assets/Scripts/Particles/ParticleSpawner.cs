using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private Particle _prefab;
    [SerializeField] private float _spawnTimer;

    private ObjectPool<Particle> _pool;

    private float _time = 0f;

    private void Start()
    {
        _pool = new ObjectPool<Particle>(_prefab);
        _pool.ObjectCreated += (particle) => particle.Initialize((particle) =>
        {
            particle.gameObject.SetActive(false);
            _pool.Release(particle);
        });
    }

    private void Update()
    {
        _time -= Time.deltaTime;

        if (_time <= 0f)
        {
            Spawn();
            _time = _spawnTimer;
        }
            
    }

    private void Spawn()
    {
        var cube = _pool.Get();
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.ResetState();
    }

    private Vector3 GetRandomPosition()
    {
        var spawnerTransform = this.transform;
        
        var scale = spawnerTransform.lossyScale;

        var randomOffset = new Vector3(
            Random.Range(-1f / 2f, 1f / 2f),
            Random.Range(-1f / 2f, 1f / 2f),
            Random.Range(-1f / 2f, 1f / 2f));

        var randomPosition = spawnerTransform.TransformPoint(randomOffset);
        return randomPosition;
    }
}
