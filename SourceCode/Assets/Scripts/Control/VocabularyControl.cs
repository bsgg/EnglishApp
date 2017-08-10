using UnityEngine;
using System.Collections;

namespace EnglishApp
{
    public class VocabularyControl : BaseControl
    {
        [Header("Vocabulary UI")]
        [SerializeField] private VocabularyBaseUI   m_VocabularyPanelUI;

        private DataDictionary.SECTION_VOCABULARY   m_SectionVocabulary;
        public DataDictionary.SECTION_VOCABULARY    SectionVocabulary
        {
            set { m_SectionVocabulary = value; }
            get { return m_SectionVocabulary; }
        }

        // Current Word
        private Word                                 m_CurrentWord;
        private int                                  m_IndexExample;

        public override void Init()
        {
            // Load dictionary
            GameManager.Instance.DataDictionary.LoadSection(m_SectionVocabulary);           

            switch (m_SectionVocabulary)
            {
                case DataDictionary.SECTION_VOCABULARY.WORDS:
                    m_VocabularyPanelUI.TitleSection = "Vocablary";
                    m_VocabularyPanelUI.TitleNextWordButton = "Next Vocablary";
                    break;
                case DataDictionary.SECTION_VOCABULARY.PHRASALVERB:
                    m_VocabularyPanelUI.TitleSection = "Phasal Verbs";
                    m_VocabularyPanelUI.TitleNextWordButton = "Next Phasal Verb";
                    break;
                case DataDictionary.SECTION_VOCABULARY.AGILITY:
                    m_VocabularyPanelUI.TitleSection = "Agility";
                    m_VocabularyPanelUI.TitleNextWordButton = "Next Agility";
                    break;
                case DataDictionary.SECTION_VOCABULARY.EXPRESSIONS:
                    m_VocabularyPanelUI.TitleSection = "Expressions";
                    m_VocabularyPanelUI.TitleNextWordButton = "Next Expression";
                    break;
            }

#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.Initialize();
#endif
            m_VocabularyPanelUI.Show();
            InitRandomWord();

        }

        public override void Finish()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.StopSpeech ();
#endif
            m_VocabularyPanelUI.Hide();
        }

        public override void Back()
        {
            Finish();
        }

        /// <summary>
        /// Sets the random word to show
        /// </summary>
        private void InitRandomWord()
        {
            m_CurrentWord = GameManager.Instance.DataDictionary.GetRandomWord(m_SectionVocabulary);
            if (m_CurrentWord == null)
            {
                Debug.LogError("VocabularyControl.InitRandomWord m_CurrentWord null");
                return;
            }
            if (m_CurrentWord.Meanings.Count > 1)
            {
                m_IndexExample = Random.Range(0, m_CurrentWord.Meanings.Count);
            }
            else
            {
                m_IndexExample = 0;
            }
           

            // Setup Gui
            string word = "<color=#5bd3de>" + m_CurrentWord.VocabularyWord;
            if (m_CurrentWord.Pronunciation != "")
            {
                word += " <" + m_CurrentWord.Pronunciation + ">";
            }
            word += "</color>";
            m_VocabularyPanelUI.Word = word;

            SetEnglishExample();
            m_VocabularyPanelUI.BtnNextExample.SetActive(false);

            switch (m_SectionVocabulary)
            {
                case DataDictionary.SECTION_VOCABULARY.PHRASALVERB:
                case DataDictionary.SECTION_VOCABULARY.AGILITY:
                case DataDictionary.SECTION_VOCABULARY.EXPRESSIONS:
                    if (m_CurrentWord.EnglishExamples.Count > 1)
                    {
                        m_VocabularyPanelUI.BtnNextExample.SetActive(true);
                    }
                    break;
            }

        }

        /// <summary>
        /// Show the final answer
        /// </summary>
        private void ShowMeaning()
        {
            // Word
            string word = "<color=#5bd3de>" + m_CurrentWord.VocabularyWord;
            if (m_CurrentWord.Pronunciation != "")
            {
                word += " <" + m_CurrentWord.Pronunciation + ">";
            }
            if ((m_CurrentWord.Meanings != null) && (m_IndexExample < m_CurrentWord.Meanings.Count))
            {
                word += "</color>:  " + m_CurrentWord.Meanings[m_IndexExample];
            }
            m_VocabularyPanelUI.Word = word;

            // Final exmample
            SetFullExample();
        }

        /// <summary>
        /// Sets the english example.
        /// </summary>
        private void SetEnglishExample()
        {
            string example = "<color=#c9e8ff>" + m_CurrentWord.EnglishExamples[m_IndexExample] + "</color>";
            m_VocabularyPanelUI.Example = example;
        }

        /// <summary>
        /// Sets the full example. (spanish + english)
        /// </summary>
        private void SetFullExample()
        {
            // Final exmample
            string example = "<color=#c9e8ff>" + m_CurrentWord.EnglishExamples[m_IndexExample] + "</color>";
            example += "\n  <size=22><color=#93d47f>- " + m_CurrentWord.SpanishExamples[m_IndexExample] + "</color></size>";
            m_VocabularyPanelUI.Example = example;
        }

        #region ButtonHandles
        /// <summary>
        /// Next word press
        /// </summary>
        public void OnNextWordPress()
        {
            //ProgressBarAnswerTime.StopProgressBar ();
#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.StopSpeech ();
#endif
            InitRandomWord();
        }

        /// <summary>
        /// Next example button in the current word
        /// </summary>
        public void OnNextExamplePress()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.StopSpeech ();
#endif
            m_IndexExample += 1;
            if (m_IndexExample >= m_CurrentWord.EnglishExamples.Count)
            {
                m_IndexExample = 0;
            }
            SetEnglishExample();
        }

        /// <summary>
        /// On show spanish button pressed
        /// </summary>
        public void OnShowMeaningButton()
        {
            ShowMeaning();
        }

        public void OnMenuPress()
        {
            GameManager.Instance.OnBackMenu();
        }

        public void OnSpeakWordPress()
        {

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                EasyTTSUtil.SpeechFlush(m_CurrentWord.VocabularyWord);
            }

        }

        public void OnSpeakExamplePress()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (m_CurrentWord.EnglishExamples != null && m_IndexExample < m_CurrentWord.EnglishExamples.Count)
                {
                    EasyTTSUtil.SpeechFlush(m_CurrentWord.EnglishExamples[m_IndexExample]);
                }
            }
        }

        #endregion ButtonHandles
    }
}
