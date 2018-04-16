using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class HangmanGameUI : UIBase
    {
        [Header("HangmanGameUI")]
        [SerializeField] private Text m_LabelNumberTries;
        public string LabelNumberTries
        {
            get { return m_LabelNumberTries.text; }
            set { m_LabelNumberTries.text = value; }
        }
        [SerializeField] private Text m_TitleLabel;
        public string TitleLabel
        {
            get { return m_TitleLabel.text; }
            set { m_TitleLabel.text = value; }
        }

        [SerializeField] private List<TextButton> m_LettersSolution;
        public List<TextButton> LettersSolution
        {
            get { return m_LettersSolution; }
            set { m_LettersSolution = value; }
        }
        [SerializeField] private List<TextButton> m_AlphabetLetters;
        public List<TextButton> AlphabetLetters
        {
            get { return m_AlphabetLetters; }
            set { m_AlphabetLetters = value; }
        }
    }
}
