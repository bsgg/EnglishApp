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

        [SerializeField] private ScrollTextUI m_rulesScroll;
        public ScrollTextUI RulesScroll
        {
            get { return m_rulesScroll; }
            set { m_rulesScroll = value; }
        }

        [SerializeField] private ScrollTextUI m_examplesScroll;
        public ScrollTextUI ExamplesScroll
        {
            get { return m_examplesScroll; }
            set { m_examplesScroll = value; }
        }

        public void OnNextGrammar()
        {
            if (OnNextGrammarClick != null)
            {
                OnNextGrammarClick();
            }
        }

    }
}
