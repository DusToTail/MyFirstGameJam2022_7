using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_CompositeTransition : Transition
{
    public TN_CompositeTransition(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }
    public List<Transition> transitions = new List<Transition>();
    public override bool CanTransition()
    {
        for(int i = 0; i < transitions.Count; i++)
        {
            if(!transitions[i].CanTransition())
                return inverted ? true : false;
        }
        return inverted ? false : true;

    }
    public void AddCondition(Transition transition) { transitions.Add(transition); }
}
