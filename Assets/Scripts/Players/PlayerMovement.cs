using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int movementSpeed;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 targetPosition = transform.position + (Vector3.forward * movementSpeed);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }
}
