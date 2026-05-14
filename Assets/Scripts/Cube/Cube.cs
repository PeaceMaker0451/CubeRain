using System;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(Timer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private float _minDespawnDelay;
    [SerializeField] private float _maxDespawnDelay;

    private ColorChanger _colorChanger;
    private Timer _despawnTimer;
    private Rigidbody _rigidBody;

    private Action<Cube> _despawnAction;

    public bool IsTriggered { get; private set;  }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _despawnTimer = GetComponent<Timer>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _despawnTimer.TimerEnded += Despawn;
    }

    private void OnDisable()
    {
        _despawnTimer.TimerEnded -= Despawn;
    }

    public void Initialize(Action<Cube> onDespawn)
    {
        _despawnAction = onDespawn;
    }

    public void ResetState()
    { 
        IsTriggered = false;
        _colorChanger.SetColor(_defaultColor);
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }
    
    public void Trigger()
    {
        if (IsTriggered)
            return;

        _colorChanger.RandomizeColor();
        float despawnDelay = GetDespawnDelay();
        _despawnTimer.StartTimer(despawnDelay);
        IsTriggered = true;
    }

    private void Despawn()
    {
        _despawnAction?.Invoke(this);
    }

    private float GetDespawnDelay()
    {
        return UnityEngine.Random.Range(_minDespawnDelay, _maxDespawnDelay);
    }
}
