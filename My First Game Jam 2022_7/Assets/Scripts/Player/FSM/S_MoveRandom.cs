using UnityEngine;

public class S_MoveRandom<T> : SingleState where T : CharacterBehaviour, IMoveRandom
{
    public bool waypointReached;
    private float _range = 5;
    private Vector3 _waypoint;
    public S_MoveRandom(float range, FiniteStateMachine fsm) : base(fsm)
    {
        this._range = range;
        waypointReached = true;
        _waypoint = Vector3.zero;
    }
    public override void OnEnter()
    {
        if (!waypointReached) { return; }
        _waypoint = fsm.AI.Character.transform.position;
        Vector3 size = fsm.AI.Character.transform.GetComponent<Collider>().bounds.size;
        Vector3 center = _waypoint;
        LayerMask avoidMask = LayerMask.GetMask(Utilities.objectLayer, Utilities.characterLayer);
        _waypoint = Utilities.GetRandomPointOnGround(0, _range, center, size, avoidMask);
        if (_waypoint != Vector3.positiveInfinity)
        {
            waypointReached = false;
            Debug.DrawLine(_waypoint, _waypoint + Vector3.up * 100, Color.red, 2);
        }
    }

    public override void OnExit()
    {
        
    }

    public override void OnStay()
    {
        if (waypointReached) { return; }
        float distance = Vector3.Distance(_waypoint, fsm.AI.Character.transform.position);
        if(distance < 0.5) 
        { 
            waypointReached = true;
            fsm.AI.Character.GetComponent<T>().ResetCurrentMoveRandomCooldown();
        }
        if(_waypoint != Vector3.positiveInfinity)
        {
            //Debug.Log("Moving towards waypoint");
            fsm.AI.Character.followPosition = _waypoint;
            fsm.AI.Character.Move();
        }
    }

}
