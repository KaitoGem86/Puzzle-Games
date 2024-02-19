using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyToast : MonoBehaviour
{
    #region Inspector Variables
    public SpriteRenderer sprite;
    public TMP_Text tvMess;
    #endregion

    #region Member Variables
    private Transform mTrans;
    #endregion

    #region Unity Methods

    private void Awake()
    {
        mTrans = GetComponent<Transform>();
    }
    #endregion

    #region Public Methods

    public void ShowMess(string mess)
    {
        tvMess.text = mess;
        gameObject.SetActive(true);
        StartCoroutine(Hide());
    }

    public IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.5f);
        tvMess.text = "";
        //gameObject.SetActive(false);
        SimplePool.Despawn(this.gameObject);
    }
    #endregion
}
