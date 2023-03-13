using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerPetsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _Name;
    [SerializeField] private TextMeshProUGUI[] _Level;
    [SerializeField] private Image[] _Icon;
    [SerializeField] private TextMeshProUGUI[] _Description;
    private PlayerPets _Player_Pets;

    [Inject]
    private void Construct(PlayerPets _pets)
    {
        _Player_Pets = _pets;
    }

    public void PetData()
    {
        if (_Player_Pets.Pets.Count > 0)
        {
            for (int i = 0; i < _Player_Pets.Pets.Count; i++)
            {
                _Icon[i].GetComponent<Image>().sprite = _Player_Pets.PetsOnScene[i].Icon;
                _Name[i].text = _Player_Pets.Pets[i].Name;
                _Level[i].text = "Level: " + _Player_Pets.PetsOnScene[i].Level;
                _Description[i].text = _Player_Pets.PetsOnScene[i].Description;
            }
        }
    }
    
}
