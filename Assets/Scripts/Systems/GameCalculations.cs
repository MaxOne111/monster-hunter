using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameCalculations
{
    public static Quaternion BulletRotation(Transform _target, Transform _player)
    {
        Vector3 _bullet_direction = _target.transform.position - _player.transform.position;
        float _angle_Rotate = Mathf.Atan(_bullet_direction.y/_bullet_direction.x) * Mathf.Rad2Deg;
        Quaternion _start_Rotate = Quaternion.Euler(0,0,_angle_Rotate + 90);

        return _start_Rotate;
    }

    public static float PlayerTotalDamage(Weapon _weapon, PlayerShoot _player)
    {
        float _total_Damage = _weapon.Damage + _weapon.Damage * (_player.IncreaseDamage/100);
        return _total_Damage;
    }
    
}
