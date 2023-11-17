using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private float _angle = 0f;


    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * _rotationSpeed, 0);
    }
}
