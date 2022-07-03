using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        //Debug.Log($"Node: Attacking ", tree.AI);
        tree.AI.GetComponent<IAttack>().Attack();
        return State.Running;
    }
}
