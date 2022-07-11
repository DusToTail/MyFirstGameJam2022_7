using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarkMouseBehaviour : CharacterBehaviour, IAttack
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
        UpdateNavMeshAgent(maxMovementSpeed);
        float actualNormalizedSpeed = _navMeshAgent.speed / maxMovementSpeed;
        if (actualNormalizedSpeed > 0.01f) { _animator.SetBool("IsMoving", true); _animator.SetFloat("Velocity", actualNormalizedSpeed); }
        else { _animator.SetBool("IsMoving", false); }
    }
    public override void Idle()
    {
        UpdateNavMeshAgent(0);
    }
    public override void UpdateNavMeshAgent(float movementSpeed)
    {
        base.UpdateNavMeshAgent(movementSpeed);
    }
    public void Attack()
    {
        _animator.SetTrigger("BasicAttack");
    }
    public override void TakeDamage(float positiveAmount) => StartCoroutine(TakeDamageCoroutine(positiveAmount));
    public override void Heal(float positiveAmount) => StartCoroutine(HealCoroutine(positiveAmount));
    public void BasicAttack()
    {
        Debug.Log($"{gameObject.name} attacks for {attackDamage} damage", this);
        // Trigger any animation / sound effect / event
        Vector3 center = attackCollider.transform.position + attackCollider.center;
        Collider[] colliders = Physics.OverlapBox(center, attackCollider.size / 2, attackCollider.transform.rotation, LayerMask.GetMask(Utilities.characterLayer));
        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag(Utilities.playerTag))
                collider.transform.GetComponent<CharacterBehaviour>()?.TakeDamage(attackDamage);
        }
        ResetCurrentAttackCooldown();
    }
    public void SpecialAttack() { }
    private IEnumerator TakeDamageCoroutine(float positiveAmount)
    {
        Debug.Log($"{gameObject.name} takes {positiveAmount} damage", this);
        // Trigger any animation / sound effect / event
        MinusHealth(positiveAmount);
        yield return null;
    }
    private IEnumerator HealCoroutine(float positiveAmount)
    {
        Debug.Log($"{gameObject.name} heals {positiveAmount}", this);
        // Trigger any animation / sound effect / event
        PlusHealth(positiveAmount);
        yield return null;
    }
    private void ReduceCurrentAttackCooldown()
    {
        if(_currentAttackCooldown < 0) { return; }
        _currentAttackCooldown -= Time.deltaTime;
    }
    private void ResetCurrentAttackCooldown()
    {
        _currentAttackCooldown = attackCooldown;
    }

}
