using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_CanAttack<T> : Transition where T : CharacterBehaviour, IAttack
{
    public TN_CanAttack(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }
    public override bool CanTransition()
    {
        if (fsm.AI.Character.GetComponent<IAttack>().CanAttack)
            return inverted ? false : true;
        return inverted ? true : false;
    }
}
