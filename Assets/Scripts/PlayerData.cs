using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class PlayerData
{
    public static int Coins { get;  set; }
    public static int Gems { get; private set; }
    private static List<GameObject> _Pets;
    private static List<Weapon> _Weapons;
    
    public static void RecieveCoins(int _coins)
    {
        if(_coins >= 0)
            Coins += _coins;
    }

    public static void RecieveGems(int _gems)
    {
        if(_gems >= 0)
            Gems += _gems;
    }

    public static bool SpendCoins(int _price)
    {
        if (Coins - _price >= 0)
        {
            Coins -= _price;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static void SpendGems(int _price)
    {
        if (Gems - _price >= 0)
        {
            Gems -= _price;
        }
    }

    public static void AddPet(GameObject _pet)
    {
        _Pets.Add(_pet);
    }

    public static void AddWeapon(Weapon _weapon)
    {
        _Weapons.Add(_weapon);
    }
}
