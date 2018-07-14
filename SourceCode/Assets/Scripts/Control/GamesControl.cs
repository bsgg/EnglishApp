using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Utility;

namespace EnglishApp
{
    public class GamesControl : Base
    {
        /// <summary>
        /// GameMenu
        /// </summary>
        [SerializeField] private UIBase m_GameMenu;

        /// <summary>
        /// Grammar Quiz
        /// </summary>
        public QuizGameControl QuizCrontol;

        /// <summary>
        /// CrossWord game control
        /// </summary>
        public CrossWordGameControl CrossWordControl;

        /// <summary>
        /// Hangman game
        /// </summary>
        public HangmanGameControl HangmanControl;

        /// <summary>
        ///  You snooze you lose game
        /// </summary>
        public CommonMistakesGameControl CommonMistakesControl;

        public enum SECTION_GAME
        {
            NONE = -1,
            MENUGAMES,
            CROSSWORD,
            HANGMAN,
            PHRASALVERBQUIZ,
            GRAMMARQUIZ,
            COMMONMISTAKES,
            DAYWORD
        }
        private SECTION_GAME  m_Section = SECTION_GAME.NONE;
        public SECTION_GAME SectionGame
        {
            set { m_Section = value; }
            get { return m_Section; }
        }

        #region BaseControl
        public override void Init()
        {
            m_Section = SECTION_GAME.MENUGAMES;
            m_GameMenu.Show();
        }

        public override void Back()
        {
            switch (m_Section)
            {
                case SECTION_GAME.MENUGAMES:
                    Finish();
                    break;
                case SECTION_GAME.CROSSWORD:
                    CrossWordControl.Finish();
                    m_GameMenu.Show();
                    m_Section = SECTION_GAME.MENUGAMES;
                    break;
                case SECTION_GAME.HANGMAN:
                    HangmanControl.Finish();
                    m_GameMenu.Show();
                    m_Section = SECTION_GAME.MENUGAMES;
                    break;
                case SECTION_GAME.PHRASALVERBQUIZ:
                case SECTION_GAME.GRAMMARQUIZ:
                    QuizCrontol.Finish();
                    m_GameMenu.Show();
                    m_Section = SECTION_GAME.MENUGAMES;
                    break;

                case SECTION_GAME.COMMONMISTAKES:
                    CommonMistakesControl.Finish();
                    m_GameMenu.Show();
                    m_Section = SECTION_GAME.MENUGAMES;
                    break;
                case SECTION_GAME.DAYWORD:

                    m_GameMenu.Show();
                    m_Section = SECTION_GAME.MENUGAMES;
                    break;

            }
        }

        public override void Finish()
        {
            m_Section = SECTION_GAME.NONE;
            m_GameMenu.Hide();
            GameManager.Instance.BackMainMenu();
        }

        public void onSectionPress(int id)
        {
            m_GameMenu.Hide();
            m_Section = (SECTION_GAME)id;
            switch (m_Section)
            {
                case SECTION_GAME.CROSSWORD:
                    CrossWordControl.Init();
                    break;
                case SECTION_GAME.HANGMAN:
                    HangmanControl.Init();
                    break;
                case SECTION_GAME.PHRASALVERBQUIZ:
                case SECTION_GAME.GRAMMARQUIZ:
                    QuizCrontol.Init();
                    break;
                case SECTION_GAME.COMMONMISTAKES:
                    CommonMistakesControl.Init();
                    break;
                case SECTION_GAME.DAYWORD:

                    break;
            }
        }

        #endregion BaseControl
    }
}
