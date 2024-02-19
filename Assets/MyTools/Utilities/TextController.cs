using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TextController : MonoBehaviour
{
    [SerializeField] TMP_Text _newText;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        // ActionEvent.OnChangeTxt += DisplayText;
    }

    // Start is called before the first frame update
    void Start()
    {
        DisplayText();
    }

    private void OnDestroy()
    {
        // ActionEvent.OnChangeTxt -= DisplayText;
    }

    private void DisplayText()
    {
        _text.text = _newText.text;
    }
}
