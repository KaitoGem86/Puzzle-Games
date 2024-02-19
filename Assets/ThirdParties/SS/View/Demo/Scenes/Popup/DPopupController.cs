using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.View;

public class DPopupController : Controller
{
    [SerializeField] Text m_Text;

    public const string SCENE_NAME = "DPopup";

    string m_Data = string.Empty;

    public override string SceneName()
    {
        return SCENE_NAME;
    }

    public override void OnActive(object data = null)
    {
        if (data != null)
        {
            m_Data = data.ToString();
            m_Text.text = m_Data;
        }
        Debug.Log(data + " OnActive");
    }

    public override void OnShown()
    {
        Debug.Log(m_Data + " OnShown");
    }

    public override void OnHidden()
    {
        Debug.Log(m_Data + " OnHidden");
    }

    public override void OnReFocus()
    {
        Debug.Log(m_Data + " OnReFocus");
    }
}