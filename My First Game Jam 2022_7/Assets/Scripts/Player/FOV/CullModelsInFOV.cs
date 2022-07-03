using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CullModelsInFOV : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Utilities.modelTag))
        {
            Utilities.ChangeLayersRecursively(other.gameObject, "Default");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Utilities.modelTag))
        {
            Utilities.ChangeLayersRecursively(other.gameObject, "Default");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Utilities.modelTag))
        {
            if(other.gameObject.transform.parent.GetComponentInChildren<CullModelsInFOV>() == null)
                Utilities.ChangeLayersRecursively(other.gameObject, Utilities.ignoreCameraLayer);
        }
    }
}
