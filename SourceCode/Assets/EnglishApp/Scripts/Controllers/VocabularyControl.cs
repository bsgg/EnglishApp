using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using LitJson;
using Utility;

namespace EnglishApp
{
    [Serializable]
    public class Word
    {
        public string Category;
        public string VocabularyWord;
        public string PictureName;
        public Sprite Sprite;
        public string Pronunciation;        
        public List<string> Meanings;
        public List<string> EnglishExamples;
        public List<string> SpanishExamples;
        public Word()
        {
            Meanings = new List<string>();
            EnglishExamples = new List<string>();
            SpanishExamples = new List<string>();
            Sprite = null;
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


    public class VocabularyControl : Base
    {
        [Header("Vocabulary UI")]
        [SerializeField] private VocabularyUI   m_UI;

        [SerializeField] private int m_SelectedCategory;

        // Current Word
        [SerializeField] private Word                                 m_CurrentWord;
        private int                                  m_IndexExample;

        [SerializeField] private int m_SelectedWordID;
        [SerializeField] private int m_SelectedExampleID;

        private bool m_AddTranslation = false;
        private bool m_PictureVisible = false;

        public void SetRandom()
        {
            m_SelectedExampleID = 0;
            int nElements = 0;

            switch(AppController.Instance.SelectedSection)
            {
                case LauncherController.EDATATYPE.VOCABULARY:
                    nElements = AppController.Instance.LauncherControl.VocabularySet.Count;
                    m_SelectedCategory = UnityEngine.Random.Range(0, nElements);
                    m_SelectedWordID = AppController.Instance.LauncherControl.VocabularySet[m_SelectedCategory].GetRandomWordID();
                    m_CurrentWord = AppController.Instance.LauncherControl.VocabularySet[m_SelectedCategory].Data[m_SelectedWordID];

                    break;
                case LauncherController.EDATATYPE.PHRASAL_VERBS:
                    nElements = AppController.Instance.LauncherControl.PhrasalVerbSet.Count;
                    m_SelectedCategory = UnityEngine.Random.Range(0, nElements);

                    m_SelectedWordID = AppController.Instance.LauncherControl.PhrasalVerbSet[m_SelectedCategory].GetRandomWordID();
                    m_CurrentWord = AppController.Instance.LauncherControl.PhrasalVerbSet[m_SelectedCategory].Data[m_SelectedWordID];
                break;
            }

            if (m_CurrentWord.EnglishExamples.Count <= 1)
            {
                m_UI.NextExampleBtn.Disable();
            }else
            {
                m_UI.NextExampleBtn.Enable();
            }

            m_AddTranslation = false;
            m_PictureVisible = false;

            UpdateUI();
        }

        private void UpdateUI()
        {
            UpdateWord(m_AddTranslation);
            UpdateExamples(m_AddTranslation);

            // Set Image
            if (m_CurrentWord.Sprite != null)
            {
                m_PictureVisible = true;
                m_UI.ImageBtn.Enable();
                m_UI.SetPicture(m_CurrentWord.Sprite);
            }
            else
            {
                m_PictureVisible = false;
                m_UI.ImageBtn.Disable();
                m_UI.SetPicture();
            }            
        }

        private void UpdateWord(bool includeTranslation)
        {
            string word = "<color=#5bd3de>" + m_CurrentWord.VocabularyWord;
            if (!string.IsNullOrEmpty(m_CurrentWord.Pronunciation))
            {
                word += " - " +  m_CurrentWord.Pronunciation + "</color>";
            }else
            {
                word += "</color>";
            }

            if (includeTranslation)
            {
                if ((m_CurrentWord.Meanings != null) && (m_SelectedExampleID < m_CurrentWord.Meanings.Count))
                {
                    word +="\n" +  m_CurrentWord.Meanings[m_SelectedExampleID];
                }
            }

            m_UI.WordLabel = word;            
        }


        private void UpdateExamples(bool includeTranslation)
        {
            string examples = "<color=#c9e8ff>Examples:</color>";

            for (int i=0;i< m_CurrentWord.EnglishExamples.Count; i++ )
            {
                examples += "\n\n<color=#c9e8ff> " + (i+1) + ": "+ m_CurrentWord.EnglishExamples[i] + "</color>";

                if (includeTranslation)
                {
                    examples += "\n<color=#93d47f> - " + m_CurrentWord.SpanishExamples[m_SelectedExampleID] + "</color>";
                }
            }

            m_UI.ExamplesScroll.SetText(examples);
        }

        #region MenuHandle

        public void OnWordBtnPress()
        {
            OnTranslateBtnPress();
        }

        public void OnAudioWordBtnPress()
        {
            Debug.Log("VocabularyControl.OnAudioWordBtnPress");
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                EasyTTSUtil.SpeechFlush(m_CurrentWord.VocabularyWord);
            }
        }

        public void OnTranslateBtnPress()
        {
            Debug.Log("VocabularyControl.OnTranslateBtnPress");

            m_AddTranslation = !m_AddTranslation;

            UpdateExamples(m_AddTranslation);
            UpdateWord(m_AddTranslation);

        }

        public void OnPictureBtnPress()
        {
            m_PictureVisible = !m_PictureVisible;
            if (m_PictureVisible)
            {
                m_UI.ImageBtn.Enable();
            }else
            {
                m_UI.ImageBtn.Disable();
            }           
        }

        public void OnNextWordBtnPress()
        {
            SetRandom();
        }        

        #endregion MenuHandle
        
        public override void Show()
        { 
            switch (AppController.Instance.SelectedSection)
            {
                case LauncherController.EDATATYPE.VOCABULARY:
                    m_UI.TitleLabel = "Vocabulary";

                    break;
                case LauncherController.EDATATYPE.PHRASAL_VERBS:
                    m_UI.TitleLabel = "Phrasal Verbs";
                break;
            }
            m_UI.Show();
        }

        public override void Hide()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.StopSpeech ();
#endif
            m_UI.Hide();
        }

        public override void Back()
        {
            Hide();
        }
    }
}
