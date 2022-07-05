using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(SendSignalNearbyBehaviour))]
public class SendSignalNearbyBehaviourEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SendSignalNearbyBehaviour behaviour = (SendSignalNearbyBehaviour)target;
        if (GUILayout.Button("Pulse"))
        {
            behaviour.Pulse();
        }
    }

}
