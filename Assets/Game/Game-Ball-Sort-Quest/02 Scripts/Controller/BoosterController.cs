using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TypeBooster
{
    Revoke,
    AddTube
}

public class BoosterController : MonoBehaviour
{
    public TypeBooster Type;
    [SerializeField] TMP_Text _quantityText;
    [SerializeField] GameObject _iconAds;

    public void DisplayBooster(int value)
    {
        _quantityText.text = value.ToString();
        //_quantityText.gameObject.SetActive(true);
        if (value > 0)
        {
            _iconAds.SetActive(false);
        }
        else
        {
            // _quantityText.gameObject.SetActive(false);
            _iconAds.SetActive(true);
        }
    }
}
