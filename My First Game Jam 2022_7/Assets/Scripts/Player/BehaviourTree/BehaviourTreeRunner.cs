using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    public BehaviourTree clone;

    private void Awake()
    {
        InitializeClone();
    }
    private void OnDestroy()
    {
        DestroyClone();
    }

    public void RunTree()
    {
        InitializeClone();
        clone.Update();
        DestroyClone();
    }

    private void InitializeClone()
    {
        clone = tree.Clone();
        clone.SetAI(GetComponent<AIController>());
    }

    private void DestroyClone()
    {
        Destroy(clone.rootNode);
        Destroy(clone);
    }

}
