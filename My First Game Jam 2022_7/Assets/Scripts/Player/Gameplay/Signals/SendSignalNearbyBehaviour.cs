using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendSignalNearbyBehaviour : MonoBehaviour
{
    public LayerMask affectLayers;
    public float maxRadius;
    public float interval;
    public bool isLoop;
    public bool playOnAwake;
    public GameObject VFX;

    private void Start()
    {
        if (playOnAwake)
            Pulse();
    }

    public void Pulse()=>StartCoroutine(PulseCoroutine());
    private IEnumerator PulseCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(interval);
        while(true)
        {
            if(VFX != null)
                Instantiate(VFX, transform.position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxRadius, affectLayers);
            foreach(var collider in colliders)
            {
                collider.gameObject.GetComponentInChildren<IReceiveSignal>()?.Execute(transform.position);
            }
            if (!isLoop) { break; }
            yield return wait;
        }
    }
}
