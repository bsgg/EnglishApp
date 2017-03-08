using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class GameCommonMistakesUI : BaseUI
    {
        [Header("GameCommonMistakesUI")]
        [SerializeField] private Text m_Question;
        public string Question
        {
            get { return m_Question.text; }
            set { m_Question.text = value; }
        }

        [Header("Options")]
        [SerializeField] private TextButton[] m_AnswersButtons;
        public int NumberAnswers
        {
            get { return m_AnswersButtons.Length; }
        }

        public void SetColorOption(Color32 colorOption, int indexOp)
        {
            if (indexOp < m_AnswersButtons.Length)
            {
                m_AnswersButtons[indexOp].ColorButton = colorOption;
            }

        }
        public void SetTextOption(string textOpt, int indexOp)
        {
            if (indexOp < m_AnswersButtons.Length)
            {
                m_AnswersButtons[indexOp].Text = textOpt;
            }
        }

        public void EnableOption(bool enable, int indexOp)
        {
            if (indexOp < m_AnswersButtons.Length)
            {
                m_AnswersButtons[indexOp].EnableButton = enable;
            }
        }
    }
}
