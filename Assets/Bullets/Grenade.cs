using UnityEngine;

public class Grenande : MonoBehaviour
{
    [SerializeField] private LayerMask _damageLayer;
    [SerializeField] private int _damage;
    [SerializeField] private float _damageRadius;
    [SerializeField] private float _explosionDelay;

    private void Start()
    {
        Invoke(nameof(Explode), _explosionDelay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Explode();

        }
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _damageRadius, _damageLayer);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<Health>(out var Health))
            {
                Health.TakeDamage(_damage); 
            }
        }

        Destroy(gameObject);
    }
}
