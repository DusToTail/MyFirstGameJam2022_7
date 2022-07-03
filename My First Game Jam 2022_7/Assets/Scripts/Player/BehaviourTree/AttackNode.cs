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
        if (tree.AI.Target == null)
        {
            Debug.Log("Node: Target is not detected", tree.AI);
            return State.Failure;
        }
        else
        {
            tree.AI.Character.followPosition = tree.AI.Target.transform.position;
            Debug.Log($"Node: Moving towards {tree.AI.Character.followPosition}", tree.AI);
            tree.AI.Character.Move();
            return State.Running;
        }
    }
}
