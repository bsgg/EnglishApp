using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class VocabularyUI : UIBase
    {        
        [Header("Vocabulary UI")]
        [SerializeField]
        private Text m_LblTitleSection;
        public string TitleSection
        {
            get { return m_LblTitleSection.text; }
            set { m_LblTitleSection.text = value; }
        }


        [SerializeField] private ScrollTextUI m_ExamplesScroll;
        public ScrollTextUI ExamplesScroll
        {
            get { return m_ExamplesScroll; }
            set { m_ExamplesScroll = value; }
        }

        [SerializeField] private Text m_LblWord;
        public string Word
        {
            get { return m_LblWord.text; }
            set { m_LblWord.text = value; }
        }


        [SerializeField]
        private Image m_ImageReference;
        public Image ImageReference
        {
            get { return m_ImageReference; }
            set { m_ImageReference = value; }
        }





        [SerializeField] private Text m_LblTitleNextWordButton;
        public string TitleNextWordButton
        {
            get { return m_LblTitleNextWordButton.text; }
            set { m_LblTitleNextWordButton.text = value; }
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

        [SerializeField]
        private GameObject m_BtnImageReference;
        public GameObject BtnImageReference
        {
            get { return m_BtnImageReference; }
            set { m_BtnImageReference = value; }
        }

        [SerializeField]
        private ImageReferencePopup m_ImagePopup;
        public ImageReferencePopup ImagePopup
        {
            get { return m_ImagePopup; }
            set { m_ImagePopup = value; }
        }
    }
}
