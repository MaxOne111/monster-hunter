using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterPoolConfig", menuName = "Configs/MonsterPoolConfig")]
public class MonsterPoolConfig : ScriptableObject
{
    [field: SerializeField] public List<Monster> Monsters { get; private set; }
    [field: SerializeField] public BossMonster Boss { get; private set; }
    [field: SerializeField] public float HealthRatio { get; private set; }
}
