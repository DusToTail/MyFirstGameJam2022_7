using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LongLegMouseBehaviour : CharacterBehaviour
{
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
    public void TriggerBasicAttack()
    {
        _animator.SetTrigger("BasicAttack");
    }
    public override void TakeDamage(float positiveAmount) => StartCoroutine(TakeDamageCoroutine(positiveAmount));
    public override void Heal(float positiveAmount) => StartCoroutine(HealCoroutine(positiveAmount));
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
}
