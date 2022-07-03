using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabbitBehaviour : CharacterBehaviour
{
    [SerializeField] private bool canMove;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        InitializeNavMeshAgent();
        InitializeHealth();
    }
    private void Update()
    {
        if (!canMove) { return; }
        Move();
        if (Input.GetKeyUp(KeyCode.A)) { TakeDamage(maxHealth / 10); }
        if(Input.GetKeyUp(KeyCode.D)) { PlusHealth(maxHealth / 10); }
    }
    public override void Move()
    {
        float speedByHealth = maxMovementSpeed * Mathf.Clamp(_curHealth / maxHealth, 0.1f, 1f);
        UpdateNavMeshAgent(speedByHealth);
    }
    public override void TakeDamage(float positiveAmount) => StartCoroutine(TakeDamageCoroutine(positiveAmount));
    public override void Heal(float positiveAmount) => StartCoroutine(HealCoroutine(positiveAmount));
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