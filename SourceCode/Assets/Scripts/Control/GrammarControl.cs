using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace EnglishApp
{
    public class GrammarControl : BaseControl
    {
        [Header("Menu Grammar")]
        [SerializeField] private BaseUI             m_GrammarMenuUI;

        [Header("Grammar UI")]
        [SerializeField] private GrammarSectionUI   m_GrammarPanelUI;

        private GrammarDictionary.SECTION_GRAMMAR   m_SectionGrammar;
        public GrammarDictionary.SECTION_GRAMMAR    SectionGrammar
        {
            set { m_SectionGrammar = value; }
            get { return m_SectionGrammar; }
        }

        private List<Grammar>                       m_ListGrammar;
        private int                                 m_IndexGrammar;
        private int                                 m_IndexRule;
        private int                                 m_MaxRules;

        #region BaseControl
        public override void Init()
        {
            m_GrammarPanelUI.Hide();
            m_GrammarMenuUI.Show();
        }

        public override void Finish()
        {
            if (m_GrammarPanelUI.IsVisible)
            {
                GameManager.Instance.MenuBarControl.ScrollMenu.HandleButtonPress -= onHandleMenuButtonGrammarPress;
                GameManager.Instance.MenuBarControl.Close();
                m_GrammarMenuUI.Hide();
            }
            else
            {
                m_GrammarMenuUI.Hide();
            }
        }

        public override void Back()
        {
            if (m_GrammarPanelUI.IsVisible)
            {
                GameManager.Instance.MenuBarControl.ScrollMenu.HandleButtonPress -= onHandleMenuButtonGrammarPress;
                GameManager.Instance.MenuBarControl.Close();
                m_GrammarPanelUI.Hide();
                m_GrammarMenuUI.Show();

            }
            else
            {

                m_GrammarMenuUI.Hide();
                GameManager.Instance.BackMainMenu();
            }
        }

        #endregion BaseControl

        public void OnSectionPress(int section)
        {
            m_SectionGrammar = (GrammarDictionary.SECTION_GRAMMAR)section;

            m_GrammarPanelUI.Show();
            m_GrammarMenuUI.Hide();
            InitGrammar();
        }

        private void InitGrammar()
        {
            // Hide popup table
            m_GrammarPanelUI.PopupTable.Hide();

            m_ListGrammar = new List<Grammar>();
            m_ListGrammar = GameManager.Instance.DataGrammar.LoadSection(m_SectionGrammar);
            if (m_ListGrammar != null)
            {
                // Setup menu scroll
                List<string> listTitleGrammar = new List<string>();
                for (int i = 0; i < m_ListGrammar.Count; i++)
                {
                    listTitleGrammar.Add(m_ListGrammar[i].Title);
                }
                string titleGrammar = GameManager.Instance.DataGrammar.GetSectionGrammarTitle(m_SectionGrammar);
                GameManager.Instance.MenuBarControl.InitScroll(titleGrammar, listTitleGrammar);
                GameManager.Instance.MenuBarControl.ScrollMenu.HandleButtonPress += onHandleMenuButtonGrammarPress;
                // Init indexGrammar with -1 to setup grammar correctly
                m_IndexGrammar = -1;
                SetGrammar(0);
                SetRule();
                m_GrammarPanelUI.Show();

            }
            else
            {
                Debug.LogError("[GrammarPanelGUI] ListGrammar is null");
            }
        }

        /// <summary>
        /// Sets the current grammar by id
        /// </summary>
        /// <param name="index">Index.</param>
        private void SetGrammar(int index)
        {
            if ((index < m_ListGrammar.Count) && (index != m_IndexGrammar))
            {
                m_IndexGrammar = index;
                m_IndexRule = 0;
                m_MaxRules = m_ListGrammar[m_IndexGrammar].Rules.Count;

                m_GrammarPanelUI.Grammar = m_ListGrammar[m_IndexGrammar].Title;
                m_GrammarPanelUI.Description = m_ListGrammar[m_IndexGrammar].Description;

                // Check if extra info is not empty
                if (m_ListGrammar[m_IndexGrammar].ExtraInfo != "")
                {
                    // Load extra info
                    string[] auxExtraInfo = m_ListGrammar[m_IndexGrammar].ExtraInfo.Split('_');
                    if ((auxExtraInfo != null) && auxExtraInfo.Length >= 2)
                    {
                        TableObject objTable = null;
                        //string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/ExtraInfo/TableMakeDo");
                        string jsonActionsString = Utils.LoadJSONResource(auxExtraInfo[1]);
                        if (jsonActionsString != "")
                        {
                            objTable = JsonMapper.ToObject<TableObject>(jsonActionsString);
                            if (objTable != null)
                            {
                                objTable.SetupTable();
                                m_GrammarPanelUI.InitPopup(objTable);
                            }
                        }

                        // Block button
                        m_GrammarPanelUI.BtnExtraInfo.ColorButton = new Color32(86, 182, 195, 255);
                        m_GrammarPanelUI.BtnExtraInfo.EnableButton = true;
                    }
                }
                else
                {
                    // Block button
                    m_GrammarPanelUI.BtnExtraInfo.ColorButton = new Color32(103, 128, 165, 255);
                    m_GrammarPanelUI.BtnExtraInfo.EnableButton = false;
                }
            }
        }
        /// <summary>
        /// Sets the rule for a given grammar
        /// </summary>
        private void SetRule()
        {
            string auxRule = m_ListGrammar[m_IndexGrammar].Rules[m_IndexRule];
            string rule = auxRule.Substring(4);
            string keyRule = auxRule.Substring(0, 4);
            m_GrammarPanelUI.Rule = rule;
            m_GrammarPanelUI.RulePages = (m_IndexRule + 1).ToString() + "/" + m_MaxRules.ToString();

            // Init examples
            // Gets all examples with the keyRule
            List<string> listEnglishExamples = new List<string>();
            List<int> listIndexExamples = new List<int>();
            for (int i = 0; i < m_ListGrammar[m_IndexGrammar].EnglishExamples.Count; i++)
            {
                string auxExample = m_ListGrammar[m_IndexGrammar].EnglishExamples[i];
                string keyExample = auxExample.Substring(0, 4);
                if (keyExample == keyRule)
                {
                    string example = auxExample.Substring(4);
                    listEnglishExamples.Add(example);
                    listIndexExamples.Add(i);
                }
            }

            string finalExample = "";
            // Fill up examples with the list
            for (int i = 0; i < listIndexExamples.Count; i++)
            {

                // Set english example
                finalExample += "<color=#c9e8ff>" + listEnglishExamples[i] + "</color>\n";
                // Find spanish examples
                int iSpanishExample = listIndexExamples[i];
                finalExample += "<size=18><color=#93d47f>  - " + m_ListGrammar[m_IndexGrammar].SpanishExamples[iSpanishExample] + "</color></size>\n\n";
            }

            m_GrammarPanelUI.Examples = finalExample;
        }
        /// <summary>
        /// Callback when finishes the progress bar
        /// </summary>
        private void onHandleMenuButtonGrammarPress(int id)
        {
            GameManager.Instance.MenuBarControl.Close();
            SetGrammar(id);
            SetRule();
        }

        #region HandleButtons

        public void OnMenuPress()
        {
            GameManager.Instance.MenuBarControl.Show();
        }

        public void OnNextRule()
        {
            m_IndexRule++;
            if (m_IndexRule >= m_MaxRules)
            {
                m_IndexRule = 0;
            }
            SetRule();

        }
        public void OnPrevRule()
        {
            m_IndexRule--;
            if (m_IndexRule < 0)
            {
                m_IndexRule = m_MaxRules - 1;
            }
            SetRule();
        }

        public void OnPopupPress()
        {
            m_GrammarPanelUI.PopupTable.Show();
        }

        #endregion HandleButtons

    }
}
