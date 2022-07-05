using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitBehaviour : CharacterBehaviour
{
    public GameObject bloodVFX;
    public GameObject healVFX;

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
        Move();
        if (Input.GetKeyUp(KeyCode.A)) { TakeDamage(maxHealth / 10); }
        if(Input.GetKeyUp(KeyCode.D)) { PlusHealth(maxHealth / 10); }
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
    public override void TakeDamage(float positiveAmount) => StartCoroutine(TakeDamageCoroutine(positiveAmount));
    public override void Heal(float positiveAmount) => StartCoroutine(HealCoroutine(positiveAmount));
    private IEnumerator TakeDamageCoroutine(float positiveAmount)
    {
        Debug.Log($"{gameObject.name} takes {positiveAmount} damage", this);
        // Trigger any animation / sound effect / event
        MinusHealth(positiveAmount);
        Instantiate(bloodVFX, transform.position, Quaternion.identity);
        yield return null;
    }
    private IEnumerator HealCoroutine(float positiveAmount)
    {
        Debug.Log($"{gameObject.name} heals {positiveAmount}", this);
        // Trigger any animation / sound effect / event
        PlusHealth(positiveAmount);
        Instantiate(healVFX, transform.position, Quaternion.identity);
        yield return null;
    }
}
