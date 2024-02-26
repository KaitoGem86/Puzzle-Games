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
    [SerializeField] GameObject _iconFree;

    public virtual void DisplayBooster(int value)
    {
        _quantityText.text = value.ToString();
        if(value == -1)
        {
            _iconAds.SetActive(false);
            _iconFree.SetActive(true);
            _quantityText.transform.parent.parent.gameObject.SetActive(false);
            return;
        }
        //_quantityText.gameObject.SetActive(true);
        if (value > 0)
        {
            _iconAds.SetActive(false);
            _iconFree.SetActive(false);
            _quantityText.transform.parent.parent.gameObject.SetActive(true);
        }
        else
        { // use ads when value <= -2
            // _quantityText.gameObject.SetActive(false);
            _iconAds.SetActive(true);
            _iconFree.SetActive(false);
            _quantityText.transform.parent.parent.gameObject.SetActive(true);
            _quantityText.text = "Ads";
        }
    }
}
