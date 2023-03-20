using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class PlayerData
{
    private static List<Pet> _Pets;
    private static List<Weapon> _Weapons;
    public static float Coins
    {
        get
        {
            return PlayerPrefs.GetFloat("Coins");
        }
        private set
        {
            if (value >= 0)
            {
                PlayerPrefs.SetFloat("Coins", value);
            }
        }
    }

    public static int Gems
    {
        get
        {
            return PlayerPrefs.GetInt("Gems");
        }
        private set
        {
            if (value >= 0)
            {
                PlayerPrefs.SetInt("Gems", value);
            }
        }
    }
    
    public static void RecieveCoins(int _coins)
    {
        if (_coins >= 0)
        {
            Coins += _coins;
        }
            
    }

    public static void RecieveGems(int _gems)
    {
        if (_gems >= 0)
        {
            Gems += _gems;
        }
            
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
    
    public static bool SpendGems(int _price)
    {
        if (Gems - _price >= 0)
        {
            Gems -= _price;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void AddPet(Pet _pet)
    {
        _Pets.Add(_pet);
    }

    public static void AddWeapon(Weapon _weapon)
    {
        _Weapons.Add(_weapon);
    }
}
