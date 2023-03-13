using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAllyConfig", menuName = "Configs/AllyConfig")]
public class AllyConfig : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float FireRate { get; private set; }
    [field: SerializeField] public float UpgradePrice { get; private set; }
    [field: SerializeField] public float PurchasePrice { get; private set; }
}
