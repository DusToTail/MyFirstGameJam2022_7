using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(NavMeshAgent))]
public abstract class CharacterBehaviour : MonoBehaviour
{
    public Vector3 followPosition;
    public float speedMultiplier = 1;
    public float CurHealthPercentage { get { return _curHealth / maxHealth; } }
    public delegate void DamageTaken();
    public event DamageTaken OnDamageTaken;
    public delegate void DamageHealed();
    public event DamageHealed OnDamageHealed;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float maxMovementSpeed;
    [SerializeField] protected bool displayGizmos;
    [SerializeField] protected float _curHealth;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;

    private void OnEnable()
    {
        GoalReachedBehaviour.OnGoalReached += StopBehaviour;
    }
    private void OnDisable()
    {
        GoalReachedBehaviour.OnGoalReached -= StopBehaviour;
    }
    private void Start()
    {
        if (!_navMeshAgent.isOnNavMesh)
            Destroy(this.gameObject);
    }
    public abstract void Idle();
    public abstract void Move();
    public abstract void TakeDamage(float positiveAmount);
    public abstract void Heal(float positiveAmount);
    public virtual void UpdateNavMeshAgent(float movementSpeed)
    {
        if (!_navMeshAgent.isOnNavMesh)
            Destroy(this.gameObject);
        _navMeshAgent.speed = movementSpeed * speedMultiplier;
        _navMeshAgent.SetDestination(followPosition);
    }
    public virtual void InitializeNavMeshAgent(bool updatePosition = true, bool updateRotation = true, bool updateUpAxis = true)
    {
        _navMeshAgent.speed = maxMovementSpeed;
        _navMeshAgent.acceleration = maxMovementSpeed * maxMovementSpeed / 3;
        _navMeshAgent.angularSpeed = 720f;
        _navMeshAgent.updatePosition = updatePosition;
        _navMeshAgent.updateRotation = updateRotation;
        _navMeshAgent.updateUpAxis = updateUpAxis;
    }
    public virtual void InitializeHealth(float normalized = 1) { _curHealth = maxHealth * normalized; }
    public void MinusHealth(float positiveAmount)
    {
        if(positiveAmount < 0) { return; }
        _curHealth -= positiveAmount;
        if(OnDamageTaken != null) { OnDamageTaken(); }
        if(_curHealth < 0) { _curHealth = 0; }
    }
    public void PlusHealth(float positiveAmount)
    {
        if (positiveAmount < 0) { return; }
        _curHealth += positiveAmount;
        if (OnDamageHealed != null) { OnDamageHealed(); }
        if (_curHealth > maxHealth) { _curHealth = maxHealth; }
    }
    private void StopBehaviour()
    {
        _navMeshAgent.speed = 0;
        _navMeshAgent.isStopped = true;
        if(GetComponent<PlayerController>() != null)
            GetComponent<PlayerController>().enabled = false;
        if (GetComponent<AIController>() != null)
            GetComponent<AIController>().enabled = false;

    }
    private void OnDrawGizmos()
    {
        if (!displayGizmos) { return; }
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(followPosition, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
