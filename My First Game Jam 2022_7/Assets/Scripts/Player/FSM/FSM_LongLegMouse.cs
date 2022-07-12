using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_LongLegMouse : FiniteStateMachine
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
        };

        // Initialize transititions
        

        // Bindings / Linkings
        // Linking from-to states in transition
        

        // Binding transitions to states
        
    }
}
