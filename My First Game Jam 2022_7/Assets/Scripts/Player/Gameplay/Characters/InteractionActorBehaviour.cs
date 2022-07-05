using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractionActorBehaviour : MonoBehaviour
{
    public void InteractObject(Vector3 atGroundPosition)
    {
        Collider[] colliders = Physics.OverlapBox(atGroundPosition, Vector3.one / 2, Quaternion.identity, LayerMask.GetMask(Utilities.objectLayer));
        foreach (var col in colliders)
        {
            col.gameObject.GetComponentInChildren<IInteractionObject>()?.OnInteracted();
        }
    }
}
