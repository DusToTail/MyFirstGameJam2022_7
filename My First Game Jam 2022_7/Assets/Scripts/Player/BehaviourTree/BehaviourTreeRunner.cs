using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    public BehaviourTree tree;
    private BehaviourTree _clone;

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
        Debug.Log($"Start run tree");
        _clone.Update();
    }

    private void InitializeClone()
    {
        _clone = tree.Clone();
        _clone.SetAI(GetComponent<AIController>());
    }

    private void DestroyClone()
    {
        Destroy(_clone.rootNode);
        Destroy(_clone);
    }

}
