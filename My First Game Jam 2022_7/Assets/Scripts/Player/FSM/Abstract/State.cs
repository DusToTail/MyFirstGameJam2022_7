using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public List<Transition> transitions = new List<Transition>();
    public FiniteStateMachine fsm;
    protected State(FiniteStateMachine fsm)
    {
        this.fsm = fsm;
    }
    public virtual void Execute()
    {
        OnStay();
        CheckTransitions();
    }
    public void AddTransition(params Transition[] trans) { foreach(var t in trans) transitions.Add(t); }
    public void CheckTransitions()
    {
        for(int i = 0; i < transitions.Count; i++)
        {
            Transition t = (Transition)transitions[i];
            if(t.type == Transition.TransitionType.HasExitTime)
            {
                if(Time.time > t.exitAtTimeInSeconds)
                {
                    transitions[i].Transit();
                    break;
                }
            }
            else
            {
                if (transitions[i].CanTransition())
                {
                    transitions[i].Transit();
                    break;
                }
            }
        }
    }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnStay();
}
