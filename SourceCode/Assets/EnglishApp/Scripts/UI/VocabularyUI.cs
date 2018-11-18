using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class VocabularyUI : UIBase
    {
        [Header("Buttons")]
        [SerializeField]
        private ButtonWithText m_TranslateBtn;
        public ButtonWithText TranslateBtn
        {
            get { return m_TranslateBtn; }
        }

        [SerializeField]
        private ButtonWithText m_ImageBtn;
        public ButtonWithText ImageBtn
        {
            get { return m_ImageBtn; }
        }

        [SerializeField]
        private ButtonWithText m_NextWordBtn;
        public ButtonWithText NextWordBtn
        {
            get { return m_NextWordBtn; }
        }        

        [SerializeField]
        private ButtonWithText m_AudioWordBtn;
        public ButtonWithText AudioWordBtn
        {
            get { return m_AudioWordBtn; }
        }

        [Header("Content")]
        [SerializeField]
        private Text m_TitleLabel;
        public string TitleLabel
        {
            get { return m_TitleLabel.text; }
            set { m_TitleLabel.text = value; }
        }

        [SerializeField]
        private Text m_WordLabel;
        public string WordLabel
        {
            get { return m_WordLabel.text; }
            set { m_WordLabel.text = value; }
        }

        [SerializeField] private Image m_Picture;
        public void SetPicture(Sprite image = null)
        {
            if (image == null)
            {
                m_Picture.gameObject.SetActive(false);
            }
            else
            {
                m_Picture.gameObject.SetActive(true);
                m_Picture.sprite = image;
            }
        }

        [SerializeField] private ScrollTextUI m_ExamplesScroll;
        public ScrollTextUI ExamplesScroll
        {
            get { return m_ExamplesScroll; }
            set { m_ExamplesScroll = value; }
        }
    }
}
