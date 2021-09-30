using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateProjectile : MonoBehaviour
{
    
    [SerializeField] private float rotationAngle;
    [SerializeField] private float repeatRate;
    [SerializeField] private float initialRotateDelay;
    private void Start()
    {
        InvokeRepeating(nameof(RotateObject), initialRotateDelay, repeatRate);
    }


    private void RotateObject()
    {
        gameObject.transform.Rotate(-Vector3.forward, rotationAngle);
    }
}
