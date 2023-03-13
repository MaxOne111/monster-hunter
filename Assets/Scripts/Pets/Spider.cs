using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spider : Pet
{
    [SerializeField] private float _Bonus_Increase = 1.5f;

    protected override void StartDescription()
    {
        Description = $"Increase player damage by {_Bonus_Increase}";
    }

    protected override IEnumerator GiveBonus()
    {
        _Player_Shoot.IncreaseDamage += _Bonus_Increase;
        while (gameObject.activeSelf)
        {
            yield return null;
        }
    }

    private void OnDisable()
    {
        _Player_Shoot.IncreaseDamage -= _Bonus_Increase;
    }
}
