using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractionObjectBehaviour : MonoBehaviour, IInteractionObject
{
    public abstract void OnInteracted(InteractionActorBehaviour byActor);
    public abstract void OnDistant();
    public abstract void OnNearby();

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Utilities.playerTag))
        {
            if(other.GetComponent<InteractionActorBehaviour>() != null)
                OnNearby();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Utilities.playerTag))
        {
            if (other.GetComponent<InteractionActorBehaviour>() != null)
                OnDistant();
        }
    }
}
