using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiniteStateMachine : MonoBehaviour
{
    public List<State> states;
    public State current;
    public State root;
    public AIController AI;

    public Dictionary<string, object> parameters = new Dictionary<string, object>();

    public void Run()
    {
        current?.Execute();
    }
    public void Exit()
    {
        current = null;
    }

    public void AddParameter(string key, object value)
    {
        parameters.TryAdd(key, value);
    }
    public object GetParameter(string key)
    {
        if(parameters.TryGetValue(key, out object value))
            return value;
        return null;
    }
}
