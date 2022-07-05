using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPulseBehaviour : MonoBehaviour
{
    [SerializeField] private Transform moveTransform;
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;
    [SerializeField] private float defaultHeightInPixels;
    [SerializeField] private float intervalsPerSecond;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private CharacterBehaviour characterBehaviour;
    private float t = 0;
    private void Update()
    {
        if(moveTransform == null) { return; }
        float healthMultiplier = 0;
        if (characterBehaviour != null) { healthMultiplier = (2 - characterBehaviour.CurHealthPercentage); }
        if (t > 1) 
        { 
            t = 0;
            Vector3 resetPosition = Vector3.Lerp(from.position, to.position, 0);
            moveTransform.position = resetPosition;
            trail.Clear();
        }
        float yDifference = defaultHeightInPixels * (1 - curve.Evaluate(t)) * healthMultiplier;

        Vector3 lerp = Vector3.Lerp(from.position, to.position, t);
        Vector3 final = lerp + moveTransform.up * yDifference;
        moveTransform.position = final;
        t += Time.deltaTime * intervalsPerSecond * healthMultiplier;
    }
}
