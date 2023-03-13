using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttackable : IAttackable
{
    public float ResetTime { get; set; }
    public int StartComboAttack { get; set; }
    public void Combo();
}
