using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAllyConfig", menuName = "Configs/AllyConfig")]
public class AllyConfig : ScriptableObject
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Sprite Icon { get; set; }
    
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float FireRate { get; set; }
    [field: SerializeField] public int UpgradePrice { get; set; }
    [field: SerializeField] public int PurchasePrice { get; set; }
}
