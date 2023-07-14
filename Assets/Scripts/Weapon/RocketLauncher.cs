using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRocketLauncher", menuName = "Weapon/RocketLauncher")]
public class RocketLauncher : Weapon, IUpgradeable
{
    [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }
    [field: SerializeField] public GameObject ExplosionEffect { get; private set; }
    [field: SerializeField] public int UpgradePrice { get; set; }
    
    public void Upgrade()
    {
        if (PlayerData.SpendCoins(UpgradePrice) && WeaponLevels.Count > 1)
        {
            WeaponLevels.Remove(WeaponLevels[0]);
            UpgradePrice *= 2;
        }
    }
    
    public override IEnumerator WeaponAttack(GameObject _target, PlayerShoot _player)
    {
        Quaternion _bullet_Rotation = GameCalculations.BulletRotation(_target.transform, _player.transform);
        float _total_Damage = GameCalculations.PlayerTotalDamage(this,_player);

        GameObject _bullet = Instantiate(BulletPrefab, _player.transform.position, _bullet_Rotation);
        while (_target && _bullet.transform.position != _target.transform.position)
        {
            _bullet.transform.position = Vector3.MoveTowards(_bullet.transform.position, _target.transform.position,
                BulletSpeed * Time.deltaTime);
            yield return null;
        }

        if (_target)
        {
            _target.GetComponent<IDamageable>().ApplyDamage(_total_Damage);
        }
        Instantiate(ExplosionEffect, _bullet.transform.position, Quaternion.identity);
        Destroy(_bullet);
    }

    public override void WeaponUIAccept(IWeaponUIVisitor _visitor)
    {
        _visitor.ShowWeaponData(this, this);
        _visitor.UpgradeableWeapon = this;
    }
}
