using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SS.View
{
    public class ManagerObject : Tweener
    {
        enum State
        {
            SHIELD_OFF,
            SHIELD_ON,
            SHIELD_FADE_IN,
            SHIELD_FADE_OUT,
            SCENE_LOADING
        }

        // Commons
        [SerializeField] Canvas m_Canvas;

        // Cameras
        [SerializeField] Camera m_BgCamera;
        [SerializeField] Camera m_UiCamera;

        // Shield & Transition
        [SerializeField] Image m_Shield;
        [SerializeField] EaseType m_FadeInEaseType = EaseType.easeInOutExpo;
        [SerializeField] EaseType m_FadeOutEaseType = EaseType.easeInOutExpo;
        [SerializeField] Color m_ShieldColor = Color.black;

        // Shield & Transition Vars
        bool m_Active;
        State m_State;

        EasingFunction m_FadeInEase;
        EasingFunction m_FadeOutEase;

        float m_StartAlpha;
        float m_EndAlpha;

        #region Camera Methods
        public void ActivateBackgroundCamera(bool active)
        {
            if (m_BgCamera != null && m_BgCamera.gameObject != null)
            {
                m_BgCamera.gameObject.SetActive(active);
            }
        }

        public Camera UICamera
        {
            get { return m_UiCamera; }
        }
        #endregion

        #region Shield & Transition methods
        public void ShieldOff()
        {
            if (m_State == State.SHIELD_ON)
            {
                m_State = State.SHIELD_OFF;
                Active = false;
            }
        }

        public void ShieldOn()
        {
            if (m_State == State.SHIELD_OFF)
            {
                m_State = State.SHIELD_ON;
                Active = true;

                m_Shield.color = Color.clear;
            }
        }

        public void ShieldOnColor()
        {
            if (m_State == State.SHIELD_OFF)
            {
                m_State = State.SHIELD_ON;
                Active = true;

                m_Shield.color = m_ShieldColor;
            }
        }

        // Scene gradually appear
        public void FadeInScene()
        {
            if (this != null)
            {
                if (Manager.SceneFadeInDuration == 0)
                {
                    ShieldOff();
                }
                else
                {
                    Active = true;

                    m_StartAlpha = 1;
                    m_EndAlpha = 0;

                    this.m_AnimationDuration = Manager.SceneFadeInDuration;
                    this.Play();

                    m_State = State.SHIELD_FADE_IN;
                }
            }
        }

        // Scene gradually disappear
        public void FadeOutScene()
        {
            if (this != null)
            {
                if (Manager.SceneFadeOutDuration == 0)
                {
                    OnFadedOut();
                    ShieldOn();
                }
                else
                {
                    Active = true;

                    m_StartAlpha = 0;
                    m_EndAlpha = 1;

                    this.m_AnimationDuration = Manager.SceneFadeOutDuration;
                    this.Play();

                    m_State = State.SHIELD_FADE_OUT;
                }
            }
        }

        public void OnFadedIn()
        {
            if (this != null)
            {
                m_State = State.SHIELD_OFF;
                Active = false;
                Manager.OnFadedIn();
            }
        }

        public void OnFadedOut()
        {
            m_State = State.SCENE_LOADING;
            Manager.OnFadedOut();
        }

        public bool Active
        {
            get
            {
                return m_Active;
            }
            protected set
            {
                m_Active = value;
                m_Shield.gameObject.SetActive(m_Active);
            }
        }

        protected override void ApplyProgress(float progress)
        {
            EasingFunction ease = (m_State == State.SHIELD_FADE_IN) ? m_FadeInEase : m_FadeOutEase;

            Color color = m_ShieldColor;
            color.a = ease(m_StartAlpha, m_EndAlpha, progress);

            m_Shield.color = color;
        }

        protected override void OnEndAnimation()
        {
            switch (m_State)
            {
                case State.SHIELD_FADE_IN:
                    OnFadedIn();
                    break;
                case State.SHIELD_FADE_OUT:
                    OnFadedOut();
                    break;
            }
        }
        #endregion

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                GameObject go = new GameObject("EventSystem");
                eventSystem = go.AddComponent<EventSystem>();
                go.AddComponent<StandaloneInputModule>();
            }
            DontDestroyOnLoad(eventSystem.gameObject);

            m_FadeInEase = GetEasingFunction(m_FadeInEaseType);
            m_FadeOutEase = GetEasingFunction(m_FadeOutEaseType);
        }

        IEnumerator Start()
        {
            yield return 0;

            if (EventSystem.current != null)
            {
                int defaultValue = EventSystem.current.pixelDragThreshold;
                EventSystem.current.pixelDragThreshold = Mathf.Max(defaultValue, (int)(defaultValue * Screen.dpi / 160f));
            }

            m_Canvas.GetComponent<Canvas>().worldCamera = this.UICamera;

            var canvases = FindObjectsOfType<Canvas>();
            foreach (var canvas in canvases)
            {
                if (canvas.renderMode != RenderMode.ScreenSpaceCamera && canvas.GetComponent<DontChangeCanvasCamera>() == null)
                {
                    canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    canvas.worldCamera = this.UICamera;
                }
            }

            yield return 0;
            ShieldOff();
        }

        protected override void Update()
        {
            base.Update();
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_STANDALONE
            UpdateInput();
#endif
        }

        void UpdateInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Manager.IsActiveShield())
                {
                    Controller controller = Manager.TopController();
                    if (controller != null)
                    {
                        controller.OnKeyBack();
                    }
                }
            }
        }
    }
}