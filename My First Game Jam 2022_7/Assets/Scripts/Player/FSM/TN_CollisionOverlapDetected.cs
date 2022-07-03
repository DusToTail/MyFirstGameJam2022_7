using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TN_CollisionOverlapDetected : Transition
{
    public Collider collider;
    public LayerMask layerMask;
    public string tag;
    public bool checkTag;
    public TN_CollisionOverlapDetected(Collider collider, LayerMask mask, bool checkTag, string tag, FiniteStateMachine fsm, bool inverted = false) : base(TransitionType.NoExitTime, fsm, 0, inverted)
    {
        this.collider = collider;
        layerMask = mask;
        this.tag = tag;
        this.checkTag = checkTag;
    }
    public override bool CanTransition()
    {
        Collider[] colliders;
        if(collider is BoxCollider box)
        {
            colliders = Physics.OverlapBox(box.transform.position + box.center, box.size / 2, box.transform.rotation, layerMask);
        }
        else if(collider is SphereCollider sphere)
        {
            colliders = Physics.OverlapSphere(sphere.transform.position, sphere.radius, layerMask);
        }
        else
        {
            CapsuleCollider capsule = (CapsuleCollider)collider;
            Vector3 sphereOffset = Vector3.one;
            if (capsule.direction == 0)
                sphereOffset = Vector3.right;
            else if (capsule.direction == 1)
                sphereOffset = Vector3.up;
            else
                sphereOffset = Vector3.forward;
            Vector3 point1 = capsule.transform.position + capsule.center + sphereOffset * (capsule.height - capsule.radius) / 2;
            Vector3 point2 = capsule.transform.position + capsule.center - sphereOffset * (capsule.height - capsule.radius) / 2;

            colliders = Physics.OverlapCapsule(point1, point2, capsule.radius, layerMask);
        }

        if(checkTag)
        {
            foreach(var current in colliders)
            {
                if (current.CompareTag(tag)) { return inverted? false : true; }
            }
        }
        else
        {
            if (colliders.Length > 0)
                return inverted? false : true;
        }
        return inverted? true : false;
    }
}
