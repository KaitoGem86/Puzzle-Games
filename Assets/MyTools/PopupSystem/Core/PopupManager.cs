using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PopupSystem
{
    public class PopupManager : MonoBehaviour
    {
        #region Popup Events

        public delegate void PopupEvent(BasePopup popup);

        public event PopupEvent EvtPopupOpen;
        public event PopupEvent EvtPopupClose;
        public event PopupEvent EvtTouchOverlay;

        #endregion

        public Canvas canvas;
        public bool usingDefaultTransparent = true;
        [SerializeField] private string popupPath;
        [Header("Tween")] 
        public Ease fadeInTweenType;
        public Ease fadeOutTweenType;
        public float fadeTweenTime;
        public float transparentAmount;
#if UNITY_EDITOR
        [ContextMenu("LoadPopupPrefabs")]
        void LoadPopupPrefabs()
        {
            var lstPrefabs = new List<BasePopup>();
            var lstNames = System.IO.Directory.GetFiles($"{popupPath}",
                "*.prefab", System.IO.SearchOption.AllDirectories);
            foreach (var itName in lstNames)
            {
                var obj = UnityEditor.AssetDatabase.LoadAssetAtPath<BasePopup>($"{itName}");
                if (obj == null) continue;
                lstPrefabs.Add(obj);
            }

            prefabs = lstPrefabs.ToArray();
            UnityEditor.EditorUtility.SetDirty(gameObject);
        }
#endif
        public BasePopup[] prefabs;
        public Image transparent;
        private Transform mTransparentTrans;
        public Stack<BasePopup> popupStacks = new Stack<BasePopup>();
        public Transform parent;
        private int defaultSortingOrder;
        private static PopupManager mInstance;
        private Queue<BasePopup> popupQueue = new Queue<BasePopup>();
        public bool hasPopupShowing;
        public static PopupManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = LoadResource<PopupManager>("PopupManager");
                }

                return mInstance;
            }
        }

        void Awake()
        {
            mInstance = this;
            mTransparentTrans = transparent.transform;
            defaultSortingOrder = canvas.sortingOrder;
      
        }

        private void Start()
        {
            EvtPopupClose += HandlePopupClose;
            hasPopupShowing = false;
        }

        private void OnDestroy()
        {
            EvtPopupClose -= HandlePopupClose;
        }

        public static T CreateNewInstance<T>()
        {
            T result = Instance.CheckInstancePopupPrebab<T>();
            return result;
        }

        public T CheckInstancePopupPrebab<T>()
        {
            System.Type type = typeof(T);
            GameObject go = null;
            for (int i = 0; i < prefabs.Length; i++)
            {
                if (IsOfType<T>(prefabs[i]))
                {
                    go = (GameObject) Instantiate(prefabs[i].gameObject, parent);
                    break;
                }
            }

            T result = go.GetComponent<T>();
            return result;
        }

        private bool IsOfType<T>(object value)
        {
            return value is T;
        }

        public void ChangeTransparentOrder(Transform topPopupTransform, bool active)
        {
            if (active)
            {
                mTransparentTrans.SetSiblingIndex(topPopupTransform.GetSiblingIndex() - 1);
                if (usingDefaultTransparent)
                {
                    ShowFade();
                }
                else
                {
                    Debug.Log("Clear all");
                    HideFade();
                }

                /*transparent.gameObject.SetActive(true && usingDefaultTransparent);*/
                hasPopupShowing = true;
            }
            else
            {
                if (parent.childCount >= 2)
                {
                    mTransparentTrans.SetSiblingIndex(parent.childCount - 2);
                    hasPopupShowing = true;
                }
                else
                {
                    HideFade();
                    hasPopupShowing = false;
                }
            }
            //Debug.Log("hasPopupShowing: "+ hasPopupShowing);
        }

        public PopupManager Preload()
        {
            return mInstance;
        }

        public bool SequenceHidePopup()
        {
            if (popupStacks.Count > 0)
                popupStacks.Peek().Hide();
            else
            {
                HideFade();
                /*transparent.gameObject.SetActive(false);*/
                hasPopupShowing = false;
            }

            return (popupStacks.Count > 0);
        }

        public void CloseAllPopup()
        {
            for (int i = 0; i < popupStacks.Count; i++)
            {
                BasePopup popup = popupStacks.Peek();
                if (popup != null)
                    popup.Hide();
            }


            HideFade();
            /*transparent.gameObject.SetActive(false);*/
        }

        public static T LoadResource<T>(string name)
        {
            GameObject go = (GameObject) GameObject.Instantiate(Resources.Load(name));
            go.name = string.Format("[{0}]", name);
            DontDestroyOnLoad(go);
            return go.GetComponent<T>();
        }

        public void SetSortingOrder(int order)
        {
            canvas.sortingOrder = order;
        }

        public void ResetOrder()
        {
            canvas.sortingOrder = defaultSortingOrder;
        }

        public void OderPopup(BasePopup popup)
        {
            if (!hasPopupShowing)
            {
                popup.ActivePopup();
            }
            else
            {
                popup.gameObject.SetActive(false);
                popupQueue.Enqueue(popup);
            }
        }

        public void OnClickOverlay()
        {
            EvtTouchOverlay?.Invoke(popupStacks.Peek());
        }

        public bool GetHasPopUp()
        {
            return hasPopupShowing;
        }


        #region Event Methods

        public void OnPopupOpen(BasePopup popup)
        {
            EvtPopupOpen?.Invoke(popup);
        }

        public void OnPopupClose(BasePopup popup)
        {
            EvtPopupClose?.Invoke(popup);
        }

        #endregion

        #region Handle Events

        private void HandlePopupClose(BasePopup popup)
        {
            if (popupQueue.Count > 0)
            {
                BasePopup nextPopup = popupQueue.Dequeue();
                nextPopup.gameObject.SetActive(true);
                nextPopup.ActivePopup();
            }
        }

        #endregion

        #region Tween

        public void ShowFade()
        {
            transparent.gameObject.SetActive(true);
            transparent.DOFade(transparentAmount, fadeTweenTime).SetEase(fadeInTweenType);
        }

        public void HideFade()
        {
            transparent.DOFade(0, fadeTweenTime).SetEase(fadeOutTweenType).OnComplete(() =>
            {
                transparent.gameObject.SetActive(false);
            });
        }

        public void DisableFadeBackground()
        {
            transparent.gameObject.SetActive(false);
        }

        public void EnableFadeBackground()
        {
            transparent.gameObject.SetActive(true);
        }

        #endregion
    }
}