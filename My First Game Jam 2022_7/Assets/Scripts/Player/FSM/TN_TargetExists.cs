using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_TargetExists : Transition
{
    public TN_TargetExists(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }
    public override bool CanTransition()
    {
        if (fsm.AI.Target != null)
            return inverted? false : true;
        return inverted? true : false;
    }
}
