using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_TransformsInProximity : Transition
{
    public Transform first;
    public Transform second;
    public float distance;
    public TN_TransformsInProximity(Transform first, Transform second, float distance, FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted)
    {
        this.first = first;
        this.second = second;
        this.distance = distance;
    }
    public override bool CanTransition()
    {
        if(Vector3.Distance(first.position, second.position) <= distance)
            return inverted? false : true;
        return inverted? true : false;
    }
}
