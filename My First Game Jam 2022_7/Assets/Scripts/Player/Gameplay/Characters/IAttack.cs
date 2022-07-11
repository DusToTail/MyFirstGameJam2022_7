using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    bool CanAttack { get; }
    public void Attack();
    public void BasicAttack();
    public void SpecialAttack();
}
