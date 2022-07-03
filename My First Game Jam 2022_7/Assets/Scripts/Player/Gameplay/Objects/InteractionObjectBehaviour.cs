using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractionObjectBehaviour : MonoBehaviour, IInteractionObject
{
    public abstract void OnInteracted();
    public abstract void OnDistant();
    public abstract void OnNearby();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Utilities.playerTag))
            OnNearby();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Utilities.playerTag))
            OnDistant();
    }
}
