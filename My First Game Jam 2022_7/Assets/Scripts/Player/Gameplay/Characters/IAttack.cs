using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    bool CanAttack { get; }
    public void TriggerBasicAttack();
    public void TriggerSpecialAttack();
    public void BasicAttackEvent();
    public void SpecialAttackEvent();
}
