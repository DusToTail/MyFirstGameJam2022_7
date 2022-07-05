using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationEventsContainer : MonoBehaviour
{
    public GameObject walkEffect;

    public void SpawnWalkEffect()
    {
        var effect = Instantiate(walkEffect, transform.position, Quaternion.identity);
        var particleSystem = effect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = particleSystem.main;
        main.startSize = GetComponentInParent<CharacterBehaviour>().speedMultiplier * main.startSize.constant;
        effect.GetComponent<SendSignalNearbyBehaviour>().maxRadius = main.startSize.constant;
        particleSystem.Play();
        effect.GetComponent<SendSignalNearbyBehaviour>().Pulse();
    }
}
