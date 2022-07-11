using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLifespanBehaviour : MonoBehaviour
{
    [SerializeField] private float lifespan;
    private float _curTimeLeft;
    private void Start()
    {
        _curTimeLeft = lifespan;
    }
    private void Update()
    {
        if(_curTimeLeft > 0)
        {
            _curTimeLeft -= Time.deltaTime;
            if(_curTimeLeft <= 0) { TriggerDeath(); }
        }
    }
    private void TriggerDeath()
    {
        Destroy(gameObject);
    }
}
