using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
   [field: SerializeField] public int Level { get; private set; }
   [field: SerializeField] public float Damage { get; private set; }
   [field: SerializeField] public Sprite Icon{ get; private set; }
    
}
