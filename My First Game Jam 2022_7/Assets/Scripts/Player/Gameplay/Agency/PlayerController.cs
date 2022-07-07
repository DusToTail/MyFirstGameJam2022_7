using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cursorMinRange;
    [SerializeField] private float cursorMaxRange;
    [SerializeField] private bool displayGizmos;
    private Vector3 _cursorGroundPosition;
    private Vector3 _cursorDirection;
    private float _cursorDistance;
    private CharacterBehaviour _character;
    private InteractionActorBehaviour _interactionActor;
    private NavMeshAgent _navMeshAgent;
    private Transform _model;

    private void Awake()
    {
        _character = GetComponent<CharacterBehaviour>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _interactionActor = GetComponentInChildren<InteractionActorBehaviour>();
        _model = transform.Find("Model");
    }
    private void Update()
    {
        _cursorGroundPosition = Utilities.GetCursorPositionOnLayers(Utilities.mousePlaneLayer);
        _cursorDirection = _cursorGroundPosition - transform.position;
        _cursorDirection -= new Vector3(0, _cursorDirection.y, 0);
        _cursorDistance = _cursorDirection.magnitude;
        _cursorDirection.Normalize();

        if(Input.GetMouseButtonDown(0))
        {
            _interactionActor?.InteractObject(_cursorGroundPosition);
        }

        float normalized = Mathf.Clamp((_cursorDistance - cursorMinRange) / (cursorMaxRange - cursorMinRange), 0, 1);
        Vector3 appliedGroundPosition = transform.position + _cursorDirection * cursorMaxRange * normalized;
        UpdateCharacterFollowPosition(appliedGroundPosition);
        UpdateCharacterSpeedMultiplier(normalized);
    }
    private void UpdateCharacterFollowPosition(Vector3 worldPosition) 
    {
        _character.followPosition = worldPosition;
    }
    private void UpdateCharacterSpeedMultiplier(float scale)
    {
        _character.speedMultiplier = scale;
    }

    private void OnDrawGizmos()
    {
        if (!displayGizmos) { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_cursorGroundPosition, 0.5f);
        Gizmos.DrawLine(transform.position + Vector3.up, _cursorGroundPosition + Vector3.up);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(cursorMaxRange, 0.1f, cursorMaxRange));
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + Vector3.up * 0.1f, new Vector3(cursorMinRange, 0.1f, cursorMinRange));
    }
}
