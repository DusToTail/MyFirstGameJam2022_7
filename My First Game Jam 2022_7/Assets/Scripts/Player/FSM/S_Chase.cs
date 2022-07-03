using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Chase<T> : SingleState where T : CharacterBehaviour
{
    public S_Chase(FiniteStateMachine fsm) : base(fsm) { }
    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnStay()
    {
        fsm.AI.Character.followPosition = fsm.AI.Target? fsm.AI.Target.transform.position : fsm.AI.Character.transform.position;
        fsm.AI.Character.Move();
    }
}
