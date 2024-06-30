using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _offset;

    [SerializeField] private float _smoothing;

    private void Start()
    {
        if (_target != null)
        {
            transform.position = _target.position + _offset;
        }
        
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.position + _offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothing * Time.deltaTime);
        }
    }

}
