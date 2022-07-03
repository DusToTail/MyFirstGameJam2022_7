using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionObject
{
    public void OnInteracted();
    public void OnNearby();
    public void OnDistant();
}
