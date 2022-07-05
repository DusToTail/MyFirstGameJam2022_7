using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : InteractionObjectBehaviour
{
    [SerializeField] private bool isTriggered;
    public override void OnDistant()
    {
    }

    public override void OnInteracted(InteractionActorBehaviour byActor)
    {
        //Debug.Log($"{transform.parent.gameObject.name} is interacted", transform.parent);
    }

    public override void OnNearby()
    {
        if (isTriggered) { return; }
        isTriggered = true;
        Debug.Log("Checkpoint reached", this);
    }
}
