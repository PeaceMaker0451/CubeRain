using UnityEngine;

public class Explosives : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _explosionForce = 50f;
    [SerializeField, Min(0f)] private float _explosionUpwardsModifier = 5f;
    [SerializeField, Min(0f)] private float _explosionRadius = 10;

    public float ExplosionForce => _explosionForce;
    public float ExplosionRadius => _explosionRadius;
    
    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if(collider.TryGetComponent<Rigidbody>(out var rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, _explosionUpwardsModifier, ForceMode.Impulse);
        }
    }

    public void SetExplosionForce(float force)
    {
        _explosionForce = force;
    }

    public void SetExplosionRadius(float radius)
    {
        _explosionRadius = radius;
    }
}
