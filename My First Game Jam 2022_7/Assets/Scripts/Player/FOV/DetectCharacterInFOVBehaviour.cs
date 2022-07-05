using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DetectCharacterInFOVBehaviour : MonoBehaviour
{
    
    public enum DetectMode
    {
        Player,
        Enemy,
        EnemyOverPlayer,
        PlayerOverEnemy,
        None
    }
    public CharacterBehaviour target;
    public DetectMode detectMode;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    [SerializeField] private float resetTime;
    private SphereCollider _collider;
    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        _collider.radius = minRadius;
    }
    public void TriggerColliderRadius()
    {
        _collider.radius = maxRadius;
        StopAllCoroutines();
        StartCoroutine(ResetCoroutine());
    }
    private IEnumerator ResetCoroutine()
    {
        yield return new WaitForSeconds(resetTime);
        _collider.radius = minRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(detectMode == DetectMode.None) { return; }
        if(detectMode == DetectMode.Player)
        {
            if (other.CompareTag(Utilities.modelTag) && other.transform.parent.CompareTag(Utilities.playerTag))
            {
                target = other.transform.parent.GetComponent<CharacterBehaviour>();
            }
        }
        else if(detectMode == DetectMode.Enemy)
        {

        }
        else if(detectMode == DetectMode.EnemyOverPlayer)
        {

        }
        else if(detectMode == DetectMode.PlayerOverEnemy)
        {
            if (other.CompareTag(Utilities.modelTag) && other.transform.parent.CompareTag(Utilities.playerTag))
            {
                target = other.transform.parent.GetComponent<CharacterBehaviour>();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (detectMode == DetectMode.None) { return; }
        if (detectMode == DetectMode.Player)
        {
            if (other.CompareTag(Utilities.modelTag) && other.transform.parent.CompareTag(Utilities.playerTag))
            {
                target = null;
            }
        }
        else if (detectMode == DetectMode.Enemy)
        {

        }
        else if (detectMode == DetectMode.EnemyOverPlayer)
        {

        }
        else if (detectMode == DetectMode.PlayerOverEnemy)
        {
            if (other.CompareTag(Utilities.modelTag) && other.transform.parent.CompareTag(Utilities.playerTag))
            {
                target = null;
            }
        }
    }
}
