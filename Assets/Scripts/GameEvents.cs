using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action _Put_Ammo;
    public static Action _Take_Ammo;
    public static Action _Monster_Death;

    public static void PutAmmo()
    {
        _Put_Ammo?.Invoke();
    }

    public static void TakeAmmo()
    {
        _Take_Ammo?.Invoke();
    }

    public static void MonsterDeath()
    {
        _Monster_Death?.Invoke();
    }
}
