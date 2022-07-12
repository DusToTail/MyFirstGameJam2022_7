using UnityEngine;

public class S_MoveRandom<T> : SingleState where T : CharacterBehaviour, IMoveRandom
{
    public float range = 5;
    public bool waypointReached;
    private Vector3 waypoint;
    public S_MoveRandom(float range, FiniteStateMachine fsm) : base(fsm)
    {
        this.range = range;
        waypointReached = true;
        waypoint = Vector3.zero;
    }
    public override void OnEnter()
    {
        if (!waypointReached) { return; }
        waypoint = fsm.AI.Character.transform.position;
        Vector3 size = fsm.AI.Character.transform.GetComponent<Collider>().bounds.size;
        Vector3 center = waypoint;
        LayerMask avoidMask = LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer);
        waypoint = Utilities.GetRandomPointOnGround(0, range, center, size, avoidMask);
        if (waypoint != Vector3.positiveInfinity)
        {
            waypointReached = false;
            Debug.DrawLine(waypoint, waypoint + Vector3.up * 100, Color.red, 2);
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnStay()
    {
        if (waypointReached) { return; }
        float distance = Vector3.Distance(waypoint, fsm.AI.Character.transform.position);
        if(distance < 0.5) 
        { 
            waypointReached = true;
            fsm.AI.Character.GetComponent<T>().ResetCurrentMoveRandomCooldown();
        }
        if(waypoint != Vector3.positiveInfinity)
        {
            //Debug.Log("Moving towards waypoint");
            fsm.AI.Character.followPosition = waypoint;
            fsm.AI.Character.Move();
        }
    }

}
