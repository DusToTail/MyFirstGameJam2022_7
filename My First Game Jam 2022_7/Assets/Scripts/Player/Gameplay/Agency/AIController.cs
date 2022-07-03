using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public CharacterBehaviour Character { get { return _character; } }
    public CharacterBehaviour Target { get { return _detectionFOV.target; } }
    public CharacterBehaviour Victim { get { return _detectionAttack.target; } }
    private CharacterBehaviour _character;
    private BehaviourTreeRunner _treeRunner;
    private DetectCharacterInFOVBehaviour _detectionFOV;
    private DetectCharacterInAttackZoneBehaviour _detectionAttack;

    private void Awake()
    {
        _character = GetComponent<CharacterBehaviour>();
        _treeRunner = GetComponent<BehaviourTreeRunner>();
        _detectionFOV = GetComponentInChildren<DetectCharacterInFOVBehaviour>();
        _detectionAttack = GetComponentInChildren<DetectCharacterInAttackZoneBehaviour>();
    }

    private void Update()
    {
        _treeRunner.RunTree();
    }

}
