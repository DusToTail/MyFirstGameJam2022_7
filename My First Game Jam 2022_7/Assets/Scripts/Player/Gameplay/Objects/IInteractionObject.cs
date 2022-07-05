using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionObject
{
    public void OnInteracted(InteractionActorBehaviour byActor);
    public void OnNearby();
    public void OnDistant();
}
