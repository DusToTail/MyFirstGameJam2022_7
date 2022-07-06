using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReader : MonoBehaviour
{
    public Dialogue[] dialogues;
    public TMPro.TMP_Text titleMesh;
    public TMPro.TMP_Text bodyMesh;
    public float charIntervalInSeconds;
    public float dialogueIntervalInSeconds;
    public bool playOnAwake;
    public bool autoPlay;
    private AudioSource _audioSource;
    private int _dialogueIndex;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _dialogueIndex = 0;
        if (playOnAwake) { ReadNext(); }
    }
    public void ReadNext()
    {
        if(dialogues.Length == 0) { return; }
        if(_dialogueIndex > dialogues.Length - 1) { return; }
        StopAllCoroutines();
        Clear();
        StartCoroutine(ReadCoroutine(_dialogueIndex));
        _dialogueIndex++;
    }
    private IEnumerator ReadCoroutine(int diaIndex)
    {
        var wait = new WaitForSeconds(charIntervalInSeconds);
        string title = dialogues[diaIndex].title;
        if(titleMesh!=null)
            titleMesh.text = title;
        char[] body = dialogues[diaIndex].body.ToCharArray();
        int bodyLength = body.Length;
        int charIndex = 0;
        while(charIndex < bodyLength)
        {
            if(bodyMesh!=null)
                bodyMesh.text += body[charIndex].ToString();
            _audioSource?.Play();
            charIndex++;
            yield return wait;
        }

        if(autoPlay)
        {
            wait = new WaitForSeconds(dialogueIntervalInSeconds);
            yield return wait;
            ReadNext();
        }
    }
    private void Clear()
    {
        if (titleMesh != null)
            titleMesh.text = "";
        if(bodyMesh != null)
            bodyMesh.text = "";
    }
}
