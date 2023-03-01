using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] private GameObject _Pet;
    [SerializeField] [Range(0, 100)] private int _Drop_Chance;

    private void PetDropChance()
    {
        int _drop = Random.Range(0, 101);
        if (_drop < _Drop_Chance)
        {
            CreatePet();
        }
    }

    protected override void Reward()
    {
        base.Reward();
        PetDropChance();
    }

    private void CreatePet()
    {
        Instantiate(_Pet, transform.position, transform.rotation);
    }
}
