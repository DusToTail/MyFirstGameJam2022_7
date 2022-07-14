using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationEventsContainer : MonoBehaviour
{
    public GameObject shockwaveEffect;
    public AudioSource basicAttackSE;
    public AudioSource specialAttackSE;

    public void SpawnShockWaveEffect()
    {
        float speedMultiplier = GetComponentInParent<CharacterBehaviour>().speedMultiplier;
        if(speedMultiplier < 0.5f) { return; }
        var effect = Instantiate(shockwaveEffect, transform.position, Quaternion.identity);
        var particleSystem = effect.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = particleSystem.main;
        main.startSize = speedMultiplier * main.startSize.constant;
        effect.GetComponent<SendSignalNearbyBehaviour>().maxRadius = main.startSize.constant;
        particleSystem.Play();
        effect.GetComponent<SendSignalNearbyBehaviour>().Pulse();
        effect.GetComponent<AudioSource>().volume = 0.1f * speedMultiplier;
        effect.GetComponent<AudioSource>().Play();
    }
    public void BasicAttack()
    {
        transform.GetComponentInParent<IAttack>()?.BasicAttackEvent();
        basicAttackSE.Play();
    }
    public void SpecialAttack()
    {
        transform.GetComponentInParent<IAttack>()?.SpecialAttackEvent();
        specialAttackSE.Play();
    }
}
