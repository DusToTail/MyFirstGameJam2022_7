using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TransparentObstacleCameraBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Transform parent = other.transform.parent;
        if(other.CompareTag(Utilities.modelTag) && parent.gameObject.layer == LayerMask.NameToLayer(Utilities.obstacleLayer))
        {
            Color originColor = other.gameObject.GetComponent<Renderer>().sharedMaterial.color;
            other.gameObject.GetComponent<Renderer>().material.color = new Color(originColor.r, originColor.g, originColor.b, 0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Transform parent = other.transform.parent;
        if (other.CompareTag(Utilities.modelTag) && parent.gameObject.layer == LayerMask.NameToLayer(Utilities.obstacleLayer))
        {
            Color originColor = other.gameObject.GetComponent<Renderer>().sharedMaterial.color;
            other.gameObject.GetComponent<Renderer>().material.color = new Color(originColor.r, originColor.g, originColor.b, 1f);
        }
    }
}
