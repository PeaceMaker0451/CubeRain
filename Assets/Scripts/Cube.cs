using System;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(Timer), typeof(Rigidbody))]
public class Cube : Particle
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private float _minDespawnDelay;
    [SerializeField] private float _maxDespawnDelay;
    
    private ColorChanger _colorChanger;
    private Timer _despawnTimer;
    private Rigidbody _rigidBody;

    
    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _despawnTimer = GetComponent<Timer>();
        _rigidBody = GetComponent<Rigidbody>();

        _despawnTimer.TimerEnded += () => Despawn();
        
        StateReset += OnReset;
        Triggered += OnCollisionConfirmed;
    }

    private void OnReset()
    {
        _colorChanger.SetColor(_defaultColor);
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionConfirmed()
    {
        _colorChanger.RandomizeColor();
        float despawnDelay = GetDespawnDelay();
        _despawnTimer.StartTimer(despawnDelay);
    }

    private float GetDespawnDelay()
    {
        float time = UnityEngine.Random.Range(_minDespawnDelay, _maxDespawnDelay);
        return time;
    }
}
