// This code is part of the SS-Scene library, released by Anh Pham (anhpt.csit@gmail.com).

using UnityEngine;
using System.Collections;

namespace SS.View
{
    public class SceneLegacyAnimation : SceneAnimation
    {
        [SerializeField]
        protected string m_ShowAnimName = "Show";

        [SerializeField]
        protected string m_HideAnimName = "Hide";

        float m_TimeAtLastFrame = 0F;
        float m_TimeAtCurrentFrame = 0F;
        float m_DeltaTime = 0F;
        float m_AccumTime = 0F;

        AnimationState m_CurrState;
        bool m_IsPlayingLegacyAnim = false;
        bool m_IsEndAnim = false;
        string m_CurrClipName;

        Animation m_Animation;
        Animation Animation
        {
            get
            {
                if (m_Animation == null)
                {
                    m_Animation = GetComponent<Animation>();
                }
                return m_Animation;
            }
        }

        void Awake()
        {
            if (Application.isPlaying)
            {
                if (Manager.SceneAnimationDuration > 0)
                {
                    Animation[m_ShowAnimName].speed = Animation[m_ShowAnimName].length / Manager.SceneAnimationDuration;
                    Animation[m_HideAnimName].speed = Animation[m_HideAnimName].length / Manager.SceneAnimationDuration;
                }
            }
        }

        public override void HideBeforeShowing()
        {
            Animation.Play(m_ShowAnimName);
            Animation[m_ShowAnimName].time = 0;
            Animation.Sample();
            Animation.Stop();
        }

        public override void Show()
        {
            PlayAnimation(Animation, m_ShowAnimName);
        }

        public override void Hide()
        {
            PlayAnimation(Animation, m_HideAnimName);
        }

        protected override void Update()
        {
            base.Update();

            if (Application.isPlaying)
            {
                AnimationUpdate();
            }
        }

        void AnimationUpdate()
        {
            m_TimeAtCurrentFrame = UnityEngine.Time.realtimeSinceStartup;
            m_DeltaTime = m_TimeAtCurrentFrame - m_TimeAtLastFrame;
            m_TimeAtLastFrame = m_TimeAtCurrentFrame; 

            if (m_IsPlayingLegacyAnim)
            {
                if (m_IsEndAnim == true)
                {
                    m_CurrState.enabled = false;
                    m_IsPlayingLegacyAnim = false;

                    if (m_CurrClipName == m_ShowAnimName)
                    {
                        OnShown();
                    }
                    else if (m_CurrClipName == m_HideAnimName)
                    {
                        OnHidden();
                    }

                    return;
                }

                m_AccumTime += m_DeltaTime * Animation[m_CurrClipName].speed;
                if (m_AccumTime >= m_CurrState.length)
                {
                    m_AccumTime = m_CurrState.length;
                    m_IsEndAnim = true;
                }
                m_CurrState.normalizedTime = m_AccumTime / m_CurrState.length;
            }
        }

        void PlayAnimation(Animation anim, string clip)
        {
            m_AccumTime = 0F;
            m_CurrClipName = clip;
            m_CurrState = anim[clip];
            m_CurrState.weight = 1;
            m_CurrState.blendMode = AnimationBlendMode.Blend;
            m_CurrState.wrapMode = WrapMode.Once;
            m_CurrState.normalizedTime = 0;
            m_CurrState.enabled = true;
            m_IsPlayingLegacyAnim = true;
            m_IsEndAnim = false;
        }
    }
}

