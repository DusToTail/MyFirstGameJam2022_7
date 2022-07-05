using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfBehaviour : CharacterBehaviour, IAttack
{
    public bool CanAttack { get { return _currentAttackCooldown <= 0; } }

    [SerializeField] private BoxCollider attackCollider;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;

    private float _currentAttackCooldown;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        InitializeNavMeshAgent();
        InitializeHealth();
    }
    private void Update()
    {
        ReduceCurrentAttackCooldown();
    }

    public override void Move()
    {
        float speedByHealth = maxMovementSpeed * Mathf.Clamp(_curHealth / maxHealth, 0.1f, 1f);
        UpdateNavMeshAgent(speedByHealth);
    }
    public override void Idle()
    {
        UpdateNavMeshAgent(0);
    }
    public override void UpdateNavMeshAgent(float movementSpeed)
    {
        base.UpdateNavMeshAgent(movementSpeed);
        _animator.SetFloat("MovementSpeed", _navMeshAgent.speed);
        _animator.SetFloat("AnimationSpeed", speedMultiplier);
    }
    public void Attack()
    {
        Debug.Log($"{gameObject.name} attacks for {attackDamage} damage", this);
        // Trigger any animation / sound effect / event

        Vector3 center = attackCollider.transform.position + attackCollider.center;
        Collider[] colliders = Physics.OverlapBox(center, attackCollider.size / 2, attackCollider.transform.rotation, LayerMask.GetMask(Utilities.characterLayer));
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(Utilities.playerTag))
                collider.transform.GetComponent<CharacterBehaviour>()?.TakeDamage(attackDamage);
        }
        ResetCurrentAttackCooldown();
    }
    public override void TakeDamage(float positiveAmount)
    {
        Debug.Log($"{gameObject.name} takes {positiveAmount} damage", this);
        // Trigger any animation / sound effect / event
        MinusHealth(positiveAmount);
    }
    public override void Heal(float positiveAmount)
    {
        Debug.Log($"{gameObject.name} heals {positiveAmount}", this);
        // Trigger any animation / sound effect / event
        PlusHealth(positiveAmount);
    }
    private void ReduceCurrentAttackCooldown()
    {
        if (_currentAttackCooldown < 0) { return; }
        _currentAttackCooldown -= Time.deltaTime;
    }
    private void ResetCurrentAttackCooldown()
    {
        _currentAttackCooldown = attackCooldown;
    }
}
