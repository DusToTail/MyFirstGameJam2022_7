using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition
{
    public enum TransitionType
    {
        HasExitTime,
        NoExitTime
    }
    public State from;
    public State to;
    public TransitionType type;
    public FiniteStateMachine fsm;
    public float exitAtTimeInSeconds;
    public bool inverted;

    protected Transition(TransitionType type, FiniteStateMachine fsm, float fixedDurationInSeconds = 0, bool inverted = false)
    {
        this.type = type;
        this.fsm = fsm;
        exitAtTimeInSeconds = Time.time + fixedDurationInSeconds;
        this.inverted = inverted;
    }
    public void Transit()
    {
        from?.OnExit();
        to?.OnEnter();
        fsm.current = to;
    }
    public void Link(State from, State to)
    {
        this.from = from;
        this.to = to;
    }
    public abstract bool CanTransition();
}
