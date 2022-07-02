using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private CharacterBehaviour _character;

    private void Awake()
    {
        _character = GetComponent<CharacterBehaviour>();
    }
    private void Update()
    {
        UpdateCharacterFollowPosition();
    }
    private void UpdateCharacterFollowPosition() 
    { 
        _character.followPosition = Utilities.GetCursorPositionOnLayers(Utilities.groundLayer); 
    }
}
