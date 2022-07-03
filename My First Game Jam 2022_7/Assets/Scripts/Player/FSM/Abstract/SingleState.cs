using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleState : State
{
    protected SingleState(FiniteStateMachine fsm) : base(fsm) { }
}
