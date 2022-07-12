using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FiniteStateMachine), true)]
public class FiniteStateMachineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FiniteStateMachine fsm = (FiniteStateMachine)target;
        if(fsm.current != null)
        {
            GUILayout.Label("Current State: " + fsm.current.ToString());
            //for(int i = 0; i < fsm.current.transitions.Count; i++)
            //{
            //    GUILayout.Label($"Transition #{i}: {fsm.current.transitions[i].ToString()} from {fsm.current.transitions[i].from.ToString()} to {fsm.current.transitions[i].to.ToString()}");
            //}
        }

    }
}
