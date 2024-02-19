using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoChangeText : MonoBehaviour {
	public string id_text;

	private Text text;
    private Font df_font;

	void Start () {
		text = GetComponent<Text>();
        if (text == null)
        {
            Debug.LogError("Object: " + this.gameObject.name + " not include text");
            return;
        }
        df_font = text.font;
		GameLanguage.Instance.evtChangeFont += ChangeFont;
		ChangeFont();
		text.text = GameLanguage.Get(id_text);
	}

	private void OnDestroy () {
		GameLanguage.Instance.evtChangeFont -= ChangeFont;
	}

	private void ChangeFont () {
		if (text != null)
			text.text = GameLanguage.Get(id_text);
        if (GameLanguage.DefaultLanguage)
            text.font = df_font;
        else
		if (GameLanguage.Font != null)
			text.font = GameLanguage.Font;
	}
}