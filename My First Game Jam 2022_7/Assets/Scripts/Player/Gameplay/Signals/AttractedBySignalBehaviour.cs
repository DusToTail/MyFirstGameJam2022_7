using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterBehaviour))]
public class AttractedBySignalBehaviour : MonoBehaviour, IReceiveSignal
{
    private CharacterBehaviour _character;

    private void Awake()
    {
        _character = GetComponent<CharacterBehaviour>();
    }

    public void Execute(Vector3 fromWorldPosition)
    {
        Debug.Log($"{gameObject.name} is attracted by something from {fromWorldPosition}", this);
        _character.GetComponentInChildren<DetectCharacterInFOVBehaviour>()?.TriggerColliderRadius();
    }
}
