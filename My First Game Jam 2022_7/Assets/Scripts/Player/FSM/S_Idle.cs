using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Idle<T> : SingleState where T : CharacterBehaviour
{
    public S_Idle(FiniteStateMachine fsm) : base(fsm) { }
    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnStay()
    {
        fsm.AI.Character.followPosition = fsm.AI.Character.transform.position;
        fsm.AI.Character.Idle();
    }
}
