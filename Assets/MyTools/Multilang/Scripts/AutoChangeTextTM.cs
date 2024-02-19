using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoChangeTextTM : MonoBehaviour
{
    public string id_text;

    private TMP_Text text;
    private TMP_FontAsset df_font;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        df_font = text.font;
     //   GameLanguage.Instance.evtChangeFont += ChangeFont;
    //    ChangeFont();
        text.text = GameLanguage.Get(id_text);
    }

    private void OnDestroy()
    {
        GameLanguage.Instance.evtChangeFont -= ChangeFont;
    }

    private void ChangeFont()
    {
        if (text != null)
            text.text = GameLanguage.Get(id_text);
        if (GameLanguage.DefaultLanguage)
            text.font = df_font;
        else
        if (GameLanguage.TMFont != null)
            text.font = GameLanguage.TMFont;
    }
}
