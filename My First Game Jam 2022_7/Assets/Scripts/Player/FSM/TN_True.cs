using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_True : Transition
{
    public TN_True(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }
    public override bool CanTransition()
    {
        return inverted? false : true;
    }
}
