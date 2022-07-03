using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Attack<T> : SingleState where T : CharacterBehaviour, IAttack
{
    public S_Attack(FiniteStateMachine fsm) : base(fsm) { }
    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void OnStay()
    {
        if(fsm.AI.Character.GetComponent<T>().CanAttack)
            fsm.AI.Character.GetComponent<T>().Attack();
        else
            fsm.AI.Character.Idle();
    }
}
