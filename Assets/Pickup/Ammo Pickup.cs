using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int _ammoAmount = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerShooting ps = other.gameObject.GetComponent<PlayerShooting>();
            if (!ps.HasMaxAmmo())
            {
                ps.GainAmmo(_ammoAmount);
                Destroy(gameObject);
            }
        }

        Debug.Log("trigger entered");
    }
}
