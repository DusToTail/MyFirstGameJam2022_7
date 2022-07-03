using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfTargetIsInFOVNode : CompositeNode
{
    public CharacterBehaviour target;
    protected override void OnStart()
    {
        target = tree.AI.Target;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(target == null)
        {
            Debug.Log("Node: Target is not detected", tree.AI);
            return children[0].Update();
        }
        else
        {
            Debug.Log($"Node: Target {target.gameObject.name} is detected", tree.AI);
            return children[1].Update();
        }
    }
}
