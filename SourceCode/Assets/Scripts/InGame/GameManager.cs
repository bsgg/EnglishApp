using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utility;
using UnityEngine.UI;

namespace EnglishApp
{
    /*[System.Serializable]
    public class ImagetItem
    {
        public Image Image;
        
    }*/



    public class GameManager : Singleton<GameManager>
    {
        public enum STATE_MENU
        {
            MAIN_MENU = 0,
            VOCABULARY_MENU,
            GRAMMAR_MENU,
            GAMES_MENU
        }

        private STATE_MENU m_StateMenu;

        [Header("Menu UI")]
        [SerializeField] private  BaseUI            m_MainMenuPanelUI;

        [Header("Menu Bar")]
        [SerializeField] private MenuLeftScrollUI m_MenuBarControl;
        public MenuLeftScrollUI MenuBarControl
        {
            get { return m_MenuBarControl; }
        }

        [Header("List of sections")]
        [SerializeField] private VocabularyControl m_VocabularySectionControl;
        public GrammarControl GrammarSectionControl;
        public GamesControl GamesSectionControl;

        private DataDictionary m_DataDictionary;
        public DataDictionary DataDictionary
        {
            get { return this.m_DataDictionary; }
        }

        private GamesDictionary m_DataGamesDictionary;
        public GamesDictionary DataGamesDictionary
        {
            get { return m_DataGamesDictionary; }
        }

        private GrammarDictionary m_DataGrammar;
        public GrammarDictionary DataGrammar
        {
            get { return m_DataGrammar; }
        }

        [SerializeField] private SpriteManager m_SpriteManager;
        public SpriteManager SpriteManager
        {
            get { return m_SpriteManager; }
        }

       

        void Start()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
		// Initialize speech
		EasyTTSUtil.Initialize (EasyTTSUtil.UnitedKingdom);
#endif

            // Load Dictionaries
            m_DataDictionary = new DataDictionary();
            m_DataGrammar = new GrammarDictionary();           
            m_DataGamesDictionary = new GamesDictionary();

            m_StateMenu = STATE_MENU.MAIN_MENU;
            m_MainMenuPanelUI.Show();
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnBackMenu();
            }
        }

        public void OnBackMenu()
        {
            switch (m_StateMenu)
            {
                case STATE_MENU.MAIN_MENU:
                    Application.Quit();
                    break;
                case STATE_MENU.VOCABULARY_MENU:
                    m_VocabularySectionControl.Finish();
                    BackMainMenu();
                    break;
                case STATE_MENU.GRAMMAR_MENU:
                    GrammarSectionControl.Back();
                    break;
                case STATE_MENU.GAMES_MENU:
                    GamesSectionControl.Back();
                    break;
            }
        }

        public void BackMainMenu()
        {
            m_StateMenu = STATE_MENU.MAIN_MENU;
            m_MainMenuPanelUI.Show();
        }

        public void OnOptionMainMenu(int indexMenu)
        {
            m_MainMenuPanelUI.Hide();

            switch (indexMenu)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    m_StateMenu = STATE_MENU.VOCABULARY_MENU;
                    m_VocabularySectionControl.SectionVocabulary = (DataDictionary.SECTION_VOCABULARY)indexMenu;
                    m_VocabularySectionControl.Init();
                    break;
                case 5:
                    m_StateMenu = STATE_MENU.GRAMMAR_MENU;
                    GrammarSectionControl.Init();
                    break;

                case 6:
                    m_StateMenu = STATE_MENU.GAMES_MENU;
                    GamesSectionControl.Init();
                    break;
            }
        }

        void OnApplicationQuit()
        {
            EasyTTSUtil.Stop();
        }
    }
}
