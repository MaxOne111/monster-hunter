using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    [SerializeField] private Image _Icon;
    [field:SerializeField] public Weapon Weapon { get; set; }
    public Button Button{get; private set; }

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    public void WeaponData(Sprite _icon)
    {
        _Icon.GetComponent<Image>().sprite = _icon;
    }
}
