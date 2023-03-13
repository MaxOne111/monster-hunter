using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AmmoOnScene : MonoBehaviour
{
    [field: SerializeField] public List<GameObject> AmmoList { get; private set; }

    private void Awake()
    {
        GameEvents._Start_Level += ResetAmmo;
    }

    public void AddAmmo(GameObject _ammo)
    {
        AmmoList.Add(_ammo);
    }

    public void RemoveAmmo(GameObject _ammo)
    {
        AmmoList.Remove(_ammo);
    }

    public void NearestAmmo(GameObject _player)
    {
        if (AmmoList.Count > 1)
        {
            GameObject _temp;
            for (int i = 0; i < AmmoList.Count; i++)
            {
                float _dist_1 = (AmmoList[i].transform.position - _player.transform.position).magnitude;
                for (int j = i+1; j < AmmoList.Count; j++)
                {
                    float _dist_2 = (AmmoList[j].transform.position - _player.transform.position).magnitude;
                    if (_dist_2 < _dist_1)
                    {
                        _temp = AmmoList[i];
                        AmmoList[i] = AmmoList[j];
                        AmmoList[j] = _temp;
                    }
                }
            }
        }
    }

    private void ResetAmmo()
    {
        for (int i = 0; i < AmmoList.Count; i++)
        {
            Destroy(AmmoList[i].gameObject);
        }
        AmmoList.Clear();
    }

    private void OnDestroy()
    {
        GameEvents._Start_Level -= ResetAmmo;
    }
}
