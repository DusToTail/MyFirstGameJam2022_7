using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public CharacterBehaviour Character { get { return _character; } }
    public CharacterBehaviour Target { get { return _detectionFOV.target; } }
    public CharacterBehaviour BasicAttackVictim { get { return DetectionBasicAttack?.target; } }
    public CharacterBehaviour SpecialAttackVictim { get { return DetectionSpecialAttack?.target; } }
    
    public DetectCharacterInAttackZoneBehaviour DetectionBasicAttack { get;private set; }
    public DetectCharacterInAttackZoneBehaviour DetectionSpecialAttack { get; private set; }

    private CharacterBehaviour _character;
    private FiniteStateMachine _machine;
    private DetectCharacterInFOVBehaviour _detectionFOV;


    private void Awake()
    {
        _character = GetComponent<CharacterBehaviour>();
        _machine = GetComponent<FiniteStateMachine>();
        _detectionFOV = GetComponentInChildren<DetectCharacterInFOVBehaviour>();
        DetectionBasicAttack = transform.Find("BasicAttackDetection")?.GetComponent<DetectCharacterInAttackZoneBehaviour>();
        DetectionSpecialAttack = transform.Find("SpecialAttackDetection")?.GetComponent<DetectCharacterInAttackZoneBehaviour>();
    }

    private void Update()
    {
        _machine.Run();
    }

}
