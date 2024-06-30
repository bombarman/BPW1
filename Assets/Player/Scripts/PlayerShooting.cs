
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Animator _animator;

    [Header("VARIABLES")]
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _clipSize = 10;
    [SerializeField] private int _maxAmmo = 40;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _fireForce = 70f;
    private int _clipCount;
    private int _currentAmmo;
    private bool _isReloading = false;

    public delegate void Delegate_SpendAmmo(int clipCount, int currentAmmo);
    public static event Delegate_SpendAmmo SpendAmmo;

    private void Start()
    {
        _clipCount = _clipSize;
        _currentAmmo = _maxAmmo;

        OnSpendAmmo();
    }

    private void Update()
    {
        if (_isReloading) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_clipCount > 0)
            {
                Shoot();
            }
            else if (_currentAmmo > 0)
            {
                _isReloading = true;
                Invoke(nameof(Reload), _reloadTime);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && _currentAmmo > 0)
        {
            _isReloading = true;
            Invoke(nameof(Reload), _reloadTime);
        }
    }

    private void Shoot()
    {
        GameObject currentBullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.forward * _fireForce, ForceMode.Impulse);
        currentBullet.GetComponent<Bullet>().SetDamage(_damage);

        _animator.SetTrigger("Shoot");

        _clipCount--;
        OnSpendAmmo();
    }

    private void Reload()
    {
        int missingBullets = _clipSize - _clipCount;

        if (_currentAmmo >= missingBullets)
        {
            _clipCount = _clipSize;
            _currentAmmo -= missingBullets;
        }
        else
        {
            _clipCount += _currentAmmo;
            _currentAmmo = 0;
        }

        OnSpendAmmo();

        _isReloading= false;
    }

    private void OnSpendAmmo()
    {
        SpendAmmo?.Invoke(_clipCount, _currentAmmo);
    }

    public void GainAmmo(int amount)
    {
        _currentAmmo += amount;
        if (_currentAmmo >= _maxAmmo)
        {
            _currentAmmo = _maxAmmo;
        }
        OnSpendAmmo();
    }

    public bool HasMaxAmmo()
    {
        return _currentAmmo == _maxAmmo;
    }
}