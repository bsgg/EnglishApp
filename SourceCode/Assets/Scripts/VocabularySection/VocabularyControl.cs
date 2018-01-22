using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using LitJson;

namespace EnglishApp
{
    [System.Serializable]
    public class Word
    {
        public string Category;
        public string VocabularyWord;
        public string ImageRef;
        public string Pronunciation;
        public List<string> Meanings;
        public List<string> EnglishExamples;
        public List<string> SpanishExamples;
        public Word()
        {
            Meanings = new List<string>();
            EnglishExamples = new List<string>();
            SpanishExamples = new List<string>();
        }
    }

    [Serializable]
    public class WordDictionary
    {
        public List<Word> Data;

        public WordDictionary()
        {
            Data = new List<Word>();
        }

        public Word GetRandomWord()
        {
            int randIndex = UnityEngine.Random.Range(0, Data.Count);
            return Data[randIndex];
        }

        public int GetRandomWordID()
        {
            return UnityEngine.Random.Range(0, Data.Count);
        }
    }


    public class VocabularyControl : BaseControl
    {
        /// <summary>
        /// Category Word type
        /// </summary>
        public enum ECATEGORY
        {
            NONE = -1,
            Actions = 0,
            Food,
            Animals,
            Places,
            Objects,
            BodyParts,
            Adjectives,
            Adverbs,
            ConnectedWords,
            PhrasalVerbs,
            Agility,
            Expressions,
            Misc,
            NUM
        }

        private string[] m_CategoryName = {
            "Actions",
            "Food",
            "Animals",
            "Places",
            "Objects",
            "BodyParts",
            "Adjectives",
            "Adverbs",
            "Connected Words",
            "Phrasal Verbs",
            "Agility",
            "Expressions",
            "Misc"
        };


        [Header("Vocabulary UI")]
        [SerializeField] private VocabularyBaseUI   m_VocabularyUI;        

        private ECATEGORY m_SelectedCategory;
        public ECATEGORY SelectedCategory
        {
            set { m_SelectedCategory = value; }
            get { return m_SelectedCategory; }
        }

        // Current Word
        private Word                                 m_CurrentWord;
        private int                                  m_IndexExample;
        private int m_SelectedWordID;
        private int m_SelectedExampleID;

        private List<WordDictionary> m_VocabularySet;


        public override void Init()
        {
            LoadDataSet();
            m_VocabularyUI.Hide();
        }

        public void LoadDataSet()
        {
            m_VocabularySet = new List<WordDictionary>();

            for (int i = 0; i < (int)(ECATEGORY.NUM); i++)
            {
                string nameData = ((ECATEGORY)i).ToString();
                string path = "Data/Vocabulary/" + nameData;
                string jsonData = Utils.LoadJSONResource(path);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    WordDictionary set = JsonMapper.ToObject<WordDictionary>(jsonData);
                    m_VocabularySet.Add(set);
                }
                else
                {
                    Debug.LogError("[DataDictionary.LoadVocabulary] " + nameData + " NULL ");
                }
            }
        }

        public void SetRandomWord()
        {            
            m_SelectedCategory = ((ECATEGORY)UnityEngine.Random.Range(0, (int)ECATEGORY.NUM)); ;
            m_SelectedWordID = m_VocabularySet[(int)m_SelectedCategory].GetRandomWordID();
            m_CurrentWord = m_VocabularySet[(int)m_SelectedCategory].Data[m_SelectedWordID];
            m_SelectedExampleID = 0;
        }

        private void SetUI()
        {
            string word = "<color=#5bd3de>" + m_CurrentWord.VocabularyWord;
            if (!string.IsNullOrEmpty(m_CurrentWord.Pronunciation))
            {
                word += " <" + m_CurrentWord.Pronunciation + ">";
            }
            word += "</color>";

            m_VocabularyUI.Word = word;

            SetEnglishExample();

            m_VocabularyUI.BtnNextExample.SetActive(false);

            if (!string.IsNullOrEmpty(m_CurrentWord.ImageRef))
            {
                m_VocabularyUI.ImagePopup.ImageReference.sprite = GameManager.Instance.SpriteManager.GetSpriteByName(m_CurrentWord.ImageRef);
                m_VocabularyUI.BtnImageReference.SetActive(true);
            }
            else
            {
                m_VocabularyUI.BtnImageReference.SetActive(false);
            }
        }

        public override void Show()
        {
            m_VocabularyUI.Show();
        }

        public override void Hide()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.StopSpeech ();
#endif
            m_VocabularyUI.Hide();
        }

        public override void Back()
        {
            Hide();
        }

        /// <summary>
        /// Sets the random word to show
        /// </summary>
       /* private void InitRandomWord()
        {
            m_CurrentWord = GameManager.Instance.VocabularyDictionary.GetRandomWord(m_SelectedCategory);
            if (m_CurrentWord == null)
            {
                Debug.LogError("VocabularyControl.InitRandomWord m_CurrentWord null");
                return;
            }
            if (m_CurrentWord.Meanings.Count > 1)
            {
                m_IndexExample = UnityEngine.Random.Range(0, m_CurrentWord.Meanings.Count);
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
            m_VocabularyUI.Word = word;

            SetEnglishExample();
            m_VocabularyUI.BtnNextExample.SetActive(false);

           /* switch (m_SelectedCategory)
            {
                case DataDictionary.SECTION_VOCABULARY.PHRASALVERB:
                case DataDictionary.SECTION_VOCABULARY.AGILITY:
                case DataDictionary.SECTION_VOCABULARY.EXPRESSIONS:
                    if (m_CurrentWord.EnglishExamples.Count > 1)
                    {
                        m_VocabularyPanelUI.BtnNextExample.SetActive(true);
                    }
                    break;
            }*/

            // Set sprite
           /* Debug.Log("m_CurrentWord.ImageRef: " + m_CurrentWord.ImageRef);
            if (!string.IsNullOrEmpty(m_CurrentWord.ImageRef))
            {
                m_VocabularyUI.ImagePopup.ImageReference.sprite = GameManager.Instance.SpriteManager.GetSpriteByName(m_CurrentWord.ImageRef);
                m_VocabularyUI.BtnImageReference.SetActive(true);
            }
            else
            {
                m_VocabularyUI.BtnImageReference.SetActive(false);
            }

        }*/

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
            m_VocabularyUI.Word = word;

            // Final exmample
            SetFullExample();
        }

        /// <summary>
        /// Sets the english example.
        /// </summary>
        private void SetEnglishExample()
        {
            string example = "<color=#c9e8ff>" + m_CurrentWord.EnglishExamples[m_IndexExample] + "</color>";
            m_VocabularyUI.Example = example;
        }

        /// <summary>
        /// Sets the full example. (spanish + english)
        /// </summary>
        private void SetFullExample()
        {
            // Final exmample
            string example = "<color=#c9e8ff>" + m_CurrentWord.EnglishExamples[m_IndexExample] + "</color>";
            example += "\n  <size=22><color=#93d47f>- " + m_CurrentWord.SpanishExamples[m_IndexExample] + "</color></size>";
            m_VocabularyUI.Example = example;
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
            //InitRandomWord();
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
           // GameManager.Instance.OnBackMenu();
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

        public void OnImageReferencePress()
        {
            m_VocabularyUI.ImagePopup.Show();
        }

        public void OnImageReferencePopupPress()
        {
            m_VocabularyUI.ImagePopup.Hide();
        }

       

        #endregion ButtonHandles
    }
}
