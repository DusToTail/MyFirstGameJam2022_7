using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GoalReachedBehaviour : InteractionObjectBehaviour
{
    public delegate void GoalReached();
    public static event GoalReached OnGoalReached;
    [SerializeField] private bool isTriggered;
    [SerializeField] private PlayableDirector clearTL;
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
        if(OnGoalReached != null)
            OnGoalReached();
        clearTL.Play();
        Debug.Log("Goal reached", this);
    }
}
