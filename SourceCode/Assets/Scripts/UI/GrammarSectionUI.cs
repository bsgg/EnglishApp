using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class GrammarSectionUI : UIBase
    {
        [Header("GrammarSectionUI")]
        [SerializeField] private PopupTableUI m_PopupTable;
        public PopupTableUI PopupTable
        {
            get { return m_PopupTable; }
            set { m_PopupTable = value; }
        }

        [Header("Labels")]
        [SerializeField] private Text           m_LblGrammar;
        public string Grammar
        {
            get { return m_LblGrammar.text; }
            set { m_LblGrammar.text = value; }
        }

        [SerializeField] private Text           m_LblDescription;
        public string Description
        {
            get { return m_LblDescription.text; }
            set { m_LblDescription.text = value; }
        }

        [SerializeField] private Text           m_LblRule;
        public string Rule
        {
            get { return m_LblRule.text; }
            set { m_LblRule.text = value; }
        }

        [SerializeField] private Text           m_LblRulePages;
        public string RulePages
        {
            get { return m_LblRulePages.text; }
            set { m_LblRulePages.text = value; }
        }

        [SerializeField] private Text           m_LblExamples;
        public string Examples
        {
            get { return m_LblExamples.text; }
            set { m_LblExamples.text = value; }
        }

        [Header("Extra Info Button")]
        [SerializeField]  private TextButton m_BtnExtraInfo;
        public TextButton BtnExtraInfo
        {
            get { return m_BtnExtraInfo; }
            set { m_BtnExtraInfo = value; }
        }      

        public override void Show()
        {
            base.Show();
            m_PopupTable.Hide();
        }

        public void InitPopup(TableObject objTable)
        {
            PopupTable.InitPopup(objTable);
        }

    }
}
