using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractionActorBehaviour : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        IInteractionObject interacted = other.gameObject.GetComponent<IInteractionObject>();
        interacted?.OnInteracted();
    }
}
