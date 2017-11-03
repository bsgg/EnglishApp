using UnityEngine;
using System.Collections;

namespace EnglishApp
{
    public class BaseUI : BaseControl
    {
        [Header("Base Panel")]
        [SerializeField] protected CanvasGroup      m_CanvasGroupBaseUI;
        [SerializeField] protected RectTransform    m_TransformUI;        
        
        public override void Show()
        {
            base.Show();  
            m_CanvasGroupBaseUI.alpha = 1.0f;
            m_CanvasGroupBaseUI.interactable = true;
            m_CanvasGroupBaseUI.blocksRaycasts = true;
        }

        public override void Hide()
        {
            base.Hide();
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

