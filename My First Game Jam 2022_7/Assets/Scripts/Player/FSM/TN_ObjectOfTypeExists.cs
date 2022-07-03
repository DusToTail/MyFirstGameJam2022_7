using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_ObjectOfTypeExists<T> : Transition where T : Object
{
    public TN_ObjectOfTypeExists(FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted) { }

    public override bool CanTransition()
    {
        if(GameObject.FindObjectOfType<T>())
            return inverted? false : true;
        return inverted? true : false;
    }
}
