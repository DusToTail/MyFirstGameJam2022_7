using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatBehaviour : CharacterBehaviour
{
    [SerializeField] private BoxCollider attackCollider;

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
        float speedByHealth = maxMovementSpeed * Mathf.Clamp(_curHealth / maxHealth, 0.1f, 1f);
        UpdateNavMeshAgent(speedByHealth);
    }
    public override void UpdateNavMeshAgent(float movementSpeed)
    {
        base.UpdateNavMeshAgent(movementSpeed);
        _animator.SetFloat("MovementSpeed", _navMeshAgent.speed);
        _animator.SetFloat("AnimationSpeed", speedMultiplier);
    }
    public override void Attack(float positiveAmount) => StartCoroutine(AttackCoroutine(positiveAmount));
    public override void TakeDamage(float positiveAmount) => StartCoroutine(TakeDamageCoroutine(positiveAmount));
    public override void Heal(float positiveAmount) => StartCoroutine(HealCoroutine(positiveAmount));
    private IEnumerator AttackCoroutine(float positiveAmount)
    {
        // Trigger any animation / sound effect / event

        Vector3 center = attackCollider.transform.position + attackCollider.center;
        Collider[] colliders = Physics.OverlapBox(center, attackCollider.size / 2, attackCollider.transform.rotation, LayerMask.NameToLayer(Utilities.characterLayer));
        foreach(Collider collider in colliders)
        {
            if (collider.CompareTag(Utilities.playerTag))
                collider.transform.GetComponent<CharacterBehaviour>().TakeDamage(positiveAmount);
        }
        yield return null;
    }
    private IEnumerator TakeDamageCoroutine(float positiveAmount)
    {
        // Trigger any animation / sound effect / event
        MinusHealth(positiveAmount);
        yield return null;
    }
    private IEnumerator HealCoroutine(float positiveAmount)
    {
        // Trigger any animation / sound effect / event
        PlusHealth(positiveAmount);
        yield return null;
    }
}
