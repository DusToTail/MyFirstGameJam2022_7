using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    private void LateUpdate()
    {
        if (target == null) { return; }
        transform.rotation = Quaternion.LookRotation(target.forward, target.up);
    }
}
