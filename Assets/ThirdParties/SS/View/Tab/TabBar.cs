using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.View
{
    public class TabBar : MonoBehaviour
    {
        TabButton[] m_Tabs;
        RectTransform m_RectTranform;

        int m_CurrentTabIndex = 0;

        private void Awake()
        {
            m_RectTranform = GetComponent<RectTransform>();

            m_Tabs = new TabButton[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                m_Tabs[i] = transform.GetChild(i).GetComponent<TabButton>();
            }
        }

        public void SetCurrentTab(int index)
        {
            m_CurrentTabIndex = index;

            var width = m_RectTranform.rect.width;
            var tabLeft = -width / 2;
            var tabBaseWidth = width / (m_Tabs.Length + 1);

            for (int i = 0; i < m_Tabs.Length; i++)
            {
                var tabWidth = (i == m_CurrentTabIndex) ? tabBaseWidth * 2 : tabBaseWidth;
                m_Tabs[i].Resize(tabWidth, tabLeft);
                tabLeft += tabWidth;

                if (i == m_CurrentTabIndex)
                {
                    m_Tabs[i].SetActive();
                }
                else
                {
                    m_Tabs[i].SetNormal();
                }
            }
        }
    }
}