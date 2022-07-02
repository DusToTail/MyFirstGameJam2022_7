using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : InteractionObjectBehaviour
{
    [SerializeField] private bool isTriggered;
    public override void OnDistant()
    {
    }

    public override void OnInteracted()
    {
    }

    public override void OnNearby()
    {
        if (isTriggered) { return; }
        Debug.Log("Checkpoint reached", this);
    }
}
