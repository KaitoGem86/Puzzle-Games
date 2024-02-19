using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    [SerializeField] TMP_Text _contentTxt;
    [SerializeField] string _key;
    [SerializeField] int value;
    [SerializeField] int _indexSpriteAssets;

    // Start is called before the first frame update
    void Start()
    {
        string result = string.Format(GameLanguage.Get(_key), value);

        if (_contentTxt.spriteAsset != null)
        {
            _contentTxt.text = result + $" <sprite={_indexSpriteAssets}>";
        }
        else
        {
            _contentTxt.text = result;
        }

    }
}
