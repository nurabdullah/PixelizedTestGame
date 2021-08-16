using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1Rotater : MonoBehaviour
{
    public Vector3 rotation;
    Rigidbody _rigidbody;
    Vector3 rot;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = Vector3.zero;
        rot = transform.eulerAngles;
    }

    private void FixedUpdate()
    {
        rot += Time.fixedDeltaTime * rotation;
        _rigidbody.MoveRotation(Quaternion.Euler(rot));
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.CompareTag("Player"))
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}
}
