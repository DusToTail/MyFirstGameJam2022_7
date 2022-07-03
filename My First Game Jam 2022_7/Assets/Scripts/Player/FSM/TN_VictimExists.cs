using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_VictimExists : Transition
{
    public TN_VictimExists(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }
    public override bool CanTransition()
    {
        if (fsm.AI.Victim != null)
            return inverted ? false : true;
        return inverted ? true : false;
    }
}
