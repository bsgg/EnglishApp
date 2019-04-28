using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class GrammarUI : UIBase
    {
        public delegate void GrammarUIAction();
        public event GrammarUIAction OnNextGrammarClick;
        public event GrammarUIAction OnExampleClick;
        public event GrammarUIAction OnTranslationClick;

        [Header("Buttons")]
        [SerializeField]
        private ButtonWithText m_nextGrammarBtn;
        public ButtonWithText NextGrammarBtn
        {
            get { return m_nextGrammarBtn; }
        }

        [Header("Content")]
        [SerializeField]
        private Text m_titleLabel;
        public string TitleLabel
        {
            get { return m_titleLabel.text; }
            set { m_titleLabel.text = value; }
        }

        [SerializeField] private GameObject m_descriptionObject;
        public GameObject DescriptionObject
        {
            get { return m_descriptionObject; }
            set { m_descriptionObject = value; }
        }


        [SerializeField] private ScrollTextUI m_descriptionScroll;
        public ScrollTextUI DescriptionScroll
        {
            get { return m_descriptionScroll; }
            set { m_descriptionScroll = value; }
        }

        [SerializeField] private GameObject m_examplesObject;
        public GameObject ExamplesObject
        {
            get { return m_examplesObject; }
            set { m_examplesObject = value; }
        }


        [SerializeField] private ScrollTextUI m_examplesScroll;
        public ScrollTextUI ExamplesScroll
        {
            get { return m_examplesScroll; }
            set { m_examplesScroll = value; }
        }

        public override void Show()
        {
            base.Show();

            m_descriptionObject.SetActive(true);
            m_examplesObject.SetActive(false);
        }

        public void OnNextGrammar()
        {
            if (OnNextGrammarClick != null)
            {
                OnNextGrammarClick();
            }
        }


        public void OnExamplePress()
        {
            if (OnExampleClick != null)
            {
                OnExampleClick();
            }
        }

        public void OnTranslationPress()
        {
            if (OnTranslationClick != null)
            {
                OnTranslationClick();
            }
        }

    }
}
