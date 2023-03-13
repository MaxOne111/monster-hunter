using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerPets : MonoBehaviour
{
    [field: SerializeField] public List<Pet> Pets { get; private set; }
    private List<Pet> _Pets_On_Scene = new List<Pet>();
    [SerializeField] private Transform[] _Pet_Positions;
    [Inject] private DiContainer _Di_Container;
    
    public List<Pet> PetsOnScene{get=>_Pets_On_Scene;}

    private void Awake()
    {
        GameEvents._Start_Level += ResetPets;
        GameEvents._Start_Level += PetOnScene;
    }

    public void AddPet(Pet _pet)
    {
        if(!Pets.Contains(_pet))
            Pets.Add(_pet);
    }

    private void PetOnScene()
    {
        if (Pets.Count > 0)
        {
            for (int i = 0; i < Pets.Count; i++)
            {
                GameObject _pet = _Di_Container.InstantiatePrefab(Pets[i].gameObject, _Pet_Positions[i].position,
                    Quaternion.identity, null);
                _Pets_On_Scene.Add(_pet.GetComponent<Pet>());
            }
        }
    }

    private void ResetPets()
    {
        if (_Pets_On_Scene.Count > 0)
        {
            for (int i = 0; i < _Pets_On_Scene.Count; i++)
            {
                _Pets_On_Scene.Remove(_Pets_On_Scene[i]);
            }
        }
    }

    private void OnDestroy()
    {
        GameEvents._Start_Level -= PetOnScene;
        GameEvents._Start_Level -= PetOnScene;
    }
}
