using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationChangeOverTimeBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool clockWise;
    [SerializeField] private Space space;
    [SerializeField] private Vector3 axis;

    private Quaternion _default;

    private void Start()
    {
        _default = transform.rotation;
        axis.Normalize();
    }

    private void Update()
    {

        if (clockWise)
        {
            transform.Rotate(axis, Time.deltaTime * speed, space);
        }
        else
        {
            transform.Rotate(axis, -Time.deltaTime * speed, space);
        }

    }


}
