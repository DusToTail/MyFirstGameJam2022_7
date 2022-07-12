using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveRandom
{
    bool CanMoveRandom { get; }
    public void ReduceCurrentMoveRandomCooldown();
    public void ResetCurrentMoveRandomCooldown();

}
