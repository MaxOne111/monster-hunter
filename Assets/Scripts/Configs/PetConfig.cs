using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPetConfig", menuName = "Configs/PetConfig")]
public class PetConfig : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}
