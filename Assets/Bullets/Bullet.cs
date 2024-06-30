using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {

        Health health = other.gameObject.GetComponent<Health>();
        if (health != null )
        {
            health.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
