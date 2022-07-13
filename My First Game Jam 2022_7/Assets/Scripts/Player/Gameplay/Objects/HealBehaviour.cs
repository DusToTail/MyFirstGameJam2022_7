using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehaviour : InteractionObjectBehaviour
{
    [SerializeField] private float healAmount;
    [SerializeField] private int sanityCost;
    [SerializeField] private bool isTriggered;
    [SerializeField] private Renderer model;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private Light pointLight;
    //[SerializeField] private GameObject popup;

    private void Start()
    {
        //popup?.SetActive(false);
    }
    public override void OnDistant()
    {
        if (isTriggered) { return; }
        foreach(var mat in model.materials)
        {
            mat.DisableKeyword("_EMISSION");
        }
        //popup?.SetActive(false);
    }

    public override void OnInteracted(InteractionActorBehaviour byActor)
    {
        if (isTriggered) { return; }
            isTriggered = true;
        //Debug.Log($"{transform.parent.gameObject.name} is interacted", transform.parent);
        byActor.gameObject.GetComponentInParent<CharacterBehaviour>()?.Heal(healAmount);
        byActor.gameObject.transform.parent.GetComponentInChildren<SanityController>()?.MinusSanity(sanityCost);
        foreach (var mat in model.materials)
        {
            mat.DisableKeyword("_EMISSION");
        }
        particles.Stop();
        pointLight.enabled = false;

        //popup?.SetActive(false);
    }

    public override void OnNearby()
    {
        if (isTriggered) { return; }
        foreach (var mat in model.materials)
        {
            mat.EnableKeyword("_EMISSION");
        }
        //popup?.SetActive(true);
    }
}
