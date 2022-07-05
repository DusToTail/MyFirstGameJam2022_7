using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehaviour : InteractionObjectBehaviour
{
    [SerializeField] private float healAmount;
    [SerializeField] private bool isTriggered;
    [SerializeField] private GameObject popup;

    private void Start()
    {
        popup?.SetActive(false);
    }
    public override void OnDistant()
    {
        if (isTriggered) { return; }
        popup?.SetActive(false);
    }

    public override void OnInteracted(InteractionActorBehaviour byActor)
    {
        if (isTriggered) { return; }
            isTriggered = true;
        //Debug.Log($"{transform.parent.gameObject.name} is interacted", transform.parent);
        byActor.gameObject.GetComponentInParent<CharacterBehaviour>()?.Heal(healAmount);
        popup?.SetActive(false);
    }

    public override void OnNearby()
    {
        if (isTriggered) { return; }
        popup?.SetActive(true);
    }
}
