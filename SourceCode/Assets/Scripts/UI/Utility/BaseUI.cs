using UnityEngine;
using System.Collections;

namespace EnglishApp
{
    public class BaseUI : MonoBehaviour
    {
        [Header("Base Panel")]
        [SerializeField] protected CanvasGroup      m_CanvasGroupBaseUI;
        [SerializeField] protected RectTransform    m_TransformUI;
        [SerializeField] protected bool             m_HideOnStart;
        protected bool                              m_IsVisible;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { m_IsVisible = value; }
        }

        void Awake()
        {
            DoAwake();
        }
        void Start()
        {
            DoStart();
        }

        void Update()
        {
            DoUpdate();
        }

        protected virtual void DoAwake(){}

        protected virtual void DoStart()
        {
            m_IsVisible = true;
            if (m_HideOnStart)
            {
                Hide();
            }
        }

        protected virtual void DoUpdate() { }

        public virtual void Show()
        {
            m_IsVisible = true;
            m_CanvasGroupBaseUI.alpha = 1.0f;
            m_CanvasGroupBaseUI.interactable = true;
            m_CanvasGroupBaseUI.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            m_IsVisible = false;
            m_CanvasGroupBaseUI.alpha = 0.0f;
            m_CanvasGroupBaseUI.interactable = false;
            m_CanvasGroupBaseUI.blocksRaycasts = false;
        }

        public virtual void ShowWithFadeIn()
        {
            StartCoroutine(RoutineFadeIn());
        }

        private IEnumerator RoutineFadeIn()
        {
            m_CanvasGroupBaseUI.alpha = 0.0f;
            
            while (m_CanvasGroupBaseUI.alpha < 1.0f)
            {
                m_CanvasGroupBaseUI.alpha += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Show();
        }


        public virtual void HideWithFadeOut()
        {
            StartCoroutine(RoutineFadeOut());
        }

        private IEnumerator RoutineFadeOut()
        {
            m_CanvasGroupBaseUI.alpha = 1.0f;

            while (m_CanvasGroupBaseUI.alpha > 0.0f)
            {
                m_CanvasGroupBaseUI.alpha -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Hide();
        }

    }
}

