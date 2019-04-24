using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Utility;

namespace EnglishApp
{
    [System.Serializable]
    public class Grammar
    {
        public string Title;
        public string Description;

        public List<string> EnglishExamples;

        public List<string> SpanishExamples;

        public Grammar()
        {
            //Rules = new List<string>();
            EnglishExamples = new List<string>();
            SpanishExamples = new List<string>();
        }

    }

    [System.Serializable]
    public class GrammarDictionary
    {
        public string Category;
        public List<Grammar> Data;

        public GrammarDictionary()
        {
            Data = new List<Grammar>();
        }
    }

    public class GrammarControl : Base
    {

        [SerializeField] private CategoryMenu m_menu;

        [Header("Grammar UI")]
        [SerializeField] private GrammarUI m_ui;

        private GrammarDictionary m_selectedCategory;

        private int m_selectedGrammar = 0;

        #region BaseControl
        public override void Init()
        {
            m_menu.Hide();
            m_ui.Hide();

        }

        public override void Show()
        {
            base.Show();

            List<string> optionsMenu = new List<string>();

            for (int i=0; i < AppController.Instance.LauncherControl.GrammarSet.Count; i++)
            {
                string category = AppController.Instance.LauncherControl.GrammarSet[i].Category;
                optionsMenu.Add(category);
            }

            m_menu.OptionList.InitScroll(optionsMenu);

            m_menu.OptionList.OnButtonPress += OnOptionPress;

            m_menu.Show();

            m_ui.OnNextGrammarClick += OnNextGrammar;
            m_ui.Show();
        }

        private void OnOptionPress(ButtonWithText optionButton)
        {
            Debug.Log("<color=cyan>" + "[GrammarControl.OnOptionPress] index " + optionButton.ButtonIndex + "Name " +  optionButton.Title + "</color>");

            if ((optionButton.ButtonIndex < 0) || (optionButton.ButtonIndex >= AppController.Instance.LauncherControl.GrammarSet.Count))
            {
                return;
            }

            m_selectedCategory = AppController.Instance.LauncherControl.GrammarSet[optionButton.ButtonIndex];

            m_selectedGrammar = 0;

            m_menu.Hide();

            SetGrammarData();

            m_ui.Show();

        }

        private void SetGrammarData()
        {
            //string auxRule = m_ListGrammar[m_IndexGrammar].Rules[m_IndexRule];
            //string rule = auxRule.Substring(4);
            //string keyRule = auxRule.Substring(0, 4);
            //m_GrammarPanelUI.Rule = rule;
            //m_GrammarPanelUI.RulePages = (m_IndexRule + 1).ToString() + "/" + m_MaxRules.ToString();

            /*string rules = m_selectedCategory.Data[m_selectedGrammar].Description + "\n\n";
            for (int i=0; i< m_selectedCategory.Data[m_selectedGrammar].Rules.Count; i++)
            {
                string auxRule = m_selectedCategory.Data[m_selectedGrammar].Rules[i];
                string rule = auxRule.Substring(4);

                string keyRule = auxRule.Substring(0, 4);

                rules += rule + "\n\n";
            }*/


            string description = m_selectedCategory.Data[m_selectedGrammar].Title + "\n\n" + m_selectedCategory.Data[m_selectedGrammar].Description;

            m_ui.DescriptionScroll.SetText(description);

            // Examples

            string examples = "<color=#c9e8ff>Examples:</color>";

            for (int i = 0; i < m_selectedCategory.Data[m_selectedGrammar].EnglishExamples.Count; i++)
            {
                examples += "\n\n<color=#c9e8ff> " + (i + 1) + ": " + m_selectedCategory.Data[m_selectedGrammar].EnglishExamples[i] + "</color>";

                if (i < m_selectedCategory.Data[m_selectedGrammar].SpanishExamples.Count)
               // if (includeTranslation)
                {
                    examples += "\n<color=#93d47f> - " + m_selectedCategory.Data[m_selectedGrammar].SpanishExamples[i] + "</color>";
                }
            }

            m_ui.ExamplesScroll.SetText(examples);

        }

        private void OnNextGrammar()
        {
            m_selectedGrammar += 1;

            if (m_selectedGrammar >= m_selectedCategory.Data.Count)
            {
                m_selectedGrammar = 0;
            }

            SetGrammarData();
        }

        public override void Hide()
        {
            m_menu.OptionList.OnButtonPress -= OnOptionPress;
            m_menu.Hide();

            m_ui.OnNextGrammarClick -= OnNextGrammar;
            m_ui.Hide();

            base.Hide();
        }


        public override void Finish()
        {
           
        }

        public override void Back()
        {
           
        }

        #endregion BaseControl

        

       
     
    }
}
