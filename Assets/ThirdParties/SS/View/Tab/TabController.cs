using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SS.View
{
    public class TabController : Controller
    {
        [SerializeField] int m_StartingScreen = 0;
        [SerializeField] GameObject m_HorizontalScrollSnap;
        [SerializeField] TabBar m_TabBar;

        protected TabSubController[] m_SubControllers;
        protected int m_CurrentTabIndex;

        public TabSubController Current
        {
            get
            {
                return m_SubControllers[m_CurrentTabIndex];
            }

            protected set
            {
                m_SubControllers[m_CurrentTabIndex] = value;
            }
        }

        public override string SceneName()
        {
            return string.Empty;
        }

        public override void OnKeyBack()
        {
            if (Current != null)
            {
                Current.OnKeyBack();
            }
        }

        public void Load(string sceneName, object data = null, string folder = "Scenes")
        {
            var prefab = Resources.Load<GameObject>(string.Format("{0}/{1}", folder, sceneName));
            var scene = Instantiate(prefab, Current.transform.parent);

            scene.transform.SetSiblingIndex(Current.transform.GetSiblingIndex());
            CopyRectTransform(Current.GetComponent<RectTransform>(), scene.GetComponent<RectTransform>());

            Current.OnHidden();
            Destroy(Current.gameObject);

            Current = scene.GetComponent<TabSubController>();
            Current.tab = this;
            Current.OnActive(data);
            Current.Hide();
            Current.Play();
        }

        public void GoToScreen(int index)
        {
            m_HorizontalScrollSnap.SendMessage("GoToScreen", index);
            m_TabBar.SetCurrentTab(index);
        }

        protected virtual void Awake()
        {
            m_SubControllers = FindObjectsOfType<TabSubController>();
            m_CurrentTabIndex = m_StartingScreen;

            var subList = new List<TabSubController>(m_SubControllers);
            subList.Sort(SortBySiblingIndex);
            m_SubControllers = subList.ToArray();

            for (int i = 0; i < m_SubControllers.Length; i++)
            {
                m_SubControllers[i].tab = this;
                m_SubControllers[i].OnActive(null);
            }
        }

        protected virtual void Start()
        {
            m_TabBar.SetCurrentTab(m_StartingScreen);
        }

        public override void OnShown()
        {
            Current.OnShown();
        }

        public virtual void OnSelectionChangeEndEvent(int index)
        {
            if (m_CurrentTabIndex != index)
            {
                m_SubControllers[index].OnShown();
                m_SubControllers[m_CurrentTabIndex].OnHidden();

                m_CurrentTabIndex = index;

                m_TabBar.SetCurrentTab(m_CurrentTabIndex);
            }
        }

        protected void CopyRectTransform(RectTransform from, RectTransform to)
        {
            to.anchorMin = from.anchorMin;
            to.anchorMax = from.anchorMax;
            to.pivot = from.pivot;

            to.anchoredPosition = from.anchoredPosition;
            to.sizeDelta = from.sizeDelta;
        }

        protected int SortBySiblingIndex(TabSubController a, TabSubController b)
        {
            int aSI = a.transform.GetSiblingIndex();
            int bSI = b.transform.GetSiblingIndex();

            if (aSI > bSI)
            {
                return 1;
            }
            else if (aSI < bSI)
            {
                return -1;
            }

            return 0;
        }
    }
}
