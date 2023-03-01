using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AmmoOnScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Ammo_List;
    public List<GameObject> AmmoList{get=>_Ammo_List;}
    
    public void AddAmmo(GameObject _ammo)
    {
        _Ammo_List.Add(_ammo);
    }

    public void RemoveAmmo(GameObject _ammo)
    {
        _Ammo_List.Remove(_ammo);
        
    }

    public void NearestAmmo(GameObject _player)
    {
        if (_Ammo_List.Count > 1)
        {
            GameObject _temp;
            for (int i = 0; i < _Ammo_List.Count; i++)
            {
                float _dist_1 = (_Ammo_List[i].transform.position - _player.transform.position).magnitude;
                for (int j = i+1; j < _Ammo_List.Count; j++)
                {
                    float _dist_2 = (_Ammo_List[j].transform.position - _player.transform.position).magnitude;
                    if (_dist_2 < _dist_1)
                    {
                        _temp = _Ammo_List[i];
                        _Ammo_List[i] = _Ammo_List[j];
                        _Ammo_List[j] = _temp;
                    }
                }
            }
        }
    }
}
