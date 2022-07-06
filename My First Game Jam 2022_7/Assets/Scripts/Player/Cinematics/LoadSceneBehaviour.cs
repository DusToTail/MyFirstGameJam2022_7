using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneBehaviour : MonoBehaviour
{
    public int sceneIndex;
    public bool playOnAwake;

    private void Start()
    {
        if(playOnAwake)
            LoadScene();
    }

    public void LoadScene()
    {
        if (sceneIndex > SceneManager.sceneCount) { return; }
        SceneManager.LoadScene(sceneIndex);
    }

    public static void LoadScene(int sceneIndex)
    {
        if(sceneIndex > SceneManager.sceneCount) { return; }
        SceneManager.LoadScene(sceneIndex);
    }

}
