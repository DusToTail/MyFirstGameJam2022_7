using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        tree.AI.Character.followPosition = tree.AI.Character.transform.position;
        //Debug.Log($"Node: Idling", tree.AI);
        tree.AI.Character.UpdateNavMeshAgent(0);
        return State.Running;
    }
}
