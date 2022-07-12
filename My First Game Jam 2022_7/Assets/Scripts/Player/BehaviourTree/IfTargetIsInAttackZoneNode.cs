using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTargetIsInAttackZoneNode : CompositeNode
{
    public CharacterBehaviour target;
    protected override void OnStart()
    {
        target = tree.AI.BasicAttackVictim;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (target == null)
        {
            //Debug.Log("Node: Target is not in attack zone", tree.AI);
            return children[0].Update();
        }
        else
        {
            //Debug.Log($"Node: Target {target.gameObject.name} is in attack zone", tree.AI);
            return children[1].Update();
        }
    }
}
