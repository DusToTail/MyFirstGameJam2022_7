using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SequenceState : State
{
    public List<State> children;
    protected SequenceState(FiniteStateMachine fsm) : base(fsm) { }

    public override void Execute()
    {
        base.Execute();
    }
}
