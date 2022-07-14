using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class SanityController : MonoBehaviour
{
    public enum Level
    {
        Low,
        High,
        Max,
        Medium
    }
    public int maxSanity;
    public int highThreshold;
    public int mediumThreshold;
    public int lowThreshold;
    public int curSanity { get; private set; }
    public Level level;
    public Volume volume;
    private ChromaticAberration _chromaticAberration;

    private void Start()
    {
        curSanity = maxSanity;
        level = Level.Max;
        volume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
        _chromaticAberration.intensity.max = maxSanity;
        _chromaticAberration.intensity.min = 0;
    }


    public void MinusSanity(int amount)
    {
        if(amount < 0) { return; }
        curSanity -= amount;
        if (curSanity < 0) { curSanity = 0; }
        StopAllCoroutines();
        StartCoroutine(TriggerViewDistortionCoroutine(amount));

        if (curSanity == maxSanity) 
        {
            level = Level.Max; return; 
        }
        if(curSanity >= highThreshold) 
        {
            level = Level.High; return; 
        }
        if (curSanity >= mediumThreshold) 
        {
            level = Level.Medium; return; 
        }
        if (curSanity >= lowThreshold) 
        {
            level = Level.Low; return; 
        }

    }

    public void PlusSanity(int amount)
    {
        if (amount < 0) { return; }
        curSanity += amount;
        if (curSanity > maxSanity) { curSanity = maxSanity; }
        if (curSanity == maxSanity) { level = Level.Max; return; }
        if (curSanity >= highThreshold) { level = Level.High; return; }
        if (curSanity >= mediumThreshold) { level = Level.Medium; return; }
        if (curSanity >= lowThreshold) { level = Level.Low; return; }
    }

    private IEnumerator TriggerViewDistortionCoroutine(int amount)
    {
        float i = 0;
        while(i < (float)amount)
        {
            _chromaticAberration.intensity.value += Time.deltaTime;
            i += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        while(_chromaticAberration.intensity.value > 0)
        {
            _chromaticAberration.intensity.value -= Time.deltaTime;
            yield return null;
        }
    }
}
