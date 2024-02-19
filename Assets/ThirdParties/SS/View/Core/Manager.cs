using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections.Generic;

namespace SS.View
{
    public class Manager
    {
        public class Data
        {
            public object data;
            public Callback onShown;
            public Callback onHidden;
            public Scene scene;
            public string sceneName;

            public Data(object data, string sceneName, Callback onShown, Callback onHidden)
            {
                this.data = data;
                this.sceneName = sceneName;
                this.onShown = onShown;
                this.onHidden = onHidden;
            }
        }

        public delegate void Callback();

        public delegate void ShowBannerDelegate(float delay);
        public delegate void HideBannerDelegate();

        static Stack<Controller> m_ControllerStack = new Stack<Controller>();
        static Queue<Data> m_DataQueue = new Queue<Data>();

        static bool m_SetupCover;
        static bool m_LoadingActive;

        static string m_MainSceneName;
        static Controller m_MainController;

        static string m_LoadingSceneName;
        static Controller m_LoadingController;

        public static int stackCount
        {
            get
            {
                return m_ControllerStack.Count;
            }
        }

        public static Controller MainController
        {
            get
            {
                return m_MainController;
            }
        }

        public static Color ShieldColor
        {
            get;
            set;
        }

        public static float SceneFadeInDuration
        {
            get;
            set;
        }

        public static float SceneFadeOutDuration
        {
            get;
            set;
        }

        public static float SceneAnimationDuration
        {
            get;
            set;
        }

        public static ManagerObject Object
        {
            get;
            protected set;
        }

        static Manager()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = 60;
#endif

            SceneManager.sceneLoaded += OnSceneLoaded;

            ShieldColor = new Color(0.235f, 0.235f, 0.235f, 0.5f);
          //  float fadeOutDuration = 0.283f;
            SceneFadeOutDuration = 0.3f;
            //float fadeInDuration = 1f;
            SceneFadeInDuration = 1f;
            SceneAnimationDuration = 0.5f;

            Object = ((GameObject)GameObject.Instantiate(Resources.Load("ManagerObject"))).GetComponent<ManagerObject>();
            Object.gameObject.name = "ManagerObject";
        }

        public static void Load(string sceneName, object data = null)
        {
            m_DataQueue.Enqueue(new Data(data, sceneName, null, null));
            m_MainSceneName = sceneName;
            Object.FadeOutScene();
        }

        public static void Reload()
        {
            if (!string.IsNullOrEmpty(m_MainSceneName))
            {
                Load(m_MainSceneName);
            }
        }

        public static void Add(string sceneName, object data = null, Callback onShown = null, Callback onHidden = null)
        {
            m_DataQueue.Enqueue(new Data(data, sceneName, onShown, onHidden));
            Object.ShieldOn();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public static void Close()
        {
            if (m_ControllerStack.Count > 1)
            {
                ActivatePreviousController(true);
            }

            if (m_ControllerStack.Count > 0)
            {
                Object.ShieldOn();
                m_ControllerStack.Peek().Hide();
            }
        }

        public static string LoadingSceneName
        {
            set
            {
                m_LoadingSceneName = value;
                SceneManager.LoadScene(m_LoadingSceneName, LoadSceneMode.Additive);
            }
            get
            {
                return m_LoadingSceneName;
            }
        }

        public static void LoadingAnimation(bool active, object data = null)
        {
            if (m_LoadingController != null)
            {
                m_LoadingActive = active;
                m_LoadingController.gameObject.SetActive(active);

                if (active)
                {
                    m_LoadingController.OnActive(data);
                }
            }
        }

        public static void OnShown(Controller controller)
        {
            if (controller.FullScreen && m_ControllerStack.Count > 1)
            {
                ActivatePreviousController(controller, false);
            }

            controller.OnShown();
            if (controller.Data.onShown != null)
            {
                controller.Data.onShown();
            }

            Object.ShieldOff();
        }

        public static void OnHidden(Controller controller)
        {
            controller.OnHidden();
            if (controller.Data.onHidden != null)
            {
                controller.Data.onHidden();
            }

            Unload();

            if (m_ControllerStack.Count > 0)
            {
                var currentController = m_ControllerStack.Peek();
                currentController.OnReFocus();
            }

            Object.ShieldOff();
        }

        public static bool IsActiveShield()
        {
            return Object.Active || m_LoadingActive;
        }

        public static Controller TopController()
        {
            if (m_ControllerStack.Count > 0)
            {
                return m_ControllerStack.Peek();
            }

            return null;
        }

        public static void OnFadedIn()
        {
            m_MainController.OnShown();
        }

        public static void OnFadedOut()
        {
            if (m_MainController != null)
            {
                m_MainController.OnHidden();
            }

            SceneManager.LoadScene(m_MainSceneName, LoadSceneMode.Single);
        }

        static void ActivatePreviousController(bool active)
        {
            ActivatePreviousController(m_ControllerStack.Peek(), active);
        }

        static void ActivatePreviousController(Controller controller, bool active)
        {
            Stack<Controller> temp = new Stack<Controller>();

            while (m_ControllerStack.Count > 0)
            {
                var top = m_ControllerStack.Pop();
                temp.Push(top);

                if (top == controller && m_ControllerStack.Count > 0)
                {
                    var previousController = m_ControllerStack.Peek();
                    previousController.gameObject.SetActive(active);
                    break;
                }
            }

            while (temp.Count > 0)
            {
                m_ControllerStack.Push(temp.Pop());
            }
        }

        static void Unload()
        {
            if (m_ControllerStack.Count > 0)
            {
                SceneManager.UnloadSceneAsync(m_ControllerStack.Pop().Data.scene);
            }
        }

        static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Get Controller
            var controller = GetController(scene);
            if (controller == null)
            {
                m_ControllerStack.Push(null);
                return;
            }

            // Loading Scene
            if (controller.SceneName() == LoadingSceneName)
            {
                controller.SetupCanvas(98);
                m_LoadingController = controller;
                GameObject.DontDestroyOnLoad(m_LoadingController.gameObject);
                LoadingAnimation(false);

                return;
            }

            // Single Mode automatically destroy all scenes, so we have to clear the stack.
            if (mode == LoadSceneMode.Single)
            {
                m_ControllerStack.Clear();
            }

            // Unload resources and collect GC.
            Resources.UnloadUnusedAssets();
            System.GC.Collect();

            // Get Data
            if (m_DataQueue.Count == 0)
            {
                m_DataQueue.Enqueue(new Data(null, scene.name, null, null));
            }

            Data data = m_DataQueue.Dequeue();
            while (data.sceneName != scene.name && m_DataQueue.Count > 0)
            {
                data = m_DataQueue.Dequeue();
            }

            if (data == null)
            {
                data = new Data(null, scene.name, null, null);
            }

            data.scene = scene;

            // Push the current scene to the stack.
            m_ControllerStack.Push(controller);

            // Setup controller
            controller.Data = data;
            controller.SetupCanvas(m_ControllerStack.Count - 1);
            controller.CreateShield();
            controller.OnActive(data.data);

            // Animation
            if (m_ControllerStack.Count == 1)
            {
                // Own Camera
                if (controller.Camera != null)
                {
                    Object.ActivateBackgroundCamera(false);

                    if (controller.Camera.GetComponent<CameraDestroyer>() == null)
                    {
                        controller.Camera.gameObject.AddComponent<CameraDestroyer>();
                    }
                }

                // Main Scene
                m_MainController = controller;
                if (string.IsNullOrEmpty(m_MainSceneName))
                {
                    m_MainSceneName = scene.name;
                }

                // Fade
                Object.FadeInScene();
            }
            else
            {
                // Popup Scene
                controller.Show();
            }
        }

        static Controller GetController(Scene scene)
        {
            var roots = scene.GetRootGameObjects();
            for (int i = 0; i < roots.Length; i++)
            {
                var controller = roots[i].GetComponent<Controller>();
                if (controller != null)
                {
                    return controller;
                }
            }
            return null;
        }
    }
}