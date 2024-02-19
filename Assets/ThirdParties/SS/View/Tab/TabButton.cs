using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.View
{
    public class TabButton : MonoBehaviour
    {
        protected RectTransform m_RectTransform;

        protected virtual void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
        }

        public virtual void Resize(float width, float left)
        {
            m_RectTransform.sizeDelta = new Vector2(width, m_RectTransform.sizeDelta.y);
            m_RectTransform.anchoredPosition = new Vector2(left + width / 2, 0);
        }

        public virtual void SetNormal()
        {
        }

        public virtual void SetActive()
        {
        }
    }
}
