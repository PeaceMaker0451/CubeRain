using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Particle>(out var particle) == false)
            return;

        particle.Trigger();
    }
}
