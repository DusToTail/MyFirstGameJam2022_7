using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_WaypointReached<T> : Transition where T : CharacterBehaviour, IMoveRandom
{
    public S_MoveRandom<T> moveRandom;
    public TN_WaypointReached(S_MoveRandom<T> moveRandom, FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted)
    {
        this.moveRandom = moveRandom;
    }
    public override bool CanTransition()
    {
        return inverted ? !moveRandom.waypointReached : moveRandom.waypointReached;
    }
}
