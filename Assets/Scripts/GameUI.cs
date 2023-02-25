using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image[] _Health;
    [SerializeField] private Image _Combo_Time;
    [SerializeField] private Image _Combo_Time_Bar;

    public void CurrentHealth(float _max_Health, float _current_Health)
    {
        for (int i = 0; i < _Health.Length; i++)
        {
            _Health[i].fillAmount = _current_Health / _max_Health;
        }
    }

    public void CurrentComboTime(float _max_Time,float _current_Time)
    {
        _Combo_Time.fillAmount = _current_Time /_max_Time;
    }

    public void ComboTimeSetActive(bool _is_Active)
    {
        if (_is_Active)
        {
            _Combo_Time_Bar.gameObject.SetActive(true);
        }
        else
        {
            _Combo_Time_Bar.gameObject.SetActive(false);
        }
            
    }
}
