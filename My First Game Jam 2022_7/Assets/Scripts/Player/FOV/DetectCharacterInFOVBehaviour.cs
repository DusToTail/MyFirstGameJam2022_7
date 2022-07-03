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
