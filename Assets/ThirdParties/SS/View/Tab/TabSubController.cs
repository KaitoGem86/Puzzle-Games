using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.View
{
    public class TabSubController : Tweener
    {
        #region Tab
        public TabController tab
        {
            get;
            set;
        }

        public virtual string SceneName()
        {
            return string.Empty;
        }

        public virtual void OnKeyBack()
        {
        }

        public virtual void OnActive(object data)
        {
        }

        public virtual void OnShown()
        {
        }

        public virtual void OnHidden()
        {
        }
        #endregion

        #region Animation
        protected CanvasGroup m_CanvasGroup;
        protected EasingFunction m_ShowEase;

        public void Hide()
        {
            m_CanvasGroup.alpha = 0;
        }

        protected override void ApplyProgress(float progress)
        {
            m_CanvasGroup.alpha = m_ShowEase(0, 1, progress);
        }

        protected override void OnEndAnimation()
        {
            OnShown();
        }

        protected virtual void Awake()
        {
            m_AnimationDuration = Manager.SceneAnimationDuration;
            m_ShowEase = GetEasingFunction(EaseType.easeInQuad);
            m_CanvasGroup = GetComponent<CanvasGroup>();

            if (m_CanvasGroup == null)
            {
                m_CanvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            if (GetComponent<Image>() == null)
            {
                gameObject.AddComponent<Image>();
            }

            if (GetComponent<Mask>() == null)
            {
                gameObject.AddComponent<Mask>();
            }

            if (transform.root.GetComponent<TabController>() == null)
            {
                OnActive(null);
                StartCoroutine(OnShownFake());
            }
        }

        private IEnumerator OnShownFake()
        {
            yield return new WaitForSeconds(Manager.SceneAnimationDuration);
            OnShown();
        }
        #endregion
    }
}
