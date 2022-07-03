using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Rat : FiniteStateMachine
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
        states = new List<State>()
        {
            new S_Passive(this), // 0
            new S_Idle<RatBehaviour>(this), // 1
            new S_Aggressive(this), // 2
            new S_Chase<RatBehaviour>(this), // 3
            new S_Attack<RatBehaviour>(this), // 4
            new S_Idle<RatBehaviour>(this) // 5
        };

        // Initialize transititions
        var _01_True = new TN_True(this);
        var _10_True = new TN_True(this);
        var _02_IfTargetExists = new TN_TargetExists(this);
        var _20_IfTargetNotExists = new TN_TargetExists(this, true);
        var _23_IfVictimNotExists = new TN_VictimExists(this, true);
        var _24_IfVictimExistsAndCanAttack = new TN_CompositeTransition(this);
        var _25_IfVictimExistsButCanNotAttack = new TN_CompositeTransition(this);
        var _32_True = new TN_True(this);
        var _42_True = new TN_True(this);
        var _52_True = new TN_True(this);

        var _IfVictimExists = new TN_VictimExists(this);
        var _IfCanAttack = new TN_CanAttack<RatBehaviour>(this);
        var _IfCanNotAttack = new TN_CanAttack<RatBehaviour>(this, true);

        _24_IfVictimExistsAndCanAttack.AddCondition(_IfVictimExists);
        _24_IfVictimExistsAndCanAttack.AddCondition(_IfCanAttack);
        _25_IfVictimExistsButCanNotAttack.AddCondition(_IfVictimExists);
        _25_IfVictimExistsButCanNotAttack.AddCondition(_IfCanNotAttack);

        // Bindings / Linkings
        // Linking from-to states in transition
        _01_True.Link(states[0], states[1]);
        _10_True.Link(states[1], states[0]);
        _02_IfTargetExists.Link(states[0], states[2]);
        _20_IfTargetNotExists.Link(states[2], states[0]);
        _23_IfVictimNotExists.Link(states[2], states[3]);
        _24_IfVictimExistsAndCanAttack.Link(states[2], states[4]);
        _25_IfVictimExistsButCanNotAttack.Link(states[2], states[5]);
        _32_True.Link(states[3], states[2]);
        _42_True.Link(states[4], states[2]);
        _52_True.Link(states[5], states[2]);

        // Binding transitions to states
        states[0].AddTransition(_02_IfTargetExists);
        states[0].AddTransition(_01_True);

        states[1].AddTransition(_10_True);

        states[2].AddTransition(_20_IfTargetNotExists);
        states[2].AddTransition(_23_IfVictimNotExists);
        states[2].AddTransition(_24_IfVictimExistsAndCanAttack);
        states[2].AddTransition(_25_IfVictimExistsButCanNotAttack);
        states[3].AddTransition(_32_True);
        states[4].AddTransition(_42_True);
        states[5].AddTransition(_52_True);

        root = states[0];
        current = root;
    }
}
