using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        curSanity = maxSanity;
        level = Level.Max;
    }

    public void MinusSanity(int amount)
    {
        if(amount < 0) { return; }
        curSanity -= amount;
        if(curSanity < 0) { curSanity = 0; }
        if(curSanity == maxSanity) { level = Level.Max; return; }
        if(curSanity >= highThreshold) { level = Level.High; return; }
        if (curSanity >= mediumThreshold) { level = Level.Medium; return; }
        if (curSanity >= lowThreshold) { level = Level.Low; return; }


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
}
