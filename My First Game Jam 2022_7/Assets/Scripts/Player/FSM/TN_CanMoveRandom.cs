using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_CanMoveRandom<T> : Transition where T : CharacterBehaviour, IMoveRandom
{
    public TN_CanMoveRandom(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }
    public override bool CanTransition()
    {
        if (fsm.AI.Character.GetComponent<T>().CanMoveRandom)
            return inverted ? false : true;
        return inverted ? true : false;
    }
}
