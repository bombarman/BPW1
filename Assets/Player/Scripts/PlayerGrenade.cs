using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private GameObject _grenade;
    [SerializeField] private Transform _firePoint;

    [Header("VARIABLES")]
    [SerializeField] private int _damage = 10;

    [SerializeField] private float _fireForce = 70f;

    private int _charges;

    public delegate void Delegate_SpendAmmo(int clipCount, int currentAmmo);
    public static event Delegate_SpendAmmo SpendAmmo;

  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireGrenade();
        }
    }

    private void FireGrenade()
    {
        GameObject currentGrenade = Instantiate(_grenade, _firePoint.position, _firePoint.rotation);
        currentGrenade.GetComponent<Rigidbody>().AddForce(transform.forward * _fireForce, ForceMode.Impulse);

        ModifyCharges(-1);
    }

    private void ModifyCharges(int amount)
    {
        _charges += amount;
    }
}
