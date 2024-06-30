using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healAmount = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().Heal(_healAmount);
            Destroy(gameObject);
        }
    }
}

