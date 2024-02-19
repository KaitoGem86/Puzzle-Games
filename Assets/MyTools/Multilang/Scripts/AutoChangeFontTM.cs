using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoChangeFontTM : MonoBehaviour
{
    private TextMeshPro text;
    private TMP_FontAsset df_font;
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        df_font = text.font;
        GameLanguage.Instance.evtChangeFont += ChangeFont;
        ChangeFont();
    }

    private void OnDestroy()
    {
        GameLanguage.Instance.evtChangeFont -= ChangeFont;
    }

    private void ChangeFont()
    {
        if (GameLanguage.DefaultLanguage)
            text.font = df_font;
        if (GameLanguage.TMFont != null)
        {
            if (text == null)
                text = GetComponent<TextMeshPro>();
            if(text!=null)
                text.font = GameLanguage.TMFont;
        }
            
    }
}
