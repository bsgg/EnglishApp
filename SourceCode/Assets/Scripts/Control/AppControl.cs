using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class AppControl : Singleton<AppControl>
    {
        public enum ESECTION { NONE = -1, Vocabulary = 0, PhrasalVerbs, Agility, Expressions, Grammar, Games, NUM };
        private ESECTION m_SelectedSection = ESECTION.NONE;

        [SerializeField] private VocabularyControl m_VocabularyControl;

        [SerializeField] private GrammarControl m_GrammarControlControl;

        private BaseControl m_CurrentControl = null;

        [SerializeField]
        private UIBase m_MainMenu;

        private void Start()
        {

#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.Initialize();
#endif

            StartCoroutine(Init());

        }

        private IEnumerator Init()
        {
            //yield return FileRequestManager.Instance.RequestFiles();

            yield return FileRequestManager.Instance.RequestIndexFiles();

            m_VocabularyControl.Init();

            m_GrammarControlControl.Init();
            m_MainMenu.Show();
        }


        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (m_CurrentControl != null)
                {
                    m_CurrentControl.Back();
                }
            }
        }

        public void ShowMainMenu()
        {
            m_VocabularyControl.Hide();
            m_MainMenu.Show();
        }

        public void OnMainMenuPress(int id)
        {
            m_SelectedSection = (ESECTION)id;
            switch (m_SelectedSection)
            {
                case  ESECTION.Vocabulary:
                    // Vocabulary
                    m_VocabularyControl.SetRandomWord();
                    m_VocabularyControl.Show();
                    m_MainMenu.Hide();

                break;
                case ESECTION.PhrasalVerbs:
                    // Vocabulary

                break;
                case ESECTION.Agility:
                    // Vocabulary

                break;
                case ESECTION.Expressions:
                    // Vocabulary

                break;
                case ESECTION.Grammar:
                    // Vocabulary

                break;
                case ESECTION.Games:
                    // Vocabulary

                break;
            }

        }
    }
}
