using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_AttackVictimExists : Transition
{
    private DetectCharacterInAttackZoneBehaviour box;
    public TN_AttackVictimExists(DetectCharacterInAttackZoneBehaviour box, FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) 
    { this.box = box; }
    public override bool CanTransition()
    {
        if (box.target != null)
            return inverted ? false : true;
        return inverted ? true : false;
    }
}
