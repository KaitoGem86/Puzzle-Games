using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveUI : MonoBehaviour
{
    private RectTransform mTrans;
    private float ratioScreen;
    private Vector2 normalAnchor;
    public Vector2 anchor;
    private void Awake()
    {
        mTrans = GetComponent<RectTransform>();
        ratioScreen = Screen.height / Screen.width;
        normalAnchor = mTrans.anchoredPosition;
    }

    private void Start()
    {
        if (ratioScreen >= 2)
        {
            Vector2 newAnchor = normalAnchor;
            newAnchor.x += anchor.x;
            newAnchor.y += anchor.y;
            mTrans.anchoredPosition = newAnchor;
        }
    }
}
