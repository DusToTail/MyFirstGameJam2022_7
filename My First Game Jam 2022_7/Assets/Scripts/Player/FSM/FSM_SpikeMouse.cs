using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_SpikeMouse : FiniteStateMachine
{
    private void Awake()
    {
        AI = GetComponent<AIController>();
    }
    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {

        // Initialize states
        var passive = new S_Passive(this);
        var passiveIdle = new S_Idle<SpikeMouseBehaviour>(this);
        var moveRandom = new S_MoveRandom<SpikeMouseBehaviour>(10, this);
        var aggressive = new S_Aggressive(this);
        var chase = new S_Chase<SpikeMouseBehaviour>(this);
        var basicAttack = new S_BasicAttack<SpikeMouseBehaviour>(this);
        var specialAttack = new S_SpecialAttack<SpikeMouseBehaviour>(this);
        var aggressiveIdle = new S_Idle<SpikeMouseBehaviour>(this);

        states = new List<State>()
        {
            passive,
            passiveIdle,
            moveRandom,
            aggressive,
            chase,
            basicAttack,
            specialAttack,
            aggressiveIdle
        };

        // Initialize transititions
        var idle_Passive = new TN_True(this);
        var moveRandom_Passive = new TN_True(this);
        var passive_Idle = new TN_True(this);
        var passive_MoveRandom = new TN_CanMoveRandom<SpikeMouseBehaviour>(this);
        var passive_Aggressive = new TN_TargetExists(this);
        var aggressive_Passive = new TN_TargetExists(this, true);
        var aggressive_Chase = new TN_AttackVictimExists(AI.DetectionBasicAttack, this, true);
        var aggressive_Idle = new TN_CompositeTransition(this);
        aggressive_Idle.AddCondition(
            new TN_AttackVictimExists(AI.DetectionSpecialAttack, this),
            new TN_CanAttack<SpikeMouseBehaviour>(this, true));
        var aggressive_SpecialAttack = new TN_CompositeTransition(this);
        aggressive_SpecialAttack.AddCondition(
            new TN_AttackVictimExists(AI.DetectionSpecialAttack, this),
            new TN_CanAttack<SpikeMouseBehaviour>(this));
        var aggressive_BasicAttack = new TN_CompositeTransition(this);
        aggressive_BasicAttack.AddCondition(
            new TN_AttackVictimExists(AI.DetectionBasicAttack, this),
            new TN_CanAttack<SpikeMouseBehaviour>(this));
        var chase_Aggressive = new TN_True(this);
        var idle_Aggressive = new TN_True(this);
        var basicAttack_Aggressive = new TN_True(this);
        var specialAttack_Aggressive = new TN_True(this);


        // Bindings / Linkings
        // Linking from-to states in transition
        idle_Passive.Link(passiveIdle, passive);
        moveRandom_Passive.Link(moveRandom, passive);
        passive_Idle.Link(passive, passiveIdle);
        passive_MoveRandom.Link(passive, moveRandom);

        passive_Aggressive.Link(passive, aggressive);
        aggressive_Passive.Link(aggressive, passive);
        aggressive_Chase.Link(aggressive, chase);
        aggressive_Idle.Link(aggressive, aggressiveIdle);
        aggressive_SpecialAttack.Link(aggressive, specialAttack);
        aggressive_BasicAttack.Link(aggressive, basicAttack);
        chase_Aggressive.Link(chase, aggressive);
        idle_Aggressive.Link(aggressiveIdle, aggressive);
        basicAttack_Aggressive.Link(basicAttack, aggressive);
        specialAttack_Aggressive.Link(specialAttack, aggressive);

        // Binding transitions to states
        passive.AddTransition(passive_Aggressive, passive_MoveRandom, passive_Idle);
        passiveIdle.AddTransition(idle_Passive);
        moveRandom.AddTransition(moveRandom_Passive);
        aggressive.AddTransition(aggressive_Passive, aggressive_SpecialAttack, aggressive_BasicAttack, aggressive_Idle, aggressive_Chase);
        chase.AddTransition(chase_Aggressive);
        basicAttack.AddTransition(basicAttack_Aggressive);
        specialAttack.AddTransition(specialAttack_Aggressive);
        aggressiveIdle.AddTransition(idle_Aggressive);


        root = passive;
        current = root;
    }
}
