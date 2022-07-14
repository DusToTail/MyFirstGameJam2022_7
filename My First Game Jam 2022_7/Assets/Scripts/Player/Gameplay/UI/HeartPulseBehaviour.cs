using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulseBehaviour : MonoBehaviour
{
    [SerializeField] private bool runHeartBeatPulse;
    [SerializeField] private Transform moveTransform;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private float defaultHeightInPixels;
    [SerializeField] private float intervalsPerSecond;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private CharacterBehaviour characterBehaviour;
    [SerializeField] private AudioSource heartBeat;
    [SerializeField] private Renderer veins;
    [SerializeField] private Renderer vignette;
    [SerializeField] private float veinSpeed;
    private float t = 0;
    private float _healthMultiplier;

    private void OnEnable()
    {
        characterBehaviour.OnDamageTaken += TriggerVeins;
    }
    private void OnDisable()
    {
        characterBehaviour.OnDamageTaken -= TriggerVeins;
    }
    private void Start()
    {
        StartCoroutine(HeartBeatSoundCoroutine());
    }
    private void Update()
    {
        if(moveTransform == null) { return; }
        _healthMultiplier = 0;
        if (characterBehaviour != null) { _healthMultiplier = (2 - characterBehaviour.CurHealthPercentage); }
        if (!runHeartBeatPulse) { return; }
        if (t > 1) 
        { 
            t = 0;
            Vector3 resetPosition = Vector3.Lerp(from.position, to.position, 0);
            moveTransform.position = resetPosition;
            trail.Clear();
        }
        float yDifference = defaultHeightInPixels * (1 - curve.Evaluate(t)) * _healthMultiplier;

        Vector3 lerp = Vector3.Lerp(from.position, to.position, t);
        Vector3 final = lerp + moveTransform.up * yDifference;
        moveTransform.position = final;
        t += Time.deltaTime * intervalsPerSecond * _healthMultiplier;
    }

    private IEnumerator HeartBeatSoundCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.4f / (intervalsPerSecond * _healthMultiplier));
            heartBeat.Play();
        }
    }
    private void TriggerVeins()
    {
        StopCoroutine(DisplayVeinCoroutine());
        StopCoroutine(DisplayVignetteCoroutine());
        StopCoroutine(IncreaseHeartBeatVolumnCoroutine());
        StartCoroutine(DisplayVeinCoroutine());
        StartCoroutine(DisplayVignetteCoroutine());
        StartCoroutine(IncreaseHeartBeatVolumnCoroutine());
    }
    private IEnumerator IncreaseHeartBeatVolumnCoroutine()
    {
        while (heartBeat.volume < 0.3f)
        {
            yield return null;
            heartBeat.volume += Time.deltaTime * veinSpeed * 3 / 5;
        }
        yield return new WaitForSeconds(2f);
        while (heartBeat.volume > 0f)
        {
            yield return null;
            heartBeat.volume -= Time.deltaTime * veinSpeed * 3 / 5;
        }

    }
    private IEnumerator DisplayVeinCoroutine()
    {
        while(veins.material.GetFloat("_VignetteScale") < 1)
        {
            yield return null;
            veins.material.SetFloat("_VignetteScale", veins.material.GetFloat("_VignetteScale") + Time.deltaTime * veinSpeed);
        }
        yield return new WaitForSeconds(2f);
        while (veins.material.GetFloat("_VignetteScale") > 0.5f)
        {
            yield return null;
            veins.material.SetFloat("_VignetteScale", veins.material.GetFloat("_VignetteScale") - Time.deltaTime * veinSpeed);
        }

    }
    private IEnumerator DisplayVignetteCoroutine()
    {
        while (vignette.material.GetFloat("_Scale") < 0.7f)
        {
            yield return null;
            vignette.material.SetFloat("_Scale", vignette.material.GetFloat("_Scale") + Time.deltaTime * veinSpeed * 2 / 5);
        }
        yield return new WaitForSeconds(2f);
        while (vignette.material.GetFloat("_Scale") > 0.5f)
        {
            yield return null;
            vignette.material.SetFloat("_Scale", vignette.material.GetFloat("_Scale") - Time.deltaTime * veinSpeed * 2 / 5);
        }
    }
}
