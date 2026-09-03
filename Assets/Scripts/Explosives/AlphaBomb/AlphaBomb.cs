using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ColorChanger), typeof(Explosives))]
public class AlphaBomb : MonoBehaviour
{
    [SerializeField] private float _minExplosionDurationSeconds = 2;
    [SerializeField] private float _maxExplosionDurationSeconds = 5;
    
    private Rigidbody _rigidBody;
    private ColorChanger _colorChanger;
    private Explosives _explosives;

    private Action _despawnAction;

    public bool IsTriggered { get; private set; }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
        _explosives = GetComponent<Explosives>();
    }

    public void Initialize(Action despawn)
    {
        _despawnAction = despawn;
    }

    public void ResetBomb()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
        _colorChanger.SetColor(_colorChanger.CurrentColor.WithAlpha(1));
        _colorChanger.SetOpaqueMode();
    }

    public void Explode()
    {
        StopAllCoroutines();
        StartCoroutine(ExplosionSequence());
    }

    private IEnumerator ExplosionSequence()
    {
        float explosionDuration = UnityEngine.Random.Range(_minExplosionDurationSeconds, _maxExplosionDurationSeconds);
        _colorChanger.SetFadeMode();
        _colorChanger.SetColorSmoothly(_colorChanger.CurrentColor.WithAlpha(0), explosionDuration);

        float time = 0;
        while (time < explosionDuration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        _explosives.Explode();
        _despawnAction?.Invoke();
    }
}
