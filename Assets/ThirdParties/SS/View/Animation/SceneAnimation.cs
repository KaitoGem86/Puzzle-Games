// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using UnityEngine;
using System.Collections;

namespace SS.View
{
    [ExecuteInEditMode]
    public class SceneAnimation : Tweener
    {
        const int WAIT_ANIM_FRAME = 4;

        enum StartState
        {
            IDLE,
            SHOW,
            HIDE
        }

        [SerializeField] Controller m_Controller;

        StartState m_StartState;
        int m_FrameCounter;

        /// <summary>
        /// After a scene is loaded, its view will be put at center of screen. So you have to put it somewhere temporary before playing the show-animation.
        /// </summary>
        public virtual void HideBeforeShowing()
        {
        }

        /// <summary>
        /// Play the show-animation. Don't forget to call OnShown right after the animation finishes.
        /// </summary>
        public virtual void Show()
        {
            OnShown();
        }

        /// <summary>
        /// Play the hide-animation. Don't forget to call OnHidden right after the animation finishes.
        /// </summary>
        public virtual void Hide()
        {
            OnHidden();
        }

        public void StartShow()
        {
            m_FrameCounter = 0;
            m_StartState = StartState.SHOW;
        }

        public void StartHide()
        {
            m_FrameCounter = 0;
            m_StartState = StartState.HIDE;
        }

        public void OnShown()
        {
            Manager.OnShown(m_Controller);
        }

        public void OnHidden()
        {
            Manager.OnHidden(m_Controller);
        }

        private void Start()
        {
            if (Application.isPlaying)
            {
                if (m_Controller != Manager.MainController)
                {
                    HideBeforeShowing();
                }
            }
        }

        void UpdateFrameCounter()
        {
            switch (m_StartState)
            {
                case StartState.SHOW:
                    m_FrameCounter++;
                    if (m_FrameCounter == WAIT_ANIM_FRAME)
                    {
                        Show();
                        m_StartState = StartState.IDLE;
                    }
                    break;
                case StartState.HIDE:
                    m_FrameCounter++;
                    if (m_FrameCounter == WAIT_ANIM_FRAME)
                    {
                        Hide();
                        m_StartState = StartState.IDLE;
                    }
                    break;
            }
        }

#if UNITY_EDITOR
        protected override void Update()
        {
            base.Update();

            if (!Application.isPlaying)
            {
                AutoFind();
            }
            else
            {
                UpdateFrameCounter();
            }
        }

        void AutoFind()
        {
            if (!Application.isPlaying)
            {
                if (m_Controller == null)
                {
                    m_Controller = FindObjectOfType<Controller>();

                    if (m_Controller != null)
                    {
                        m_Controller.Animation = this;
                    }
                }
            }
        }
#else
        protected override void Update()
        {
            base.Update();

            UpdateFrameCounter();
        }
#endif
    }
}

