using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    public float smoothSpeed = 0.125f;


    private void Update()
    {
        transform.position = target.position + offset;
    }
}
