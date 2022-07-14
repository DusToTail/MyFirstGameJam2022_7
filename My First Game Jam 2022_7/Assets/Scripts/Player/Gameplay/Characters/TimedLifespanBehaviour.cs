using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLifespanBehaviour : MonoBehaviour
{
    [SerializeField] private float lifespan;
    private float _curTimeLeft;
    private Renderer _renderer;
    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }
    private void Start()
    {
        _curTimeLeft = lifespan;
        StartCoroutine(FadeInCoroutine());
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
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float i = 0;
        while (i < 1)
        {
            foreach (var mat in _renderer.materials)
            {
                mat.SetFloat("_Alpha", i);
                mat.SetFloat("_Speed", 1 / Mathf.Clamp(i, 0.1f, 1));
                mat.SetFloat("_Power", (1 / Mathf.Clamp(i, 0.1f, 1)) * 0.2f);
            }
            i += Time.deltaTime / 2f;
            yield return null;
        }
    }
    private IEnumerator FadeOutCoroutine()
    {
        float i = 1;
        while (i > 0)
        {
            foreach (var mat in _renderer.materials)
            {
                mat.SetFloat("_Alpha", i);
                mat.SetFloat("_Speed", 1 / Mathf.Clamp(i, 0.1f, 1));
                mat.SetFloat("_Power", (1 / Mathf.Clamp(i, 0.1f, 1)) * 0.2f);
            }
            i -= Time.deltaTime / 2f;
            yield return null;
        }
        Destroy(gameObject);
    }
}
