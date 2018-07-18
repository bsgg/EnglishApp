﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utility
{
    public class ButtonWithText : UIBase
    {
        public delegate void ButtonWithTextAction(ButtonWithText button);
        public event ButtonWithTextAction OnButtonClicked;

        [SerializeField] private EventTrigger m_EventTrigger;
        public EventTrigger EventTriggerComponent
        {
            get { return m_EventTrigger; }
        }

        [SerializeField] private Text m_Title;
        public string Title
        {
            get { return m_Title.text; }
            set { m_Title.text = value; }
        }

        [SerializeField] private Color m_EnableColor;
        [SerializeField] private Color m_DisableColor;

        [SerializeField] private Image m_Icon;
        public Image Icon
        {
            get { return m_Icon; }
            set { m_Icon = value; }
        }


        private int m_IdButton;
        public int IdButton
        {
            get { return m_IdButton; }
            set { m_IdButton = value; }
        }

        public void Enable()
        {
            if (m_EventTrigger != null)
            {
                m_EventTrigger.enabled = true;
            }

            if (m_Icon != null)
            {
                m_Icon.color = m_EnableColor;
            }
        }

        public void Disable()
        {
            if (m_EventTrigger != null)
            {
                m_EventTrigger.enabled = false;
            }

            if (m_Icon != null)
            {
                m_Icon.color = m_DisableColor;
            }
        }

        public void Set(int idButton,string title, ButtonWithTextAction action)
        {
            if (m_Title != null)
            {
                m_Title.text = title;
            }

            m_IdButton = idButton;

            if ((action != null) && (m_EventTrigger != null))
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) => { action(this); });

                if (m_EventTrigger != null)
                {
                    for (int i = m_EventTrigger.triggers.Count - 1; i >= 0; i--)
                    {
                        m_EventTrigger.triggers.RemoveAt(i);
                    }

                    m_EventTrigger.triggers.Add(entry);
                }
            }
        }

        public void SetIcon(Sprite icon)
        {
            if (m_Icon != null)
            {
                m_Icon.sprite = icon;
            }
        }
    }
   
}
