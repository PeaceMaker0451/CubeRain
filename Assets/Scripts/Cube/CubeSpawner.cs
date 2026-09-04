using System;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnTime;

    private Timer _createTimer;

    protected override void Awake()
    {
        base.Awake();
        _createTimer = GetComponent<Timer>();
    }

    private void Start()
    {
        _createTimer.StartTimer(_spawnTime);
    }

    private void OnEnable()
    {
        _createTimer.TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        _createTimer.TimerEnded -= OnTimerEnded;
    }

    protected override void InitializeParticle(Cube particle, Action despawnAction)
    {
        particle.Initialize(despawnAction);
    }

    protected override void DespawnParticle(Cube particle)
    {
        particle.gameObject.SetActive(false);
    }

    private void OnTimerEnded()
    {
        var cube = Spawn();
        cube.gameObject.SetActive(true);
        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.ResetParticle();

        _createTimer.StartTimer(_spawnTime);
    }

    private Vector3 GetRandomPosition()
    {
        var spawnerTransform = this.transform;
        float unitCubeSize = 1f;

        var scale = spawnerTransform.lossyScale;

        var randomOffset = new Vector3(
            UnityEngine.Random.Range(-unitCubeSize / 2f, unitCubeSize / 2f),
            UnityEngine.Random.Range(-unitCubeSize / 2f, unitCubeSize / 2f),
            UnityEngine.Random.Range(-unitCubeSize / 2f, unitCubeSize / 2f));

        var randomPosition = spawnerTransform.TransformPoint(randomOffset);
        return randomPosition;
    }
}
