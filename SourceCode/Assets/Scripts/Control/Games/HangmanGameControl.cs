using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CrossWordUtil;
using Utility;

namespace EnglishApp
{
    public class HangmanGameControl : BaseControl
    {
        /// <summary>
        /// GUI Hangman
        /// </summary>
        [SerializeField] private HangmanGameUI          m_HangmanGUI;

        //private Word currentWord;
        private int maxTries = 8;
        private int currentNumberTries = 0;

        private string currentWord;
        // Alphabet to create new letters
        string alphabetLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Colors for alphabet buttons
        private Color32 normalColor = new Color32(255, 255, 255, 255);
        private Color32 wrongColor = new Color32(255, 0, 0, 255);
        private Color32 rightColor = new Color32(147, 212, 127, 255);

        #region BaseControl
        /// <summary>
        /// Inits hangman logic
        /// </summary>
        private void initHangman()
        {
            // Hide all letters
            for (int i = 0; i < m_HangmanGUI.LettersSolution.Count; i++)
            {
                m_HangmanGUI.LettersSolution[i].Hide();
            }

            // Get random category in words 
            Random.InitState((int)System.DateTime.Now.Ticks);
            string auxCategory = "";

            // Create a list of available categories
            List<VocabularyControl.ECATEGORY> lExcludeCategories = new List<VocabularyControl.ECATEGORY>();
            lExcludeCategories.Add(VocabularyControl.ECATEGORY.ConnectedWords);

            do
            {
                //Word auxW = GameManager.Instance.VocabularyDictionary.GetRandomWord(lExcludeCategories);
                //currentWord = auxW.VocabularyWord;
                //auxCategory = auxW.Category;


            } while (currentWord.Length > m_HangmanGUI.LettersSolution.Count);

            if (currentWord != null)
            {
                // Sets title
                m_HangmanGUI.TitleLabel = "Hangman - " + auxCategory;
                // Sets number tries
                currentNumberTries = 0;
                m_HangmanGUI.LabelNumberTries = "Tries: " + currentNumberTries.ToString() + "/" + maxTries.ToString();

                currentWord = currentWord.ToUpper();
                if (currentWord.Length > 0)
                {
                    //Discard all letters inside ()
                    int startIndex = -1;
                    for (int i = 0; i < currentWord.Length; i++)
                    {
                        if (currentWord[i] == '(')
                        {
                            startIndex = i;
                            break;
                        }
                    }
                    if (startIndex > -1)
                    {
                        currentWord = currentWord.Remove(startIndex);
                    }

                    bool lookForChance = true;
                    // Setups the word
                    for (int i = 0; i < currentWord.Length; i++)
                    {
                        m_HangmanGUI.LettersSolution[i].labelComponet.text = "";
                        m_HangmanGUI.LettersSolution[i].Show();
                        // Change color letter if is equal to "", hide the letter
                        if (currentWord[i] == ' ')
                        {
                            m_HangmanGUI.LettersSolution[i].Hide();
                            m_HangmanGUI.LettersSolution[i].labelComponet.text = " ";
                        }

                        if (currentWord[i] == '-')
                        {
                            m_HangmanGUI.LettersSolution[i].labelComponet.text = "-";
                        }

                        // Get chance and show the letter
                        if ((lookForChance) && (currentWord[i] != ' ') && (currentWord[i] != '-'))
                        {
                            float chance = Random.Range(0.0f, 100.0f);

                            if (chance < 40.0f)
                            {
                                m_HangmanGUI.LettersSolution[i].labelComponet.text = currentWord[i].ToString();
                                lookForChance = false;
                            }
                        }
                    }
                }
                // Init alphabet buttons
                for (int i = 0; i < m_HangmanGUI.AlphabetLetters.Count; i++)
                {
                    m_HangmanGUI.AlphabetLetters[i].EnableButton = true;
                    m_HangmanGUI.AlphabetLetters[i].ColorButton = normalColor;
                }
            }

        }

        public override void Init()
        {
            // Load data
           // GameManager.Instance.DataDictionary.LoadSection(DataDictionary.SECTION_VOCABULARY.WORDS);

            // Init hangman
            initHangman();

            m_HangmanGUI.Show();
        }

        public override void Back()
        {
            this.Finish();
        }

        public override void Finish()
        {
            m_HangmanGUI.Hide();
        }


        #endregion BaseControl

        public void OnAlphabetButtonPressed(int index)
        {
            // Find all letters inside current word
            char auxLetter = alphabetLetters[index];
            bool correctLetter = false;
            for (int i = 0; i < currentWord.Length; i++)
            {
                // Letter selected is correct, put into the letters and mark as find letter
                if (currentWord[i] == auxLetter)
                {
                    correctLetter = true;
                    m_HangmanGUI.LettersSolution[i].labelComponet.text = auxLetter.ToString();
                }
            }

            // Incorrect letter
            if (!correctLetter)
            {
                // Mark that letter in alphabet as a wrong letter and add 1 to number tries
                currentNumberTries += 1;
                m_HangmanGUI.LabelNumberTries = "Tries: " + currentNumberTries.ToString() + "/" + maxTries.ToString();

                m_HangmanGUI.AlphabetLetters[index].ColorButton = wrongColor;

                if (currentNumberTries >= maxTries)
                {
                    m_HangmanGUI.LabelNumberTries= "FINISH";
                    // Disable all alphabet letters and show the real word
                    for (int i = 0; i < currentWord.Length; i++)
                    {
                        m_HangmanGUI.LettersSolution[i].labelComponet.text = currentWord[i].ToString();
                    }

                    for (int i = 0; i < m_HangmanGUI.AlphabetLetters.Count; i++)
                    {
                        m_HangmanGUI.AlphabetLetters[i].EnableButton = false;
                    }
                }
            }
            else
            {
                m_HangmanGUI.AlphabetLetters[index].ColorButton = rightColor;
                // Check if all letter are set up
                bool finishWord = true;
                for (int i = 0; i < currentWord.Length; i++)
                {
                    if (m_HangmanGUI.LettersSolution[i].labelComponet.text != currentWord[i].ToString())
                    {
                        finishWord = false;

                    }
                }
                if (finishWord)
                {
                    m_HangmanGUI.LabelNumberTries = "WELL DONE!";
                    for (int i = 0; i < m_HangmanGUI.AlphabetLetters.Count; i++)
                    {
                        m_HangmanGUI.AlphabetLetters[i].EnableButton = false;
                    }
                }
            }
        }

        /// <summary>
        /// On retry pressed handler
        /// </summary>
        public void OnRetryPressed()
        {
            initHangman();

        }
    }
}
