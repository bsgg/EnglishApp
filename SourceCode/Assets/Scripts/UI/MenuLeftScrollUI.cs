using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class MenuLeftScrollUI : UIBase
    {
        [Header("Title object")]
        [SerializeField] private Text m_TitleMenu;

        [Header("Menu Animator object")]
        [SerializeField] private Animator m_MenuAnimator;

        [Header("Scroll object")]
        [SerializeField] private ScrollPanelUI m_ScrollMenu;
        public ScrollPanelUI ScrollMenu
        {
            get { return m_ScrollMenu; }
        }

        [Header("Background object")]
        [SerializeField] private GameObject m_Background;

        public void InitScroll(string title, List<string> data)
        {
            m_TitleMenu.text = title;
            m_ScrollMenu.InitScroll(data);
        }

        public override void Show()
        {
            base.Show();
            m_MenuAnimator.SetBool("MoveToRight", true);
            m_MenuAnimator.SetBool("Idle", false);
        }        

        public void Close()
        {
            m_MenuAnimator.SetBool("MoveToLeft", true);
            m_MenuAnimator.SetBool("Idle", false);
        }

        public void Disable()
        {
            m_MenuAnimator.SetBool("MoveToRight", false);
            m_MenuAnimator.SetBool("MoveToLeft", false);
            m_MenuAnimator.SetBool("Idle", true);
        }

        public void OnEndMoveToRight()
        {
            m_MenuAnimator.SetBool("MoveToRight", false);
            m_Background.SetActive(true);
        }
        public void OnEndMoveToLeft()
        {
            m_MenuAnimator.SetBool("MoveToLeft", false);
            m_Background.SetActive(false);
        }
    }
}
