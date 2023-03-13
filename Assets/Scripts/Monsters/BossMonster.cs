using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossMonster : Monster
{
    [SerializeField] private Pet _Pet;
    [SerializeField] [Range(0, 100)] private int _Drop_Chance;
    private PlayerPets _Player_Pets;
    [Inject] private DiContainer _Di_Container;


    [Inject]
    private void Construct(PlayerPets _pets)
    {
        _Player_Pets = _pets;
    }
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
        _Di_Container.InstantiatePrefab(_Pet.gameObject, transform.position, Quaternion.identity,null);
        _Player_Pets.AddPet(_Pet);
    }
}
