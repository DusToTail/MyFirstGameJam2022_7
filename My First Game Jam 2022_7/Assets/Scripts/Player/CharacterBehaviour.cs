using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(NavMeshAgent))]
public abstract class CharacterBehaviour : MonoBehaviour
{
    public Vector3 followPosition { get; set; }
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float maxMovementSpeed;
    [SerializeField] protected bool displayGizmos;
    protected float _curHealth;
    protected NavMeshAgent _navMeshAgent;
    public abstract void Move();
    public abstract void TakeDamage(float positiveAmount);
    public virtual void UpdateNavMeshAgent(float movementSpeed)
    {
        _navMeshAgent.speed = movementSpeed;
        _navMeshAgent.SetDestination(followPosition);
    }
    public virtual void InitializeNavMeshAgent(bool updatePosition = true, bool updateRotation = true, bool updateUpAxis = true)
    {
        _navMeshAgent.speed = maxMovementSpeed;
        _navMeshAgent.acceleration = maxMovementSpeed * maxMovementSpeed / 2;
        _navMeshAgent.angularSpeed = 720f;
        _navMeshAgent.stoppingDistance = 0.5f;
        _navMeshAgent.updatePosition = updatePosition;
        _navMeshAgent.updateRotation = updateRotation;
        _navMeshAgent.updateUpAxis = updateUpAxis;
    }
    public virtual void InitializeHealth() { _curHealth = maxHealth; }
    public void MinusHealth(float positiveAmount)
    {
        if(positiveAmount < 0) { return; }
        _curHealth -= positiveAmount;
        if(_curHealth < 0) { _curHealth = 0; }
    }
    public void PlusHealth(float positiveAmount)
    {
        if (positiveAmount < 0) { return; }
        _curHealth += positiveAmount;
        if (_curHealth > maxHealth) { _curHealth = maxHealth; }
    }
    private void OnDrawGizmos()
    {
        if (!displayGizmos) { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(followPosition, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
