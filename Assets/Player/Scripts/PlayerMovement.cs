using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _animator;
    private float _xInput;
    private float _zInput;

    [SerializeField] private float _movementSpeed;

    private Vector3 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void MyInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _zInput = Input.GetAxisRaw("Vertical");

        _direction = new(_xInput, 0, _zInput);

        //_animator.SetFloat("Speed", _direction.magnitude);
    }

    private void Move()
    {
        _direction.Normalize();
        _direction *= _movementSpeed * 50 * Time.fixedDeltaTime;
        _rb.velocity = _direction;
    }
}
