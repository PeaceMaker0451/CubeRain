using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private Particle _prefab;
    [SerializeField] private float _spawnTime;

    private ObjectPool<Particle> _pool;
    private Timer _createTimer;

    private void Start()
    {
        _pool = new ObjectPool<Particle>(_prefab);
        _pool.ObjectCreated += InitializeParticle;

        _createTimer = GetComponent<Timer>();
        _createTimer.TimerEnded += OnTimerEnded;
        _createTimer.StartTimer(_spawnTime);
    }

    private void OnDestroy()
    {
        _pool.ObjectCreated -= InitializeParticle; //мне правда нужно как по мантре отписываться от событий, которые полностью находятся под контролем этого объекта (и на этом объекте) и не могут быть сломаны "случайно"? 
        _createTimer.TimerEnded -= OnTimerEnded;
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
        float unitCubeSize = 1f;
        
        var scale = spawnerTransform.lossyScale;

        var randomOffset = new Vector3(
            Random.Range(-unitCubeSize / 2f, unitCubeSize / 2f),
            Random.Range(-unitCubeSize / 2f, unitCubeSize / 2f),
            Random.Range(-unitCubeSize / 2f, unitCubeSize / 2f));

        var randomPosition = spawnerTransform.TransformPoint(randomOffset);
        return randomPosition;
    }

    private void OnTimerEnded()
    {
        Spawn();
        _createTimer.StartTimer(_spawnTime);
    }

    private void InitializeParticle(Particle particle)
    {
        particle.Initialize(DespawnParticle);
    }
    
    private void DespawnParticle(Particle particle)
    {
        particle.gameObject.SetActive(false);
        _pool.Release(particle);
    }
}
