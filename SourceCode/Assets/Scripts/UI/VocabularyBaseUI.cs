using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class VocabularyBaseUI : BaseUI
    {        
        [Header("Vocabulary UI")]
        [SerializeField] private Text m_LblTitleSection;
        public string TitleSection
        {
            get { return m_LblTitleSection.text; }
            set { m_LblTitleSection.text = value; }
        }

        [SerializeField] private Text m_LblTitleNextWordButton;
        public string TitleNextWordButton
        {
            get { return m_LblTitleNextWordButton.text; }
            set { m_LblTitleNextWordButton.text = value; }
        }

        [SerializeField] private Text m_LblWord;
        public string Word
        {
            get { return m_LblWord.text; }
            set { m_LblWord.text = value; }
        }

        [SerializeField] private Text m_LblExample;
        public string Example
        {
            get { return m_LblExample.text; }
            set { m_LblExample.text = value; }
        }

        [SerializeField] private GameObject m_BtnNextExample;
        public GameObject BtnNextExample
        {
            get { return m_BtnNextExample; }
            set { m_BtnNextExample = value; }
        }
    }
}
