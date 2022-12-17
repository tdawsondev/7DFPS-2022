using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public float RotationSpeed;

    private Quaternion _lookRotation;
    private Vector3 _direction;
    public bool allowUpdate = false;

    private void Start()
    {
        target = Player.Instance.transform;
    }

    private void Update()
    {
        if (target && allowUpdate)
        {
            _direction = (target.position - transform.position).normalized;
            _direction.y = 0;
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, RotationSpeed * Time.deltaTime);
        }

    }
}
