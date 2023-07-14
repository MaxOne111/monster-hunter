using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class Pet : MonoBehaviour
{   public string Name { get; private set; }
    public int Level { get; private set; }
    public float Damage { get; private set; }
    public Sprite Icon { get; private set; }
    public string Description { get; protected set; }

    [SerializeField] private PetConfig _Config;
    protected PlayerShoot _Player_Shoot;
    
    [Inject]
    private void Construct(PlayerShoot _shoot)
    {
        _Player_Shoot = _shoot;
    }
    private void Awake()
    {
        GameEvents._Start_Level += DestroyPet;
    }

    private void Start()
    {
        StartStats();
        StartDescription();
    }

    private void OnEnable()
    {
        StartCoroutine(GiveBonus());
    }

    private void StartStats()
    {
        Name = _Config.Name;
        Level = _Config.Level;
        Damage = _Config.Damage;
        Icon = _Config.Icon;
    }

    protected abstract void StartDescription();

    protected abstract IEnumerator GiveBonus();

    public void Buy()
    {
       
    }

    public int UpgradePrice { get; set; }

    public void Upgrade()
    {
        
    }
    
    private void DestroyPet()
    {
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        GameEvents._Start_Level -= DestroyPet;
    }
}
