using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private CharacterBehaviour _character;
    private BehaviourTreeRunner _treeRunner;

    private void Awake()
    {
        _character = GetComponent<CharacterBehaviour>();
        _treeRunner = GetComponent<BehaviourTreeRunner>();
    }

    private void Update()
    {
        _treeRunner.RunTree();
    }
}
